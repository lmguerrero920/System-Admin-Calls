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
    public class CustomersController : Controller
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: Customers
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Index()
        {
            var customers = db.Customers.Include(c => c.DocumentType);
            return View(await customers.ToListAsync());
        }

        /// <summary>
        /// Metodo generado para  ver detalles de cliente
        /// </summary>
        /// <returns></returns>
        // GET: Customers/Details/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        [Authorize(Roles = "Adviser,Admin")]
        public ActionResult Create()
        {
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, EnvironmentSystem.DocumentTypeId, EnvironmentSystem.Name);
            return View();
        }

        /// <summary>
        /// Metodo generado para  agregar un cliente
        /// </summary>
        /// <returns></returns>
        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Create( Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }

            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, EnvironmentSystem.DocumentTypeId, EnvironmentSystem.Name
                , customer.DocumentTypeId);
            return View(customer);
        }

        /// <summary>
        /// Metodo generado para  editar un cliente
        /// </summary>
        /// <returns></returns>
        /// 
        // GET: Customers/Edit/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, EnvironmentSystem.DocumentTypeId, EnvironmentSystem.Name
                ,customer.DocumentTypeId);
            return View(customer);
        }

        // POST: Customers/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, EnvironmentSystem.DocumentTypeId, EnvironmentSystem.Name, customer.DocumentTypeId);
            return View(customer);
        }

        /// <summary>
        /// Metodo generado para quitar un cliente  
        /// </summary>
        /// <returns></returns>
        // GET: Customers/Delete/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
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
