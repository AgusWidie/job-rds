using ApiDms.Models;
using ApiDms.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiDms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionsController : ControllerBase
    {
        private readonly DMSDbContext _db;
        private IConfiguration _config;
        public CollectionsController(DMSDbContext context, IConfiguration config)
        {
            _db = context;
            _config = config;
        }

        // GET: api/<CollectionsController>
        [HttpGet]
        public async Task<ActionResult<ResponseListData>> Get()
        {
            ResponseListData resp = new ResponseListData();
            List<Collection> lst_collection =  new List<Collection>();
            try
            {
                lst_collection = _db.Collections.AsNoTracking().ToList();

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = lst_collection.Cast<object>().ToList();

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

        // GET api/<CollectionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseData>> Get(int id)
        {
            ResponseData resp = new ResponseData();

            var collection = await _db.Collections.Where(m => m.id == id).FirstOrDefaultAsync();
            if (collection == null)
            {
                return NotFound();
            }

            resp.code = 200;
            resp.error = false;
            resp.message = "success";
            resp.data = collection;

            return resp;
        }

        // POST api/<CollectionsController>
        [HttpPost("Create")]
        public async Task<ActionResult<ResponseData>> Create(Collection content)
        {
            ResponseData resp = new ResponseData();
            using (var dbTrans = _db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    if (content == null)
                    {
                        resp.code = 400;
                        resp.error = false;
                        resp.message = "Collection is null";

                        return resp;
                    }
                    else
                    {
                        var collection = await _db.Collections.Where(m => m.collection_name.ToUpper() == content.collection_name.ToUpper()).FirstOrDefaultAsync();
                        if (collection == null)
                        {
                            string myuuidAsString = getNewId();

                            Collection col = new Collection();
                            col.collection_id = myuuidAsString;
                            col.collection_name = content.collection_name.ToUpper();
                            col.description = content.description;
                            col.status = content.status;
                            col.created_at = DateTime.Now;
                            col.created_by = content.created_by;
                            col.updated_at = DateTime.Now;
                            col.updated_by = content.created_by;
                            col.last_updated_at = DateTime.Now;
                            _db.Collections.Add(col);
                            await _db.SaveChangesAsync();

                            ApiDms.Models.Directory dir = new ApiDms.Models.Directory();
                            dir.directory_id = getNewId();
                            dir.directory_name = content.collection_name.ToUpper();
                            dir.disk = content.collection_name.ToUpper();
                            dir.path_name = content.collection_name.ToUpper();
                            dir.parent_id = 0;
                            dir.collection_id = myuuidAsString;
                            dir.owner_id = content.created_by;
                            dir.status = 1;
                            dir.created_at = DateTime.Now;
                            dir.created_by = content.created_by;
                            dir.updated_at = null;
                            dir.updated_by = null;
                            _db.Directories.Add(dir);
                            await _db.SaveChangesAsync();

                            try
                            {
                                string Storage = _config["AppSettings:Storage"];
                                var basePath = Path.Combine(Storage + content.collection_name.ToUpper());
                                bool basePathExists = System.IO.Directory.Exists(basePath);
                                if (!basePathExists) System.IO.Directory.CreateDirectory(basePath);

                            }
                            catch (Exception ex)
                            {
                                dbTrans.Rollback();
                                dbTrans.Dispose();

                                resp.code = 500;
                                resp.error = true;
                                resp.message = "error create folder collection create : " + ex.Message;

                                return resp;
                            }


                            dbTrans.Commit();
                            dbTrans.Dispose();

                            resp.code = 200;
                            resp.error = false;
                            resp.message = "create collection success";

                            return resp;
                        }
                        else
                        {
                            dbTrans.Commit();
                            dbTrans.Dispose();

                            resp.code = 409;
                            resp.error = false;
                            resp.message = "collection already exist";

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

            
        }

        
        [HttpPost("Update")]
        public async Task<ActionResult<ResponseData>> Update(Collection content)
        {
            string original_folder_name = "";
            string new_folder_name = "";

            ResponseData resp = new ResponseData();
            using (var dbTrans = _db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
            {
                try
                {
                    var collection = await _db.Collections.Where(m => m.id == content.id).FirstOrDefaultAsync();
                    if (collection == null)
                    {
                        resp.code = 404;
                        resp.error = false;
                        resp.message = "not found";
                        return resp;
                    }
                    else
                    {
                        original_folder_name = collection.collection_name;
                        new_folder_name = content.collection_name;

                        collection.id = content.id;
                        collection.collection_name = content.collection_name;
                        collection.description = content.description;
                        collection.status = content.status;
                        collection.last_updated_at = collection.updated_at;
                        collection.updated_at = DateTime.Now;
                        collection.updated_by = content.created_by;
                        _db.Update(collection);
                        await _db.SaveChangesAsync();

                        var dir = await _db.Directories.Where(m => m.collection_id == content.collection_id).FirstOrDefaultAsync();
                        if (dir != null)
                        {
                            dir.directory_name = content.collection_name.ToUpper();
                            dir.disk = content.collection_name.ToUpper();
                            dir.path_name = content.collection_name.ToUpper();
                            dir.last_updated_at = collection.updated_at;
                            dir.updated_at = DateTime.Now;
                            dir.updated_by = content.created_by;
                            _db.Update(dir);
                            await _db.SaveChangesAsync();
                        }

                        if(original_folder_name == new_folder_name)
                        {
                            resp.code = 400;
                            resp.error = false;
                            resp.message = "collection name must different.";

                            return resp;
                        }

                        var lst_doc = _db.Documents.Where(x => x.collection_id == collection.id.ToString()).ToList();
                        lst_doc.ForEach(c => { c.file_path = c.file_path.Replace(original_folder_name, new_folder_name); c.last_updated_at = c.updated_at; c.updated_at = DateTime.Now; });
                        _db.UpdateRange(lst_doc);
                        await _db.SaveChangesAsync();

                        string Storage = _config["AppSettings:Storage"];
                        System.IO.Directory.Move(Storage + "/" + original_folder_name, Storage + "/" + new_folder_name);

                        dbTrans.Commit();
                        dbTrans.Dispose();

                        resp.code = 200;
                        resp.error = false;
                        resp.message = "success";

                        return resp;
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

        private string getNewId()
        {
            var ticks = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).Ticks;
            var ans = DateTime.Now.Ticks - ticks;
            var myUniqueFileName = ans.ToString("x").ToLower();
            return myUniqueFileName;
        }
    }
}
