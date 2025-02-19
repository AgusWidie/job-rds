using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using WebBarangBukti.Help;
using WebBarangBukti.Models;
using WebBarangBukti.Service;
using WebBarangBukti.Service.IService;
using WebBarangBukti.ViewModels;

namespace WebBarangBukti.Controllers
{
    public class HdTransaksiController : Controller
    {
        private readonly ILogger<HdTransaksiController> _logger;
        private IConfiguration _config;
        private IHdTransaksiService _hdTransaksiService;

        public HdTransaksiController(ILogger<HdTransaksiController> logger, IConfiguration config, IHdTransaksiService hdTransaksiService)
        {
            _logger = logger;
            _config = config;
            _hdTransaksiService = hdTransaksiService;
        }

        public IActionResult Index(CancellationToken cancellationToken)
        {
            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _hdTransaksiService.ListDataHdTransaksi(accessToken, cancellationToken);
            var dataList = JsonConvert.DeserializeObject<List<HdTransaksi>>(JsonConvert.SerializeObject(resp.Result.Data));

            List<SelectListItem> jenisTransaksiSelectItems = new List<SelectListItem>();
            jenisTransaksiSelectItems.Add(new SelectListItem { Text = "Sita", Value = "0" });
            jenisTransaksiSelectItems.Add(new SelectListItem { Text = "Lelang", Value = "1" });
            ViewBag.JenisTrans = jenisTransaksiSelectItems;
            return View(dataList);
        }


        public IActionResult DetailTransaksi(string IdTransaksi, string NoPerkara, CancellationToken cancellationToken)
        {
            List<SelectListItem> lstItemDtBarbuk = new List<SelectListItem>();
            lstItemDtBarbuk.Add(new SelectListItem { Text = "", Value = "" });

            List<SelectListItem> jenisTransaksi = new List<SelectListItem>();
            jenisTransaksi.Add(new SelectListItem { Text = "", Value = "" });
            jenisTransaksi.Add(new SelectListItem { Text = "Pelelangan", Value = "0" });
            jenisTransaksi.Add(new SelectListItem { Text = "Penyerahan", Value = "1" });

            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _hdTransaksiService.ListDataHdTransaksiById(IdTransaksi, accessToken, cancellationToken);
            var resp_item_barbuk = _hdTransaksiService.ListItemBarangBukti(NoPerkara, accessToken, cancellationToken);

            var listDtBarbuk = JsonConvert.DeserializeObject<List<DtBarangBuktiModel>>(JsonConvert.SerializeObject(resp_item_barbuk.Result.Data));
            if(listDtBarbuk != null) {

                foreach(var item in listDtBarbuk)
                {
                    lstItemDtBarbuk.Add(new SelectListItem { Text = item.NamaBarangBukti, Value = item.IdDtBarangBukti });
                }
              
            }

            ViewBag.NamaBarangBukti = lstItemDtBarbuk;
            ViewBag.JenisTransaksi = jenisTransaksi;
            var dataList = JsonConvert.DeserializeObject<List<HdTransaksi>>(JsonConvert.SerializeObject(resp.Result.Data));
            return View(dataList);
        }

        public async Task<ActionResult> CreateTransaksi(HdTransaksi param, CancellationToken cancellationToken)
        {

            string? accessToken = HttpContext.Session.GetString("token");

            if(param.files != null)
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

            var res_data = _hdTransaksiService.AddHdTransaksi(param, accessToken, cancellationToken);
            if (res_data.Result.Error == false) {

                TempData["MessageSuccessTransaksi"] = res_data.Result.Message;
                return RedirectToAction("Index", "HdTransaksi");

            } else {

                TempData["MessageErrorTransaksi"] = res_data.Result.Message;
                return RedirectToAction("Index", "HdTransaksi");
            }

        }

        public async Task<ActionResult> UpdateTransaksi(HdTransaksi param, CancellationToken cancellationToken)
        {

            string? accessToken = HttpContext.Session.GetString("token");

            if(param.files != null)
            {
                var file = param.files;
                if (file.Length > 0) {

                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        var fileBytes = ms.ToArray();
                        string base64file = Convert.ToBase64String(fileBytes);
                        var extension = Path.GetExtension(file.FileName);

                        param.Base64File = base64file;
                        param.Extension = extension;
                        param.FileSize = (int?)file.Length;
                        param.ContentType = file.ContentType;
                        param.FileName = file.FileName;
                    }
                } 
            }

            var res_data = _hdTransaksiService.UpdateHdTransaksi(param, accessToken, cancellationToken);
            if (res_data.Result.Error == false) {

                TempData["MessageSuccessTransaksi"] = res_data.Result.Message;
                return RedirectToAction("Index", "HdTransaksi");

            } else {

                TempData["MessageErrorTransaksi"] = res_data.Result.Message;
                return RedirectToAction("Index", "HdTransaksi");
            }
        }

        public async Task<ActionResult> PreviewFile(string IdHdTransaksi, CancellationToken cancellationToken)
        {
            //if (HttpContext.Session.GetString("user_id") == null)
            //{
            //    //return RedirectToAction("Logout", "Auth");
            //}

            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _hdTransaksiService.PreviewFile(IdHdTransaksi, accessToken, cancellationToken);
            var dataList = JsonConvert.DeserializeObject<GetFileModel>(JsonConvert.SerializeObject(resp.Result.Data));
            return Json(new { res = dataList, error = resp.Result.Error, message = resp.Result.Message });

        }
    }
}
