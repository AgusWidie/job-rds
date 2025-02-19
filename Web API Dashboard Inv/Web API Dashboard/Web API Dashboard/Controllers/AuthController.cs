using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WEB_API_DASHBOARD.Models;
using WEB_API_DASHBOARD.ViewModels;
using BC = BCrypt.Net.BCrypt;

namespace WEB_API_DASHBOARD.Controllers
{
    public class AuthController : ApiController
    {
        private DBERPEntities db_erp = new DBERPEntities();
        private int tokenExpired = 1147; // in minute

        // POST: auth/login
        [HttpPost()]
        [Route("auth/login")]
        [ResponseType(typeof(ResponseLoginViewModel))]
        public async Task<ResponseLoginViewModel> Post(LoginViewModel value)
        {
            ResponseLoginViewModel response = new ResponseLoginViewModel();
            try
            {

                if (!ModelState.IsValid)
                {
                    response.code = (int)HttpStatusCode.BadRequest;
                    response.status = false;
                    response.user_id = null;
                    response.username = null;
                    response.role_id = null;
                    response.role = null;
                    response.message = "Authentication failed";
                    response.token = "";
                    response.refreshToken = "";
                    return response;
                }

                if(value.user_id == null || value.user_id == "")
                {
                    response.code = (int)HttpStatusCode.BadRequest;
                    response.status = false;
                    response.user_id = null;
                    response.username = null;
                    response.role_id = null;
                    response.role = null;
                    response.message = "Warning : User ID Cannot Be Empty.";
                    response.token = "";
                    response.refreshToken = "";
                    return response;
                }

                if (value.password == null || value.password == "")
                {
                    response.code = (int)HttpStatusCode.BadRequest;
                    response.status = false;
                    response.user_id = null;
                    response.username = null;
                    response.role_id = null;
                    response.role = null;
                    response.message = "Warning : Password Cannot Be Empty.";
                    response.token = "";
                    response.refreshToken = "";
                    return response;
                }

                V_LOGIN user = db_erp.V_LOGIN.Where(m => m.user_id == value.user_id.Trim() && m.status == 1 && m.user_erp != null).AsNoTracking().FirstOrDefault();
                // check account found 
                if (user == null)
                {
                    // authentication failed
                    MS_LOG_LOGIN log = new MS_LOG_LOGIN();
                    log.user_id = value.user_id;
                    log.login_at = DateTime.Now;
                    log.login_status = 3;
                    log.message = "User Not Found.";
                    db_erp.MS_LOG_LOGIN.Add(log);
                    await db_erp.SaveChangesAsync();

                    response.code = (int)HttpStatusCode.Unauthorized;
                    response.status = false;
                    response.user_id = null;
                    response.username = null;
                    response.role_id = null;
                    response.role = null;
                    response.message = "Warning : User Not Found.";
                    response.token = "";
                    response.refreshToken = "";
                    return response;
                }
                else if (user != null && !BC.Verify(value.password, user.password))
                {
                    MS_LOG_LOGIN logLogin = new MS_LOG_LOGIN();
                    logLogin.user_id = value.user_id;
                    logLogin.login_at = DateTime.Now;
                    logLogin.login_status = 2;
                    logLogin.message = "Authentication failed, wrong password";
                    db_erp.MS_LOG_LOGIN.Add(logLogin);
                    await db_erp.SaveChangesAsync();

                    response.code = (int)HttpStatusCode.NotFound;
                    response.status = false;
                    response.user_id = null;
                    response.username = null;
                    response.role_id = null;
                    response.role = null;
                    response.message = "Warning : Authentication failed, wrong password";
                    response.token = "";
                    response.refreshToken = "";
                    return response;
                }
                else if (user != null && BC.Verify(value.password, user.password))
                {
                    var claims = new[] {
                            new Claim(ClaimTypes.NameIdentifier, user.user_id),
                            new Claim(ClaimTypes.Name, user.username),
                            new Claim(ClaimTypes.Role, user.role_id)
                        };

                    DateTime expired = DateTime.Now.AddMinutes(tokenExpired);
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Y2F0Y2hlciUyMHdvbmclMjBsb3ZlJTIwLm5ldA=="));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                    var tokenDescriptor = new JwtSecurityToken(
                        "XX",
                        "XX",
                        claims,
                        expires: expired,
                        signingCredentials: credentials
                        );
                    var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

                    string refreshToken = RandomString(32);

                    MS_USERS uSER = db_erp.MS_USERS.Where(m => m.user_id.Trim() == user.user_id.Trim() && user.user_erp != null).AsNoTracking().FirstOrDefault();
                    uSER.refresh_token = refreshToken;
                    uSER.last_updated_at = uSER.updated_at;
                    uSER.updated_at = DateTime.Now;
                    uSER.invalid_login = 0;
                    await db_erp.SaveChangesAsync();

                    //authentication successful
                    MS_LOG_LOGIN logLogin = new MS_LOG_LOGIN();
                    logLogin.user_id = user.user_id;
                    logLogin.login_at = DateTime.Now;
                    logLogin.login_status = 1;
                    logLogin.message = "Authentication success";
                    db_erp.MS_LOG_LOGIN.Add(logLogin);
                    await db_erp.SaveChangesAsync();

                    response.code = (int)HttpStatusCode.OK;
                    response.status = true;
                    response.message = "Authentication success";
                    response.user_id = uSER.user_id;
                    response.username = uSER.username;
                    response.role_id = uSER.role_id;
                    response.role = uSER.role_id;
                    response.token = token.ToString();
                    response.refreshToken = refreshToken;
                    response.tokenExpiration = expired;
                }
                return response;
            }
            catch (Exception ex)
            {

                response.code = (int)HttpStatusCode.InternalServerError;
                response.status = false;
                response.user_id = null;
                response.username = null;
                response.role_id = null;
                response.role = null;
                string message = "";
                if (ex.InnerException != null) {
                    message = ex.InnerException.Message;
                } else {
                    message = ex.Message;
                }
                response.message = "Error Auth Web : " + message;
                response.token = "";
                response.refreshToken = "";
                return response;
            }

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
