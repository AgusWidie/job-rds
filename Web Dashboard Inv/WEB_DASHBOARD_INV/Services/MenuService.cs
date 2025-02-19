using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WEB_DASHBOARD_INV.Models;
using WEB_DASHBOARD_INV.ViewModels;
using System.Net;

namespace WEB_DASHBOARD_INV.Services
{
    public class MenuService
    {
        public async Task<MenuViewModel> GetMenuUser(string user_id, string apiUrl, string authorization, string token)
        {
            MenuViewModel Obj = new MenuViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", authorization + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Menu/GetMenuUser?user_id=" + user_id);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    Obj = JsonConvert.DeserializeObject<MenuViewModel>(Response);
                }
                return Obj;
            }

        }

        public async Task<List<V_MENU_INV>> GetMenuController(string user_id, string menu_controller, string apiUrl, string authorization, string token)
        {
            List<V_MENU_INV> Obj = new List<V_MENU_INV>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", authorization + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync("Svc/Menu/GetMenuController?user_id=" + user_id + "&menu_controller=" + menu_controller);
                if (response.IsSuccessStatusCode)
                {
                    var Response = response.Content.ReadAsStringAsync().Result;
                    Obj = JsonConvert.DeserializeObject<List<V_MENU_INV>>(Response);
                }
                return Obj;
            }

        }
    }
}