using ApiDms.Models;
using ApiDms.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFillingController : ControllerBase
    {
        private readonly DMSDbContext _db;
        private IConfiguration _config;
        public EFillingController(DMSDbContext context, IConfiguration config)
        {
            _db = context;
            _config = config;
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult<Response>> UploadFile(UploadVM content, List<IFormFile> files)
        {
            Response resp = new Response();

            if (content == null)
            {
                resp.code = 200;
                resp.status = false;
                resp.message = "Content is null";

                return resp;
            }
            else
            {
                var docType = await _db.DocumentTypes.Where(m => m.document_type_name.ToUpper() == content.document_type.ToUpper()).FirstOrDefaultAsync();
                if (docType == null)
                {
                    resp.code = 200;
                    resp.status = false;
                    resp.message = "document types not found";
                    return resp;
                }

                var collection = await _db.Collections.Where(m => m.collection_name.ToUpper() == content.collection_name.ToUpper()).FirstOrDefaultAsync();
                if (collection == null)
                {
                    resp.code = 200;
                    resp.status = false;
                    resp.message = "collection not found";
                    return resp;
                }
                else
                {
                    var directory = await _db.Directories.Where(m => m.collection_id == collection.collection_id 
                    && m.directory_name == content.reg_no).FirstOrDefaultAsync();
                    if(directory == null)
                    {
                        resp.code = 200;
                        resp.status = false;
                        resp.message = "directory not found";
                        return resp;
                    }
                    else
                    {
                        string Storage = _config["AppSettings:Storage"];

                        foreach (var file in files)
                        {

                            //var basePath = Path.Combine(System.IO.Directory.GetCurrentDirectory() + "\\Files\\");
                            //bool basePathExists = System.IO.Directory.Exists(basePath);
                            //if (!basePathExists) System.IO.Directory.CreateDirectory(basePath);


                            var basePath = Path.Combine(Storage + "\\" + content.reg_no);
                            bool basePathExists = System.IO.Directory.Exists(basePath);
                            if (!basePathExists) System.IO.Directory.CreateDirectory(basePath);

                            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            var filePath = Path.Combine(basePath, "\\V1_" + file.FileName);
                            var filePath2 = content.reg_no + "\\V1_" + file.FileName;
                            var extension = Path.GetExtension(file.FileName);
                            long fileSize = file.Length;
                            //if (!System.IO.File.Exists(filePath))
                            //{
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            var fileModel = new Document
                            {

                                file_name = fileName,
                                content_type = file.ContentType,
                                extension = extension,
                                file_path = filePath2,
                                file_size = fileSize,
                                collection_id = collection.collection_id,
                                version = 1,
                                document_type_id = docType.document_type_id,
                                directory_id = directory.directory_id,
                                created_at = DateTime.UtcNow,
                                created_by = content.created_by,
                            };
                            _db.Documents.Add(fileModel);
                            await _db.SaveChangesAsync();
                            //}
                        }

                        resp.code = 200;
                        resp.status = false;
                        resp.message = "upload file success";
                        return resp;
                    }
                }
            }
        }
    }
}
