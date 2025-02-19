using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using WEB_API_DASHBOARD.Models;
using WEB_API_DASHBOARD.ViewModels;

namespace WEB_API_DASHBOARD.Controllers
{
    [Authorize]
    public class DashboardController : ApiController
    {

        private DBERPEntities db_erp = new DBERPEntities();

        [HttpGet()]
        [ResponseType(typeof(ResponseMessage))]
        public async Task<IHttpActionResult> GetWarehouseUser(string user_id)
        {
            ResponseMessage res = new ResponseMessage();
            try
            {
                var data_warehouse_user = db_erp.USER_ACCESS_DATA.Where(x => x.user_id == user_id).AsNoTracking().ToList();
                return Ok(data_warehouse_user);
            }

            catch (Exception ex)
            {
                res.code = 500;
                res.status = 0;
                string message = "";
                if (ex.InnerException != null)
                {
                    message = ex.InnerException.Message;
                }
                else
                {
                    message = ex.Message;
                }
                res.IsSucceed = false;
                res.message = "Error GetWarehouseUser : " + message;
                return Content(HttpStatusCode.InternalServerError, res);
            }
        }

        public List<V_WORK_ORDER_ASSEMBLY_DASHBOARD> GetWorkOrderDashboard(string wh_id)
        {
            ResponseMessage res = new ResponseMessage();
            try
            {
                List<V_WORK_ORDER_ASSEMBLY_DASHBOARD> lst_data_work_order = new List<V_WORK_ORDER_ASSEMBLY_DASHBOARD>();
                PaginationFilter paginationFilter = new PaginationFilter();
                var headers = Request.Headers;
                if (headers.Contains("Paging"))
                {
                    string paging = headers.GetValues("Paging").First();
                    paginationFilter = JsonConvert.DeserializeObject<PaginationFilter>(paging);

                }

                string date_work_order = paginationFilter.filterString;
                DateTime work_order_date = Convert.ToDateTime(date_work_order);
                if (date_work_order != null && date_work_order != "") {

                    lst_data_work_order = db_erp.V_WORK_ORDER_ASSEMBLY_DASHBOARD.Where(x => x.wh_id == wh_id 
                                          && x.created_at.Value.Year == work_order_date.Year && x.created_at.Value.Month == work_order_date.Month 
                                          && x.created_at.Value.Day == work_order_date.Day).OrderByDescending(x => x.operation_id).AsNoTracking().ToList();
                } else {
                    lst_data_work_order = db_erp.V_WORK_ORDER_ASSEMBLY_DASHBOARD.Where(x => x.wh_id == wh_id).OrderByDescending(x => x.operation_id).AsNoTracking().ToList();
                }

                int count = lst_data_work_order.Count();
                int CurrentPage = paginationFilter.pageNumber;
                int PageSize = paginationFilter.pageSize;
                int TotalCount = count;
                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
                var items = lst_data_work_order.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                PagingHeaders pagingHeaders = new PagingHeaders();
                PaginationMetadata paginationMetadata = new PaginationMetadata();
                paginationMetadata = pagingHeaders.Create(TotalPages, CurrentPage, TotalCount, PageSize);
                // Setting Header  
                HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

                return items;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

    }
}