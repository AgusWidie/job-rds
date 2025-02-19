using ApiBarangBukti.Help;
using ApiBarangBukti.Models;
using ApiBarangBukti.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiBarangBukti.Repository
{
    public class HdBarangBuktiRepositories : IHdBarangBukti
    {
        public readonly IConfiguration _configuration;
        public readonly DbsiramContext _context;
        public readonly ILogUser _logUserService;
        public HdBarangBuktiRepositories(IConfiguration Configuration, DbsiramContext context, ILogUser logUserService)
        {
            _configuration = Configuration;
            _context = context;
            _logUserService = logUserService;
        }

        public async Task<GlobalObjectResponse> AddHdBarangBukti(HdBarangBukti parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {
                if (parameter.FileName != null && parameter.FileName != "") {
                    string Storage = _configuration["AppSettings:Storage"];
                    parameter.FilePath = Storage + "\\" + parameter.FileName;

                    byte[] fileByteArray = Convert.FromBase64String(parameter.Base64File);
                    //System.IO.File.WriteAllBytes(parameter.FilePath, fileByteArray);
                    System.IO.File.WriteAllBytes(parameter.FilePath, Crypto.EncryptFileSha256(fileByteArray));
                }

                parameter.Base64File = null;
                parameter.File = parameter.FilePath;
                parameter.IdHdBarangBukti = GetNewID.GenNewID();
                parameter.CreateAt = DateTime.Now;
                parameter.UpdateAt = DateTime.Now;
                _context.HdBarangBuktis.Add(parameter);
                await _context.SaveChangesAsync();

               
                LogAktivitasUser param = new LogAktivitasUser();
                param.LogId = GetNewID.GenNewID();
                param.DocumentId = parameter.IdHdBarangBukti.ToString();
                param.Judul = parameter.Nama;
                param.Status = 1;
                param.CreateAt = DateTime.Now;
                param.UpdateAt = DateTime.Now;
                param.CreateBy = parameter.CreateBy;

                await _logUserService.AddLogUser(param, cancellationToken);

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Tambah Header Barang Bukti.";
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

        public async Task<GlobalObjectResponse> UpdateHdBarangBukti(HdBarangBukti parameter, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            try
            {
                var upd_hd_brg_bukti = _context.HdBarangBuktis.Where(x => x.IdHdBarangBukti == parameter.IdHdBarangBukti).FirstOrDefault();
                if(upd_hd_brg_bukti != null)
                {
                    if (parameter.FileName != null && parameter.FileName != "") {
                        string Storage = _configuration["AppSettings:Storage"];
                        upd_hd_brg_bukti.FileName = parameter.FileName;
                        upd_hd_brg_bukti.FilePath = Storage + "\"" + parameter.FileName;
                        upd_hd_brg_bukti.File = upd_hd_brg_bukti.FilePath;
                        upd_hd_brg_bukti.Extension = parameter.Extension;
                        upd_hd_brg_bukti.ContentType = parameter.ContentType;
                        upd_hd_brg_bukti.FileSize = parameter.FileSize;
                    }

                    upd_hd_brg_bukti.Nama = parameter.Nama;
                    upd_hd_brg_bukti.NoRegistrasi = parameter.NoRegistrasi;
                    upd_hd_brg_bukti.NoPerkara = parameter.NoPerkara;
                    upd_hd_brg_bukti.TanggalWaktuPenyerahan = parameter.TanggalWaktuPenyerahan;
                    upd_hd_brg_bukti.Instansi = parameter.Instansi;
                    upd_hd_brg_bukti.File = parameter.File;
                    upd_hd_brg_bukti.DisitaDari = parameter.DisitaDari;
                    upd_hd_brg_bukti.NoBapPenyitaan = parameter.NoBapPenyitaan;
                    upd_hd_brg_bukti.TempatPenyitaan = parameter.TempatPenyitaan;
                    upd_hd_brg_bukti.NomorSprintPenyitaan = parameter.NomorSprintPenyitaan;
                    upd_hd_brg_bukti.Keterangan = parameter.Keterangan;
                    upd_hd_brg_bukti.UpdateAt = DateTime.Now;
                    upd_hd_brg_bukti.CreateBy = parameter.CreateBy;
                    _context.HdBarangBuktis.Update(upd_hd_brg_bukti);
                    await _context.SaveChangesAsync();

                    if (parameter.FileName != null && parameter.FileName != "") {
                        if (System.IO.File.Exists(upd_hd_brg_bukti.FilePath)) {
                            System.IO.File.Delete(upd_hd_brg_bukti.FilePath);
                        }

                        byte[] fileByteArray = Convert.FromBase64String(parameter.Base64File);
                        //System.IO.File.WriteAllBytes(parameter.FilePath, fileByteArray);
                        System.IO.File.WriteAllBytes(parameter.FilePath, Crypto.EncryptFileSha256(fileByteArray));
                    }

                } else {

                    res.Code = 404;
                    res.Message = MessageRepositories.MessageFailed + ": Id Hd Barang Bukti : " + parameter.IdHdBarangBukti + " Tidak Ada.";
                    res.Error = false;
                    return res;
                }

                LogAktivitasUser param = new LogAktivitasUser();
                param.LogId = GetNewID.GenNewID();
                param.DocumentId = parameter.IdHdBarangBukti.ToString();
                param.Judul = parameter.Nama;
                param.Status = 1;
                param.CreateAt = DateTime.Now;
                param.UpdateAt = DateTime.Now;
                param.CreateBy = parameter.CreateBy;

                await _logUserService.AddLogUser(param, cancellationToken);

                res.Code = 200;
                res.Message = MessageRepositories.MessageSuccess + " Update Header Barang Bukti.";
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

        public async Task<GlobalObjectListResponse> ListDataHdBarangBukti(CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<HdBarangBukti> lst_hd_brg_bukti = new List<HdBarangBukti>();
            try
            {
                System.GC.Collect();
                lst_hd_brg_bukti = _context.HdBarangBuktis.OrderBy(x => x.Id).AsNoTracking().ToList();

                res.Code = 200;
                res.Data = lst_hd_brg_bukti.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Header Barang Bukti."; ;
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

        public async Task<GlobalObjectListResponse> ListDataHdBarangBuktiById(string IdHdBarangBukti, CancellationToken cancellationToken)
        {
            GlobalObjectListResponse res = new GlobalObjectListResponse();
            List<HdBarangBukti> lst_hd_brg_bukti = new List<HdBarangBukti>();
            try
            {
                System.GC.Collect();
                lst_hd_brg_bukti = _context.HdBarangBuktis.Where(x => x.IdHdBarangBukti == IdHdBarangBukti).AsNoTracking().ToList();

                res.Code = 200;
                res.Data = lst_hd_brg_bukti.Cast<object>().ToList();
                res.Message = MessageRepositories.MessageSuccess + " Get Data Header Barang Bukti."; ;
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

        public async Task<GlobalObjectResponse> GetPreviewFile(string IdHdBarangBukti, CancellationToken cancellationToken)
        {
            GlobalObjectResponse res = new GlobalObjectResponse();
            HdBarangBukti? hd_brg_bukti = new HdBarangBukti();
            try
            {
                GetFileModel fileModel = new GetFileModel();
                byte[] fileBytes;
                System.GC.Collect();

                hd_brg_bukti = _context.HdBarangBuktis.Where(x => x.IdHdBarangBukti == IdHdBarangBukti).AsNoTracking().FirstOrDefault();
                if(hd_brg_bukti != null)
                {
                    if (hd_brg_bukti.Extension == ".pdf") {
                        fileBytes = System.IO.File.ReadAllBytes(hd_brg_bukti.FilePath);
                        fileModel.Base64File = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
                    }

                    if (hd_brg_bukti.Extension == ".docx") {
                        fileBytes = System.IO.File.ReadAllBytes(hd_brg_bukti.FilePath);
                        fileModel.Base64File = Convert.ToBase64String(Help.ConvertFile.ConvertDocxToPdf(Crypto.DecryptFileSha256(fileBytes)));
                    }

                    if (hd_brg_bukti.Extension == ".png" || hd_brg_bukti.Extension == ".jpg" || hd_brg_bukti.Extension == ".bmp") {
                        fileBytes = System.IO.File.ReadAllBytes(hd_brg_bukti.FilePath);
                        //fileModel.Base64File = Convert.ToBase64String(Help.ConvertFile.ConvertImageToPDF(Crypto.DecryptFileSha256(fileBytes), hd_brg_bukti.FileName));
                        fileModel.Base64File = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
                    }

                    fileModel.FileName = hd_brg_bukti.FileName;
                    fileModel.ContentType = hd_brg_bukti.ContentType;
                    fileModel.Extension = hd_brg_bukti.Extension;
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
