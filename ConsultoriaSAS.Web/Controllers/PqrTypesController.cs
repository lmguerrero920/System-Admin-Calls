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
    public class PqrTypesController : Controller
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: PqrTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.PqrTypes.ToListAsync());
        }

        /// <summary>
        /// Metodo generado para  ver  detalles  Tipo PQR
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: PqrTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PqrType pqrType = await db.PqrTypes.FindAsync(id);
            if (pqrType == null)
            {
                return HttpNotFound();
            }
            return View(pqrType);
        }

        /// <summary>
        /// Metodo generado para  crear   Tipo PQR
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: PqrTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PqrTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PqrType pqrType)
        {
            if (ModelState.IsValid)
            {
                db.PqrTypes.Add(pqrType);
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }

            return View(pqrType);
        }

        // GET: PqrTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PqrType pqrType = await db.PqrTypes.FindAsync(id);
            if (pqrType == null)
            {
                return HttpNotFound();
            }
            return View(pqrType);
        }

        /// <summary>
        /// Metodo generado para  modificar  Tipo PQR
        /// </summary>
        /// <returns></returns>
        // POST: PqrTypes/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( PqrType pqrType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pqrType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }
            return View(pqrType);
        }

        /// <summary>
        /// Metodo generado para  quitar    Tipo PQR
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: PqrTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PqrType pqrType = await db.PqrTypes.FindAsync(id);
            if (pqrType == null)
            {
                return HttpNotFound();
            }
            return View(pqrType);
        }

        // POST: PqrTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PqrType pqrType = await db.PqrTypes.FindAsync(id);
            db.PqrTypes.Remove(pqrType);
            await db.SaveChangesAsync();
            return RedirectToAction(EnvironmentSystem.Index);
        }

        /// <summary>
        /// Metodo generado para cerrar conexión
        /// </summary>
        /// <returns></returns>
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
