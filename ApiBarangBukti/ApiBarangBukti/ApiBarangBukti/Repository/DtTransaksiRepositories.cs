using ApiBarangBukti.Help;
using ApiBarangBukti.Models;
using ApiBarangBukti.Repository.IRepository;
using ApiBarangBukti.ViewModel;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

namespace ApiBarangBukti.Repository
{
    public class DtTransaksiRepositories : IDtTransaksi
    {
        public readonly IConfiguration _configuration;
        public readonly DbsiramContext _context;
        public readonly ILogUser _logUserService;
        public DtTransaksiRepositories(IConfiguration Configuration, DbsiramContext context, ILogUser logUserService)
        {
            _configuration = Configuration;
            _context = context;
            _logUserService = logUserService;
        }

        public async Task<GlobalObjectResponse> AddDtTransaksi(DtTransaksi parameter, CancellationToken cancellationToken)
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
                parameter.CreateAt = DateTime.Now;
                parameter.UpdateAt = DateTime.Now;
                _context.DtTransaksis.Add(parameter);
                await _context.SaveChangesAsync();

                LogAktivitasUser param = new LogAktivitasUser();
                param.LogId = GetNewID.GenNewID();
                param.DocumentId = parameter.IdTransaksi.ToString();
                param.Judul = parameter.PemenangLelangInstansi;
                param.Status = 1;
                param.CreateAt = DateTime.Now;
                param.UpdateAt = DateTime.Now;
                param.CreateBy = parameter.CreateBy;

                await _logUserService.AddLogUser(param, cancellationToken);

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Tambah Detail Transaksi.";
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

