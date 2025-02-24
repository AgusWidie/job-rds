using Microsoft.EntityFrameworkCore;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.Models.Warranty;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;

namespace WEB_API_WARRANTY_TSJ.Repositories
{
    public class WarrantyRepositories : IWarrantyRepositories
    {
        public readonly IConfiguration _configuration;
        public readonly DBWARContext _context;
        public readonly IErrorRepositories _errorRepositories;
        public readonly ILogRequestActivationQRRepositories _activationQRRepositories;

        public WarrantyRepositories(IConfiguration Configuration, DBWARContext context, IErrorRepositories errorRepositories, ILogRequestActivationQRRepositories activationQRRepositories)
        {
            _configuration = Configuration;
            _context = context;
            _errorRepositories = errorRepositories;
            _activationQRRepositories = activationQRRepositories;
        }

        public async Task<GlobalObjectResponse> AddWarranty(WarrantyRequest parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogWarranty request = new LogWarranty();
            LogError _addError = new LogError();
            List<WarrantyRequest> lst_warranty_req = new List<WarrantyRequest>();
            string jsonStr = "";

            using (var dbTrans = _context.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                   
                    if (parameter.SerialCode == null || parameter.SerialCode == "")
                    {

                        dbTrans.Rollback();
                        dbTrans.Dispose();

                        res.Code = 404;
                        res.Message = MessageRepositories.MessageFailed + " => Serial Code : " + parameter.SerialCode + " Empty.";
                        res.Error = false;

                        jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "AddWarranty";
                        _addError.ServiceError = res.Message;
                        _addError.LogJson = jsonStr;
                        _addError.CreatedBy = parameter.CreatedBy;
                        _addError.ErrorDate = DateTime.Now;

                        await _errorRepositories.AddLogError(_addError, cancellationToken);

                        return res;
                    }

                    if (parameter.ActivationCode == null || parameter.ActivationCode == "")
                    {
                        dbTrans.Rollback();
                        dbTrans.Dispose();

                        res.Code = 404;
                        res.Message = MessageRepositories.MessageFailed + " => Activation Code : " + parameter.ActivationCode + " Empty.";
                        res.Error = false;

                        jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "AddWarranty";
                        _addError.ServiceError = res.Message;
                        _addError.LogJson = jsonStr;
                        _addError.CreatedBy = parameter.CreatedBy;
                        _addError.ErrorDate = DateTime.Now;

                        await _errorRepositories.AddLogError(_addError, cancellationToken);

                        return res;
                    }

                    if (parameter.RegistrationCode == null || parameter.RegistrationCode == "")
                    {
                        dbTrans.Rollback();
                        dbTrans.Dispose();

                        res.Code = 404;
                        res.Message = MessageRepositories.MessageFailed + " => Registration Code : " + parameter.RegistrationCode + " Empty.";
                        res.Error = false;

                        jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "AddWarranty";
                        _addError.ServiceError = res.Message;
                        _addError.LogJson = jsonStr;
                        _addError.CreatedBy = parameter.CreatedBy;
                        _addError.ErrorDate = DateTime.Now;

                        await _errorRepositories.AddLogError(_addError, cancellationToken);

                        return res;
                    }

                    if (parameter.QrCodeFull == null || parameter.QrCodeFull == "")
                    {
                        dbTrans.Rollback();
                        dbTrans.Dispose();

                        res.Code = 404;
                        res.Message = MessageRepositories.MessageFailed + " => QR Code : " + parameter.QrCodeFull + " Empty.";
                        res.Error = false;

                        jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "AddWarranty";
                        _addError.ServiceError = res.Message;
                        _addError.LogJson = jsonStr;
                        _addError.CreatedBy = parameter.CreatedBy;
                        _addError.ErrorDate = DateTime.Now;

                        await _errorRepositories.AddLogError(_addError, cancellationToken);

                        return res;
                    }

                    var dataReg = _context.BarcodeSerialQrs.Where(x => x.SerialCode == parameter.SerialCode && x.RegistrationCode == parameter.RegistrationCode).AsNoTracking().FirstOrDefault();
                    if (dataReg == null)
                    {

                        dbTrans.Rollback();
                        dbTrans.Dispose();

                        res.Code = 404;
                        res.Message = MessageRepositories.MessageFailed + " => Serial Code : " + parameter.SerialCode + " And Registration Code : " + parameter.RegistrationCode + " Not Found.";
                        res.Error = false;

                        jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "AddWarranty";
                        _addError.ServiceError = res.Message;
                        _addError.LogJson = jsonStr;
                        _addError.CreatedBy = parameter.CreatedBy;
                        _addError.ErrorDate = DateTime.Now;

                        await _errorRepositories.AddLogError(_addError, cancellationToken);

                        return res;
                    }

                    var splitQR = parameter.QrCodeFull.Split('|');
                    //if (splitQR[4].ToString() != "TSJ") {
                    //    request.Source = "INOAC";
                    //    request.ProductCode = splitQR[0].ToString() + "|" + splitQR[1].ToString() + "|" + splitQR[2].ToString() + "|" + splitQR[3].ToString();

                    //} else {
                    //    request.Source = "TSJ";
                    //    request.ProductCode = splitQR[2].ToString();

                    //}

                    request.ProductCode = splitQR[2].ToString();
                    request.LogWarrantyId = DateTime.Now.Ticks.ToString().ToUpper();
                    request.QrCode = "https://www.vita-foam.com/warranty/Activation/" + parameter.ActivationCode;
                    request.SerialCode = parameter.SerialCode;
                    request.ActivationCode = parameter.ActivationCode;
                    request.RegistrationCode = parameter.RegistrationCode;
                    request.ActivationAt = DateTime.Now;
                    request.ActivationBy = parameter.CreatedBy;
                    request.UpdatedAt = DateTime.Now;
                    request.CreatedBy = parameter.CreatedBy;
                    _context.LogWarranties.Add(request);
                    await _context.SaveChangesAsync();

                    dbTrans.Commit();
                    dbTrans.Dispose();

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);
                    LogRequestActivation ReqAct = new LogRequestActivation();
                    ReqAct.LogRequestId = DateTime.Now.Ticks.ToString().ToUpper();
                    ReqAct.LogJsonActivation = jsonStr;
                    ReqAct.CreatedAt = DateTime.Now;
                    ReqAct.CreatedBy = parameter.CreatedBy;

                    await _activationQRRepositories.AddLogRequestActivation(ReqAct, cancellationToken);

                    res.Code = 200;
                    res.Message = MessageRepositories.MessageSuccess + " Create Activation Warranty.";
                    res.Error = false;
                    return res;
                }

