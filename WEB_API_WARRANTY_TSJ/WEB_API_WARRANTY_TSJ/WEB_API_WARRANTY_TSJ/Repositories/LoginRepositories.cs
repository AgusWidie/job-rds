using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.ModelsDBERP;
using WEB_API_WARRANTY_TSJ.ModelsDBERP.Login;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;
using BC = BCrypt.Net.BCrypt;

namespace WEB_API_WARRANTY_TSJ.Repositories
{
    public class LoginRepositories : ILoginRepositories
    {
        public readonly IConfiguration _configuration;
        public readonly ERPContext _context;
        public readonly IErrorRepositories _errorRepositories;

        public LoginRepositories(IConfiguration Configuration, ERPContext context, IErrorRepositories errorRepositories)
        {
            _configuration = Configuration;
            _context = context;
            _errorRepositories = errorRepositories;
        }

        public async Task<GlobalObjectResponse> LoginUserWeb(LoginRequest parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();
            LoginResponse logRes = new LoginResponse();

            try
            {

                if (parameter.UserId == null || parameter.UserId == "")
                {
                    res.Code = 400;
                    res.Message = MessageRepositories.MessageFailed + " => User ID : " + parameter.UserId + " Empty.";
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserWeb";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                if (parameter.Password == null || parameter.Password == "")
                {
                    res.Code = 400;
                    res.Message = MessageRepositories.MessageFailed + " => Password : " + parameter.Password + " Empty.";
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserWeb";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                VLogin user = _context.VLogins.Where(m => m.UserId == parameter.UserId && m.Status == 1 && m.UserErp != null).AsNoTracking().FirstOrDefault();

                if (user == null)
                {
                    res.Code = 404;
                    res.Message = MessageRepositories.MessageFailed + " => User Id : " + parameter.UserId + " Not Found.";
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserWeb";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    MsLogLogin log = new MsLogLogin();
                    log.UserId = parameter.UserId;
                    log.LoginAt = DateTime.Now;
                    log.LoginStatus = 3;
                    log.Message = "User Not Found.";
                    _context.MsLogLogins.Add(log);
                    await _context.SaveChangesAsync();
                    return res;

                }
                else if (user != null && !BC.Verify(parameter.Password, user.Password))
                {
                    MsLogLogin log = new MsLogLogin();
                    log.UserId = parameter.UserId;
                    log.LoginAt = DateTime.Now;
                    log.LoginStatus = 2;
                    log.Message = "Authentication failed, wrong password.";
                    _context.MsLogLogins.Add(log);
                    await _context.SaveChangesAsync();

                    res.Code = 400;
                    res.Message = MessageRepositories.MessageFailed + " : " + log.Message;
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserWeb";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);
                }
                else if (user != null && BC.Verify(parameter.Password, user.Password))
                {
                    string refreshToken = RandomString(32);
                    MsUser uSER = _context.MsUsers.Where(m => m.UserId.Trim() == user.UserId.Trim() && user.UserErp != null).AsNoTracking().FirstOrDefault();
                    uSER.RefreshToken = refreshToken;
                    uSER.LastUpdatedAt = uSER.UpdatedAt;
                    uSER.UpdatedAt = DateTime.Now;
                    uSER.InvalidLogin = 0;
                    await _context.SaveChangesAsync();

                    //authentication successful
                    MsLogLogin logLogin = new MsLogLogin();
                    logLogin.UserId = user.UserId;
                    logLogin.LoginAt = DateTime.Now;
                    logLogin.LoginStatus = 1;
                    logLogin.Message = "Authentication Success";
                    _context.MsLogLogins.Add(logLogin);
                    await _context.SaveChangesAsync();

                    string Token = "";
                    Token = GenerateTokenUser(user.UserId, user.Username, user.RoleId, cancellationToken);

                    if (Token.Substring(0, 5) == "Error")
                    {

                        res.Code = 400;
                        res.Message = MessageRepositories.MessageFailed + " : " + Token;
                        res.Error = true;

                        var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "LoginUserWeb";
                        _addError.ServiceError = res.Message;
                        _addError.LogJson = jsonStr;
                        _addError.CreatedBy = parameter.UserId;
                        _addError.ErrorDate = DateTime.Now;

                        await _errorRepositories.AddLogError(_addError, cancellationToken);

                        return res;
                    }

                    logRes.UserId = uSER.UserId;
                    logRes.UserName = uSER.Username;
                    logRes.RoleId = uSER.RoleId;
                    logRes.Role = uSER.RoleId;
                    logRes.Token = Token;
                    logRes.RefreshToken = refreshToken;
                    logRes.TokenExpired = DateTime.Now.AddHours(Convert.ToInt32(_configuration.GetValue<string>("Jwt:ExpiredJwt")));
                }

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Login User : " + parameter.UserId;
                res.Data = logRes;
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserWeb";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUser";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                _addError.ServiceName = "LoginUser";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.UserId;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

        }

        public async Task<GlobalObjectResponse> LoginUserMobile(LoginRequest parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();
            LoginResponse logRes = new LoginResponse();

            try
            {

                if (parameter.UserId == null || parameter.UserId == "")
                {
                    res.Code = 400;
                    res.Message = MessageRepositories.MessageFailed + " => User ID : " + parameter.UserId + " Empty.";
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserMobile";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                if (parameter.Password == null || parameter.Password == "")
                {
                    res.Code = 400;
                    res.Message = MessageRepositories.MessageFailed + " => Password : " + parameter.Password + " Empty.";
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserMobile";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                VLogin user = _context.VLogins.Where(m => m.UserErp == parameter.UserId && m.Status == 1 && m.UserErp != null).AsNoTracking().FirstOrDefault();

                if(user == null)
                {
                    res.Code = 404;
                    res.Message = MessageRepositories.MessageFailed + " => User Id : " + parameter.UserId + " Not Found.";
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserMobile";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    MsLogLogin log = new MsLogLogin();
                    log.UserId = parameter.UserId;
                    log.LoginAt = DateTime.Now;
                    log.LoginStatus = 3;
                    log.Message = "User Not Found.";
                    _context.MsLogLogins.Add(log);
                    await _context.SaveChangesAsync();
                    return res;

                }
                else if (user != null && !BC.Verify(parameter.Password, user.Password))
                {
                    MsLogLogin log = new MsLogLogin();
                    log.UserId = parameter.UserId;
                    log.LoginAt = DateTime.Now;
                    log.LoginStatus = 2;
                    log.Message = "Authentication failed, wrong password.";
                    _context.MsLogLogins.Add(log);
                    await _context.SaveChangesAsync();

                    res.Code = 400;
                    res.Message = MessageRepositories.MessageFailed + " : " + log.Message;
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserMobile";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);
                }
                else if (user != null && BC.Verify(parameter.Password, user.Password))
                {
                    string refreshToken = RandomString(32);
                    MsUser uSER = _context.MsUsers.Where(m => m.UserId.Trim() == user.UserId.Trim() && user.UserErp != null).AsNoTracking().FirstOrDefault();
                    uSER.RefreshToken = refreshToken;
                    uSER.LastUpdatedAt = uSER.UpdatedAt;
                    uSER.UpdatedAt = DateTime.Now;
                    uSER.InvalidLogin = 0;
                    await _context.SaveChangesAsync();

                    //authentication successful
                    MsLogLogin logLogin = new MsLogLogin();
                    logLogin.UserId = user.UserId;
                    logLogin.LoginAt = DateTime.Now;
                    logLogin.LoginStatus = 1;
                    logLogin.Message = "Authentication Success";
                    _context.MsLogLogins.Add(logLogin);
                    await _context.SaveChangesAsync();

                    string Token = "";
                    Token = GenerateTokenUser(user.UserId, user.Username, user.RoleId, cancellationToken);

                    if(Token.Substring(0,5) == "Error")  {

                        res.Code = 400;
                        res.Message = MessageRepositories.MessageFailed + " : " + Token;
                        res.Error = true;

                        var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "LoginUserMobile";
                        _addError.ServiceError = res.Message;
                        _addError.LogJson = jsonStr;
                        _addError.CreatedBy = parameter.UserId;
                        _addError.ErrorDate = DateTime.Now;

                        await _errorRepositories.AddLogError(_addError, cancellationToken);

                        return res;
                    }

                    logRes.UserId = uSER.UserId;
                    logRes.UserName = uSER.Username;
                    logRes.RoleId = uSER.RoleId;
                    logRes.Role = uSER.RoleId;
                    logRes.Token = Token;
                    logRes.RefreshToken = refreshToken;
                    logRes.TokenExpired = DateTime.Now.AddHours(Convert.ToInt32(_configuration.GetValue<string>("Jwt:ExpiredJwt")));
                }

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Login User : " + parameter.UserId;
                res.Data = logRes;
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserMobile";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUserMobile";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                _addError.ServiceName = "LoginUserMobile";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.UserId;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

        }

        public async Task<GlobalObjectResponse> RefreshToken(RefreshTokenRequest parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();
            LoginResponse logRes = new LoginResponse();

            try
            {

                if (parameter.UserId == null || parameter.UserId == "")
                {
                    res.Code = 400;
                    res.Message = MessageRepositories.MessageFailed + " => User ID : " + parameter.UserId + " Empty.";
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "RefreshToken";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);
                    return res;
                }
            
                VUser user = _context.VUsers.Where(m => m.UserErp == parameter.UserId && m.Status == 1 && m.UserErp != null).AsNoTracking().FirstOrDefault();
                if (user == null)
                {
                    res.Code = 404;
                    res.Message = MessageRepositories.MessageFailed + " => User Id : " + parameter.UserId + " Not Found.";
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "RefreshToken";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);
                    return res;

                }
               
                if (user != null && !parameter.RefreshToken.Equals(user.RefreshToken))
                {
                  
                    res.Code = 400;
                    res.Message = MessageRepositories.MessageFailed + " : Refresh Token Failed.";
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "LoginUser";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);
                    return res;
                }

                string refreshToken = RandomString(32);
                MsUser User = _context.MsUsers.Where(m => m.UserId == parameter.UserId).FirstOrDefault();
                if(User != null)
                {
                    User.RefreshToken = refreshToken;
                    User.LastUpdatedAt = User.UpdatedAt;
                    User.UpdatedAt = DateTime.Now;
                    await _context.SaveChangesAsync();
                }

                MsLogLogin logLogin = new MsLogLogin();
                logLogin.UserId = User.UserId;
                logLogin.LoginAt = DateTime.Now;
                logLogin.LoginStatus = 1;
                logLogin.Message = "Refresh token success";
                _context.MsLogLogins.Add(logLogin);
                await _context.SaveChangesAsync();

                VLogin usr = await _context.VLogins.Where(u => u.UserId == parameter.UserId && u.UserErp != null).AsNoTracking().FirstOrDefaultAsync();

                logRes.UserId = usr.UserId;
                logRes.UserName = usr.Username;
                logRes.RoleId = usr.RoleId;
                logRes.Role = usr.RoleId;
                logRes.RefreshToken = refreshToken;
                logRes.TokenExpired = DateTime.Now.AddMinutes(1147);

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Refresh Token, User Id : " + parameter.UserId;
                res.Data = logRes;
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "RefreshToken";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "RefreshToken";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.UserId;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                _addError.ServiceName = "RefreshToken";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.UserId;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public string GenerateTokenUser(string UserId, string UserName, string RoleId, CancellationToken cancellationToken)
        {
            string TokenUser = "";
            try
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", UserId),
                        new Claim("UserName", UserName),
                        new Claim("RoleId", RoleId)
                    }),
                    Issuer = _configuration.GetValue<string>("Jwt:Issuer"),
                    Audience = _configuration.GetValue<string>("Jwt:Audience"),
                    Expires = DateTime.Now.AddHours(Convert.ToInt32(_configuration.GetValue<string>("Jwt:ExpiredJwt"))),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return tokenString;

            }
            catch (Exception ex)
            {
                return "Error : " + ex.Message;
            }

        }
    }
}
