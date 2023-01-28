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
    public class CallsController : Controller
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        [Authorize(Roles = "Adviser,Admin")]
        // GET: Calls
        public async Task<ActionResult> Index()
        {
            var calls = db.Calls.Include(c => c.Customers);
            return View(await calls.ToListAsync());
        }
         /// <summary>
         /// Metodo generado para contar el volumen de llamadas en el dia
         /// </summary>
         /// <returns></returns>
        public int TotalCallsInDay()
        {
            var calls = db.Calls.Include(c => c.Customers);
            
            int totalCount = db.Calls.Where(x=>x.DateRegister == DateTime.Now).Count(); 
            return totalCount;
        }

        /// <summary>
        /// Metodo generado para contar el volumen de llamadas la semana
        /// </summary>
        /// <returns></returns>
        public int TotalCallsInWeek()
        {
            var calls = db.Calls.Include(c => c.Customers);
            int totalCount = db.Calls.Where(x => x.DateRegister.AddDays(-7) <= DateTime.Now).Count();
            return totalCount;
        }


        /// <summary>
        /// Metodo generado para  ver detalles de llamada
        /// </summary>
        /// <returns></returns>
        // GET: Calls/Details/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Call call = await db.Calls.FindAsync(id);
            if (call == null)
            {
                return HttpNotFound();
            }
            return View(call);
        }

        /// <summary>
        /// Metodo generado para  agregar llamada 
        /// </summary>
        /// <returns></returns>
        // GET: Calls/Create
        [Authorize(Roles = "Adviser,Admin")]
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name);
            return View();
        }

        // POST: Calls/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Create(Call call)
        {
            if (ModelState.IsValid)
            {
                db.Calls.Add(call);
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }

            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name, call.CustomerId);
            return View(call);
        }

        /// <summary>
        /// Metodo generado para editar un registro de llamada 
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: Calls/Edit/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Call call = await db.Calls.FindAsync(id);
            if (call == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name, call.CustomerId);
            return View(call);
        }

        // POST: Calls/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(Call call)
        {
            if (ModelState.IsValid)
            {
                db.Entry(call).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }
            ViewBag.CustomerId = new SelectList(db.Customers, EnvironmentSystem.CustomerId, EnvironmentSystem.Name, call.CustomerId);
            return View(call);
        }

        /// <summary>
        /// Metodo generado para quitar un registro de llamada
        /// </summary>
        /// <returns></returns>
        // GET: Calls/Delete/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Call call = await db.Calls.FindAsync(id);
            if (call == null)
            {
                return HttpNotFound();
            }
            return View(call);
        }

        // POST: Calls/Delete/5
        [Authorize(Roles = "Adviser,Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Call call = await db.Calls.FindAsync(id);
            db.Calls.Remove(call);
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