        public async Task<GlobalObjectResponse> UpdateDtTransaksi(DtTransaksi parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {
                var upd_dt_trans = _context.DtTransaksis.Where(x => x.Id == parameter.Id).FirstOrDefault();
                if (upd_dt_trans != null)
                {
                    if (parameter.FileName != null && parameter.FileName != "")
                    {
                        string Storage = _configuration["AppSettings:Storage"];
                        upd_dt_trans.FileName = parameter.FileName;
                        upd_dt_trans.FilePath = Storage + "\\" + parameter.FileName;
                        upd_dt_trans.File = upd_dt_trans.FilePath;
                        upd_dt_trans.Extension = parameter.Extension;
                        upd_dt_trans.ContentType = parameter.ContentType;
                        upd_dt_trans.FileSize = parameter.FileSize;
                    }

                    upd_dt_trans.Base64File = null;
                    upd_dt_trans.IdBarangBukti = parameter.IdBarangBukti;
                    upd_dt_trans.PemenangLelangInstansi = parameter.PemenangLelangInstansi;
                    upd_dt_trans.Harga = parameter.Harga;
                    upd_dt_trans.TanggalTerjual = parameter.TanggalTerjual;
                    upd_dt_trans.TanggalPenyerahan = parameter.TanggalPenyerahan;
                    upd_dt_trans.File = parameter.File;
                    upd_dt_trans.JenisTransaksi = parameter.JenisTransaksi;
                    upd_dt_trans.UpdateAt = DateTime.Now;
                    upd_dt_trans.CreateBy = parameter.CreateBy;
                    _context.DtTransaksis.Update(upd_dt_trans);
                    await _context.SaveChangesAsync();

                    if (parameter.FileName != null && parameter.FileName != "")
                    {
                        if (System.IO.File.Exists(upd_dt_trans.FilePath))
                        {
                            System.IO.File.Delete(upd_dt_trans.FilePath);
                        }

                        byte[] fileByteArray = Convert.FromBase64String(parameter.Base64File);
                        //System.IO.File.WriteAllBytes(parameter.FilePath, fileByteArray);
                        System.IO.File.WriteAllBytes(parameter.FilePath, Crypto.EncryptFileSha256(fileByteArray));
                    }

                }
                else
                {

                    res.Code = 404;
                    res.Message = MessageRepositories.MessageFailed + ": Id Dt Transaksi: " + parameter.Id + " Tidak Ada.";
                    res.Error = false;
                    return res;
                }


                LogAktivitasUser param = new LogAktivitasUser();
                param.LogId = GetNewID.GenNewID();
                param.DocumentId = parameter.IdTransaksi.ToString();
                param.Judul = parameter.PemenangLelangInstansi;
                param.Status = 1;
                param.CreateAt = DateTime.Now;
                param.UpdateAt = DateTime.Now;
                param.CreateBy = parameter.CreateBy;

                await _logUserService.AddLogUser(param, cancellationToken);

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Update Detail Transaksi.";
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

        public async Task<GlobalObjectListResponse> ListDataDtTransaksi(string IdTransaksi, string NoPerkara, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<DtTransaksiModel> lst_dt_trans = new List<DtTransaksiModel>();
            try
            {
                System.GC.Collect();


                lst_dt_trans = (from dtTrans in _context.DtTransaksis
                                join hdTrans in _context.HdTransaksis on dtTrans.IdTransaksi equals hdTrans.IdTransaksi
                                join dtBarbuk in _context.DtBarangBuktis on dtTrans.IdBarangBukti equals dtBarbuk.IdDtBarangBukti
                                where hdTrans.IdTransaksi == IdTransaksi && hdTrans.NoPerkara == NoPerkara
                                orderby dtBarbuk.NamaBarangBukti

                                select new DtTransaksiModel
                                {
                                    Id = dtTrans.Id,
                                    IdTransaksi = dtTrans.IdTransaksi,
                                    IdBarangBukti = dtTrans.IdBarangBukti,
                                    NamaBarangBukti = dtBarbuk.NamaBarangBukti,
                                    JenisTransaksi = dtTrans.JenisTransaksi,
                                    PemenangLelangInstansi = dtTrans.PemenangLelangInstansi,
                                    Harga = dtTrans.Harga,
                                    TanggalTerjual = Convert.ToDateTime(dtTrans.TanggalTerjual).ToString("yyyy-MM-dd"),
                                    TanggalPenyerahan = Convert.ToDateTime(dtTrans.TanggalPenyerahan).ToString("yyyy-MM-dd"),
                                    File = dtTrans.FileName,
                                   
                                }).AsNoTracking().ToList();

                res.Code = 200;
                res.Data = lst_dt_trans.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Detail Transaksi."; ;
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

        public async Task<GlobalObjectResponse> GetPreviewFile(int Id, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            DtTransaksi? dt_trans = new DtTransaksi();
            try
            {
                GetFileModel fileModel = new GetFileModel();
                byte[] fileBytes;
                System.GC.Collect();

                dt_trans = _context.DtTransaksis.Where(x => x.Id == Id).AsNoTracking().FirstOrDefault();
                if (dt_trans != null)
                {
                    if (dt_trans.Extension == ".pdf")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(dt_trans.FilePath);
                        fileModel.Base64File = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
                    }

                    if (dt_trans.Extension == ".docx")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(dt_trans.FilePath);
                        fileModel.Base64File = Convert.ToBase64String(Help.ConvertFile.ConvertDocxToPdf(Crypto.DecryptFileSha256(fileBytes)));
                    }

                    if (dt_trans.Extension == ".png" || dt_trans.Extension == ".jpg" || dt_trans.Extension == ".bmp")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(dt_trans.FilePath);
                        //fileModel.Base64File = Convert.ToBase64String(Help.ConvertFile.ConvertImageToPDF(Crypto.DecryptFileSha256(fileBytes), dt_trans.FileName));
                        fileModel.Base64File = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
                    }

                    fileModel.FileName = dt_trans.FileName;
                    fileModel.ContentType = dt_trans.ContentType;
                    fileModel.Extension = dt_trans.Extension;
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
