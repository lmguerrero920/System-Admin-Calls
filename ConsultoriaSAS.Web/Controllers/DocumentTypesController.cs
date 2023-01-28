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
    public class DocumentTypesController : Controller
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: DocumentTypes
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.DocumentTypes.ToListAsync());
        }

        /// <summary>
        /// Metodo generado para  ver detalle del tipo de documento 
        /// </summary>
        /// <returns></returns>
        // GET: DocumentTypes/Details/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documentType = await db.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return HttpNotFound();
            }
            return View(documentType);
        }

        /// <summary>
        /// Metodo generado para  crear detalle del tipo de documento 
        /// </summary>
        /// <returns></returns>
        // GET: DocumentTypes/Create
        [Authorize(Roles = "Adviser,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DocumentTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Create(DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                db.DocumentTypes.Add(documentType);
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }

            return View(documentType);
        }

        /// <summary>
        /// Metodo generado para  modificar detalle del tipo de documento 
        /// </summary>
        /// <returns></returns>
        // GET: DocumentTypes/Edit/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documentType = await db.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return HttpNotFound();
            }
            return View(documentType);
        }


        // POST: DocumentTypes/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit( DocumentType documentType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }
            return View(documentType);
        }

        // GET: DocumentTypes/Delete/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentType documentType = await db.DocumentTypes.FindAsync(id);
            if (documentType == null)
            {
                return HttpNotFound();
            }
            return View(documentType);
        }

        /// <summary>
        /// Metodo generado para  quitar  del tipo de documento 
        /// </summary>
        /// <returns></returns>
        // POST: DocumentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DocumentType documentType = await db.DocumentTypes.FindAsync(id);
            db.DocumentTypes.Remove(documentType);
            await db.SaveChangesAsync();
            return RedirectToAction(EnvironmentSystem.Index);
        }
        /// <summary>
        /// Metodo generado para  cerrar conexión
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
