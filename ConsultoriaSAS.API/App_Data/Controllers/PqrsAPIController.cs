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
    public class PqrsAPIController : ApiController
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: api/PqrsAPI
        public IQueryable<Pqr> GetPqrs()
        {
            return db.Pqrs;
        }

        // GET: api/PqrsAPI/5
        [ResponseType(typeof(Pqr))]
        public async Task<IHttpActionResult> GetPqr(int id)
        {
            Pqr pqr = await db.Pqrs.FindAsync(id);
            if (pqr == null)
            {
                return NotFound();
            }

            return Ok(pqr);
        }

        // PUT: api/PqrsAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPqr(int id, Pqr pqr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pqr.PqrId)
            {
                return BadRequest();
            }

            db.Entry(pqr).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PqrExists(id))
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

        // POST: api/PqrsAPI
        [ResponseType(typeof(Pqr))]
        public async Task<IHttpActionResult> PostPqr(Pqr pqr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pqrs.Add(pqr);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pqr.PqrId }, pqr);
        }

        // DELETE: api/PqrsAPI/5
        [ResponseType(typeof(Pqr))]
        public async Task<IHttpActionResult> DeletePqr(int id)
        {
            Pqr pqr = await db.Pqrs.FindAsync(id);
            if (pqr == null)
            {
                return NotFound();
            }

            db.Pqrs.Remove(pqr);
            await db.SaveChangesAsync();

            return Ok(pqr);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PqrExists(int id)
        {
            return db.Pqrs.Count(e => e.PqrId == id) > 0;
        }
    }
}