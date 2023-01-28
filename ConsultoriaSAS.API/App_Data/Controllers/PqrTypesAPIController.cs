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
    public class PqrTypesAPIController : ApiController
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: api/PqrTypesAPI
        public IQueryable<PqrType> GetPqrTypes()
        {
            return db.PqrTypes;
        }

        // GET: api/PqrTypesAPI/5
        [ResponseType(typeof(PqrType))]
        public async Task<IHttpActionResult> GetPqrType(int id)
        {
            PqrType pqrType = await db.PqrTypes.FindAsync(id);
            if (pqrType == null)
            {
                return NotFound();
            }

            return Ok(pqrType);
        }

        // PUT: api/PqrTypesAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPqrType(int id, PqrType pqrType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pqrType.PqrTypeId)
            {
                return BadRequest();
            }

            db.Entry(pqrType).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PqrTypeExists(id))
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

        // POST: api/PqrTypesAPI
        [ResponseType(typeof(PqrType))]
        public async Task<IHttpActionResult> PostPqrType(PqrType pqrType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PqrTypes.Add(pqrType);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pqrType.PqrTypeId }, pqrType);
        }

        // DELETE: api/PqrTypesAPI/5
        [ResponseType(typeof(PqrType))]
        public async Task<IHttpActionResult> DeletePqrType(int id)
        {
            PqrType pqrType = await db.PqrTypes.FindAsync(id);
            if (pqrType == null)
            {
                return NotFound();
            }

            db.PqrTypes.Remove(pqrType);
            await db.SaveChangesAsync();

            return Ok(pqrType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PqrTypeExists(int id)
        {
            return db.PqrTypes.Count(e => e.PqrTypeId == id) > 0;
        }
    }
}