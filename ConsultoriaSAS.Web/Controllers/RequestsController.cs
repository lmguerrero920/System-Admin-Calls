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
    public class RequestsController : Controller
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: Requests
        public async Task<ActionResult> Index()
        {
            var requests = db.Requests.Include(r => r.Customers);
            return View(await requests.ToListAsync());
        }
        /// <summary>
        /// Metodo generado para  ver  detalles  Solicitud
        /// </summary>
        /// <returns></returns>
        // GET: Requests/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name);
            return View();
        }

        /// <summary>
        /// Metodo generado para  crear  Solicitud
        /// </summary>
        /// <returns></returns>
        /// 
        // POST: Requests/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Request request)
        {
            if (ModelState.IsValid)
            {
                db.Requests.Add(request);
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }

            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name, request.CustomerId);
            return View(request);
        }

        // GET: Requests/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name, request.CustomerId);
            return View(request);
        }

        /// <summary>
        /// Metodo generado para  editar Solicitud
        /// </summary>
        /// <returns></returns>
        // POST: Requests/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( Request request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(request).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index );
            }
            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name ,request.CustomerId);
            return View(request);
        }

        /// <summary>
        /// Metodo generado para  Quitar  Solicitud
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: Requests/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = await db.Requests.FindAsync(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Request request = await db.Requests.FindAsync(id);
            db.Requests.Remove(request);
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
