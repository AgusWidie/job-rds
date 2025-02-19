using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading;
using WebBarangBukti.Models;
using WebBarangBukti.Service;
using WebBarangBukti.Service.IService;
using WebBarangBukti.ViewModels;

namespace WebBarangBukti.Controllers
{
    public class DtTransaksiController : Controller
    {
        private readonly ILogger<DtTransaksiController> _logger;
        private IConfiguration _config;
        private IDtTransaksiService _dtTransaksiService;

        public DtTransaksiController(ILogger<DtTransaksiController> logger, IConfiguration config, IDtTransaksiService dtTransaksiService)
        {
            _logger = logger;
            _config = config;
            _dtTransaksiService = dtTransaksiService;
        }

        public IActionResult GetDataDetailTransaksi(string IdHdTransaksi, string NoPerkara, CancellationToken cancellationToken)
        {
            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _dtTransaksiService.ListDataDtTransaksi(IdHdTransaksi, NoPerkara, accessToken, cancellationToken);
            var dataList = JsonConvert.DeserializeObject<List<DtTransaksiModel>>(JsonConvert.SerializeObject(resp.Result.Data));
            return Json(new { res = dataList, error = resp.Result.Error, message = resp.Result.Message });
        }

        public async Task<ActionResult> CreateDetailTransaksi(DtTransaksi param, CancellationToken cancellationToken)
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

            var res_data = _dtTransaksiService.AddDtTransaksi(param, accessToken, cancellationToken);
            if (res_data.Result.Error == false)  {

                TempData["MessageSuccessDetailTransaksi"] = res_data.Result.Message;
                return RedirectToAction("DetailTransaksi", "HdTransaksi", new { IdTransaksi = param.IdTransaksi, NoPerkara = param.NoPerkara });

            } else {

                TempData["MessageErrorDetailTransaksi"] = res_data.Result.Message;
                return RedirectToAction("DetailTransaksi", "HdTransaksi", new { IdTransaksi = param.IdTransaksi, NoPerkara = param.NoPerkara });
            }

            //return Json(new { res = res_data.Result.Data, error = res_data.Result.Error, message = res_data.Result.Message });
        }

        public async Task<ActionResult> UpdateDetailTransaksi(DtTransaksi param, CancellationToken cancellationToken)
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
            var res_data = _dtTransaksiService.UpdateDtTransaksi(param, accessToken, cancellationToken);
            //return Json(new { res = res_data.Result.Data, error = res_data.Result.Error, message = res_data.Result.Message });
            if (res_data.Result.Error == false) {

                TempData["MessageSuccessDetailTransaksi"] = res_data.Result.Message;
                return RedirectToAction("DetailTransaksi", "HdTransaksi", new { IdTransaksi = param.IdTransaksi, NoPerkara = param.NoPerkara });

            } else {

                TempData["MessageErrorDetailTransaksi"] = res_data.Result.Message;
                return RedirectToAction("DetailTransaksi", "HdTransaksi", new { IdTransaksi = param.IdTransaksi, NoPerkara = param.NoPerkara });
            }
        }

        public async Task<ActionResult> PreviewFile(int Id, CancellationToken cancellationToken)
        {
            //if (HttpContext.Session.GetString("user_id") == null)
            //{
            //    //return RedirectToAction("Logout", "Auth");
            //}

            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _dtTransaksiService.PreviewFile(Id, accessToken, cancellationToken);
            var dataList = JsonConvert.DeserializeObject<GetFileModel>(JsonConvert.SerializeObject(resp.Result.Data));
            return Json(new { res = dataList, error = resp.Result.Error, message = resp.Result.Message });

        }
    }
}
