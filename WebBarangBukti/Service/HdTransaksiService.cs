using Newtonsoft.Json;
using System.Net.Http.Headers;
using WebBarangBukti.Help;
using WebBarangBukti.Models;
using WebBarangBukti.Service.IService;

namespace WebBarangBukti.Service
{
    public class HdTransaksiService : IHdTransaksiService
    {
        private readonly ILogger<HdTransaksiService> _logger;
        private IConfiguration _config;

        public HdTransaksiService(ILogger<HdTransaksiService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<GlobalObjectResponse> AddHdTransaksi(HdTransaksi parameter, string accessToken, CancellationToken cancellationToken)
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
                    HttpResponseMessage Res = await client.PostAsync("api/HdTransaksi/AddHdTransaksi", httpContent);
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

        public async Task<GlobalObjectResponse> UpdateHdTransaksi(HdTransaksi parameter, string accessToken, CancellationToken cancellationToken)
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
                    HttpResponseMessage Res = await client.PutAsync("api/HdTransaksi/UpdateHdTransaksi", httpContent);
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

        public async Task<GlobalObjectListResponse> ListDataHdTransaksi(string accessToken, CancellationToken cancellationToken)
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
                    HttpResponseMessage Res = await client.GetAsync("api/HdTransaksi/ListDataHdTransaksi");
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

        public async Task<GlobalObjectListResponse> ListDataHdTransaksiById(string IdTransaction, string accessToken, CancellationToken cancellationToken)
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
                    HttpResponseMessage Res = await client.GetAsync("api/HdTransaksi/ListDataHdTransaksiById?IdTransaction=" + IdTransaction);
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

        public async Task<GlobalObjectResponse> PreviewFile(string IdHdTransaksi, string accessToken, CancellationToken cancellationToken)
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
                    HttpResponseMessage Res = await client.GetAsync("api/HdTransaksi/GetPreviewFile?IdHdTransaksi=" + IdHdTransaksi);
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

        public async Task<GlobalObjectListResponse> ListItemBarangBukti(string NoPerkara, string accessToken, CancellationToken cancellationToken)
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
                    HttpResponseMessage Res = await client.GetAsync("api/DtBarangBukti/ListItemDtBarangBukti?NoPerkara=" + NoPerkara);
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
    }
}
