using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ConsultoriaSAS.Data.Context;
using ConsultoriaSAS.Entity.Entities;

namespace ConsultoriaSAS.API.Controllers
{
    public class RequestTypesAPIController : ApiController
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: api/RequestTypesAPI
        public IQueryable<RequestType> GetRequestTypes()
        {
            return db.RequestTypes;
        }

        // GET: api/RequestTypesAPI/5
        [ResponseType(typeof(RequestType))]
        public async Task<IHttpActionResult> GetRequestType(int id)
        {
            RequestType requestType = await db.RequestTypes.FindAsync(id);
            if (requestType == null)
            {
                return NotFound();
            }

            return Ok(requestType);
        }

        // PUT: api/RequestTypesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRequestType(int id, RequestType requestType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != requestType.RequestTypeId)
            {
                return BadRequest();
            }

            db.Entry(requestType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/RequestTypesAPI
        [ResponseType(typeof(RequestType))]
        public async Task<IHttpActionResult> PostRequestType(RequestType requestType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RequestTypes.Add(requestType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = requestType.RequestTypeId }, requestType);
        }

        // DELETE: api/RequestTypesAPI/5
        [ResponseType(typeof(RequestType))]
        public async Task<IHttpActionResult> DeleteRequestType(int id)
        {
            RequestType requestType = await db.RequestTypes.FindAsync(id);
            if (requestType == null)
            {
                return NotFound();
            }

            db.RequestTypes.Remove(requestType);
            await db.SaveChangesAsync();

            return Ok(requestType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RequestTypeExists(int id)
        {
            return db.RequestTypes.Count(e => e.RequestTypeId == id) > 0;
        }
    }
}