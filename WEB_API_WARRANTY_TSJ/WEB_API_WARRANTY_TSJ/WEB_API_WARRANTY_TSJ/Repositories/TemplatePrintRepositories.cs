using Microsoft.EntityFrameworkCore;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;
using WEB_API_WARRANTY_TSJ.Services.IService;

namespace WEB_API_WARRANTY_TSJ.Repositories
{
    public class TemplatePrintRepositories : ITemplatePrintRepositories
    {
        public readonly IConfiguration _configuration;
        public readonly DBWARContext _context;
        public readonly IPrinterService _printerService;
        public readonly IErrorRepositories _errorRepositories;

        public TemplatePrintRepositories(IConfiguration Configuration, DBWARContext context, IPrinterService printerService, IErrorRepositories errorRepositories)
        {
            _configuration = Configuration;
            _context = context;
            _printerService = printerService;
            _errorRepositories = errorRepositories;
        }

        public async Task<GlobalObjectResponse> AddTemplatePrint(TemplatePrint parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();
            TemplatePrint request = new TemplatePrint();
            try
            {
                var checkTemplate = _context.TemplatePrints.Where(x => x.TemplateName == parameter.TemplateName).FirstOrDefault();
                if(checkTemplate != null)
                {
                    res.Code = 409;
                    res.Message = MessageRepositories.MessageFailed + " Template Name : " + parameter.TemplateName + " ALready Exist.";
                    res.Error = true;
                    return res;
                }

                request.TemplateName = parameter.TemplateName;
                request.Base64Logo = parameter.Base64Logo;
                request.Source = parameter.Source;
                request.CreatedAt = DateTime.Now;
                request.CreatedBy = parameter.CreatedBy;
                request.UpdatedAt = DateTime.Now;
                request.UpdatedBy = parameter.UpdatedBy;
                _context.TemplatePrints.Add(request);
                await _context.SaveChangesAsync();

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Create Template Print ";
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {

                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "AddTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "AddTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                _addError.ServiceName = "AddTemplatePrint";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.CreatedBy;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

        }

        public async Task<GlobalObjectResponse> UpdateTemplatePrint(TemplatePrint parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();
            TemplatePrint request = new TemplatePrint();
            try
            {
                var updTemplate = _context.TemplatePrints.Where(x => x.TemplateName == parameter.TemplateName).FirstOrDefault();
                if(updTemplate != null)
                {
                    updTemplate.TemplateName = parameter.TemplateName;
                    updTemplate.Base64Logo = parameter.Base64Logo;
                    updTemplate.Source = parameter.Source;
                    updTemplate.Active = parameter.Active;
                    updTemplate.PrintDefault = parameter.PrintDefault;
                    updTemplate.UpdatedAt = DateTime.Now;
                    updTemplate.UpdatedBy = parameter.UpdatedBy;
                    _context.TemplatePrints.Update(updTemplate);
                    await _context.SaveChangesAsync();

                }

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Update Template Print ";
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {

                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "UpdateTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "UpdateTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                _addError.ServiceName = "UpdateTemplatePrint";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.CreatedBy;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

        }

        public async Task<GlobalObjectResponse> UpdateDefaultTemplatePrint(TemplatePrint parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();
            TemplatePrint request = new TemplatePrint();
            try
            {
                var checkDefault = _context.TemplatePrints.Where(x => x.TemplateName != parameter.TemplateName.Trim() && x.PrintDefault == true && x.Active == true).ToList();
                if (checkDefault != null)  {

                    checkDefault.ForEach(c => { c.PrintDefault = false; });
                    _context.UpdateRange(checkDefault);
                    await _context.SaveChangesAsync();
                }

                var updTemplate = _context.TemplatePrints.Where(x => x.TemplateName == parameter.TemplateName.Trim() && x.Active == true).FirstOrDefault();
                if (updTemplate != null)
                {
                    updTemplate.PrintDefault = true;
                    updTemplate.UpdatedBy = parameter.CreatedBy;
                    updTemplate.UpdatedAt = DateTime.Now;
                    _context.TemplatePrints.Update(updTemplate);
                    await _context.SaveChangesAsync();

                }

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Update Default Template Print ";
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {

                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "UpdateTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "UpdateTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                _addError.ServiceName = "UpdateTemplatePrint";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.CreatedBy;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

        }

        public async Task<GlobalObjectResponse> DeleteTemplatePrint(string TemplateName, string? Source, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogError _addError = new LogError();

            try
            {

                var dataTemplatePrint = _context.TemplatePrints.Where(x => x.TemplateName == TemplateName && x.Source == Source).FirstOrDefault();
                if (dataTemplatePrint != null)
                {
                    dataTemplatePrint.Active = false;
                    _context.TemplatePrints.Update(dataTemplatePrint);
                    await _context.SaveChangesAsync();
                }

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Delete Template Print.";
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {

                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {

                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

        }

        public async Task<GlobalObjectListResponse> ListDataTemplatePrint(string? TemplateName, string? Source, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<TemplatePrint> lst_temp_print = new List<TemplatePrint>();
            LogError _addError = new LogError();

            try
            {
                System.GC.Collect();
                if (TemplateName != "" && TemplateName != null)
                {
                    lst_temp_print = _context.TemplatePrints.Where(x => x.TemplateName == TemplateName && x.Source == Source && x.Active == true).OrderBy(x => x.Id).AsNoTracking().ToList();
                }
                else 
                {
                    lst_temp_print = _context.TemplatePrints.Where(x => x.Source == Source && x.Active == true).OrderBy(x => x.Id).AsNoTracking().ToList();
                }
                res.Code = 200;
                res.Data = lst_temp_print.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Template Print."; ;
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    _addError.ServiceName = "ListDataTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    _addError.ServiceName = "ListDataTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                _addError.ServiceName = "ListDataTemplatePrint";
                _addError.ServiceError = res.Message;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }
        }

        public async Task<GlobalObjectListResponse> GetDataDefaultTemplatePrint(string? Source, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<TemplatePrint> lst_temp_print = new List<TemplatePrint>();
            LogError _addError = new LogError();

            try
            {
                System.GC.Collect();
                lst_temp_print = _context.TemplatePrints.Where(x => x.Source == Source && x.PrintDefault == true).OrderBy(x => x.Id).AsNoTracking().ToList();
                res.Code = 200;
                res.Data = lst_temp_print.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Default Template Print."; ;
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    _addError.ServiceName = "GetDataDefaultTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    _addError.ServiceName = "GetDataDefaultTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                _addError.ServiceName = "GetDataDefaultTemplatePrint";
                _addError.ServiceError = res.Message;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }
        }

        public async Task<GlobalObjectListResponse> DataDefaultTemplatePrint(string? Source, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<TemplatePrint> lst_temp_print = new List<TemplatePrint>();
            LogError _addError = new LogError();

            try
            {
                System.GC.Collect();
                lst_temp_print = _context.TemplatePrints.Where(x => x.Source == Source && x.Active == true).OrderBy(x => x.Id).AsNoTracking().ToList();


                res.Code = 200;
                res.Data = lst_temp_print.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Default Template Print."; ;
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    _addError.ServiceName = "ListDataTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                return res;
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    _addError.ServiceName = "ListDataTemplatePrint";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                _addError.ServiceName = "ListDataTemplatePrint";
                _addError.ServiceError = res.Message;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }
        }
    }
}