                catch (DbUpdateConcurrencyException ex)
                {
                    dbTrans.Rollback();
                    dbTrans.Dispose();

                    if (ex.InnerException.Message != null)
                    {
                        res.Code = 500;
                        res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                        res.Error = true;

                        jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "AddWarranty";
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

                    _addError.ServiceName = "AddWarranty";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    dbTrans.Dispose();

                    if (ex.InnerException.Message != null)
                    {
                        res.Code = 500;
                        res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                        res.Error = true;

                        jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                        _addError.ServiceName = "AddWarranty";
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

                    _addError.ServiceName = "AddWarranty";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

            }

        }

        public async Task<GlobalObjectListResponse> ListDataWarranty(DateTime? createdAtFrom, DateTime? createdAtTo, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<LogWarranty> lst_warranty = new List<LogWarranty>();
            LogError _addError = new LogError();

            try
            {
                System.GC.Collect();
                lst_warranty = _context.LogWarranties.Where(x => x.CreatedAt >= createdAtFrom && x.CreatedAt <= createdAtTo).OrderBy(x => x.Id).AsNoTracking().ToList();

                res.Code = 200;
                res.Data = lst_warranty.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Warranty."; ;
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

                    _addError.ServiceName = "ListDataWarranty";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                _addError.ServiceName = "ListDataWarranty";
                _addError.ServiceError = res.Message;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    _addError.ServiceName = "ListDataWarranty";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                _addError.ServiceName = "ListDataWarranty";
                _addError.ServiceError = res.Message;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }
        }

        public async Task<GlobalObjectListResponse> ListGetDataWarrantyRegistration(string? registrationCode, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<LogWarranty> lst_warranty = new List<LogWarranty>();
            LogError _addError = new LogError();

            try
            {
                System.GC.Collect();

                if(registrationCode == null || registrationCode == "")
                {
                    res.Code = 400;
                    res.Message = MessageRepositories.MessageFailed + " Registration Code Empty.";
                    res.Error = true;
                    return res;
                }

                lst_warranty = _context.LogWarranties.Where(x => x.RegistrationCode == registrationCode).OrderBy(x => x.Id).AsNoTracking().ToList();
                if(lst_warranty == null)
                {
                    lst_warranty = _context.LogWarranties.Where(x => x.ActivationCode == registrationCode).OrderBy(x => x.Id).AsNoTracking().ToList();
                }

                res.Code = 200;
                res.Data = lst_warranty.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Warranty Registration.";
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

                    _addError.ServiceName = "ListGetDataWarrantyRegistration";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                _addError.ServiceName = "ListGetDataWarrantyRegistration";
                _addError.ServiceError = res.Message;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    _addError.ServiceName = "ListGetDataWarrantyRegistration";
                    _addError.ServiceError = res.Message;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }
                res.Code = 500;
                res.Message = MessageRepositories.MessageError + " : " + ex.Message;
                res.Error = true;

                _addError.ServiceName = "ListGetDataWarrantyRegistration";
                _addError.ServiceError = res.Message;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }
        }

