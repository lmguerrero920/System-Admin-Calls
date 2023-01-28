using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConsultoriaSAS.Data.Context;
using ConsultoriaSAS.Entity.Entities;
using ConsultoriaSAS.Web.Resources;

namespace ConsultoriaSAS.Web.Controllers
{
    public class WorkStationsController : Controller
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: WorkStations
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.WorkStations.ToListAsync());
        }

        // GET: WorkStations/Details/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkStation workStation = await db.WorkStations.FindAsync(id);
            if (workStation == null)
            {
                return HttpNotFound();
            }
            return View(workStation);
        }

        // GET: WorkStations/Create
        [Authorize(Roles = "Adviser,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkStations/Create 
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Create( WorkStation workStation)
        {
            if (ModelState.IsValid)
            {
                db.WorkStations.Add(workStation);
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }

            return View(workStation);
        }

        // GET: WorkStations/Edit/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkStation workStation = await db.WorkStations.FindAsync(id);
            if (workStation == null)
            {
                return HttpNotFound();
            }
            return View(workStation);
        }

        // POST: WorkStations/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(  WorkStation workStation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workStation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }
            return View(workStation);
        }

        // GET: WorkStations/Delete/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkStation workStation = await db.WorkStations.FindAsync(id);
            if (workStation == null)
            {
                return HttpNotFound();
            }
            return View(workStation);
        }

        // POST: WorkStations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkStation workStation = await db.WorkStations.FindAsync(id);
            db.WorkStations.Remove(workStation);
            await db.SaveChangesAsync();
            return RedirectToAction(EnvironmentSystem.Index);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
