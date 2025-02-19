using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEB_DASHBOARD_INV.ViewModels;
using WEB_ERP_TSJ.Models;

namespace WEB_DASHBOARD_INV.Controllers
{
    public class AuthController : Controller
    {
        string authorization = Properties.Settings.Default.Authorization;

        // GET: Auth
        public ActionResult Index()
        {
          
            Session.Abandon();
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel collection)
        {
            string apiUrl = Properties.Settings.Default.ApiUrl;
            ResponseLoginViewModel ObjResponse = new ResponseLoginViewModel();
            try
            {
                ResponseLoginViewModel _res = new ResponseLoginViewModel();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    var jsonString = JsonConvert.SerializeObject(collection);
                    HttpContent httpContent = new StringContent(jsonString);
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("auth/login", httpContent);
                    if (response.IsSuccessStatusCode) {
                        var Response = response.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<ResponseLoginViewModel>(Response);
                        
                        if(ObjResponse.message == "Warning : Authentication failed, wrong password" || ObjResponse.message == "User Not Active.") {
                            _res = JsonConvert.DeserializeObject<ResponseLoginViewModel>(Response);
                            return Json(new { _res }, JsonRequestBehavior.AllowGet);
                        }

                        if (ObjResponse.user_id == null) {
                            _res = JsonConvert.DeserializeObject<ResponseLoginViewModel>(Response);
                            return Json(new { _res }, JsonRequestBehavior.AllowGet);
                        }

                        Session["user_id"] = ObjResponse.user_id.ToString();
                        Session["username"] = ObjResponse.username;
                        Session["role_id"] = ObjResponse.role_id;
                        Session["role"] = ObjResponse.role;
                        Session["role_name"] = ObjResponse.role;
                        Session["email"] = collection.user_id;
                        Session["token"] = ObjResponse.token;
                        Session["expired"] = Convert.ToDateTime(ObjResponse.tokenExpiration).ToString("yyyy-MM-dd HH:mm:ss");
                        Session.Timeout = 500;

                        string wh_id = "";
                        List<USER_ACCESS_DATA> _resUser = new List<USER_ACCESS_DATA>();
                        using (var clientUser = new HttpClient())
                        {
                            clientUser.BaseAddress = new Uri(apiUrl);
                            clientUser.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            clientUser.DefaultRequestHeaders.Add("Authorization", authorization + ObjResponse.token);
                            HttpResponseMessage Res = await clientUser.GetAsync("Svc/Dashboard/GetWarehouseUser?user_id=" + ObjResponse.user_id.ToString());

                            var ResponseUserAccesData = Res.Content.ReadAsStringAsync().Result;
                            _resUser = JsonConvert.DeserializeObject<List<USER_ACCESS_DATA>>(ResponseUserAccesData);
                        }

                        if (_resUser.Count() > 0) {
                            Session["wh_id"] = _resUser.FirstOrDefault().wh_id;
                            wh_id = _resUser.FirstOrDefault().wh_id;
                            if (wh_id == "WHWIPCT") {
                                Session["WarehouseName"] = "CUTTING";
                            } else if (wh_id == "CVRG") {
                                Session["WarehouseName"] = "COVERING";
                            } else {
                                Session["WarehouseName"] = "";
                            }

                        } else {

                            Session["wh_id"] = "WHWIPCT";
                            wh_id = "WHWIPCT";
                            if (wh_id == "WHWIPCT") {
                                Session["WarehouseName"] = "CUTTING";
                            } else if (wh_id == "CVRG") {
                                Session["WarehouseName"] = "COVERING";
                            } else {
                                Session["WarehouseName"] = "";
                            }
                        }

                        _res = JsonConvert.DeserializeObject<ResponseLoginViewModel>(Response);
                        return Json(new { _res }, JsonRequestBehavior.AllowGet);

                    } else {

                        var Response = response.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<ResponseLoginViewModel>(Response);
                        _res = JsonConvert.DeserializeObject<ResponseLoginViewModel>(Response);
                        return Json(new { _res }, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch(Exception ex)
            {
                if(ex.InnerException.Message != null) {
                    TempData["MessageLogin"] = "Error Login : " + ex.InnerException.Message;
                } else {
                    TempData["MessageLogin"] = "Error Login : " + ex.Message;
                }
                ModelState.AddModelError(string.Empty, "");
                return View("Index", collection);
            }
        }

    }
}