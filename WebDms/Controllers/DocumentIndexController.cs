using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebDms.Models;
using WebDms.ViewModels;

namespace WebDms.Controllers
{
    public class DocumentIndexController : Controller
    {
        private readonly ILogger<DocumentIndexController> _logger;
        private IConfiguration _config;
        public DocumentIndexController(ILogger<DocumentIndexController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<ActionResult> Index(string document_type_id)
        {
            string? accessToken = "";

            ResponseData resp = new ResponseData();
            DocumentIndexVM? docIndex = new DocumentIndexVM();

            string ApiUrl = _config["AppSettings:ApiUrl"];
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage Res = await client.GetAsync("api/DocumentIndex?document_type_id=" + document_type_id);
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                    docIndex = JsonConvert.DeserializeObject<DocumentIndexVM>(JsonConvert.SerializeObject(resp.data));
                }
                else
                {
                    return RedirectToAction("Index", "Auth");
                }

                List<SelectItem> statusSelectItems = new List<SelectItem>();
                statusSelectItems.Add(new SelectItem { value = 0, text = "Tidak Aktif" });
                statusSelectItems.Add(new SelectItem { value = 1, text = "Aktif" });
                ViewBag.Status = statusSelectItems;

                List<SelectListItem> ruleSelectItems = new List<SelectListItem>();
                ruleSelectItems.Add(new SelectListItem { Text = "", Value = "" });
                ruleSelectItems.Add(new SelectListItem { Text = "Text", Value = "Text" });
                ruleSelectItems.Add(new SelectListItem { Text = "Number", Value = "Number" });
                ruleSelectItems.Add(new SelectListItem { Text = "Date", Value = "Date" });
                ViewBag.Rules = ruleSelectItems;

                return View(docIndex);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] DocumentIndex collection)
        {
            
            string? accessToken = "";
            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? ObjResponse = new ResponseData();
            try
            {
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
                    HttpResponseMessage Res = await client.PostAsync("api/DocumentIndex/Create", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<ResponseData>(Response);
                        if (ObjResponse.code != 200)
                        {
                            TempData["Message"] = ObjResponse.message;
                            //return RedirectToAction("Index", "DocumentIndex", new { document_type_id = collection.document_type_id });
                            return Json(new { ObjResponse = ObjResponse, error = ObjResponse.error, message = ObjResponse.message });
                        }
                    } else {

                        TempData["Message"] = ObjResponse.message;
                        //return RedirectToAction("Index", "DocumentIndex", new { document_type_id = collection.document_type_id });
                        return Json(new { ObjResponse = ObjResponse, error = ObjResponse.error, message = ObjResponse.message });
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                //return RedirectToAction("Index", "DocumentIndex", new { document_type_id = collection.document_type_id });
                return Json(new { ObjResponse = ObjResponse, error = ObjResponse.error, message = ex.Message });
            }

            TempData["Message"] = ObjResponse.message;
            //return RedirectToAction("Index", "DocumentIndex", new { document_type_id = collection.document_type_id });
            return Json(new { ObjResponse = ObjResponse, error = ObjResponse.error, message = ObjResponse.message });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DocumentIndex collection)
        {

            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? ObjResponse = new ResponseData();
            try
            {
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
                    HttpResponseMessage Res = await client.PostAsync("api/DocumentIndex/Update", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<ResponseData>(Response);
                        if (ObjResponse.code != 200)
                        {
                            TempData["Message"] = ObjResponse.message;
                            return RedirectToAction("Index", "DocumentIndex", new { document_type_id = collection.document_type_id });
                        }
                        
                    }
                    else
                    {
                        TempData["Message"] = ObjResponse.message;
                        return RedirectToAction("Index", "DocumentIndex", new { document_type_id = collection.document_type_id });
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction("Index", "DocumentIndex", new { document_type_id = collection.document_type_id });
            }

            TempData["Message"] = ObjResponse.message;
            return RedirectToAction("Index", "DocumentIndex", new { document_type_id = collection.document_type_id });
        }

        [HttpGet]
        public async Task<ActionResult> DeleteIndex(int? id)
        {
            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? ObjResponse = new ResponseData();
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
                    HttpResponseMessage Res = await client.DeleteAsync("api/DocumentIndex/Delete/" + id);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        ObjResponse = JsonConvert.DeserializeObject<ResponseData>(Response);
                    }

                }

                return Json(new { ObjResponse = ObjResponse, error = ObjResponse.error, message = ObjResponse.message });
            }
            catch (Exception ex)
            {

                return Json(new { ObjResponse = ObjResponse, error = ObjResponse.error, message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetIndexDocType(string DocumentTypeId)
        {
            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseListData? resp = new ResponseListData();
            List<DocumentIndex> lst_doc_index = new List<DocumentIndex>();
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
                    HttpResponseMessage Res = await client.GetAsync("api/DocumentIndex/GetListDataIndex?document_type_id=" + DocumentTypeId);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseListData>(Response);
                        lst_doc_index = JsonConvert.DeserializeObject<List<DocumentIndex>>(JsonConvert.SerializeObject(resp.data));
                    }

                }

                return Json(new { res = lst_doc_index, error = resp.error, message = resp.message });
            }
            catch (Exception ex)
            {

                return Json(new { res = lst_doc_index, error = resp.error, message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetIndexValueDocType(string DocumentTypeId)
        {
            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseListData? resp = new ResponseListData();
            List<DocumentIndexValueVM> lst_doc_index = new List<DocumentIndexValueVM>();
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
                    HttpResponseMessage Res = await client.GetAsync("api/DocumentIndex/GetDataIndexValue?document_type_id=" + DocumentTypeId);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseListData>(Response);
                        lst_doc_index = JsonConvert.DeserializeObject<List<DocumentIndexValueVM>>(JsonConvert.SerializeObject(resp.data));
                    }

                }

                return Json(new { res = lst_doc_index, error = resp.error, message = resp.message });
            }
            catch (Exception ex)
            {

                return Json(new { res = lst_doc_index, error = resp.error, message = ex.Message });
            }

        }

        [HttpGet]
        public async Task<ActionResult> GetIndex(string DocumentTypeId, string DocumentIndexId)
        {
            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseListData? resp = new ResponseListData();
            List<DocumentIndex> lst_doc_index = new List<DocumentIndex>();
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
                    HttpResponseMessage Res = await client.GetAsync("api/DocumentIndex/GetDataIndex?document_type_id=" + DocumentTypeId + "&document_index_id=" + DocumentIndexId);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseListData>(Response);
                        lst_doc_index = JsonConvert.DeserializeObject<List<DocumentIndex>>(JsonConvert.SerializeObject(resp.data));
                    }

                }

                return Json(new { res = lst_doc_index, error = resp.error, message = resp.message });
            }
            catch (Exception ex)
            {

                return Json(new { res = lst_doc_index, error = resp.error, message = ex.Message });
            }

        }
    }
}
