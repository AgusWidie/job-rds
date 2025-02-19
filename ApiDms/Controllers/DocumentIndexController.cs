using ApiDms.Models;
using ApiDms.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiDms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentIndexController : ControllerBase
    {
        private readonly DMSDbContext _db;

        public DocumentIndexController(DMSDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData>> Get(string document_type_id)
        {
            ResponseData resp = new ResponseData();
            DocumentIndexVM data = new DocumentIndexVM();

            try
            {
                var doc_types = _db.DocumentTypes.Where(x => x.document_type_id == document_type_id).AsNoTracking().FirstOrDefault();
                var doc_index = _db.DocumentIndices.Where(x => x.document_type_id == document_type_id).AsNoTracking().ToList();

                data.document_type = doc_types;
                data.document_index = doc_index;

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = data;
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

        [HttpGet("GetListDataIndex")]
        public async Task<ActionResult<ResponseListData>> GetListDataIndex(string document_type_id)
        {
            ResponseListData resp = new ResponseListData();

            try
            {
                var list_index = _db.DocumentIndices.Where(x => x.document_type_id == document_type_id).AsNoTracking().ToList();

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = list_index.Cast<object>().ToList();
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

        [HttpGet("GetDataIndex")]
        public async Task<ActionResult<ResponseListData>> GetDataIndex(string document_type_id, string document_index_id)
        {
            ResponseListData resp = new ResponseListData();

            try
            {
                var list_index = _db.DocumentIndices.Where(x => x.document_type_id == document_type_id && x.index_id == document_index_id).AsNoTracking().ToList();

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = list_index.Cast<object>().ToList();
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

        [HttpGet("GetDataIndexValue")]
        public async Task<ActionResult<ResponseListData>> GetDataIndexValue(string document_type_id)
        {
            ResponseListData resp = new ResponseListData();
            List<DocumentIndexValueVM> lst_index_value = new List<DocumentIndexValueVM>();
            try
            {
                lst_index_value = (from idx in _db.DocumentIndices join idx_value in _db.DocumentIndicesValue on idx.index_id equals idx_value.index_id
                                   where idx.document_type_id == document_type_id
                                   select new DocumentIndexValueVM
                                   {
                                        id = idx_value.id,
                                        index_id = idx_value.index_id,
                                        index_value = idx_value.index_value,

                                   }).AsNoTracking().ToList();

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = lst_index_value.Cast<object>().ToList();
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

        [HttpPost("Create")]
        public async Task<ActionResult<ResponseData>> Create(DocumentIndex content)
        {
            ResponseData resp = new ResponseData();

            try
            {
                if (content == null)
                {
                    resp.code = 400;
                    resp.error = false;
                    resp.message = "Document Index is null";

                    return resp;
                }
                else
                {
                    var user = await _db.DocumentIndices.Where(m => m.index_name == content.index_name && m.index_value == content.index_value).FirstOrDefaultAsync();
                    if (user == null)
                    {
                        DocumentIndex docIdx = new DocumentIndex();
                        docIdx.index_id = getNewId();
                        docIdx.document_type_id = content.document_type_id;
                        docIdx.index_name = content.index_name;
                        docIdx.rules = content.rules;
                        docIdx.status = content.status;
                        docIdx.created_at = DateTime.Now;
                        docIdx.created_by = content.created_by;
                        docIdx.updated_at = DateTime.Now;
                        docIdx.updated_by = content.created_by;
                        docIdx.last_updated_at = DateTime.Now;
                        _db.DocumentIndices.Add(docIdx);
                        await _db.SaveChangesAsync();

                        resp.code = 200;
                        resp.error = false;
                        resp.message = "create document index success";

                        return resp;
                    }
                    else
                    {
                        resp.code = 409;
                        resp.error = false;
                        resp.message = "document index already exist";

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

        [HttpPost("Update")]
        public async Task<ActionResult<ResponseData>> Update(DocumentIndex content)
        {
            ResponseData resp = new ResponseData();

            try
            {
                var doctIdx = await _db.DocumentIndices.Where(m => m.id == content.id).FirstOrDefaultAsync();
                if (doctIdx == null)
                {
                    resp.code = 404;
                    resp.error = false;
                    resp.message = "not found";
                    return resp;
                }
                else
                {
                    doctIdx.index_name = content.index_name;
                    doctIdx.index_value = content.index_value;
                    doctIdx.status = content.status;
                    doctIdx.last_updated_at = doctIdx.updated_at;
                    doctIdx.updated_at = DateTime.Now;
                    doctIdx.updated_by = content.created_by;
                    _db.DocumentIndices.Update(doctIdx);
                    await _db.SaveChangesAsync();

                    resp.code = 200;
                    resp.error = false;
                    resp.message = "update document index success";

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

        [HttpDelete("DeleteIndex/{id}")]
        public async Task<ActionResult<ResponseData>> DeleteIndex(int id)
        {
            ResponseData resp = new ResponseData();
            try
            {
                var document = await _db.DocumentIndices.Where(m => m.id == id).FirstOrDefaultAsync();
                if (document != null)  {
                    
                    _db.Remove(document);
                    await _db.SaveChangesAsync();

                    resp.code = 200;
                    resp.error = false;
                    resp.message = "delete document index success";

                } else {

                    resp.code = 404;
                    resp.error = false;
                    resp.message = "document index id : " + id.ToString() + " Not Found.";
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
    }
}
