using ApiDms.Help;
using ApiDms.Models;
using ApiDms.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace ApiDms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrowserController : ControllerBase
    {
        private readonly DMSDbContext _db;
        private IConfiguration _config;
        public BrowserController(DMSDbContext context, IConfiguration config)
        {
            _db = context;
            _config = config;
        }

        // GET: api/<CollectionsController>
        [HttpGet]
        public async Task<ActionResult<ResponseData>> Get()
        {
            ResponseData resp = new ResponseData();
            BrowserVM browserVM = new BrowserVM();
            try
            {
                List<DocumentVM> documentList = new List<DocumentVM>();
                var document_types = await _db.DocumentTypes.ToListAsync();
                browserVM.document_types = document_types;

                documentList = (from doc in _db.VDocuments
                                select new DocumentVM
                                {
                                    id = doc.id,
                                    document_id = doc.document_id,
                                    file_name = doc.file_name,
                                    file_size = doc.file_size / 1000,
                                    content_type = doc.content_type,
                                    extension = doc.extension,
                                    file_path = doc.file_path,
                                    collection_id = doc.collection_id,
                                    document_type_id = doc.document_type_id,
                                    document_type_name = doc.document_type_name,
                                    id_directory = doc.id_directory,
                                    directory_id = doc.directory_id,
                                    reference = doc.reference,
                                    document_no = doc.document_no,
                                    document_name = doc.document_name,
                                    encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                    version = doc.version,
                                    date_version = doc.date_version,
                                    expired_date = doc.expired_date,
                                    download_date = doc.download_date,
                                    owner_id = doc.owner_id,
                                    status = doc.status,
                                    created_at = doc.created_at,
                                    created_by = doc.created_by,
                                    favorite = doc.favorite,
                                    id_tags = doc.id_tags,
                                    tags_json = doc.tags_json

                                }).AsNoTracking().ToList();

            
                browserVM.documents = documentList;
                resp.code = 200;
                resp.message = "success";
                resp.error = false;
                resp.data = browserVM;
                return resp;
            }
            catch(DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        [HttpGet("GetDocumentAll")]
        public async Task<ActionResult<ResponseData>> GetDocumentAll(string? document_type_id, string? document_index_id, 
            string? document_index_value, string? user_id)
        {
            ResponseData resp = new ResponseData();
            BrowserVM browserVM = new BrowserVM();
            try
            {
                List<DocumentVM> documentList = new List<DocumentVM>();
                RulesViewModel rulesModel = new RulesViewModel();
              

                var document_types = await _db.DocumentTypes.ToListAsync();
                browserVM.document_types = document_types;

                List<string> lst_doc_delete = new List<string>();
                if (user_id == null || user_id == "") {
                    lst_doc_delete = _db.DocumentDelete.Where(x => x.status == 1).AsNoTracking().Select(x => x.document_id).ToList();
                } else {
                    lst_doc_delete = _db.DocumentDelete.Where(x => user_id == user_id && x.status == 1).Select(x => x.document_id).AsNoTracking().ToList();
                }

                if (document_type_id != null && document_type_id != "" && document_type_id != "SelectType") {

                    var doc_index = _db.DocumentIndices.Where(x => x.index_id == document_index_id).AsNoTracking().FirstOrDefault();
                    if (doc_index != null) {
                        rulesModel = JsonConvert.DeserializeObject<RulesViewModel>(doc_index.rules);
                    }

                    if (rulesModel.type == "Date") {
                        document_index_value = Convert.ToDateTime(document_index_value).ToString("yyyy-MM-dd");
                    }

                    List<string> list_doc_id = new List<string>();
                    var lst_index_value = _db.DocumentIndicesValue.Where(x => x.document_type_id == document_type_id && x.index_id == document_index_id && x.index_value == document_index_value).AsNoTracking().Select(x => x.document_id).ToList();
                    if (lst_index_value.Count() > 0) {
                        list_doc_id = lst_index_value;
                    }

                    if (document_type_id == "DocumentName")
                    {

                        documentList = (from doc in _db.VDocuments
                                        where doc.document_name.Trim() == document_index_value.Trim() && !lst_doc_delete.Contains(doc.document_id)
                                        select new DocumentVM
                                        {
                                            id = doc.id,
                                            document_id = doc.document_id,
                                            file_name = doc.file_name,
                                            file_size = doc.file_size / 1000,
                                            content_type = doc.content_type,
                                            extension = doc.extension,
                                            file_path = doc.file_path,
                                            collection_id = doc.collection_id,
                                            document_type_id = doc.document_type_id,
                                            document_type_name = doc.document_type_name,
                                            id_directory = doc.id_directory,
                                            directory_id = doc.directory_id,
                                            reference = doc.reference,
                                            document_no = doc.document_no,
                                            document_name = doc.document_name,
                                            encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                            version = doc.version,
                                            date_version = doc.date_version,
                                            expired_date = doc.expired_date,
                                            download_date = doc.download_date,
                                            owner_id = doc.owner_id,
                                            status = doc.status,
                                            created_at = doc.created_at,
                                            created_by = doc.created_by,
                                            favorite = doc.favorite,
                                            id_tags = doc.id_tags,
                                            tags_json = doc.tags_json

                                        }).AsNoTracking().ToList();
                    }
                    else
                    {

                        documentList = (from doc in _db.VDocuments
                                        where list_doc_id.Contains(doc.document_id) && !lst_doc_delete.Contains(doc.document_id)
                                        select new DocumentVM
                                        {
                                            id = doc.id,
                                            document_id = doc.document_id,
                                            file_name = doc.file_name,
                                            file_size = doc.file_size / 1000,
                                            content_type = doc.content_type,
                                            extension = doc.extension,
                                            file_path = doc.file_path,
                                            collection_id = doc.collection_id,
                                            document_type_id = doc.document_type_id,
                                            document_type_name = doc.document_type_name,
                                            id_directory = doc.id_directory,
                                            directory_id = doc.directory_id,
                                            reference = doc.reference,
                                            document_no = doc.document_no,
                                            document_name = doc.document_name,
                                            encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                            version = doc.version,
                                            date_version = doc.date_version,
                                            expired_date = doc.expired_date,
                                            download_date = doc.download_date,
                                            owner_id = doc.owner_id,
                                            status = doc.status,
                                            created_at = doc.created_at,
                                            created_by = doc.created_by,
                                            favorite = doc.favorite,
                                            id_tags = doc.id_tags,
                                            tags_json = doc.tags_json

                                        }).AsNoTracking().ToList();
                    }

                } else {

                    documentList = (from doc in _db.VDocuments
                                    where !lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();

                }

                browserVM.documents = documentList;

                resp.code = 200;
                resp.message = "success";
                resp.error = false;
                resp.data = browserVM;
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

          

        }


        [HttpGet("GetDir")]
        public async Task<ActionResult<ResponseData>> GetDir(int? id_dir, string? document_type_id, 
             string? document_index_id, string? document_index_value, string? user_id)
        {
            ResponseData resp = new ResponseData();
            List<DocumentVM> documentList = new List<DocumentVM>();
            Models.Directory dir = new Models.Directory();
            BrowserVM browserVM = new BrowserVM();

            try
            {
                int? id_directory = 0;
                string advance_search = "";
                dir = await _db.Directories.Where(x => x.id == id_dir).FirstOrDefaultAsync();
                if (dir == null)
                {
                    dir = await _db.Directories.Where(x => x.parent_id == id_dir).FirstOrDefaultAsync();
                }

                var document_types = await _db.DocumentTypes.ToListAsync();
                browserVM.document_types = document_types;
                browserVM.directory = dir;
                RulesViewModel rulesModel = new RulesViewModel();

                List<string> lst_doc_delete = new List<string>();

                if (user_id == null || user_id == "") {
                    lst_doc_delete = _db.DocumentDelete.Where(x => x.status == 1).AsNoTracking().Select(x => x.document_id).ToList();
                } else {
                    lst_doc_delete = _db.DocumentDelete.Where(x => user_id == user_id && x.status == 1).Select(x => x.document_id).AsNoTracking().ToList();
                }

                if (document_type_id != null && document_type_id != "")
                {
                    var doc_index = _db.DocumentIndices.Where(x => x.index_id == document_index_id).AsNoTracking().FirstOrDefault();
                    if(doc_index != null) {
                        rulesModel = JsonConvert.DeserializeObject<RulesViewModel>(doc_index.rules);
                    }

                    if(rulesModel.type == "Date") {
                        document_index_value = Convert.ToDateTime(document_index_value).ToString("yyyy-MM-dd");
                    }

                    List<string> list_doc_id = new List<string>();
                    var lst_index_value = _db.DocumentIndicesValue.Where(x => x.document_type_id == document_type_id && x.index_id == document_index_id && x.index_value == document_index_value).AsNoTracking().Select(x => x.document_id).ToList();
                    if (lst_index_value.Count() > 0)
                    {
                        list_doc_id = lst_index_value;
                    }

                    if(document_type_id == "DocumentName")  {

                        documentList = (from doc in _db.VDocuments
                                        where doc.document_name.Trim() == document_index_value.Trim() && !lst_doc_delete.Contains(doc.document_id)
                                        select new DocumentVM
                                        {
                                            id = doc.id,
                                            document_id = doc.document_id,
                                            file_name = doc.file_name,
                                            file_size = doc.file_size / 1000,
                                            content_type = doc.content_type,
                                            extension = doc.extension,
                                            file_path = doc.file_path,
                                            collection_id = doc.collection_id,
                                            document_type_id = doc.document_type_id,
                                            document_type_name = doc.document_type_name,
                                            id_directory = doc.id_directory,
                                            directory_id = doc.directory_id,
                                            reference = doc.reference,
                                            document_no = doc.document_no,
                                            document_name = doc.document_name,
                                            encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                            version = doc.version,
                                            date_version = doc.date_version,
                                            expired_date = doc.expired_date,
                                            download_date = doc.download_date,
                                            owner_id = doc.owner_id,
                                            status = doc.status,
                                            created_at = doc.created_at,
                                            created_by = doc.created_by,
                                            favorite = doc.favorite,
                                            id_tags = doc.id_tags,
                                            tags_json = doc.tags_json

                                        }).AsNoTracking().ToList();
                    } else {

                        documentList = (from doc in _db.VDocuments
                                        where list_doc_id.Contains(doc.document_id) && !lst_doc_delete.Contains(doc.document_id)
                                        select new DocumentVM
                                        {
                                            id = doc.id,
                                            document_id = doc.document_id,
                                            file_name = doc.file_name,
                                            file_size = doc.file_size / 1000,
                                            content_type = doc.content_type,
                                            extension = doc.extension,
                                            file_path = doc.file_path,
                                            collection_id = doc.collection_id,
                                            document_type_id = doc.document_type_id,
                                            document_type_name = doc.document_type_name,
                                            id_directory = doc.id_directory,
                                            directory_id = doc.directory_id,
                                            reference = doc.reference,
                                            document_no = doc.document_no,
                                            document_name = doc.document_name,
                                            encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                            version = doc.version,
                                            date_version = doc.date_version,
                                            expired_date = doc.expired_date,
                                            download_date = doc.download_date,
                                            owner_id = doc.owner_id,
                                            status = doc.status,
                                            created_at = doc.created_at,
                                            created_by = doc.created_by,
                                            favorite = doc.favorite,
                                            id_tags = doc.id_tags,
                                            tags_json = doc.tags_json

                                        }).AsNoTracking().ToList();
                    }


                } else {

                    id_directory = dir.id;
                    documentList = (from doc in _db.VDocuments
                                    where doc.id_directory == id_directory && !lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();
                }

                browserVM.documents = documentList;
                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = browserVM;
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }
        }

        [HttpGet("GetDocumentVersions")]
        public async Task<ActionResult<ResponseData>> GetDocumentVersions(string? document_id, string? user_id)
        {
            ResponseData resp = new ResponseData();
            List<DocumentVersionVM> documentList = new List<DocumentVersionVM>();
            try
            {
                List<string> lst_doc_delete = new List<string>();

                if (user_id == null || user_id == "") {
                    lst_doc_delete = _db.DocumentDelete.Where(x => x.status == 1).AsNoTracking().Select(x => x.document_id).ToList();
                }  else {
                    lst_doc_delete = _db.DocumentDelete.Where(x => user_id == user_id && x.status == 1).Select(x => x.document_id).AsNoTracking().ToList();
                }

                documentList = (from doc in _db.VDocumentVersions
                                where doc.document_id == document_id && !lst_doc_delete.Contains(doc.document_id)
                                select new DocumentVersionVM
                                {
                                    id = doc.id,
                                    document_id = doc.document_id,
                                    document_name = doc.document_name,
                                    name = doc.name,
                                    version_number = doc.version_number,
                                    file_size = doc.file_size,
                                    file_path = doc.file_path,
                                    extension = doc.extension,
                                    created_at = doc.created_at.ToString("yyyy-MM-dd HH:mm:ss")

                                }).AsNoTracking().ToList();

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = documentList.Cast<Object>().ToList();
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }
        }

        [HttpGet("GetDirAdvanceSearch")]
        public async Task<ActionResult<ResponseData>> GetDirAdvanceSearch(int? id_dir, string? document_type_id, string? index_value, 
            string? document_content, string? tags_json, string? user_id)
        {
            ResponseData resp = new ResponseData();
            List<DocumentVM> documentList = new List<DocumentVM>();
            Models.Directory dir = new Models.Directory();
            BrowserVM browserVM = new BrowserVM();

            try
            {
                int? id_directory = 0;
                string advance_search = "";
                dir = await _db.Directories.Where(x => x.id == id_dir).FirstOrDefaultAsync();
                if (dir == null)
                {
                    dir = await _db.Directories.Where(x => x.parent_id == id_dir).FirstOrDefaultAsync();
                }

                var document_types = await _db.DocumentTypes.ToListAsync();
                browserVM.document_types = document_types;
                browserVM.directory = dir;

                List<DocumentIndexValueVM> lst_idx = new List<DocumentIndexValueVM>();
                List<string> lst_idx_id = new List<string>();
                List<string> lst_idx_value = new List<string>();
                List<string> lst_doc_delete = new List<string>();

               if (user_id == null || user_id == "") {
                    lst_doc_delete = _db.DocumentDelete.Where(x => x.status == 1).AsNoTracking().Select(x => x.document_id).ToList();
                } else {
                    lst_doc_delete = _db.DocumentDelete.Where(x => user_id == user_id && x.status == 1).Select(x => x.document_id).AsNoTracking().ToList();
                }

                if (document_type_id != null && document_type_id != "" && document_type_id != "SelectType")
                {
                    if(index_value != null && index_value != "") {
                        lst_idx = JsonConvert.DeserializeObject<List<DocumentIndexValueVM>>(index_value);
                        lst_idx_id = lst_idx.Select(x => x.index_id).ToList();
                        lst_idx_value = lst_idx.Select(x => x.index_value).ToList();
                    }
                  
                    List<string> list_doc_id = new List<string>();
                    var lst_index_value = _db.DocumentIndicesValue.Where(x => x.document_type_id == document_type_id && lst_idx_id.Contains(x.index_id) && lst_idx_value.Contains(x.index_value)).AsNoTracking().Select(x => x.document_id).ToList();
                    if (lst_index_value.Count() > 0) {
                        list_doc_id = lst_index_value;
                    }

                    documentList = (from doc in _db.VDocuments
                                    where list_doc_id.Contains(doc.document_id) && !lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();

                }
                else if(document_content != null && document_content != "")
                {
                    documentList = (from doc in _db.VDocuments
                                    where doc.document_name == document_content && !lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();
                }
                else if (tags_json != null && tags_json != "")
                {
                    List<DocumentTagVM> lst_tag = new List<DocumentTagVM>();
                    List<string> lst_tag_value = new List<string>();

                    lst_tag = JsonConvert.DeserializeObject<List<DocumentTagVM>>(tags_json);
                    lst_tag_value = lst_tag.Select(x => x.document_tag).ToList();
                    var lst_doc_id = _db.DocumentTagsValue.Where(x => lst_tag_value.Contains(x.document_tag_name)).AsNoTracking().Select(x => x.document_id).ToList();

                    documentList = (from doc in _db.VDocuments
                                    where lst_doc_id.Contains(doc.document_id) && !lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();
                }
                else
                {

                    id_directory = dir.id;
                    documentList = (from doc in _db.VDocuments
                                    where doc.id_directory == id_directory && !lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();
                }

                browserVM.documents = documentList;
                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = browserVM;
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }
        }

        [HttpGet("GetDeleteDoc")]
        public async Task<ActionResult<ResponseData>> GetDeleteDoc(string? document_type_id, string? document_index_id, 
            string? document_index_value, string? user_id)
        {
            ResponseData resp = new ResponseData();
            List<DocumentVM> documentList = new List<DocumentVM>();
            Models.Directory dir = new Models.Directory();
            BrowserVM browserVM = new BrowserVM();

            try
            {
             
                RulesViewModel rulesModel = new RulesViewModel();
                List<string> lst_doc_delete = new List<string>();

                if (user_id == null || user_id == "") {
                    lst_doc_delete = _db.DocumentDelete.Where(x => x.status == 1).AsNoTracking().Select(x => x.document_id).ToList();
                } else {
                    lst_doc_delete = _db.DocumentDelete.Where(x => user_id == user_id && x.status == 1).Select(x => x.document_id).AsNoTracking().ToList();
                }

                if (document_type_id != null && document_type_id != "")
                {
                    var doc_index = _db.DocumentIndices.Where(x => x.index_id == document_index_id).AsNoTracking().FirstOrDefault();
                    if (doc_index != null)  {
                        rulesModel = JsonConvert.DeserializeObject<RulesViewModel>(doc_index.rules);
                    }

                    if (rulesModel.type == "Date") {
                        document_index_value = Convert.ToDateTime(document_index_value).ToString("yyyy-MM-dd");
                    }

                    List<string> list_doc_id = new List<string>();
                    var lst_index_value = _db.DocumentIndicesValue.Where(x => x.document_type_id == document_type_id && x.index_id == document_index_id && x.index_value == document_index_value).AsNoTracking().Select(x => x.document_id).ToList();
                    if (lst_index_value.Count() > 0)
                    {
                        list_doc_id = lst_index_value;
                    }

                    if (document_type_id == "DocumentName")
                    {

                        documentList = (from doc in _db.VDocuments
                                        where doc.document_name.Trim() == document_index_value.Trim() && !lst_doc_delete.Contains(doc.document_id)
                                        select new DocumentVM
                                        {
                                            id = doc.id,
                                            document_id = doc.document_id,
                                            file_name = doc.file_name,
                                            file_size = doc.file_size / 1000,
                                            content_type = doc.content_type,
                                            extension = doc.extension,
                                            file_path = doc.file_path,
                                            collection_id = doc.collection_id,
                                            document_type_id = doc.document_type_id,
                                            document_type_name = doc.document_type_name,
                                            id_directory = doc.id_directory,
                                            directory_id = doc.directory_id,
                                            reference = doc.reference,
                                            document_no = doc.document_no,
                                            document_name = doc.document_name,
                                            encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                            version = doc.version,
                                            date_version = doc.date_version,
                                            expired_date = doc.expired_date,
                                            download_date = doc.download_date,
                                            owner_id = doc.owner_id,
                                            status = doc.status,
                                            created_at = doc.created_at,
                                            created_by = doc.created_by,
                                            favorite = doc.favorite,
                                            id_tags = doc.id_tags,
                                            tags_json = doc.tags_json

                                        }).AsNoTracking().ToList();
                    }
                    else
                    {

                        documentList = (from doc in _db.VDocuments
                                        where list_doc_id.Contains(doc.document_id) && !lst_doc_delete.Contains(doc.document_id)
                                        select new DocumentVM
                                        {
                                            id = doc.id,
                                            document_id = doc.document_id,
                                            file_name = doc.file_name,
                                            file_size = doc.file_size / 1000,
                                            content_type = doc.content_type,
                                            extension = doc.extension,
                                            file_path = doc.file_path,
                                            collection_id = doc.collection_id,
                                            document_type_id = doc.document_type_id,
                                            document_type_name = doc.document_type_name,
                                            id_directory = doc.id_directory,
                                            directory_id = doc.directory_id,
                                            reference = doc.reference,
                                            document_no = doc.document_no,
                                            document_name = doc.document_name,
                                            encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                            version = doc.version,
                                            date_version = doc.date_version,
                                            expired_date = doc.expired_date,
                                            download_date = doc.download_date,
                                            owner_id = doc.owner_id,
                                            status = doc.status,
                                            created_at = doc.created_at,
                                            created_by = doc.created_by,
                                            favorite = doc.favorite,
                                            id_tags = doc.id_tags,
                                            tags_json = doc.tags_json

                                        }).AsNoTracking().ToList();
                    }

                }
                else
                {
                    
                    documentList = (from doc in _db.VDocuments
                                    where lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();
                }

                var document_types = await _db.DocumentTypes.ToListAsync();
                browserVM.document_types = document_types;
                browserVM.documents = documentList;

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = browserVM;
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }
        }

        [HttpGet("GetDeleteDocAdvanceSearch")]
        public async Task<ActionResult<ResponseData>> GetDeleteDocAdvanceSearch(string? document_type_id, string? index_value,
            string? document_content, string? tags_json, string? user_id)
        {
            ResponseData resp = new ResponseData();
            List<DocumentVM> documentList = new List<DocumentVM>();
            Models.Directory dir = new Models.Directory();
            BrowserVM browserVM = new BrowserVM();

            try
            {
                int? id_directory = 0;
               
                var document_types = await _db.DocumentTypes.ToListAsync();
                browserVM.document_types = document_types;
                browserVM.directory = dir;

                List<DocumentIndexValueVM> lst_idx = new List<DocumentIndexValueVM>();
                List<string> lst_idx_id = new List<string>();
                List<string> lst_idx_value = new List<string>();

                List<string> lst_doc_delete = new List<string>();

                if (user_id == null || user_id == "") {
                    lst_doc_delete = _db.DocumentDelete.Where(x => x.status == 1).AsNoTracking().Select(x => x.document_id).ToList();
                } else {
                    lst_doc_delete = _db.DocumentDelete.Where(x => user_id == user_id && x.status == 1).Select(x => x.document_id).AsNoTracking().ToList();
                }

                if (document_type_id != null && document_type_id != "" && document_type_id != "SelectType")
                {
                    if (index_value != null && index_value != "" && index_value != "SelectType") {
                        lst_idx = JsonConvert.DeserializeObject<List<DocumentIndexValueVM>>(index_value);
                        lst_idx_id = lst_idx.Select(x => x.index_id).ToList();
                        lst_idx_value = lst_idx.Select(x => x.index_value).ToList();
                    }

                    List<string> list_doc_id = new List<string>();
                    var lst_index_value = _db.DocumentIndicesValue.Where(x => x.document_type_id == document_type_id && lst_idx_id.Contains(x.index_id) 
                                           && lst_idx_value.Contains(x.index_value)).AsNoTracking().Select(x => x.document_id).ToList();
                    if (lst_index_value.Count() > 0)
                    {
                        list_doc_id = lst_index_value;
                    }

                    documentList = (from doc in _db.VDocuments
                                    where list_doc_id.Contains(doc.document_id) && lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();

                }
                else if (document_content != null && document_content != "")
                {
                    documentList = (from doc in _db.VDocuments
                                    where doc.document_name == document_content && lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();
                }
                else if (tags_json != null && tags_json != "")
                {
                    List<DocumentTagVM> lst_tag = new List<DocumentTagVM>();
                    List<string> lst_tag_value = new List<string>();

                    lst_tag = JsonConvert.DeserializeObject<List<DocumentTagVM>>(tags_json);
                    lst_tag_value = lst_tag.Select(x => x.document_tag).ToList();
                    var lst_doc_id = _db.DocumentTagsValue.Where(x => lst_tag_value.Contains(x.document_tag_name)).AsNoTracking().Select(x => x.document_id).ToList();

                    documentList = (from doc in _db.VDocuments
                                    where lst_doc_id.Contains(doc.document_id) && lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();
                }
                else
                {
                    documentList = (from doc in _db.VDocuments
                                    where lst_doc_delete.Contains(doc.document_id)
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();
                }

                browserVM.documents = documentList;
                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = browserVM;
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }
        }

        [HttpGet("GetFavoriteDoc")]
        public async Task<ActionResult<ResponseData>> GetFavoriteDoc(int? id_dir, string? document_type_id, string? document_index_id, 
            string? document_index_value, string user_id)
        {
            ResponseData resp = new ResponseData();
            List<DocumentVM> documentList = new List<DocumentVM>();
            Models.Directory dir = new Models.Directory();
            BrowserVM browserVM = new BrowserVM();

            try
            {
               

                RulesViewModel rulesModel = new RulesViewModel();
                if (document_type_id != null && document_type_id != "")
                {
                    var doc_index = _db.DocumentIndices.Where(x => x.index_id == document_index_id).AsNoTracking().FirstOrDefault();
                    if (doc_index != null)
                    {
                        rulesModel = JsonConvert.DeserializeObject<RulesViewModel>(doc_index.rules);
                    }

                    if (rulesModel.type == "Date") {
                        document_index_value = Convert.ToDateTime(document_index_value).ToString("yyyy-MM-dd");
                    }

                    List<string> lst_doc_delete = new List<string>();

                    if (user_id == null || user_id == "") {
                        lst_doc_delete = _db.DocumentDelete.Where(x => x.status == 1).AsNoTracking().Select(x => x.document_id).ToList();
                    } else {
                        lst_doc_delete = _db.DocumentDelete.Where(x => user_id == user_id && x.status == 1).Select(x => x.document_id).AsNoTracking().ToList();
                    }

                    List<string> list_doc_id = new List<string>();
                    var lst_index_value = _db.DocumentIndicesValue.Where(x => x.document_type_id == document_type_id && x.index_id == document_index_id && x.index_value == document_index_value).AsNoTracking().Select(x => x.document_id).ToList();
                    if (lst_index_value.Count() > 0)  {
                        list_doc_id = lst_index_value;
                    }

                    if (document_type_id == "DocumentName")
                    {

                        documentList = (from doc in _db.VDocuments
                                        where doc.document_name == document_index_value && !lst_doc_delete.Contains(doc.document_id)
                                        select new DocumentVM
                                        {
                                            id = doc.id,
                                            document_id = doc.document_id,
                                            file_name = doc.file_name,
                                            file_size = doc.file_size / 1000,
                                            content_type = doc.content_type,
                                            extension = doc.extension,
                                            file_path = doc.file_path,
                                            collection_id = doc.collection_id,
                                            document_type_id = doc.document_type_id,
                                            document_type_name = doc.document_type_name,
                                            id_directory = doc.id_directory,
                                            directory_id = doc.directory_id,
                                            reference = doc.reference,
                                            document_no = doc.document_no,
                                            document_name = doc.document_name,
                                            encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                            version = doc.version,
                                            date_version = doc.date_version,
                                            expired_date = doc.expired_date,
                                            download_date = doc.download_date,
                                            owner_id = doc.owner_id,
                                            status = doc.status,
                                            created_at = doc.created_at,
                                            created_by = doc.created_by,
                                            favorite = doc.favorite,
                                            id_tags = doc.id_tags,
                                            tags_json = doc.tags_json

                                        }).AsNoTracking().ToList();
                    }
                    else
                    {

                        documentList = (from doc in _db.VDocuments
                                        where list_doc_id.Contains(doc.document_id) && !lst_doc_delete.Contains(doc.document_id)
                                        select new DocumentVM
                                        {
                                            id = doc.id,
                                            document_id = doc.document_id,
                                            file_name = doc.file_name,
                                            file_size = doc.file_size / 1000,
                                            content_type = doc.content_type,
                                            extension = doc.extension,
                                            file_path = doc.file_path,
                                            collection_id = doc.collection_id,
                                            document_type_id = doc.document_type_id,
                                            document_type_name = doc.document_type_name,
                                            id_directory = doc.id_directory,
                                            directory_id = doc.directory_id,
                                            reference = doc.reference,
                                            document_no = doc.document_no,
                                            document_name = doc.document_name,
                                            encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                            version = doc.version,
                                            date_version = doc.date_version,
                                            expired_date = doc.expired_date,
                                            download_date = doc.download_date,
                                            owner_id = doc.owner_id,
                                            status = doc.status,
                                            created_at = doc.created_at,
                                            created_by = doc.created_by,
                                            favorite = doc.favorite,
                                            id_tags = doc.id_tags,
                                            tags_json = doc.tags_json

                                        }).AsNoTracking().ToList();
                    }

                } else {

                    documentList = (from doc in _db.VDocuments
                      
                                    select new DocumentVM
                                    {
                                        id = doc.id,
                                        document_id = doc.document_id,
                                        file_name = doc.file_name,
                                        file_size = doc.file_size / 1000,
                                        content_type = doc.content_type,
                                        extension = doc.extension,
                                        file_path = doc.file_path,
                                        collection_id = doc.collection_id,
                                        document_type_id = doc.document_type_id,
                                        document_type_name = doc.document_type_name,
                                        id_directory = doc.id_directory,
                                        directory_id = doc.directory_id,
                                        reference = doc.reference,
                                        document_no = doc.document_no,
                                        document_name = doc.document_name,
                                        encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                        version = doc.version,
                                        date_version = doc.date_version,
                                        expired_date = doc.expired_date,
                                        download_date = doc.download_date,
                                        owner_id = doc.owner_id,
                                        status = doc.status,
                                        created_at = doc.created_at,
                                        created_by = doc.created_by,
                                        favorite = doc.favorite,
                                        id_tags = doc.id_tags,
                                        tags_json = doc.tags_json

                                    }).AsNoTracking().ToList();
                }

                var document_types = await _db.DocumentTypes.ToListAsync();
                browserVM.document_types = document_types;
                browserVM.documents = documentList;

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = browserVM;
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }
        }

        [HttpGet("{id}/{idx}")]
        public async Task<ActionResult<List<JsTreeModel>>> Get(int id, int idx)
        {
            var folder = await _db.FCJsTreeModels.FromSqlInterpolated($"SELECT * FROM dms.FC_DIR_TREE({id},{idx})").ToListAsync();
            if (folder.Count == 0)
            {
                var root = await _db.FCJsTreeModels.FromSqlInterpolated($"SELECT * FROM dms.v_root WHERE id = {id.ToString()}").ToListAsync();
                return root;
            }
            return folder;
        }

        [HttpPost("GetPath")]
        public async Task<ActionResult<ResponseListData>> GetPath(GetPathVM content)
        {
            ResponseListData resp = new ResponseListData();
            List<DocumentVM> documentList = new List<DocumentVM>();
            BrowserVM browserVM = new BrowserVM();

            try
            {
                documentList = (from doc in _db.Documents
                                join dir in _db.Directories on doc.directory_id equals dir.directory_id
                                select new DocumentVM
                                {
                                    id = doc.id,
                                    document_id = doc.document_id,
                                    file_name = doc.file_name,
                                    content_type = doc.content_type,
                                    extension = doc.extension,
                                    file_path = doc.file_path,
                                    collection_id = doc.collection_id,
                                    document_type_id = doc.document_type_id,
                                    id_directory = dir.id,
                                    directory_id = dir.directory_id,
                                    reference = doc.reference,
                                    document_no = doc.document_no,
                                    document_name = doc.document_name,
                                    encrypt_file = Convert.ToBase64String(System.IO.File.ReadAllBytes(doc.file_path)),
                                    version = doc.version,
                                    date_version = doc.date_version,
                                    expired_date = doc.expired_date,
                                    download_date = doc.download_date,
                                    owner_id = doc.owner_id,
                                    status = doc.status,
                                    created_at = doc.created_at,
                                    created_by = doc.created_by,

                                }).OrderBy(x => x.id).AsNoTracking().ToList();

                browserVM.documents = documentList;

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = browserVM.documents.Cast<object>().ToList();
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        [HttpPost("CreateFolder")]
        public async Task<ActionResult<ResponseData>> CreateFolder(ApiDms.Models.Directory content)
        {
            ResponseData resp = new ResponseData();

            try
            {
                if (content == null)
                {
                    resp.code = 400;
                    resp.error = false;
                    resp.message = "Directory is null";

                    return resp;
                }
                else
                {
                    var parent = await _db.Directories.FindAsync(content.parent_id);
                    //var parent = await _db.Directories.Where(x => x.parent_id == content.parent_id).FirstOrDefaultAsync();

                    if (content.parent_id != null)
                    {
                        var dir = await _db.Directories.Where(x => x.directory_name.ToLower() == content.directory_name.ToLower()
                        && x.collection_id == content.collection_id
                        && x.parent_id == content.parent_id
                        ).FirstOrDefaultAsync();
                        if (dir == null)
                        {
                            content.directory_id = getNewId();
                            content.directory_name = content.directory_name;
                            content.disk = "";
                            content.path_name = parent.path_name + "\\" + content.directory_name;
                            content.parent_id = content.parent_id;
                            content.collection_id = content.collection_id;
                            content.owner_id = content.created_by;
                            content.status = 1;
                            content.created_at = DateTime.Now;
                            content.created_by = content.created_by;
                            content.updated_at = null;
                            content.updated_by = null;

                            string Storage = _config["AppSettings:Storage"];
                            var basePath = Path.Combine(Storage + content.path_name.Trim());
                            bool basePathExists = System.IO.Directory.Exists(basePath);
                            if (!basePathExists) System.IO.Directory.CreateDirectory(basePath);

                            _db.Add(content);
                            await _db.SaveChangesAsync();

                            resp.code = 200;
                            resp.error = false;
                            resp.message = "create directory success";

                            return resp;
                        }
                        else
                        {
                            resp.code = 409;
                            resp.error = false;
                            resp.message = "directory alreadi exist";

                            return resp;
                        }
                    }
                    else
                    {
                        resp.code = 404;
                        resp.error = false;
                        resp.message = "parent is not found";

                        return resp;
                    }
                }
            }
            catch(DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }
            
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult<ResponseData>> UploadFile(UploadFileVM content)
        {
            ResponseData resp = new ResponseData();
            List<DocumentIndexValueVM> lst_doc_index = new List<DocumentIndexValueVM>();
            List<DocumentTagVM> lst_doc_tag = new List<DocumentTagVM>();

            using (var dbTrans = _db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (content == null)
                    {
                        resp.code = 400;
                        resp.error = false;
                        resp.message = "Content is null";

                        return resp;
                    }
                    else
                    {
                        var directory = await _db.Directories.Where(m => m.id == Convert.ToInt32(content.directory_id)).FirstOrDefaultAsync();
                        if (directory == null)
                        {
                            resp.code = 404;
                            resp.error = false;
                            resp.message = "directory not found";
                            return resp;
                        }
                        else
                        {

                            string Storage = _config["AppSettings:Storage"];
                            string file_path = Storage + directory.path_name + "\\" + content.file_name + content.extension;

                            string document_id = getNewId();
                            var fileModel = new Models.Document
                            {
                                document_id = document_id,
                                file_name = content.document_name,
                                content_type = content.content_type,
                                extension = content.extension,
                                file_path = file_path,
                                file_size = content.file_size,
                                collection_id = directory.collection_id,
                                version = 1,
                                document_type_id = content.document_type_id,
                                document_name = content.document_name,
                                document_no = content.document_no,
                                reference = content.reference,
                                date_version = content.date_version,
                                expired_date = content.expired_date,
                                directory_id = directory.directory_id,
                                created_at = DateTime.Now,
                                created_by = content.created_by
                            };
                            _db.Documents.Add(fileModel);
                            await _db.SaveChangesAsync();

                            var fileVersionModel = new Models.DocumentVersions
                            {
                                document_id = document_id,
                                version_number = 1,
                                name = content.document_name,
                                content_type = content.content_type,
                                extension = content.extension,
                                file_path = file_path,
                                file_size = content.file_size,
                                expired_date = content.expired_date,
                                created_at = DateTime.Now,
                                created_by = content.created_by
                            };
                            _db.DocumentVersions.Add(fileVersionModel);
                            await _db.SaveChangesAsync();

                            if (content.index_value != null && content.index_value != "")
                            {
                                lst_doc_index = JsonConvert.DeserializeObject<List<DocumentIndexValueVM>>(content.index_value);

                                foreach (var dataIndex in lst_doc_index)
                                {
                                    var Index = new Models.DocumentIndexValue
                                    {
                                        document_id = fileModel.document_id,
                                        document_type_id = content.document_type_id,
                                        index_id = dataIndex.index_id,
                                        index_value = dataIndex.index_value,
                                        created_at = DateTime.Now,
                                        created_by = content.created_by,
                                    };
                                    _db.DocumentIndicesValue.Add(Index);
                                    await _db.SaveChangesAsync();
                                }
                            }

                            dbTrans.Commit();
                            dbTrans.Dispose();

                            byte[] fileByteArray = Convert.FromBase64String(content.base64file);
                            System.IO.File.WriteAllBytes(file_path, Crypto.EncryptFileSha256(fileByteArray));

                            resp.code = 200;
                            resp.error = false;
                            resp.message = "upload file success";
                            return resp;
                        }
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    dbTrans.Rollback();
                    dbTrans.Dispose();

                    resp.code = 500;
                    if (ex.InnerException.Message != null)
                    {
                        resp.message = ex.InnerException.Message;
                    }
                    else
                    {
                        resp.message = ex.Message;
                    }

                    resp.error = true;
                    resp.data = null;
                    return resp;
                }

                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    dbTrans.Dispose();

                    resp.code = 500;
                    if (ex.InnerException.Message != null)
                    {
                        resp.message = ex.InnerException.Message;
                    }
                    else
                    {
                        resp.message = ex.Message;
                    }

                    resp.error = true;
                    resp.data = null;
                    return resp;
                }
            }

            

        }

        
        [HttpPost("EditUploadFile")]
        public async Task<ActionResult<ResponseData>> EditUploadFile(UploadFileVM content)
        {
            ResponseData resp = new ResponseData();
            List<DocumentIndexValueVM> lst_doc_index = new List<DocumentIndexValueVM>();
            List<DocumentTagVM> lst_doc_tag = new List<DocumentTagVM>();


            using (var dbTrans = _db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (content == null)
                    {
                        resp.code = 400;
                        resp.error = false;
                        resp.message = "Content is null";

                        return resp;

                    }
                    else
                    {

                        var directory = await _db.Directories.Where(m => m.id == Convert.ToInt32(content.directory_id)).FirstOrDefaultAsync();
                        if (directory == null)
                        {
                            resp.code = 404;
                            resp.error = false;
                            resp.message = "directory not found";
                            return resp;
                        }
                        else
                        {

                            var upd_document = _db.Documents.Where(x => x.id == content.id).FirstOrDefault();
                            if (upd_document != null)
                            {
                                upd_document.file_name = content.document_name;
                                upd_document.expired_date = content.expired_date;
                                upd_document.updated_at = DateTime.Now;
                                upd_document.updated_by = content.created_by;
                                _db.Documents.Update(upd_document);
                                await _db.SaveChangesAsync();

                            }

                            if (content.index_value != null && content.index_value != "") {
                                lst_doc_index = JsonConvert.DeserializeObject<List<DocumentIndexValueVM>>(content.index_value);

                                foreach (var dataIndex in lst_doc_index)
                                {
                                    var upd_index = _db.DocumentIndicesValue.Where(x => x.index_id == dataIndex.index_id).FirstOrDefault();
                                    if (upd_index != null)
                                    {
                                        upd_index.index_value = dataIndex.index_value;
                                        upd_index.last_updated_at = upd_index.updated_at;
                                        upd_index.updated_at = DateTime.Now;
                                        upd_index.updated_by = content.created_by;

                                        _db.DocumentIndicesValue.Update(upd_index);
                                        await _db.SaveChangesAsync();
                                    }
                                }

                            }

                            dbTrans.Commit();
                            dbTrans.Dispose();

                            resp.code = 200;
                            resp.error = false;
                            resp.message = "upload file success";
                            return resp;
                        }
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    dbTrans.Rollback();
                    dbTrans.Dispose();

                    resp.code = 500;
                    if (ex.InnerException.Message != null)
                    {
                        resp.message = ex.InnerException.Message;
                    }
                    else
                    {
                        resp.message = ex.Message;
                    }

                    resp.error = true;
                    resp.data = null;
                    return resp;
                }

                catch (Exception ex)
                {

                    dbTrans.Rollback();
                    dbTrans.Dispose();

                    resp.code = 500;
                    if (ex.InnerException.Message != null)
                    {
                        resp.message = ex.InnerException.Message;
                    }
                    else
                    {
                        resp.message = ex.Message;
                    }

                    resp.error = true;
                    resp.data = null;
                    return resp;
                }
            }
        }

        //[HttpGet("DownloadFile/{id}")]
        //public async Task<ActionResult<ResponseData>> DownloadFile(int id)
        //{
        //    ResponseData resp = new ResponseData();
        //    DownloadFileVM downloadFileVM = new DownloadFileVM();
        //    try
        //    {
        //        var document = await _db.Documents.Where(m => m.id == id).FirstOrDefaultAsync();
        //        if(document != null)
        //        {
        //            downloadFileVM.file_name = document.file_name;
        //            downloadFileVM.document_id = document.document_id;
        //            downloadFileVM.document_no = document.document_no;
        //            downloadFileVM.file_path = document.file_path;
        //            downloadFileVM.content_type = document.content_type;
        //            downloadFileVM.extension = document.extension;
        //            downloadFileVM.date_version = document.date_version;
        //            downloadFileVM.expired_date = document.expired_date;

        //            byte[] fileBytes = System.IO.File.ReadAllBytes(document.file_path);
        //            downloadFileVM.decrypt_file = Crypto.DecryptFileSha256(fileBytes);

        //            document.download_date = DateTime.Now;
        //            _db.Update(document);
        //            await _db.SaveChangesAsync();
        //        }

        //        resp.code = 200;
        //        resp.error = false;
        //        resp.message = "success";
        //        resp.data = downloadFileVM;
        //        return resp;
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        resp.code = 500;
        //        if (ex.InnerException.Message != null)
        //        {
        //            resp.message = ex.InnerException.Message;
        //        }
        //        else
        //        {
        //            resp.message = ex.Message;
        //        }

        //        resp.error = true;
        //        resp.data = null;
        //        return resp;
        //    }

        //    catch (Exception ex)
        //    {
        //        resp.code = 500;
        //        if (ex.InnerException.Message != null)
        //        {
        //            resp.message = ex.InnerException.Message;
        //        }
        //        else
        //        {
        //            resp.message = ex.Message;
        //        }

        //        resp.error = true;
        //        resp.data = null;
        //        return resp;
        //    }

        //}

        [HttpGet("DownloadFile/{id}")]
        public async Task<ActionResult<ResponseData>> DownloadFile(int id)
        {
            ResponseData resp = new ResponseData();
            DownloadFileVM downloadFileVM = new DownloadFileVM();
            try
            {
                var document = await _db.DocumentVersions.Where(m => m.id == id).FirstOrDefaultAsync();
                if (document != null)
                {
                    var upd_doc = _db.Documents.Where(x => x.document_id == document.document_id).FirstOrDefault();
                    if (upd_doc != null)
                    {
                        downloadFileVM.file_name = upd_doc.file_name;
                        downloadFileVM.document_no = upd_doc.document_no;
                        upd_doc.download_date = DateTime.Now;
                        _db.Documents.Update(upd_doc);
                        await _db.SaveChangesAsync();
                    }
                   
                    downloadFileVM.document_id = document.document_id;
                    downloadFileVM.file_path = document.file_path;
                    downloadFileVM.content_type = document.content_type;
                    downloadFileVM.extension = document.extension;

#pragma warning disable CS8604 // Possible null reference argument.
                    byte[] fileBytes = System.IO.File.ReadAllBytes(document.file_path);
#pragma warning restore CS8604 // Possible null reference argument.
                    downloadFileVM.decrypt_file = Crypto.DecryptFileSha256(fileBytes);

                }

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = downloadFileVM;
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        //[HttpGet("PreviewFile/{id}")]
        //public async Task<ActionResult<ResponseData>> PreviewFile(int id)
        //{
        //    ResponseData resp = new ResponseData();
        //    DownloadFileVM downloadFileVM = new DownloadFileVM();
        //    byte[] fileBytes;
        //    try
        //    {
        //        //string TempFileHtmlContent = _config["AppSettings:TempFileHtmlContent"];
        //        //string TempFilePDF = _config["AppSettings:TempFilePDF"];

        //        var document = await _db.Documents.Where(m => m.id == id).AsNoTracking().FirstOrDefaultAsync();
        //        if (document != null)
        //        {
        //            if(document.extension == ".pdf")
        //            {
        //                fileBytes = System.IO.File.ReadAllBytes(document.file_path);

        //                downloadFileVM.file_name = document.file_name;
        //                downloadFileVM.document_id = document.document_id;
        //                downloadFileVM.document_no = document.document_no;
        //                downloadFileVM.file_path = document.file_path;
        //                downloadFileVM.encrypt_file = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
        //                downloadFileVM.content_type = document.content_type;
        //                downloadFileVM.extension = document.extension;
        //                downloadFileVM.date_version = document.date_version;
        //                downloadFileVM.expired_date = document.expired_date;
        //            }

        //            else if (document.extension == ".docx")
        //            {
        //                fileBytes = System.IO.File.ReadAllBytes(document.file_path);

        //                downloadFileVM.file_name = document.file_name;
        //                downloadFileVM.document_id = document.document_id;
        //                downloadFileVM.document_no = document.document_no;
        //                downloadFileVM.file_path = document.file_path;
        //                //downloadFileVM.encrypt_file = Convert.ToBase64String(Help.AsposeFile.ConvertWordToPDF(Crypto.DecryptFileSha256(fileBytes), document.file_name));
        //                downloadFileVM.encrypt_file = Convert.ToBase64String(Help.ConvertFile.ConvertDocxToPdf(Crypto.DecryptFileSha256(fileBytes)));
        //                downloadFileVM.content_type = document.content_type;
        //                downloadFileVM.extension = document.extension;
        //                downloadFileVM.date_version = document.date_version;
        //                downloadFileVM.expired_date = document.expired_date;
        //            }
        //            else if (document.extension == ".png" || document.extension == ".jpg" || document.extension == ".bmp")
        //            {
        //                fileBytes = System.IO.File.ReadAllBytes(document.file_path);

        //                downloadFileVM.file_name = document.file_name;
        //                downloadFileVM.document_id = document.document_id;
        //                downloadFileVM.document_no = document.document_no;
        //                downloadFileVM.file_path = document.file_path;
        //                //downloadFileVM.encrypt_file = Convert.ToBase64String(Help.AsposeFile.ConvertImageToPDF(Crypto.DecryptFileSha256(fileBytes), document.file_name));
        //                downloadFileVM.encrypt_file = Convert.ToBase64String(Help.ConvertFile.ConvertImageToPDF(Crypto.DecryptFileSha256(fileBytes), document.file_name));
        //                downloadFileVM.content_type = document.content_type;
        //                downloadFileVM.extension = document.extension;
        //                downloadFileVM.date_version = document.date_version;
        //                downloadFileVM.expired_date = document.expired_date;
        //            }
        //            else
        //            {
        //                fileBytes = System.IO.File.ReadAllBytes(document.file_path);

        //                downloadFileVM.file_name = document.file_name;
        //                downloadFileVM.document_id = document.document_id;
        //                downloadFileVM.document_no = document.document_no;
        //                downloadFileVM.file_path = document.file_path;
        //                downloadFileVM.encrypt_file = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
        //                downloadFileVM.content_type = document.content_type;
        //                downloadFileVM.extension = document.extension;
        //                downloadFileVM.date_version = document.date_version;
        //                downloadFileVM.expired_date = document.expired_date;
        //            }
        //        }

        //        resp.code = 200;
        //        resp.error = false;
        //        resp.message = "success";
        //        resp.data = downloadFileVM;
        //        return resp;
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        resp.code = 500;
        //        if (ex.InnerException.Message != null)
        //        {
        //            resp.message = ex.InnerException.Message;
        //        }
        //        else
        //        {
        //            resp.message = ex.Message;
        //        }

        //        resp.error = true;
        //        resp.data = null;
        //        return resp;
        //    }

        //    catch (Exception ex)
        //    {
        //        resp.code = 500;
        //        if (ex.InnerException.Message != null)
        //        {
        //            resp.message = ex.InnerException.Message;
        //        }
        //        else
        //        {
        //            resp.message = ex.Message;
        //        }

        //        resp.error = true;
        //        resp.data = null;
        //        return resp;
        //    }

        //}

        [HttpGet("PreviewFile/{id}")]
        public async Task<ActionResult<ResponseData>> PreviewFile(int id)
        {
            ResponseData resp = new ResponseData();
            DownloadFileVM downloadFileVM = new DownloadFileVM();
            byte[] fileBytes;
            try
            {
                string file_name = "";
                var document = await _db.DocumentVersions.Where(m => m.id == id).AsNoTracking().FirstOrDefaultAsync();
                if (document != null)
                {
                    var doc = await _db.Documents.Where(m => m.document_id == document.document_id).AsNoTracking().FirstOrDefaultAsync();
                    if (doc != null)
                    {
                        file_name = doc.file_name;
                    }

                    if (document.extension == ".pdf")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(document.file_path);
                        downloadFileVM.document_id = document.document_id;
                        downloadFileVM.file_path = document.file_path;
                        downloadFileVM.encrypt_file = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
                        downloadFileVM.content_type = document.content_type;
                        downloadFileVM.extension = document.extension;
                    }

                    else if (document.extension == ".docx")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(document.file_path);
                        downloadFileVM.document_id = document.document_id;
                        downloadFileVM.file_path = document.file_path;
                        //downloadFileVM.encrypt_file = Convert.ToBase64String(Help.AsposeFile.ConvertWordToPDF(Crypto.DecryptFileSha256(fileBytes), document.file_name));
                        downloadFileVM.encrypt_file = Convert.ToBase64String(Help.ConvertFile.ConvertDocxToPdf(Crypto.DecryptFileSha256(fileBytes)));
                        downloadFileVM.content_type = document.content_type;
                        downloadFileVM.extension = document.extension;
                    }
                    else if (document.extension == ".png" || document.extension == ".jpg" || document.extension == ".bmp")
                    {
                        fileBytes = System.IO.File.ReadAllBytes(document.file_path);
                        downloadFileVM.document_id = document.document_id;
                        downloadFileVM.file_path = document.file_path;
                        //downloadFileVM.encrypt_file = Convert.ToBase64String(Help.AsposeFile.ConvertImageToPDF(Crypto.DecryptFileSha256(fileBytes), document.file_name));
                        downloadFileVM.encrypt_file = Convert.ToBase64String(Help.ConvertFile.ConvertImageToPDF(Crypto.DecryptFileSha256(fileBytes), file_name));
                        downloadFileVM.content_type = document.content_type;
                        downloadFileVM.extension = document.extension;
                    }
                    else
                    {
                        fileBytes = System.IO.File.ReadAllBytes(document.file_path);
                        downloadFileVM.document_id = document.document_id;
                        downloadFileVM.file_path = document.file_path;
                        downloadFileVM.encrypt_file = Convert.ToBase64String(Crypto.DecryptFileSha256(fileBytes));
                        downloadFileVM.content_type = document.content_type;
                        downloadFileVM.extension = document.extension;
                    }
                }

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = downloadFileVM;
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        [HttpDelete("DeleteFile")]
        public async Task<ActionResult<ResponseData>> DeleteFile(int id, string? user_id)
        {
            ResponseData resp = new ResponseData();
            try
            {
                var document = await _db.Documents.Where(m => m.id == id).FirstOrDefaultAsync();
                if (document != null)
                {
                    if(user_id == "") {
                        user_id = null;
                    }
                    document.last_updated_at = document.updated_at;
                    document.updated_at = DateTime.Now;
                    document.updated_by = user_id;
                    _db.Documents.Update(document);
                    await _db.SaveChangesAsync();

                    var upd_doc_del = await _db.DocumentDelete.Where(x => x.document_id == document.document_id).FirstOrDefaultAsync();
                    if(upd_doc_del != null) {

                        upd_doc_del.status = 1;
                        upd_doc_del.updated_at = DateTime.Now;
                        upd_doc_del.updated_by = user_id;
                        _db.DocumentDelete.Update(upd_doc_del);
                        await _db.SaveChangesAsync();

                    } else {

                        DocumentDelete delData = new DocumentDelete();
                        delData.document_delete_id = getNewId();
                        delData.document_id = document.document_id;
                        delData.user_id = user_id;
                        delData.status = 1;
                        delData.created_at = DateTime.Now;
                        delData.created_by = user_id;
                        delData.updated_at = DateTime.Now;
                        delData.updated_by = user_id;
                        _db.DocumentDelete.Add(delData);
                        await _db.SaveChangesAsync();

                    }


                    resp.code = 200;
                    resp.error = false  ;
                    resp.message = "delete file success";

                } else {

                    resp.code = 404;
                    resp.error = false;
                    resp.message = "document id : " + id.ToString() + " Not Found.";
                }
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        [HttpPut("UpdateRestoreDelete")]
        public async Task<ActionResult<ResponseData>> UpdateRestoreDelete(DocumentVM content)
        {
            ResponseData resp = new ResponseData();
            try
            {
                var document = await _db.Documents.Where(m => m.id == content.id).FirstOrDefaultAsync();
                if (document != null)
                {
                    document.updated_by = content.updated_by;
                    document.last_updated_at = document.updated_at;
                    document.updated_at = DateTime.Now;
                    _db.Documents.Update(document);
                    await _db.SaveChangesAsync();

                    var upd_doc_del = await _db.DocumentDelete.Where(x => x.document_id == document.document_id).FirstOrDefaultAsync();
                    if(upd_doc_del != null)
                    {
                        upd_doc_del.status = 0;
                        upd_doc_del.updated_by = content.updated_by;
                        upd_doc_del.updated_at = DateTime.Now;
                        _db.DocumentDelete.Update(upd_doc_del);
                        await _db.SaveChangesAsync();
                    }

                    resp.code = 200;
                    resp.error = false;
                    resp.message = "restore delete success";

                }
                else
                {

                    resp.code = 404;
                    resp.error = false;
                    resp.message = "document id : " + content.id.ToString() + " Not Found.";
                }
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        [HttpPut("UpdateFavorite")]
        public async Task<ActionResult<ResponseData>> UpdateFavorite(DocumentVM content)
        {
            ResponseData resp = new ResponseData();
            try
            {
                var document = await _db.Documents.Where(m => m.id == content.id).FirstOrDefaultAsync();
                if (document != null)  
                {
                    document.updated_by = content.updated_by;
                    document.last_updated_at = document.updated_at;
                    document.updated_at = DateTime.Now;
                    await _db.SaveChangesAsync();

                    var upd_fav = _db.DocumentFavorites.Where(x => x.document_id == document.document_id).FirstOrDefault();
                    if(upd_fav != null) {

                        if(upd_fav.status == 0)
                        {
                            upd_fav.status = 1;
                            upd_fav.updated_at = DateTime.Now;
                            upd_fav.updated_by = content.created_by;

                            _db.DocumentFavorites.Update(upd_fav);
                            await _db.SaveChangesAsync();

                        } else {

                            upd_fav.status = 0;
                            upd_fav.updated_at = DateTime.Now;
                            upd_fav.updated_by = content.created_by;

                            _db.DocumentFavorites.Update(upd_fav);
                            await _db.SaveChangesAsync();
                        }

                    } else {

                        var favoriteModel = new Models.DocumentFavorite
                        {
                            document_id = document.document_id,
                            user_id = content.created_by,
                            status = 1,
                            created_at = DateTime.Now,
                            created_by = content.created_by,
                            last_updated_at = DateTime.Now,
                            updated_at = DateTime.Now,
                            updated_by = content.created_by
                        };
                        _db.DocumentFavorites.Add(favoriteModel);
                        await _db.SaveChangesAsync();
                    }
                   

                    resp.code = 200;
                    resp.error = false;
                    resp.message = "update favorite document success";

                }
                else
                {

                    resp.code = 404;
                    resp.error = false;
                    resp.message = "document id : " + content.id.ToString() + " Not Found.";
                }
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        [HttpPut("UpdateRestoreFavorite")]
        public async Task<ActionResult<ResponseData>> UpdateRestoreFavorite(DocumentVM content)
        {
            ResponseData resp = new ResponseData();
            try
            {
                var document = await _db.Documents.Where(m => m.id == content.id).FirstOrDefaultAsync();
                if (document != null)
                {
                    //document.updated_by = content.updated_by;
                    //document.last_updated_at = document.updated_at;
                    //document.updated_at = DateTime.Now;
                    //await _db.SaveChangesAsync();

                    var upd_fav = _db.DocumentFavorites.Where(x => x.document_id == document.document_id).FirstOrDefault();
                    if (upd_fav != null) {

                        if (upd_fav.status == 1 || upd_fav.status == null) {
                            upd_fav.status = 0;
                            upd_fav.updated_at = DateTime.Now;
                            upd_fav.updated_by = content.created_by;

                            _db.DocumentFavorites.Update(upd_fav);
                            await _db.SaveChangesAsync();
                        }

                    }
                  
                    resp.code = 200;
                    resp.error = false;
                    resp.message = "update favorite document success";

                } else {

                    resp.code = 404;
                    resp.error = false;
                    resp.message = "document id : " + content.id.ToString() + " Not Found.";
                }
                return resp;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

        }

        private string getNewId()
        {
            var ticks = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            var myUniqueFileName = ans.ToString("x").ToLower();
            return myUniqueFileName;
        }

        [HttpPost("CreateTags")]
        public async Task<ActionResult<ResponseData>> CreateTags(DocumentTag content)
        {
            ResponseData resp = new ResponseData();

            try
            {
                if (content == null)
                {
                    resp.code = 400;
                    resp.error = false;
                    resp.message = "Tags is null";

                    return resp;
                }
                else
                {
                    List<DocumentTagValue> lst_tag_value = new List<DocumentTagValue>();
                    List<DocumentTagVM> lst_tag = new List<DocumentTagVM>();

                    var tags = await _db.DocumentTags.Where(m => m.document_id == content.document_id).FirstOrDefaultAsync();
                    if (tags == null)
                    {
                        DocumentTag docTags = new DocumentTag();
                        docTags.document_id = content.document_id;
                        docTags.tags_json = content.tags_json;
                        docTags.user_id = content.user_id;
                        docTags.created_at = DateTime.Now;
                        docTags.created_by = content.created_by;
                        docTags.updated_at = DateTime.Now;
                        docTags.updated_by = content.created_by;
                        _db.DocumentTags.Add(docTags);
                        await _db.SaveChangesAsync();

                        var tagsDelete = _db.DocumentTagsValue
                                         .Where(e => e.document_id == content.document_id)
                                         .ToList();

                        _db.DocumentTagsValue.RemoveRange(tagsDelete);
                        await _db.SaveChangesAsync();

                        lst_tag = JsonConvert.DeserializeObject<List<DocumentTagVM>>(content.tags_json);
                        foreach(var data in lst_tag)
                        {
                            DocumentTagValue docTagsValue = new DocumentTagValue();
                            docTagsValue.document_tag_value_id = getNewId();
                            docTagsValue.document_id = content.document_id;
                            docTagsValue.document_tag_name = data.document_tag;
                            docTagsValue.user_id = content.user_id;
                            docTagsValue.created_at = DateTime.Now;
                            docTagsValue.created_by = content.created_by;
                            docTagsValue.updated_at = DateTime.Now;
                            docTagsValue.updated_by = content.created_by;

                            lst_tag_value.Add(docTagsValue);
                        }

                        if(lst_tag_value.Count() > 0)
                        {
                            _db.DocumentTagsValue.AddRange(lst_tag_value);
                            await _db.SaveChangesAsync();

                        }

                        resp.code = 200;
                        resp.error = false;
                        resp.message = "save document tags success";

                        return resp;
                    }
                    else
                    {

                        tags.tags_json = content.tags_json;
                        tags.updated_at = DateTime.Now;
                        tags.updated_by = content.created_by;
                        _db.DocumentTags.Update(tags);
                        await _db.SaveChangesAsync();

                        var tagsDelete = _db.DocumentTagsValue
                                        .Where(e => e.document_id == content.document_id)
                                        .ToList();

                        _db.DocumentTagsValue.RemoveRange(tagsDelete);
                        await _db.SaveChangesAsync();

                        lst_tag = JsonConvert.DeserializeObject<List<DocumentTagVM>>(content.tags_json);
                        foreach (var data in lst_tag)
                        {
                            DocumentTagValue docTagsValue = new DocumentTagValue();
                            docTagsValue.document_tag_value_id = getNewId();
                            docTagsValue.document_id = content.document_id;
                            docTagsValue.document_tag_name = data.document_tag;
                            docTagsValue.user_id = content.user_id;
                            docTagsValue.created_at = DateTime.Now;
                            docTagsValue.created_by = content.created_by;
                            docTagsValue.updated_at = DateTime.Now;
                            docTagsValue.updated_by = content.created_by;

                            lst_tag_value.Add(docTagsValue);
                        }

                        if (lst_tag_value.Count() > 0)
                        {
                            _db.DocumentTagsValue.AddRange(lst_tag_value);
                            await _db.SaveChangesAsync();

                        }

                        resp.code = 200;
                        resp.error = false;
                        resp.message = "save document tags success";

                        return resp;
                    }
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }


        }

        [HttpPost("CreateDocumentVersion")]
        public async Task<ActionResult<ResponseData>> CreateDocumentVersion(UploadFileVM content)
        {
            ResponseData resp = new ResponseData();

            try
            {
                if (content == null)
                {
                    resp.code = 400;
                    resp.error = false;
                    resp.message = "Document version is null";

                    return resp;
                }
                else
                {
                    string Storage = "";
                    string file_path = "";

                    var data_dir = _db.Directories.Where(x => x.id == Convert.ToInt32(content.directory_id)).AsNoTracking().FirstOrDefault();
                    var check_doc_vers = _db.DocumentVersions.Where(x => x.document_id == content.document_id).FirstOrDefault();

                    if(check_doc_vers != null)
                    {
                        Storage = _config["AppSettings:Storage"];
                        if(data_dir != null)   {
                            file_path = Storage + data_dir.path_name + "\\" + content.document_name + content.extension;
                        }
                   
                        DocumentVersions docVers = new DocumentVersions();
                        docVers.document_id = content.document_id;
                        docVers.name = content.document_name;
                        docVers.user_id = content.created_by;
                        docVers.version_number = check_doc_vers.version_number + 1;
                        docVers.extension = content.extension;
                        docVers.content_type = content.content_type;
                        docVers.file_size = content.file_size;
                        docVers.file_path = file_path;
                        docVers.expired_date = content.expired_date;
                        docVers.created_at =DateTime.Now;
                        docVers.created_by = content.created_by;
                        _db.DocumentVersions.Add(docVers);
                        await _db.SaveChangesAsync();

                    } else {

                        Storage = _config["AppSettings:Storage"];
                        if (data_dir != null)  {
                            file_path = Storage + data_dir.path_name + "\\" + content.document_name + content.extension;
                        }

                        DocumentVersions docVers = new DocumentVersions();
                        docVers.document_id = content.document_id;
                        docVers.name = content.document_name;
                        docVers.user_id = content.created_by;
                        docVers.version_number = 1;
                        docVers.extension = content.extension;
                        docVers.content_type = content.content_type;
                        docVers.file_size = content.file_size;
                        docVers.file_path = file_path;
                        docVers.expired_date = content.expired_date;
                        docVers.created_at = DateTime.Now;
                        docVers.created_by = content.created_by;
                        _db.DocumentVersions.Add(docVers);
                        await _db.SaveChangesAsync();

                    }

                    byte[] fileByteArray = Convert.FromBase64String(content.base64file);
                    System.IO.File.WriteAllBytes(file_path, Crypto.EncryptFileSha256(fileByteArray));

                    resp.code = 200;
                    resp.error = false;
                    resp.message = "save document version success";

                    return resp;
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }

            catch (Exception ex)
            {
                resp.code = 500;
                if (ex.InnerException.Message != null)
                {
                    resp.message = ex.InnerException.Message;
                }
                else
                {
                    resp.message = ex.Message;
                }

                resp.error = true;
                resp.data = null;
                return resp;
            }


        }
    }
}
