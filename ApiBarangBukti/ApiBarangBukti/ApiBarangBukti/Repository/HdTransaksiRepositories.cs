using ApiBarangBukti.Help;
using ApiBarangBukti.Models;
using ApiBarangBukti.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace ApiBarangBukti.Repository
{
    public class HdTransaksiRepositories : IHdTransaksi
    {
        public readonly IConfiguration _configuration;
        public readonly DbsiramContext _context;
        public readonly ILogUser _logUserService;
        public HdTransaksiRepositories(IConfiguration Configuration, DbsiramContext context, ILogUser logUserService)
        {
            _configuration = Configuration;
            _context = context;
            _logUserService = logUserService;
        }

        public async Task<GlobalObjectResponse> AddHdTransaksi(HdTransaksi parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {
                if (parameter.FileName != null && parameter.FileName != "")
                {
                    string Storage = _configuration["AppSettings:Storage"];
                    parameter.FilePath = Storage + "\\" + parameter.FileName;

                    byte[] fileByteArray = Convert.FromBase64String(parameter.Base64File);
                    //System.IO.File.WriteAllBytes(parameter.FilePath, fileByteArray);
                    System.IO.File.WriteAllBytes(parameter.FilePath, Crypto.EncryptFileSha256(fileByteArray));
                }

                parameter.Base64File = null;
                parameter.IdTransaksi = GetNewID.GenNewID();
                parameter.CreateAt = DateTime.Now;
                parameter.UpdateAt = DateTime.Now;
                _context.HdTransaksis.Add(parameter);
                await _context.SaveChangesAsync();

            
                LogAktivitasUser param = new LogAktivitasUser();
                param.LogId = GetNewID.GenNewID();
                param.DocumentId = parameter.IdTransaksi.ToString();
                param.Judul = parameter.JudulTransaksi;
                param.NomorTransaksi = parameter.NoPerkara;
                param.Status = 1;
                param.CreateAt = DateTime.Now;
                param.UpdateAt = DateTime.Now;
                param.CreateBy = parameter.CreateBy;

                await _logUserService.AddLogUser(param, cancellationToken);

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Tambah Header Transaksi.";
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

        public async Task<GlobalObjectResponse> UpdateHdTransaksi(HdTransaksi parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {
                var upd_hd_trans = _context.HdTransaksis.Where(x => x.IdTransaksi == parameter.IdTransaksi).FirstOrDefault();
                if (upd_hd_trans != null) {

                    if(parameter.FileName != null && parameter.FileName != "") {
                        string Storage = _configuration["AppSettings:Storage"];
                        upd_hd_trans.FileName = parameter.FileName;
                        upd_hd_trans.FilePath = Storage + "\\" + parameter.FileName;
                        upd_hd_trans.Extension = parameter.Extension;
                        upd_hd_trans.ContentType = parameter.ContentType;
                        upd_hd_trans.FileSize = parameter.FileSize;
                    }
                   
                    upd_hd_trans.NamaTransaksi = parameter.NamaTransaksi;
                    upd_hd_trans.JenisTransaksi = parameter.JenisTransaksi;
                    upd_hd_trans.NoPerkara = parameter.NoPerkara;
                    upd_hd_trans.JudulTransaksi = parameter.JudulTransaksi;
                    upd_hd_trans.Pic = parameter.Pic;
                    upd_hd_trans.File = parameter.File;
                    upd_hd_trans.TanggalTransaksi = parameter.TanggalTransaksi;
                    upd_hd_trans.UpdateAt = DateTime.Now;
                    upd_hd_trans.CreateBy = parameter.CreateBy;
                    _context.HdTransaksis.Update(upd_hd_trans);
                    await _context.SaveChangesAsync();

                    if (parameter.FileName != null && parameter.FileName != "")  {
                        if (System.IO.File.Exists(upd_hd_trans.FilePath)) {
                            System.IO.File.Delete(upd_hd_trans.FilePath);
                        }

                        byte[] fileByteArray = Convert.FromBase64String(parameter.Base64File);
                        //System.IO.File.WriteAllBytes(parameter.FilePath, fileByteArray);
                        System.IO.File.WriteAllBytes(parameter.FilePath, Crypto.EncryptFileSha256(fileByteArray));
                    }

                } else {

                    res.Code = 404;
                    res.Message = MessageRepositories.MessageFailed + ": Id Hd Transaksi : " + parameter.IdTransaksi + " Tidak Ada.";
                    res.Error = false;
                    return res;
                }

                LogAktivitasUser param = new LogAktivitasUser();
                param.LogId = GetNewID.GenNewID();
                param.DocumentId = parameter.IdTransaksi.ToString();
                param.Judul = parameter.JudulTransaksi;
                param.NomorTransaksi = parameter.NoPerkara;
                param.Status = 1;
                param.CreateAt = DateTime.Now;
                param.UpdateAt = DateTime.Now;
                param.CreateBy = parameter.CreateBy;

                await _logUserService.AddLogUser(param, cancellationToken);


                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Update Header Transaksi.";
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

        public async Task<GlobalObjectListResponse> ListDataHdTransaksi(CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<HdTransaksi> lst_hd_trans = new List<HdTransaksi>();
            try
            {
                System.GC.Collect();
                lst_hd_trans = _context.HdTransaksis.OrderBy(x => x.Id).AsNoTracking().ToList();

                res.Code = 200;
                res.Data = lst_hd_trans.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Header Transaksi."; ;
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

        public async Task<GlobalObjectListResponse> ListDataHdTransaksiById(string IdTransaksi, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<HdTransaksi> lst_hd_trans = new List<HdTransaksi>();
            try
            {
                System.GC.Collect();
                lst_hd_trans = _context.HdTransaksis.Where(x => x.IdTransaksi == IdTransaksi).AsNoTracking().ToList();

                res.Code = 200;
                res.Data = lst_hd_trans.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Header Transaksi By Id Transaction."; ;
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
        public async Task<GlobalObjectResponse> GetPreviewFile(string IdHdTransaksi, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            HdTransaksi? hd_trans = new HdTransaksi();
            try
            {
                GetFileModel fileModel = new GetFileModel();
                byte[] fileBytes;
                System.GC.Collect();

                hd_trans = _context.HdTransaksis.Where(x => x.IdTransaksi == IdHdTransaksi).AsNoTracking().FirstOrDefault();
                if (hd_trans != null)
                {
                    if (hd_trans.Extension == ".pdf")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(hd_trans.FilePath);
                        fileModel.Base64File = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
                    }

                    if (hd_trans.Extension == ".docx")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(hd_trans.FilePath);
                        fileModel.Base64File = Convert.ToBase64String(Help.ConvertFile.ConvertDocxToPdf(Crypto.DecryptFileSha256(fileBytes)));
                    }

                    if (hd_trans.Extension == ".png" || hd_trans.Extension == ".jpg" || hd_trans.Extension == ".bmp")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(hd_trans.FilePath);
                        //fileModel.Base64File = Convert.ToBase64String(Help.ConvertFile.ConvertImageToPDF(Crypto.DecryptFileSha256(fileBytes), hd_trans.FileName));
                        fileModel.Base64File = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
                    }

                    fileModel.FileName = hd_trans.FileName;
                    fileModel.ContentType = hd_trans.ContentType;
                    fileModel.Extension = hd_trans.Extension;
                }

                res.Code = 200;
                res.Data = fileModel;
                res.Message = MessageRepositories.MessageSuccess + " Get Data Preview File."; ;
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
    }
}
