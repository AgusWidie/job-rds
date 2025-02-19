using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebDms.Models;
using WebDms.ViewModels;

namespace WebDms.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly ILogger<CollectionsController> _logger;
        private IConfiguration _config;
        public CollectionsController(ILogger<CollectionsController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public async Task<ActionResult> Index()
        {
           
            string? accessToken = "";
            ResponseListData resp = new ResponseListData();
            List<Collection>? collections = new List<Collection>();

            string ApiUrl = _config["AppSettings:ApiUrl"];

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                HttpResponseMessage Res = await client.GetAsync("api/Collections");
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<ResponseListData>(Response);
                    collections = JsonConvert.DeserializeObject<List<Collection>>(JsonConvert.SerializeObject(resp.data));
                }
                else
                {
                    return RedirectToAction("Index", "Auth");
                }

                List<SelectItem> statusSelectItems = new List<SelectItem>();
                statusSelectItems.Add(new SelectItem { value = 0, text = "Tidak Aktif" });
                statusSelectItems.Add(new SelectItem { value = 1, text = "Aktif" });
                ViewBag.Status = statusSelectItems;
                return View(collections);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Collection collection)
        {
           
            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? resp = new ResponseData();
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
                    HttpResponseMessage Res = await client.PostAsync("api/Collections/Create", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        if (resp.code != 200)
                        {
                            TempData["Message"] = resp.message;
                            return RedirectToAction(nameof(Index));
                        }
                       
                    }
                    else
                    {
                        TempData["Message"] = resp.message;
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = resp.message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Collection collection)
        {

            string? accessToken = "";

            string ApiUrl = _config["AppSettings:ApiUrl"];
            ResponseData? resp = new ResponseData();
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
                    HttpResponseMessage Res = await client.PostAsync("api/Collections/Update", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        var Response = Res.Content.ReadAsStringAsync().Result;
                        resp = JsonConvert.DeserializeObject<ResponseData>(Response);
                        if (resp.code != 200)
                        {
                            TempData["Message"] = resp.message;
                            return RedirectToAction(nameof(Index));
                        }
                       
                    }
                    else
                    {
                        TempData["Message"] = resp.message;
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["Message"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = resp.message;
            return RedirectToAction(nameof(Index));
        }
    }
}
