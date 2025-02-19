using ApiDms.Models;
using ApiDms.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiDms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentTypesController : ControllerBase
    {
        private readonly DMSDbContext _db;

        public DocumentTypesController(DMSDbContext context)
        {
            _db = context;
        }

        // GET: api/<DocumentTypesController>
        [HttpGet]
        public async Task<ActionResult<ResponseListData>> Get()
        {
            ResponseListData resp = new ResponseListData();
            List<DocumentType> lst_doc_type = new List<DocumentType>();
            try
            {
                lst_doc_type = _db.DocumentTypes.AsNoTracking().ToList();

                resp.code = 200;
                resp.error = false;
                resp.message = "success";
                resp.data = lst_doc_type.Cast<object>().ToList();

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

        // GET api/<DocumentTypesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseData>> Get(int id)
        {
            ResponseData resp = new ResponseData();

            var documentType = await _db.DocumentTypes.Where(m => m.id == id).FirstOrDefaultAsync();
            if (documentType == null)
            {
                return NotFound();
            }

            resp.code = 200;
            resp.error = false;
            resp.message = "success";
            resp.data = documentType;

            return resp;
        }

        // POST api/<DocumentTypesController>
        [HttpPost("Create")]
        public async Task<ActionResult<ResponseData>> Create(DocumentType content)
        {
            ResponseData resp = new ResponseData();

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
                    var user = await _db.DocumentTypes.Where(m => m.document_type_id == content.document_type_id).FirstOrDefaultAsync();
                    if (user == null)
                    {
                        DocumentType doctype = new DocumentType();
                        doctype.id = content.id;
                        doctype.document_type_id = getNewId();
                        doctype.document_type_name = content.document_type_name;
                        doctype.description = content.description;
                        doctype.status = content.status;
                        doctype.created_at = DateTime.Now;
                        doctype.created_by = content.created_by;
                        doctype.updated_at = DateTime.Now;
                        doctype.updated_by = content.created_by;
                        doctype.last_updated_at = DateTime.Now;
                        _db.DocumentTypes.Add(doctype);
                        await _db.SaveChangesAsync();

                        resp.code = 200;
                        resp.error = false;
                        resp.message = "create document types success";

                        return resp;
                    }
                    else
                    {
                        resp.code = 409;
                        resp.error = false;
                        resp.message = "document types already exist";

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

        // PUT api/<DocumentTypesController>/5
        [HttpPost("Update")]
        public async Task<ActionResult<ResponseData>> Update(DocumentType content)
        {
            ResponseData resp = new ResponseData();

            try
            {
                var doctype = await _db.DocumentTypes.Where(m => m.id == content.id && m.document_type_id == content.document_type_id).FirstOrDefaultAsync();
                if (doctype == null)
                {
                    resp.code = 404;
                    resp.error = false;
                    resp.message = "not found";
                    return resp;
                }
                else
                {
                    doctype.document_type_id = content.document_type_id;
                    doctype.document_type_name = content.document_type_name;
                    doctype.description = content.description;
                    doctype.status = content.status;
                    doctype.last_updated_at = doctype.updated_at;
                    doctype.updated_at = DateTime.Now;
                    doctype.updated_by = content.created_by;
                    _db.DocumentTypes.Update(doctype);
                    await _db.SaveChangesAsync();

                    resp.code = 200;
                    resp.error = false;
                    resp.message = "update document type success";

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

        // DELETE api/<DocumentTypesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

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
