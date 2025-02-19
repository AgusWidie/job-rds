using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.Xml;
using WebDms.Models;
using WebDms.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebDms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _config;
        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public async Task<ActionResult> Index(string document_type_id, string document_index_id, string document_index_value)
        {
           
            string? accessToken = "";
            ResponseData resp = new ResponseData();
            BrowserVM browserVM = new BrowserVM();

            string ApiUrl = _config["AppSettings:ApiUrl"];
            string user_id = "";
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage Res = await client.GetAsync("api/Browser/GetDocumentAll?document_type_id=" + document_type_id + "&document_index_id=" + document_index_id + 
                                                                "&document_index_value=" + document_index_value + "&user_id=" + user_id);
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                    browserVM = JsonConvert.DeserializeObject<BrowserVM>(JsonConvert.SerializeObject(resp.data));
                }
                else
                {
                    return RedirectToAction("Index", "Auth");
                }

                if (browserVM.directory == null) {
                    ViewBag.dirId = 0;

                } else {
                    ViewBag.dirId = 1;
                }


                List<SelectListItem> lst_document_types = new List<SelectListItem>();
                List<SelectListItem> lst_document_types_upload_file = new List<SelectListItem>();
                List<SelectListItem> lst_document_types_advance_search = new List<SelectListItem>();
                lst_document_types.Add(new SelectListItem { Text = "Select Type", Value = "SelectType" });
                lst_document_types.Add(new SelectListItem { Text = "Document Name", Value = "DocumentName" });
                lst_document_types_advance_search.Add(new SelectListItem { Text = "", Value = "" });
                lst_document_types_upload_file.Add(new SelectListItem { Text = "", Value = "" });

                foreach (var data in browserVM.document_types)
                {
                    lst_document_types.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                    lst_document_types_advance_search.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                    lst_document_types_upload_file.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                }

                ViewBag.collId = 1;
                ViewBag.lstDocTypes = lst_document_types;
                ViewBag.lst_document_types_upload_file = lst_document_types_upload_file;
                ViewBag.lstDocTypesAdvanceSearch = lst_document_types_advance_search;

                if (document_type_id != null && document_type_id != "") {
                    TempData["fDocumentTypeId"] = document_type_id;
                } else {
                    TempData["fDocumentTypeId"] = "";
                }

                if (document_index_id != null && document_index_id != "") {
                    TempData["fDocumentIndexId"] = document_index_id;
                } else {
                    TempData["fDocumentIndexId"] = "";
                }

                if (document_index_value != null && document_index_value != "") {
                    TempData["fDocumentIndexValue"] = document_index_value;
                } else {
                    TempData["fDocumentIndexValue"] = "";
                }

                return View(browserVM);
            }
        }

        
        public async Task<ActionResult<ResponseData>> Dir(int id, string document_type_id, string document_index_id, 
                                                          string document_index_value)
        {
            string? accessToken = "";

            ResponseData resp = new ResponseData();
            BrowserVM browserVM = new BrowserVM();

            try
            {
                string ApiUrl = _config["AppSettings:ApiUrl"];
                string user_id = "";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.GetAsync("api/Browser/GetDir?id_dir=" + id.ToString() + "&document_type_id=" + document_type_id + "&document_index_id=" + document_index_id + "&document_index_value=" + document_index_value + "&user_id=" + user_id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        browserVM = JsonConvert.DeserializeObject<BrowserVM>(JsonConvert.SerializeObject(resp.data));
                    }
                }

               
                List<SelectListItem> lst_document_types = new List<SelectListItem>();
                List<SelectListItem> lst_document_types_upload_file = new List<SelectListItem>();
                List<SelectListItem> lst_document_types_advance_search = new List<SelectListItem>();
                lst_document_types.Add(new SelectListItem { Text = "Select Type", Value = "SelectType" });
                lst_document_types.Add(new SelectListItem { Text = "Document Name", Value = "DocumentName" });
                lst_document_types_advance_search.Add(new SelectListItem { Text = "", Value = "" });
                lst_document_types_upload_file.Add(new SelectListItem { Text = "", Value = "" });

                foreach (var data in browserVM.document_types)
                {
                    lst_document_types.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                    lst_document_types_advance_search.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                    lst_document_types_upload_file.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                }

                ViewBag.collId = 1;
                ViewBag.dirId = id;
                ViewBag.lstDocTypes = lst_document_types;
                ViewBag.lst_document_types_upload_file = lst_document_types_upload_file;
                ViewBag.lstDocTypesAdvanceSearch = lst_document_types_advance_search;

                if (document_type_id != null && document_type_id != "") {
                    TempData["fDocumentTypeId"] = document_type_id;
                } else {
                    TempData["fDocumentTypeId"] = "";
                }

                if (document_index_id != null && document_index_id != "") {
                    TempData["fDocumentIndexId"] = document_index_id;
                } else {
                    TempData["fDocumentIndexId"] = "";
                }

                if (document_index_value != null && document_index_value != "") {
                    TempData["fDocumentIndexValue"] = document_index_value;
                } else  {
                    TempData["fDocumentIndexValue"] = "";
                }

                return View(browserVM);
            }
            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        public async Task<ActionResult<ResponseData>> GetDocVersions(int id, string document_id)
        {
            string? accessToken = "";

            ResponseData resp = new ResponseData();
            List<DocumentVersionVM> docVersionsVM = new List<DocumentVersionVM>();

            try
            {
                string ApiUrl = _config["AppSettings:ApiUrl"];
                string user_id = "";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.GetAsync("api/Browser/GetDocumentVersions?document_id=" + document_id + "&user_id=" + user_id + "");
                   
                    if (Res.IsSuccessStatusCode) {

                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        docVersionsVM = JsonConvert.DeserializeObject<List<DocumentVersionVM>>(JsonConvert.SerializeObject(resp.data));

                        if (resp.code != 200) {
                            TempData["Message"] = resp.message;
                            return Json(new { error = true, message = resp.message, _res = docVersionsVM });
                        }

                    } else {

                        TempData["Message"] = resp.message;
                        return Json(new { error = true, message = resp.message, _res = docVersionsVM });
                    }
                }

                return Json(new { error = false, message = resp.message, _res = docVersionsVM });

            }
            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        public async Task<ActionResult<ResponseData>> Delete(string document_type_id, string document_index_id, string document_index_value)
        {
            string? accessToken = "";

            ResponseData resp = new ResponseData();
            BrowserVM browserVM = new BrowserVM();

            try
            {
                string ApiUrl = _config["AppSettings:ApiUrl"];
                string user_id = "";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.GetAsync("api/Browser/GetDeleteDoc?document_type_id=" + document_type_id + 
                                                                    "&document_index_id=" + document_index_id + "&document_index_value=" + document_index_value + "&user_id=" + user_id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        browserVM = JsonConvert.DeserializeObject<BrowserVM>(JsonConvert.SerializeObject(resp.data));
                    }
                }


                List<SelectListItem> lst_document_types = new List<SelectListItem>();
                List<SelectListItem> lst_document_types_upload_file = new List<SelectListItem>();
                List<SelectListItem> lst_document_types_advance_search = new List<SelectListItem>();
                lst_document_types.Add(new SelectListItem { Text = "Select Type", Value = "SelectType" });
                lst_document_types.Add(new SelectListItem { Text = "Document Name", Value = "DocumentName" });
                lst_document_types_advance_search.Add(new SelectListItem { Text = "", Value = "" });

                foreach (var data in browserVM.document_types)
                {
                    lst_document_types.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                    lst_document_types_advance_search.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                }

                ViewBag.collId = 1;
                //ViewBag.dirId = id;
                ViewBag.lstDocTypes = lst_document_types;
                ViewBag.lstDocTypesAdvanceSearch = lst_document_types_advance_search;

                if (document_type_id != null && document_type_id != "") {
                    TempData["fDocumentTypeId"] = document_type_id;
                }
                else {
                    TempData["fDocumentTypeId"] = "";
                }

                if (document_index_id != null && document_index_id != "") {
                    TempData["fDocumentIndexId"] = document_index_id;
                }
                else {
                    TempData["fDocumentIndexId"] = "";
                }

                if (document_index_value != null && document_index_value != "") {
                    TempData["fDocumentIndexValue"] = document_index_value;
                }
                else {
                    TempData["fDocumentIndexValue"] = "";
                }

                return View(browserVM);
            }
            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        public async Task<ActionResult<ResponseData>> GetDeleteAdvanceSearch(string document_type_id, string index_value, string document_content, string tags_json)
        {
            string? accessToken = "";

            ResponseData resp = new ResponseData();
            BrowserVM browserVM = new BrowserVM();

            try
            {
                string ApiUrl = _config["AppSettings:ApiUrl"];
                string user_id = "";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.GetAsync("api/Browser/GetDeleteDocAdvanceSearch?document_type_id=" + document_type_id +
                                                                    "&index_value=" + index_value + "&document_content=" + document_content + "&tags_json=" + tags_json + "&user_id=" + user_id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        browserVM = JsonConvert.DeserializeObject<BrowserVM>(JsonConvert.SerializeObject(resp.data));
                    }
                }

                List<SelectListItem> lst_document_types = new List<SelectListItem>();
                List<SelectListItem> lst_document_types_upload_file = new List<SelectListItem>();
                List<SelectListItem> lst_document_types_advance_search = new List<SelectListItem>();
                lst_document_types.Add(new SelectListItem { Text = "Select Type", Value = "SelectType" });
                lst_document_types.Add(new SelectListItem { Text = "Document Name", Value = "DocumentName" });
                lst_document_types_advance_search.Add(new SelectListItem { Text = "", Value = "" });
                lst_document_types_upload_file.Add(new SelectListItem { Text = "", Value = "" });

                foreach (var data in browserVM.document_types)
                {
                    lst_document_types.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                    lst_document_types_advance_search.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                    lst_document_types_upload_file.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                }

                ViewBag.collId = 1;
                //ViewBag.dirId = id_dir;
                ViewBag.lstDocTypes = lst_document_types;
                ViewBag.lst_document_types_upload_file = lst_document_types_upload_file;
                ViewBag.lstDocTypesAdvanceSearch = lst_document_types_advance_search;

                TempData["fDocumentTypeId"] = document_type_id;

                return View(browserVM);
            }
            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }
        public async Task<ActionResult<ResponseData>> Favorite(int id, string document_type_id, string document_index_id, string document_index_value)
        {
            string? accessToken = "";

            ResponseData resp = new ResponseData();
            BrowserVM browserVM = new BrowserVM();

            try
            {
                string ApiUrl = _config["AppSettings:ApiUrl"];

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.GetAsync("api/Browser/GetFavoriteDoc?id_dir=" + id.ToString() + "&document_type_id=" + document_type_id + "&document_index_id=" + document_index_id + "&document_index_value=" + document_index_value);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        browserVM = JsonConvert.DeserializeObject<BrowserVM>(JsonConvert.SerializeObject(resp.data));
                    }
                }

                List<SelectListItem> lst_document_types = new List<SelectListItem>();
                lst_document_types.Add(new SelectListItem { Text = "", Value = "" });
                foreach (var data in browserVM.document_types)
                {
                    lst_document_types.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                }

                ViewBag.collId = 1;
                ViewBag.dirId = id;
                ViewBag.lstDocTypes = lst_document_types;

                if (document_type_id != null && document_type_id != "") {
                    TempData["fDocumentTypeId"] = document_type_id;
                } else {
                    TempData["fDocumentTypeId"] = "";
                }

                if (document_index_id != null && document_index_id != "") {
                    TempData["fDocumentIndexId"] = document_index_id;
                } else {
                    TempData["fDocumentIndexId"] = "";
                }

                if (document_index_value != null && document_index_value != "") {
                    TempData["fDocumentIndexValue"] = document_index_value;
                } else {
                    TempData["fDocumentIndexValue"] = "";
                }

                return View(browserVM);
            }
            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        public async Task<ActionResult<ResponseData>> GetDirAdvanceSearch(int? id_dir, string document_type_id, string index_value, string document_content, string tags_json)
        {
            string? accessToken = "";

            ResponseData resp = new ResponseData();
            BrowserVM browserVM = new BrowserVM();

            try
            {
                string ApiUrl = _config["AppSettings:ApiUrl"];
                string user_id = "";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.GetAsync("api/Browser/GetDirAdvanceSearch?id_dir=" + id_dir.ToString() + "&document_type_id=" + document_type_id + 
                                                                    "&index_value=" + index_value + "&document_content=" + document_content + "&tags_json=" + tags_json + "&user_id=" + user_id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        browserVM = JsonConvert.DeserializeObject<BrowserVM>(JsonConvert.SerializeObject(resp.data));
                    }
                }

                List<SelectListItem> lst_document_types = new List<SelectListItem>();
                List<SelectListItem> lst_document_types_upload_file = new List<SelectListItem>();
                List<SelectListItem> lst_document_types_advance_search = new List<SelectListItem>();
                lst_document_types.Add(new SelectListItem { Text = "Select Type", Value = "SelectType" });
                lst_document_types.Add(new SelectListItem { Text = "Document Name", Value = "DocumentName" });
                lst_document_types_advance_search.Add(new SelectListItem { Text = "", Value = "" });
                lst_document_types_upload_file.Add(new SelectListItem { Text = "", Value = "" });

                foreach (var data in browserVM.document_types)
                {
                    lst_document_types.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                    lst_document_types_advance_search.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                    lst_document_types_upload_file.Add(new SelectListItem { Text = data.document_type_name, Value = data.document_type_id.ToString() });
                }

                ViewBag.collId = 1;
                ViewBag.dirId = id_dir;
                ViewBag.lstDocTypes = lst_document_types;
                ViewBag.lst_document_types_upload_file = lst_document_types_upload_file;
                ViewBag.lstDocTypesAdvanceSearch = lst_document_types_advance_search;

                TempData["fDocumentTypeId"] = document_type_id;
                
                return View(browserVM);
            }
            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        public async Task<ActionResult> Folders(int id, int idx)
        {
            string? accessToken = "";

            List<JsTreeModel> jsTreeModel = new List<JsTreeModel>();

            string ApiUrl = _config["AppSettings:ApiUrl"];

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage Res = await client.GetAsync("api/Browser/"+ id + "/"+idx);
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    jsTreeModel = JsonConvert.DeserializeObject<List<JsTreeModel>>(Response);
                }
            }
            return Json(jsTreeModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFolder(WebDms.Models.Directory collection)
        {
            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? resp = new ResponseData();
            try
            {
                collection.status = 1;

                var FormString = JsonConvert.SerializeObject(collection);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    HttpContent httpContent = new StringContent(FormString);
                    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.PostAsync("api/Browser/CreateFolder", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        if (resp.code == 200)
                        {
                            TempData["Message"] = "create folder successfully.";
                            return RedirectToAction("Dir", "Home", new { id = collection.directory_id });
                        }
                        else
                        {
                            TempData["Message"] = resp.message;
                            return RedirectToAction("Dir", "Home", new { id = collection.directory_id });
                        }
                    }
                    else
                    {
                        TempData["Message"] = resp.message;
                        return RedirectToAction("Dir", "Home", new { id = collection.directory_id });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error : " + ex.Message;
                return RedirectToAction("Dir", "Home", new { id = collection.directory_id });
            }

        }

        [HttpPost]
        public async Task<IActionResult> UploadFiles(MultipleFilesModel content)
        {
           
            foreach (var file in content.files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        string base64file = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data
                        var extension = Path.GetExtension(file.FileName);

                        UploadFileVM upf = new UploadFileVM();
                        upf.directory_id = content.directory_id;
                        upf.document_type_id = content.document_type_id;
                        upf.index_value = content.index_value;
                        upf.document_name = content.DocumentName;
                        upf.file_name = content.DocumentName;
                        upf.content_type = file.ContentType;
                        upf.extension = extension;
                        upf.base64file = base64file;
                        upf.file_size = file.Length;
                        upf.reference = content.reference;
                        upf.document_no = content.document_no;
                        upf.date_version = content.date_version;
                        upf.expired_date = content.expired_date;
                        upf.document_tag = content.document_tag;

                        string? accessToken = "";

                        string ApiUrl = _config["AppSettings:ApiUrl"];
                        ResponseData? ObjResponse = new ResponseData();
                        try
                        {
                            var FormString = JsonConvert.SerializeObject(upf);
                            HttpClientHandler clientHandler = new HttpClientHandler();
                            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                            using (var client = new HttpClient(clientHandler))
                            {
                                client.BaseAddress = new Uri(ApiUrl);
                                HttpContent httpContent = new StringContent(FormString);
                                httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                                HttpResponseMessage Res = await client.PostAsync("api/Browser/UploadFile", httpContent);
                                if (Res.IsSuccessStatusCode)
                                {
                                    var Response = Res.Content.ReadAsStringAsync().Result;
                                    ObjResponse = JsonConvert.DeserializeObject<ResponseData>(Response);
                                    if (ObjResponse.code != 200)
                                    {
                                        TempData["Message"] = ObjResponse.message;
                                        return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                                    }
                                }
                                else
                                {
                                    TempData["Message"] = ObjResponse.message;
                                    return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            TempData["Message"] = ex.Message;
                            return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                        }
                    }

                } else {

                    TempData["Message"] = "File Not Found.";
                    return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                }

            }

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Dir", "Home", new { id = content.directory_id });
        }

        [HttpPost]
        public async Task<IActionResult> EditUploadFiles(MultipleFilesModel content)
        {
            if (content.date_version == null)
            {
                TempData["Message"] = "Date Version Cannot Be Empty.";
                return RedirectToAction("Dir", "Home", new { id = content.directory_id });
            }

            if (content.expired_date == null)
            {
                TempData["Message"] = "Expired Date Cannot Be Empty.";
                return RedirectToAction("Dir", "Home", new { id = content.directory_id });
            }

            foreach (var file in content.files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);

                        var fileBytes = ms.ToArray();
                        string base64file = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data
                        var extension = Path.GetExtension(file.FileName);

                        UploadFileVM upf = new UploadFileVM();
                        upf.id = content.id;
                        upf.directory_id = content.directory_id;
                        upf.document_type_id = content.document_type_id;
                        upf.index_value = content.index_value;
                        upf.document_name = content.DocumentName;
                        upf.file_name = content.DocumentName;
                        upf.content_type = file.ContentType;
                        upf.extension = extension;
                        upf.base64file = base64file;
                        upf.file_size = file.Length;
                        upf.reference = content.reference;
                        upf.document_no = content.document_no;
                        upf.date_version = content.date_version;
                        upf.expired_date = content.expired_date;

                        string? accessToken = "";

                        string ApiUrl = _config["AppSettings:ApiUrl"];
                        ResponseData? ObjResponse = new ResponseData();
                        try
                        {
                            var FormString = JsonConvert.SerializeObject(upf);
                            HttpClientHandler clientHandler = new HttpClientHandler();
                            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                            using (var client = new HttpClient(clientHandler))
                            {
                                client.BaseAddress = new Uri(ApiUrl);
                                HttpContent httpContent = new StringContent(FormString);
                                httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                                HttpResponseMessage Res = await client.PostAsync("api/Browser/EditUploadFile", httpContent);
                                if (Res.IsSuccessStatusCode)
                                {
                                    var Response = Res.Content.ReadAsStringAsync().Result;
                                    ObjResponse = JsonConvert.DeserializeObject<ResponseData>(Response);
                                    if (ObjResponse.code != 200)
                                    {
                                        TempData["Message"] = ObjResponse.message;
                                        return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                                    }
                                    
                                }
                                else
                                {
                                    TempData["Message"] = ObjResponse.message;
                                    return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["Message"] = ex.Message;
                            return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                        }
                    }

                } else {

                    TempData["Message"] = "File Not Found.";
                    return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                }

            }

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Dir", "Home", new { id = content.directory_id });
        }

        [HttpPost]
        public async Task<IActionResult> AddFileVersion(DocumentVersionVM content)
        {
            string user_id = "";
            foreach (var file in content.files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        string base64file = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data
                        var extension = Path.GetExtension(file.FileName);

                        UploadFileVM upf = new UploadFileVM();
                        upf.directory_id = content.directory_id;
                        upf.document_id = content.document_id;
                        upf.document_name = content.name;
                        upf.file_name = content.name;
                        upf.content_type = file.ContentType;
                        upf.extension = extension;
                        upf.base64file = base64file;
                        upf.file_size = file.Length;
                        upf.expired_date = content.expired_date;
                        upf.created_by = user_id;

                        string? accessToken = "";

                        string ApiUrl = _config["AppSettings:ApiUrl"];
                        ResponseData? ObjResponse = new ResponseData();
                        try
                        {
                            var FormString = JsonConvert.SerializeObject(upf);
                            HttpClientHandler clientHandler = new HttpClientHandler();
                            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                            using (var client = new HttpClient(clientHandler))
                            {
                                client.BaseAddress = new Uri(ApiUrl);
                                HttpContent httpContent = new StringContent(FormString);
                                httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                                HttpResponseMessage Res = await client.PostAsync("api/Browser/CreateDocumentVersion", httpContent);
                                if (Res.IsSuccessStatusCode)
                                {
                                    var Response = Res.Content.ReadAsStringAsync().Result;
                                    ObjResponse = JsonConvert.DeserializeObject<ResponseData>(Response);
                                    if (ObjResponse.code != 200)
                                    {
                                        TempData["Message"] = ObjResponse.message;
                                        return RedirectToAction("Index", "Home");
                                    }
                                }
                                else
                                {
                                    TempData["Message"] = ObjResponse.message;
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["Message"] = ex.Message;
                            return RedirectToAction("Index", "Home");
                        }
                    }

                }
                else
                {

                    TempData["Message"] = "File Not Found.";
                    return RedirectToAction("Index", "Home");
                }

            }

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Dir", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddFileVersionDir(DocumentVersionVM content)
        {
            string user_id = "";
            foreach (var file in content.files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        string base64file = Convert.ToBase64String(fileBytes);
                        // act on the Base64 data
                        var extension = Path.GetExtension(file.FileName);

                        UploadFileVM upf = new UploadFileVM();
                        upf.directory_id = content.directory_id;
                        upf.document_id = content.document_id;
                        upf.document_name = content.name;
                        upf.file_name = content.name;
                        upf.content_type = file.ContentType;
                        upf.extension = extension;
                        upf.base64file = base64file;
                        upf.file_size = file.Length;
                        upf.expired_date = content.expired_date;
                        upf.created_by = user_id;

                        string? accessToken = "";

                        string ApiUrl = _config["AppSettings:ApiUrl"];
                        ResponseData? ObjResponse = new ResponseData();
                        try
                        {
                            var FormString = JsonConvert.SerializeObject(upf);
                            HttpClientHandler clientHandler = new HttpClientHandler();
                            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                            using (var client = new HttpClient(clientHandler))
                            {
                                client.BaseAddress = new Uri(ApiUrl);
                                HttpContent httpContent = new StringContent(FormString);
                                httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                                client.DefaultRequestHeaders.Clear();
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                                HttpResponseMessage Res = await client.PostAsync("api/Browser/CreateDocumentVersion", httpContent);
                                if (Res.IsSuccessStatusCode)
                                {
                                    var Response = Res.Content.ReadAsStringAsync().Result;
                                    ObjResponse = JsonConvert.DeserializeObject<ResponseData>(Response);
                                    if (ObjResponse.code != 200)
                                    {
                                        TempData["Message"] = ObjResponse.message;
                                        return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                                    }
                                }
                                else
                                {
                                    TempData["Message"] = ObjResponse.message;
                                    return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["Message"] = ex.Message;
                            return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                        }
                    }

                }
                else
                {

                    TempData["Message"] = "File Not Found.";
                    return RedirectToAction("Dir", "Home", new { id = content.directory_id });
                }

            }

            TempData["Message"] = "File successfully uploaded to File System.";
            return RedirectToAction("Dir", "Home", new { id = content.directory_id });
        }

        //[HttpGet]
        //[Route("/Home/DownloadFiles/{id}")]
        //public async Task<ActionResult> DownloadFiles(int id)
        //{

        //    string? accessToken = "";
        //    string ApiUrl = _config["AppSettings:ApiUrl"];
        //    ResponseData? resp = new ResponseData();
        //    DownloadFileVM browserVM = new DownloadFileVM();
        //    try
        //    {

        //        HttpClientHandler clientHandler = new HttpClientHandler();
        //        clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        //        using (var client = new HttpClient(clientHandler))
        //        {
        //            client.BaseAddress = new Uri(ApiUrl);
        //            client.DefaultRequestHeaders.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
        //            HttpResponseMessage Res = await client.GetAsync("api/Browser/DownloadFile/" + id.ToString());
        //            if (Res.IsSuccessStatusCode)
        //            {
        //                var Response = Res.Content.ReadAsStringAsync().Result;
        //                resp = JsonConvert.DeserializeObject<ResponseData>(Response);
        //                browserVM = JsonConvert.DeserializeObject<DownloadFileVM>(JsonConvert.SerializeObject(resp.data));
        //            }
        //        }

        //        //var file_byte = doc.decrypt_file;
        //        //return File(doc.decrypt_file, doc.content_type, doc.file_name + doc.extension);

        //        return File(browserVM.decrypt_file, browserVM.content_type, browserVM.file_name + browserVM.extension);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }


        //}

        [HttpGet]
        [Route("DownloadFiles")]
        public async Task<ActionResult> DownloadFiles(int id)
        {

            string? accessToken = "";
            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? resp = new ResponseData();
            DownloadFileVM browserVM = new DownloadFileVM();
            try
            {

                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.GetAsync("api/Browser/DownloadFile/" + id.ToString());
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        browserVM = JsonConvert.DeserializeObject<DownloadFileVM>(JsonConvert.SerializeObject(resp.data));
                    }
                }

                //var file_byte = doc.decrypt_file;
                //return File(doc.decrypt_file, doc.content_type, doc.file_name + doc.extension);

                return File(browserVM.decrypt_file, browserVM.content_type, browserVM.file_name + browserVM.extension);
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        [HttpPost]
        [Route("/Home/UpdateFavorite")]
        public async Task<ActionResult<ResponseData>> UpdateFavorite([FromBody] DocumentVM content)
        {

            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? resp = new ResponseData();
            DownloadFileVM browserVM = new DownloadFileVM();
            try
            {
                var FormString = JsonConvert.SerializeObject(content);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    HttpContent httpContent = new StringContent(FormString);
                    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.PutAsync("api/Browser/UpdateFavorite", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        if (resp.code != 200)
                        {
                            TempData["Message"] = resp.message;
                            return Json(new { error = true, message = resp.message });
                        }

                    } else  {

                        TempData["Message"] = resp.message;
                        return Json(new { error = true, message = resp.message });
                    }
                }

                return Json(new { error = false, message = resp.message });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }


        }

        [HttpPost]
        [Route("/Home/UpdateRestoreFavorite")]
        public async Task<ActionResult<ResponseData>> UpdateRestoreFavorite([FromBody] DocumentVM content)
        {

            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? resp = new ResponseData();
            DownloadFileVM browserVM = new DownloadFileVM();
            try
            {
                var FormString = JsonConvert.SerializeObject(content);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    HttpContent httpContent = new StringContent(FormString);
                    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.PutAsync("api/Browser/UpdateRestoreFavorite", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        if (resp.code != 200)
                        {
                            TempData["Message"] = resp.message;
                            return Json(new { error = true, message = resp.message });
                        }

                    }
                    else
                    {

                        TempData["Message"] = resp.message;
                        return Json(new { error = true, message = resp.message });
                    }
                }

                return Json(new { error = false, message = resp.message });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }


        }

        [HttpGet]
        [Route("/Home/PreviewFile")]
        public async Task<ActionResult<ResponseData>> PreviewFile(int id)
        {

            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? resp = new ResponseData();
            DownloadFileVM browserVM = new DownloadFileVM();
            try
            {
                DownloadFileVM Doc = new DownloadFileVM();
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.GetAsync("api/Browser/PreviewFile/" + id.ToString());
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        browserVM = JsonConvert.DeserializeObject<DownloadFileVM>(JsonConvert.SerializeObject(resp.data));
                    }
                }

                return Json(new { encrypt_file = browserVM.encrypt_file, content_type = browserVM.content_type });
            }
            catch (Exception ex)
            {
                return Json(new { encrypt_file = "", content_type = "" });
            }


        }

        [HttpGet]
        public async Task<ActionResult> DeleteFile(int? id, int? idx)
        {
            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? resp = new ResponseData();
            try
            {
                string user_id = "";
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.DeleteAsync("api/Browser/DeleteFile?id=" + id + "&user_id=" + user_id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                    }

                }

                return Json(new  { resp = resp, error = resp.error, message = resp.message });
            }
            catch (Exception ex)
            {

                return Json(new { resp = resp, error = resp.error, message = ex.Message });
            }
           
        }

        [HttpPost]
        [Route("/Home/UpdateRestoreDelete")]
        public async Task<ActionResult<ResponseData>> UpdateRestoreDelete([FromBody] DocumentVM content)
        {

            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? resp = new ResponseData();
            DownloadFileVM browserVM = new DownloadFileVM();
            try
            {
                var FormString = JsonConvert.SerializeObject(content);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    HttpContent httpContent = new StringContent(FormString);
                    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.PutAsync("api/Browser/UpdateRestoreDelete", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        if (resp.code != 200)
                        {
                            TempData["Message"] = resp.message;
                            return Json(new { error = true, message = resp.message });
                        }

                    }
                    else
                    {

                        TempData["Message"] = resp.message;
                        return Json(new { error = true, message = resp.message });
                    }
                }

                return Json(new { error = false, message = resp.message });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }


        }

        public bool ByteArrayToFile(string fileName, byte[] byteArray)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }

        [HttpPost]
        [Route("/Home/AddTags")]
        public async Task<ActionResult<ResponseData>> AddTags([FromBody] DocumentTags content)
        {

            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? resp = new ResponseData();
            DownloadFileVM browserVM = new DownloadFileVM();
            try
            {
                var FormString = JsonConvert.SerializeObject(content);
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    HttpContent httpContent = new StringContent(FormString);
                    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.PostAsync("api/Browser/CreateTags", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        if (resp.code != 200)
                        {
                            TempData["Message"] = resp.message;
                            return Json(new { error = true, message = resp.message });
                        }

                    }
                    else
                    {

                        TempData["Message"] = resp.message;
                        return Json(new { error = true, message = resp.message });
                    }
                }

                return Json(new { error = false, message = resp.message });
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = ex.Message });
            }


        }

        public byte[] sha512(byte[] bytes)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] hash = sha512.ComputeHash(bytes);
            //return GetStringFromHash(hash);
            return hash;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
