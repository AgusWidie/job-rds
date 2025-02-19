using ApiBarangBukti.Help;
using ApiBarangBukti.Models;
using ApiBarangBukti.Repository.IRepository;
using ApiBarangBukti.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace ApiBarangBukti.Repository
{
    public class DtBarangBuktiRepositories : IDtBarangBukti
    {
        public readonly IConfiguration _configuration;
        public readonly DbsiramContext _context;
        public readonly ILogUser _logUserService;
        public DtBarangBuktiRepositories(IConfiguration Configuration, DbsiramContext context, ILogUser logUserService)
        {
            _configuration = Configuration;
            _context = context;
            _logUserService = logUserService;
        }

        public async Task<GlobalObjectResponse> AddDtBarangBukti(DtBarangBukti parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {
                var lst_id_dt_barang_bukti =  await _context.DtBarangBuktis.OrderByDescending(x => x.IdDtBarangBukti).AsNoTracking().FirstOrDefaultAsync();
                if(lst_id_dt_barang_bukti != null) {
                    parameter.IdDtBarangBukti = GenNumber.LastDetailIdBarangBukti(lst_id_dt_barang_bukti.IdDtBarangBukti);
                } else {
                    parameter.IdDtBarangBukti = GenNumber.FirstDetailIdBarangBukti();
                }

                if (parameter.FileName != null && parameter.FileName != "")  {
                    string Storage = _configuration["AppSettings:Storage"];
                    parameter.FilePath = Storage + "\\" + parameter.FileName;

                    byte[] fileByteArray = Convert.FromBase64String(parameter.Base64File);
                    //System.IO.File.WriteAllBytes(parameter.FilePath, fileByteArray);
                    System.IO.File.WriteAllBytes(parameter.FilePath, Crypto.EncryptFileSha256(fileByteArray));
                }

                parameter.Base64File = null;
                parameter.CreateAt = DateTime.Now;
                parameter.UpdateAt = DateTime.Now;
                _context.DtBarangBuktis.Add(parameter);
                await _context.SaveChangesAsync();

                LogAktivitasUser param = new LogAktivitasUser();
                param.LogId = GetNewID.GenNewID();
                param.DocumentId = parameter.IdDtBarangBukti;
                param.Judul = parameter.NamaBarangBukti;
                param.Status = 1;
                param.CreateAt = DateTime.Now;
                param.UpdateAt = DateTime.Now;
                param.CreateBy = parameter.CreateBy;

                await _logUserService.AddLogUser(param, cancellationToken);

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Tambah Detail Barang Bukti." ;
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

        public async Task<GlobalObjectResponse> UpdateDtBarangBukti(DtBarangBukti parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {
                var upd_dt_brg_bukti = _context.DtBarangBuktis.Where(x => x.Id == parameter.Id).FirstOrDefault();
                if (upd_dt_brg_bukti != null)
                {
                    if (parameter.FileName != null && parameter.FileName != "")
                    {
                        string Storage = _configuration["AppSettings:Storage"];
                        upd_dt_brg_bukti.FileName = parameter.FileName;
                        upd_dt_brg_bukti.FilePath = Storage + "\\" + parameter.FileName;
                        upd_dt_brg_bukti.File = upd_dt_brg_bukti.FilePath;
                        upd_dt_brg_bukti.Extension = parameter.Extension;
                        upd_dt_brg_bukti.ContentType = parameter.ContentType;
                        upd_dt_brg_bukti.FileSize = parameter.FileSize;
                    }

                    upd_dt_brg_bukti.Base64File = null;
                    upd_dt_brg_bukti.NamaBarangBukti = parameter.NamaBarangBukti;
                    upd_dt_brg_bukti.Identitas = parameter.Identitas;
                    upd_dt_brg_bukti.Kondisi = parameter.Kondisi;
                    upd_dt_brg_bukti.Jumlah = parameter.Jumlah;
                    upd_dt_brg_bukti.StatusEksekusi = parameter.StatusEksekusi;
                    upd_dt_brg_bukti.StatusAkhir = parameter.StatusAkhir;
                    upd_dt_brg_bukti.File = parameter.File;
                    upd_dt_brg_bukti.UpdateAt = DateTime.Now;
                    upd_dt_brg_bukti.CreateBy = parameter.CreateBy;
                    _context.DtBarangBuktis.Update(upd_dt_brg_bukti);
                    await _context.SaveChangesAsync();

                    if (parameter.FileName != null && parameter.FileName != "")
                    {
                        if (System.IO.File.Exists(upd_dt_brg_bukti.FilePath))
                        {
                            System.IO.File.Delete(upd_dt_brg_bukti.FilePath);
                        }

                        byte[] fileByteArray = Convert.FromBase64String(parameter.Base64File);
                        //System.IO.File.WriteAllBytes(parameter.FilePath, fileByteArray);
                        System.IO.File.WriteAllBytes(parameter.FilePath, Crypto.EncryptFileSha256(fileByteArray));
                    }

                } else {

                    res.Code = 404;
                    res.Message = MessageRepositories.MessageFailed + ": Id Dt Barang Bukti : " + parameter.IdDtBarangBukti + " Tidak Ada.";
                    res.Error = false;
                    return res;
                }

                LogAktivitasUser param = new LogAktivitasUser();
                param.LogId = GetNewID.GenNewID();
                param.DocumentId = upd_dt_brg_bukti.IdDtBarangBukti;
                param.Judul = upd_dt_brg_bukti.NamaBarangBukti;
                param.Status = 1;
                param.CreateAt = DateTime.Now;
                param.UpdateAt = DateTime.Now;
                param.CreateBy = parameter.CreateBy;

                await _logUserService.AddLogUser(param, cancellationToken);

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Update Detail Barang Bukti.";
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

        public async Task<GlobalObjectListResponse> ListDataDtBarangBukti(string IdHdBarangBukti, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<DtBarangBukti> lst_dt_brg_bukti = new List<DtBarangBukti>();
            try
            {
                System.GC.Collect();
                lst_dt_brg_bukti = _context.DtBarangBuktis.Where(x => x.IdHdBarangBukti == IdHdBarangBukti).OrderBy(x => x.IdHdBarangBukti == IdHdBarangBukti).AsNoTracking().ToList();

                res.Code = 200;
                res.Data = lst_dt_brg_bukti.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Detail Barang Bukti.";
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

        public async Task<GlobalObjectResponse> GetPreviewFile(string IdDtBarangBukti, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            DtBarangBukti? dt_brg_bukti = new DtBarangBukti();
            try
            {
                GetFileModel fileModel = new GetFileModel();
                byte[] fileBytes;
                System.GC.Collect();

                dt_brg_bukti = _context.DtBarangBuktis.Where(x => x.IdDtBarangBukti == IdDtBarangBukti).AsNoTracking().FirstOrDefault();
                if (dt_brg_bukti != null)
                {
                    if (dt_brg_bukti.Extension == ".pdf")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(dt_brg_bukti.FilePath);
                        fileModel.Base64File = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
                    }

                    if (dt_brg_bukti.Extension == ".docx")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(dt_brg_bukti.FilePath);
                        fileModel.Base64File = Convert.ToBase64String(Help.ConvertFile.ConvertDocxToPdf(Crypto.DecryptFileSha256(fileBytes)));
                    }

                    if (dt_brg_bukti.Extension == ".png" || dt_brg_bukti.Extension == ".jpg" || dt_brg_bukti.Extension == ".bmp")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(dt_brg_bukti.FilePath);
                        fileModel.Base64File = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
                        //fileModel.Base64File = Convert.ToBase64String(Help.ConvertFile.ConvertImageToPDF(Crypto.DecryptFileSha256(fileBytes), dt_brg_bukti.FileName));
                    }

                    fileModel.FileName = dt_brg_bukti.FileName;
                    fileModel.ContentType = dt_brg_bukti.ContentType;
                    fileModel.Extension = dt_brg_bukti.Extension;
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

        public async Task<GlobalObjectListResponse> ListItemDtBarangBukti(string NoPerkara, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<DtBarangBuktiModel> lst_dt_barbuk = new List<DtBarangBuktiModel>();
            try
            {
                System.GC.Collect();
                lst_dt_barbuk = (from hdBarbuk in _context.HdBarangBuktis join dtBarbuk in _context.DtBarangBuktis on hdBarbuk.IdHdBarangBukti equals dtBarbuk.IdHdBarangBukti
                                 where hdBarbuk.NoPerkara == NoPerkara
                                 orderby dtBarbuk.NamaBarangBukti

                                 select new DtBarangBuktiModel
                                 {
                                    Id = dtBarbuk.Id,
                                    IdDtBarangBukti = dtBarbuk.IdDtBarangBukti,
                                    NamaBarangBukti = dtBarbuk.NamaBarangBukti

                                 }).AsNoTracking().ToList();

                res.Code = 200;
                res.Data = lst_dt_barbuk.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data item Barang Bukti."; ;
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
