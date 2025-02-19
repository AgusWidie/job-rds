using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEB_DASHBOARD_INV.Models;
using WEB_DASHBOARD_INV.Services;
using WEB_DASHBOARD_INV.ViewModels;
using WEB_ERP_TSJ.Models;
using System.Net.NetworkInformation;
using iTextSharp.text.pdf;

namespace WEB_DASHBOARD_INV.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        string apiUrl = Properties.Settings.Default.ApiUrl;
        string authorization = Properties.Settings.Default.Authorization;
        MenuService _menuService = new MenuService();
        public async Task<ActionResult> Index(DateTime? fDateWorkOrder, int? pNumber = 1, int? pSize = 10)
        {
            
            PaginationFilter pf = new PaginationFilter();
            if (fDateWorkOrder != null)  {
                pf.filterString = Convert.ToDateTime(fDateWorkOrder).ToString("yyyy-MM-dd");
            } 

            TempData["fWorkOrderDate"] = pf.filterString;
            pf.pageNumber = pNumber;
            pf.pageSize = pSize;
            var pagingString = JsonConvert.SerializeObject(pf);
            PaginationMetadata paginationMetadata = new PaginationMetadata();

            List<V_WORK_ORDER_ASSEMBLY_DASHBOARD> Obj = new List<V_WORK_ORDER_ASSEMBLY_DASHBOARD>();
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                string user_id = Session["user_id"].ToString();
                string wh_id = Session["wh_id"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", authorization + token);
                client.DefaultRequestHeaders.Add("Paging", pagingString);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Dashboard/GetWorkOrderDashboard?wh_id=" + wh_id);
                if (response.IsSuccessStatusCode)
                {
                    var ResponseHeader = response.Headers;
                    var pagingHeader = ResponseHeader.Where(m => m.Key == "Paging-Headers").FirstOrDefault();
                    paginationMetadata = JsonConvert.DeserializeObject<PaginationMetadata>(pagingHeader.Value.FirstOrDefault());

                    var Response = response.Content.ReadAsStringAsync().Result;
                    Obj = JsonConvert.DeserializeObject<List<V_WORK_ORDER_ASSEMBLY_DASHBOARD>>(Response);
                }
                else
                {
                    return Redirect("~/Auth/Index");
                }
            }

            if (pf.filterString == "")
                ViewData["F1"] = "";
            ViewData["F1"] = pf.filterString;

            ViewBag.Pagination = paginationMetadata;
            ViewData["pSize"] = pf.pageSize.ToString();
            return View(Obj);
        }

        public async Task<ActionResult> IndexNotFound()
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            MenuViewModel menuView = await _menuService.GetMenuUser(Session["user_id"].ToString(), apiUrl, authorization, Session["token"].ToString());
            List<V_MENU_INV> l_menu_inv = menuView.ListMenu.Where(x => x.menu_controller == controllerName).ToList();

            ViewBag.ListMenuParent = menuView.ListMenuParent;
            ViewBag.ListMenu = menuView.ListMenu;
            ViewBag.ListMenuController = l_menu_inv;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CheckSession()
        {
            if (Session["user_id"] == null)
            {
                return Json(new { message = "Session Not Active.", bolSession = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { message = "Session Already Active.", bolSession = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetDashboard(string wh_id)
        {
            using (var client = new HttpClient())
            {
                string token = Session["token"].ToString();
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", authorization + token);
                HttpResponseMessage Res = await client.GetAsync("Svc/Dashboard/GetWorkOrderDashboard?wh_id=" + wh_id.Trim());
                List<V_WORK_ORDER_ASSEMBLY_DASHBOARD> _res = new List<V_WORK_ORDER_ASSEMBLY_DASHBOARD>();
                var Response = Res.Content.ReadAsStringAsync().Result;
                _res = JsonConvert.DeserializeObject<List<V_WORK_ORDER_ASSEMBLY_DASHBOARD>>(Response);
                return Json(new { _res = _res }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}