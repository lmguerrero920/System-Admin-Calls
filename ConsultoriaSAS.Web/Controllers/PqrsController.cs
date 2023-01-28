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
    public class PqrsController : Controller
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: Pqrs
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Index()
        {
            var pqrs = db.Pqrs.Include(p => p.Customers).Include(p => p.PqrTypes);
            return View(await pqrs.ToListAsync());
        }

        /// <summary>
        /// Metodo generado para  ver  detalles  PQR
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: Pqrs/Details/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pqr pqr = await db.Pqrs.FindAsync(id);
            if (pqr == null)
            {
                return HttpNotFound();
            }
            return View(pqr);
        }

        // GET: Pqrs/Create
        [Authorize(Roles = "Adviser,Admin")]
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name);
            ViewBag.PqrTypeId = new SelectList(db.PqrTypes, EnvironmentSystem.PqrTypeId, EnvironmentSystem.Name);
            return View();
        }

        /// <summary>
        /// Metodo generado para  agregar  PQR
        /// </summary>
        /// <returns></returns>
        // POST: Pqrs/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Create(Pqr pqr)
        {
            if (ModelState.IsValid)
            {
                db.Pqrs.Add(pqr);
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }
            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name);
            ViewBag.PqrTypeId = new SelectList(db.PqrTypes, EnvironmentSystem.PqrTypeId, EnvironmentSystem.Name);
            return View(pqr);
        }

        // GET: Pqrs/Edit/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pqr pqr = await db.Pqrs.FindAsync(id);
            if (pqr == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name);
            ViewBag.PqrTypeId = new SelectList(db.PqrTypes, EnvironmentSystem.PqrTypeId, EnvironmentSystem.Name);
            return View(pqr);
        }

        /// <summary>
        /// Metodo generado para  editar  detalles  PQR
        /// </summary>
        /// <returns></returns>
        // POST: Pqrs/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(Pqr pqr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pqr).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }
            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name);
            ViewBag.PqrTypeId = new SelectList(db.PqrTypes, EnvironmentSystem.PqrTypeId, EnvironmentSystem.Name);
            return View(pqr);
        }

        // GET: Pqrs/Delete/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pqr pqr = await db.Pqrs.FindAsync(id);
            if (pqr == null)
            {
                return HttpNotFound();
            }
            return View(pqr);
        }

        /// <summary>
        /// Metodo generado para  quitar  detalles  PQR
        /// </summary>
        /// <returns></returns>
        // POST: Pqrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pqr pqr = await db.Pqrs.FindAsync(id);
            db.Pqrs.Remove(pqr);
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
