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
    public class CallsAPIController : ApiController
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: api/CallsAPI
        public IQueryable<Call> GetCalls()
        {
            return db.Calls;
        }

        // GET: api/CallsAPI/5
        [ResponseType(typeof(Call))]
        public async Task<IHttpActionResult> GetCall(int id)
        {
            Call call = await db.Calls.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            return Ok(call);
        }

        // PUT: api/CallsAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCall(int id, Call call)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != call.CallId)
            {
                return BadRequest();
            }

            db.Entry(call).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CallExists(id))
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

        // POST: api/CallsAPI
        [ResponseType(typeof(Call))]
        public async Task<IHttpActionResult> PostCall(Call call)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Calls.Add(call);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = call.CallId }, call);
        }

        // DELETE: api/CallsAPI/5
        [ResponseType(typeof(Call))]
        public async Task<IHttpActionResult> DeleteCall(int id)
        {
            Call call = await db.Calls.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }

            db.Calls.Remove(call);
            await db.SaveChangesAsync();

            return Ok(call);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CallExists(int id)
        {
            return db.Calls.Count(e => e.CallId == id) > 0;
        }
    }
}