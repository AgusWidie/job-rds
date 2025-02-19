using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using WebBarangBukti.Models;
using WebBarangBukti.Service;
using WebBarangBukti.Service.IService;
using WebBarangBukti.ViewModels;

namespace WebBarangBukti.Controllers
{

    public class DtBarangBuktiController : Controller
    {
        private readonly ILogger<DtBarangBuktiController> _logger;
        private IConfiguration _config;
        private IDtBarangBuktiService _dtBarangBuktiService;

        public DtBarangBuktiController(ILogger<DtBarangBuktiController> logger, IConfiguration config, IDtBarangBuktiService dtBarangBuktiService)
        {
            _logger = logger;
            _config = config;
            _dtBarangBuktiService = dtBarangBuktiService;
        }

        public async Task<ActionResult> GetDataDetailBarangBukti(string IdHdBarangBukti, CancellationToken cancellationToken)
        {
            //if (HttpContext.Session.GetString("user_id") == null)
            //{
            //    //return RedirectToAction("Logout", "Auth");
            //}

            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _dtBarangBuktiService.ListDataDtBarangBukti(IdHdBarangBukti, accessToken, cancellationToken);
            var dataList = JsonConvert.DeserializeObject<List<DtBarangBukti>>(JsonConvert.SerializeObject(resp.Result.Data)).OrderByDescending(x => x.UpdateAt);
            return Json(new { res = dataList, error = resp.Result.Error, message = resp.Result.Message });
        }

        public async Task<ActionResult> CreateDetailBarangBukti(DtBarangBukti param, CancellationToken cancellationToken)
        {
           
            string? accessToken = HttpContext.Session.GetString("token");
            if (param.files != null) {

                var file = param.files;
                if (file.Length > 0) {

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

            var res_data = _dtBarangBuktiService.AddDtBarangBukti(param, accessToken, cancellationToken);
            //return Json(new { res = res_data.Result.Data, error = res_data.Result.Error, message = res_data.Result.Message });
            if (res_data.Result.Error == false) {

                TempData["MessageSuccessDetailBarbuk"] = res_data.Result.Message;
                return RedirectToAction("DetailBarangBukti", "HdBarangBukti", new { IdHdBarangBukti = param.IdHdBarangBukti });

            } else {

                TempData["MessageErrorDetailBarbuk"] = res_data.Result.Message;
                return RedirectToAction("DetailBarangBukti", "HdBarangBukti", new { IdHdBarangBukti = param.IdHdBarangBukti });
            }
        }

        public async Task<ActionResult> UpdateDetailBarangBukti(DtBarangBukti param, CancellationToken cancellationToken)
        {

            string? accessToken = HttpContext.Session.GetString("token");
            if (param.files != null) {

                var file = param.files;
                if (file.Length > 0)  {

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

            var res_data = _dtBarangBuktiService.UpdateDtBarangBukti(param, accessToken, cancellationToken);
            //return Json(new { res = res_data.Result.Data, error = res_data.Result.Error, message = res_data.Result.Message });
            if (res_data.Result.Error == false) {

                TempData["MessageSuccessDetailBarbuk"] = res_data.Result.Message;
                return RedirectToAction("DetailBarangBukti", "HdBarangBukti", new { IdHdBarangBukti = param.IdHdBarangBukti });

            } else {

                TempData["MessageErrorDetailBarbuk"] = res_data.Result.Message;
                return RedirectToAction("DetailBarangBukti", "HdBarangBukti", new { IdHdBarangBukti = param.IdHdBarangBukti });
            }
        }

        public async Task<ActionResult> PreviewFile(string IdDtBarangBukti, CancellationToken cancellationToken)
        {
            //if (HttpContext.Session.GetString("user_id") == null)
            //{
            //    //return RedirectToAction("Logout", "Auth");
            //}

            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _dtBarangBuktiService.PreviewFile(IdDtBarangBukti, accessToken, cancellationToken);
            var dataList = JsonConvert.DeserializeObject<GetFileModel>(JsonConvert.SerializeObject(resp.Result.Data));
            return Json(new { res = dataList, error = resp.Result.Error, message = resp.Result.Message });

        }

        public IActionResult GetListItemBarangBukti(string NoPerkara, CancellationToken cancellationToken)
        {
            string? accessToken = HttpContext.Session.GetString("token");
            var resp = _dtBarangBuktiService.ListItemDtBarangBukti(NoPerkara, accessToken, cancellationToken);
            var dataList = JsonConvert.DeserializeObject<List<DtBarangBuktiModel>>(JsonConvert.SerializeObject(resp.Result.Data));
            return Json(new { res = dataList, error = resp.Result.Error, message = resp.Result.Message });
        }
    }
}
