using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebBarangBukti.Help;
using WebBarangBukti.Models;
using WebBarangBukti.Service.IService;

namespace WebBarangBukti.Service
{
    public class HdBarangBuktiService : IHdBarangBuktiService
    {
        private readonly ILogger<HdBarangBuktiService> _logger;
        private IConfiguration _config;

        public HdBarangBuktiService(ILogger<HdBarangBuktiService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<GlobalObjectResponse> AddHdBarangBukti(HdBarangBukti parameter, string accessToken, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {

                string ApiUrl = _config["AppSettings:ApiUrl"];
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    var JsonStr = JsonConvert.SerializeObject(parameter);
                    HttpContent httpContent = new StringContent(JsonStr);
                    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.PostAsync("api/HdBarangBukti/AddHdBarangBukti", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        string responseContent = await Res.Content.ReadAsStringAsync();
                        res = ResponseAPI.ResponseSuccessAPI(responseContent, Convert.ToInt32(Res.StatusCode));
                    }
                    else
                    {
                        string responseContent = await Res.Content.ReadAsStringAsync();
                        res = ResponseAPI.ResponseErrorAPI(responseContent, Convert.ToInt32(Res.StatusCode));
                    }
                    return res;
                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageService.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;
                    return res;
                }
                res.Code = 500;
                res.Message = MessageService.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

        }

        public async Task<GlobalObjectResponse> UpdateHdBarangBukti(HdBarangBukti parameter, string accessToken, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {

                string ApiUrl = _config["AppSettings:ApiUrl"];
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                using (var client = new HttpClient(clientHandler))
                {
                    client.BaseAddress = new Uri(ApiUrl);
                    var JsonStr = JsonConvert.SerializeObject(parameter);
                    HttpContent httpContent = new StringContent(JsonStr);
                    httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    HttpResponseMessage Res = await client.PutAsync("api/HdBarangBukti/UpdateHdBarangBukti", httpContent);
                    if (Res.IsSuccessStatusCode)
                    {
                        string responseContent = await Res.Content.ReadAsStringAsync();
                        res = ResponseAPI.ResponseSuccessAPI(responseContent, Convert.ToInt32(Res.StatusCode));
                    }
                    else
                    {
                        string responseContent = await Res.Content.ReadAsStringAsync();
                        res = ResponseAPI.ResponseErrorAPI(responseContent, Convert.ToInt32(Res.StatusCode));
                    }
                    return res;
                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageService.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;
                    return res;
                }
                res.Code = 500;
                res.Message = MessageService.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

        }

        public async Task<GlobalObjectListResponse> ListDataHdBarangBukti(string accessToken, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
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
                    HttpResponseMessage Res = await client.GetAsync("api/HdBarangBukti/ListDataHdBarangBukti");
                    if (Res.IsSuccessStatusCode)
                    {
                        string responseContent = await Res.Content.ReadAsStringAsync();
                        res = ResponseAPI.ResponseListSuccessAPI(responseContent, Convert.ToInt32(Res.StatusCode));
                    }
                    else
                    {
                        string responseContent = await Res.Content.ReadAsStringAsync();
                        res = ResponseAPI.ResponseListErrorAPI(responseContent, Convert.ToInt32(Res.StatusCode));
                    }
                    return res;
                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageService.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;
                    return res;
                }
                res.Code = 500;
                res.Message = MessageService.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

        }

        public async Task<GlobalObjectListResponse> ListDataHdBarangBuktiById(string IdHdBarangBukti, string accessToken, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
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
                    HttpResponseMessage Res = await client.GetAsync("api/HdBarangBukti/ListDataHdBarangBuktiById?IdHdBarangBukti=" + IdHdBarangBukti);
                    if (Res.IsSuccessStatusCode)
                    {
                        string responseContent = await Res.Content.ReadAsStringAsync();
                        res = ResponseAPI.ResponseListSuccessAPI(responseContent, Convert.ToInt32(Res.StatusCode));
                    }
                    else
                    {
                        string responseContent = await Res.Content.ReadAsStringAsync();
                        res = ResponseAPI.ResponseListErrorAPI(responseContent, Convert.ToInt32(Res.StatusCode));
                    }
                    return res;
                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageService.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;
                    return res;
                }
                res.Code = 500;
                res.Message = MessageService.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

        }

        public async Task<GlobalObjectResponse> PreviewFile(string IdHdBarangBukti, string accessToken, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
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
                    HttpResponseMessage Res = await client.GetAsync("api/HdBarangBukti/GetPreviewFile?IdHdBarangBukti=" + IdHdBarangBukti);
                    if (Res.IsSuccessStatusCode)
                    {
                        string responseContent = await Res.Content.ReadAsStringAsync();
                        res = ResponseAPI.ResponseSuccessAPI(responseContent, Convert.ToInt32(Res.StatusCode));
                    }
                    else
                    {
                        string responseContent = await Res.Content.ReadAsStringAsync();
                        res = ResponseAPI.ResponseErrorAPI(responseContent, Convert.ToInt32(Res.StatusCode));
                    }
                    return res;
                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageService.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;
                    return res;
                }
                res.Code = 500;
                res.Message = MessageService.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

        }
    }
}
