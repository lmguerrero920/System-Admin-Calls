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
    public class WorkStationsAPIController : ApiController
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: api/WorkStationsAPI
        public IQueryable<WorkStation> GetWorkStations()
        {
            return db.WorkStations;
        }

        // GET: api/WorkStationsAPI/5
        [ResponseType(typeof(WorkStation))]
        public async Task<IHttpActionResult> GetWorkStation(int id)
        {
            WorkStation workStation = await db.WorkStations.FindAsync(id);
            if (workStation == null)
            {
                return NotFound();
            }

            return Ok(workStation);
        }

        // PUT: api/WorkStationsAPI/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWorkStation(int id, WorkStation workStation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workStation.WorkStationId)
            {
                return BadRequest();
            }

            db.Entry(workStation).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkStationExists(id))
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

        // POST: api/WorkStationsAPI
        [ResponseType(typeof(WorkStation))]
        public async Task<IHttpActionResult> PostWorkStation(WorkStation workStation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.WorkStations.Add(workStation);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = workStation.WorkStationId }, workStation);
        }

        // DELETE: api/WorkStationsAPI/5
        [ResponseType(typeof(WorkStation))]
        public async Task<IHttpActionResult> DeleteWorkStation(int id)
        {
            WorkStation workStation = await db.WorkStations.FindAsync(id);
            if (workStation == null)
            {
                return NotFound();
            }

            db.WorkStations.Remove(workStation);
            await db.SaveChangesAsync();

            return Ok(workStation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkStationExists(int id)
        {
            return db.WorkStations.Count(e => e.WorkStationId == id) > 0;
        }
    }
}