        public async Task<GlobalObjectResponse> UpdateWarrantyRegistration(LogWarrantyRequest parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            LogWarranty request = new LogWarranty();
            LogError _addError = new LogError();

            try
            {

                if (parameter.RegistrationCode == null || parameter.RegistrationCode == "")
                {
                    res.Code = 400;
                    res.Message = MessageRepositories.MessageFailed + " => Registration Code : " + parameter.RegistrationCode + " Empty.";
                    res.Error = true;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "UpdateWarrantyRegistration";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

                var upd_warranty = _context.LogWarranties.Where(x => x.RegistrationCode == parameter.RegistrationCode).FirstOrDefault();
                if(upd_warranty != null)
                {
                    upd_warranty.CustomerName = parameter.CustomerName;
                    upd_warranty.Gender = parameter.Gender;
                    upd_warranty.Email = parameter.Email;
                    upd_warranty.PlaceOfBirth = parameter.PlaceOfBirth;
                    upd_warranty.DateOfBirth = parameter.DateOfBirth;
                    upd_warranty.Telephone = parameter.Telephone;
                    upd_warranty.Additional1 = parameter.Additional1;
                    upd_warranty.Additional2 = parameter.Additional2;
                    upd_warranty.Additional3 = parameter.Additional3;
                    upd_warranty.Additional4 = parameter.Additional4;
                    upd_warranty.Additional5 = parameter.Additional5;
                    upd_warranty.RegistrationAt = DateTime.Now;
                    upd_warranty.UpdatedAt = DateTime.Now;
                    upd_warranty.CreatedBy = parameter.CreatedBy;
                    _context.LogWarranties.Update(request);
                    await _context.SaveChangesAsync();

                } else {

                    res.Code = 404;
                    res.Message = MessageRepositories.MessageFailed + " => Data Warranty, Registration Code : " + parameter.RegistrationCode + " Not Found.";
                    res.Error = false;

                    var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "UpdateWarrantyRegistration";
                    _addError.ServiceError = res.Message;
                    _addError.LogJson = jsonStr;
                    _addError.CreatedBy = parameter.CreatedBy;
                    _addError.ErrorDate = DateTime.Now;

                    await _errorRepositories.AddLogError(_addError, cancellationToken);

                    return res;
                }

               
                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Update Warranty Registration.";
                res.Error = false;
                return res;
            }

            catch (DbUpdateConcurrencyException ex)
            {
                string jsonStr = "";
                if (ex.InnerException.Message != null)
                {
                    res.Code = 500;
                    res.Message = MessageRepositories.MessageError + " : " + ex.InnerException.Message;
                    res.Error = true;

                    jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(parameter);

                    _addError.ServiceName = "UpdateWarrantyRegistration";
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

                _addError.ServiceName = "UpdateWarrantyRegistration";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.CreatedBy;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

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

                    _addError.ServiceName = "UpdateWarrantyRegistration";
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

                _addError.ServiceName = "UpdateWarrantyRegistration";
                _addError.ServiceError = res.Message;
                _addError.LogJson = jsonStr;
                _addError.CreatedBy = parameter.CreatedBy;
                _addError.ErrorDate = DateTime.Now;

                await _errorRepositories.AddLogError(_addError, cancellationToken);

                return res;
            }

        }
    }
}
