using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Threading;
using WebBarangBukti.Models;
using WebBarangBukti.Service;
using WebBarangBukti.Service.IService;
using WebBarangBukti.ViewModels;

namespace WebBarangBukti.Controllers
{

    public class HdBarangBuktiController : Controller
    {
        private readonly ILogger<HdBarangBuktiController> _logger;
        private IConfiguration _config;
        private IHdBarangBuktiService _hdBarangBuktiService;

        public HdBarangBuktiController(ILogger<HdBarangBuktiController> logger, IConfiguration config, IHdBarangBuktiService hdBarangBuktiService)
        {
            _logger = logger;
            _config = config;
            _hdBarangBuktiService = hdBarangBuktiService;
        }

        public IActionResult Index(CancellationToken cancellationToken)
        {
            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _hdBarangBuktiService.ListDataHdBarangBukti(accessToken, cancellationToken);
            var dataList = JsonConvert.DeserializeObject<List<HdBarangBukti>>(JsonConvert.SerializeObject(resp.Result.Data));
            return View(dataList);
        }

        public IActionResult DetailBarangBukti(string IdHdBarangBukti, CancellationToken cancellationToken)
        {
            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _hdBarangBuktiService.ListDataHdBarangBuktiById(IdHdBarangBukti, accessToken, cancellationToken);
            var dataList = JsonConvert.DeserializeObject<List<HdBarangBukti>>(JsonConvert.SerializeObject(resp.Result.Data));

            List<SelectListItem> statusEksekusi = new List<SelectListItem>();
            statusEksekusi.Add(new SelectListItem { Text = "Pilih Status Barang Bukti", Value = "" });
            statusEksekusi.Add(new SelectListItem { Text = "Dirampas Untuk Negara", Value = "Dirampas Untuk Negara" });
            statusEksekusi.Add(new SelectListItem { Text = "Dirampas Untuk Dimusnahkan", Value = "Dirampas Untuk Dimusnahkan" });
            statusEksekusi.Add(new SelectListItem { Text = "Dikembalikan kepada yang di tentukan dalam Putusan", Value = "Dikembalikan kepada yang di tentukan dalam Putusan" });
            statusEksekusi.Add(new SelectListItem { Text = "Dilekatkan pada Berkas Perkara", Value = "Dilekatkan pada Berkas Perkara" });
            ViewBag.StatusEksekusi = statusEksekusi;

            List<SelectListItem> statusAkhir = new List<SelectListItem>();
            statusAkhir.Add(new SelectListItem { Text = "", Value = "" });
            ViewBag.StatusAkhir = statusAkhir;

            return View(dataList);
        }

        public async Task<ActionResult> CreateBarangBukti(HdBarangBukti param, CancellationToken cancellationToken)
        {

            string? accessToken = HttpContext.Session.GetString("token");

            if (param.files != null)
            {
                var file = param.files;
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string base64file = Convert.ToBase64String(fileBytes);
                        string? extension = Path.GetExtension(file.FileName);

                        param.Base64File = base64file;
                        param.Extension = extension;
                        param.FileSize = (int?)file.Length;
                        param.ContentType = file.ContentType;
                        param.FileName = file.FileName;
                    }
                }

            }

            var res_data = _hdBarangBuktiService.AddHdBarangBukti(param, accessToken, cancellationToken);
            if (res_data.Result.Error == false)
            {

                TempData["MessageSuccessBarangBukti"] = res_data.Result.Message;
                return RedirectToAction("Index", "HdBarangBukti");

            }
            else
            {

                TempData["MessageErrorBarangBukti"] = res_data.Result.Message;
                return RedirectToAction("Index", "HdBarangBukti");
            }
        }

        public async Task<ActionResult> UpdateBarangBukti(HdBarangBukti param, CancellationToken cancellationToken)
        {

            string? accessToken = HttpContext.Session.GetString("token");

            if (param.files != null)
            {

                var file = param.files;
                if (file.Length > 0)
                {

                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string base64file = Convert.ToBase64String(fileBytes);
                        string? extension = Path.GetExtension(file.FileName);

                        param.Base64File = base64file;
                        param.Extension = extension;
                        param.FileSize = (int?)file.Length;
                        param.ContentType = file.ContentType;
                        param.FileName = file.FileName;
                    }
                }

            }

            var res_data = _hdBarangBuktiService.UpdateHdBarangBukti(param, accessToken, cancellationToken);
            if (res_data.Result.Error == false)
            {

                TempData["MessageSuccessBarangBukti"] = res_data.Result.Message;
                return RedirectToAction("Index", "HdBarangBukti");

            }
            else
            {

                TempData["MessageErrorBarangBukti"] = res_data.Result.Message;
                return RedirectToAction("Index", "HdBarangBukti");
            }
        }

        public async Task<ActionResult> PreviewFile(string IdHdBarangBukti, CancellationToken cancellationToken)
        {
            //if (HttpContext.Session.GetString("user_id") == null)
            //{
            //    //return RedirectToAction("Logout", "Auth");
            //}

            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _hdBarangBuktiService.PreviewFile(IdHdBarangBukti, accessToken, cancellationToken);
            var dataList = JsonConvert.DeserializeObject<GetFileModel>(JsonConvert.SerializeObject(resp.Result.Data));
            return Json(new { res = dataList, error = resp.Result.Error, message = resp.Result.Message });

        }
    }

}
