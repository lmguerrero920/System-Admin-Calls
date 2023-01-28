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
    public class RequestTypesController : Controller
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: RequestTypes
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Index()
        {
            return View(await db.RequestTypes.ToListAsync());
        }

        // GET: RequestTypes/Details/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestType requestType = await db.RequestTypes.FindAsync(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            return View(requestType);
        }

        // GET: RequestTypes/Create
        [Authorize(Roles = "Adviser,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RequestTypes/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Create( RequestType requestType)
        {
            if (ModelState.IsValid)
            {
                db.RequestTypes.Add(requestType);
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }

            return View(requestType);
        }

        // GET: RequestTypes/Edit/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestType requestType = await db.RequestTypes.FindAsync(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            return View(requestType);
        }

        // POST: RequestTypes/Edit/5 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit( RequestType requestType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }
            return View(requestType);
        }

        // GET: RequestTypes/Delete/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestType requestType = await db.RequestTypes.FindAsync(id);
            if (requestType == null)
            {
                return HttpNotFound();
            }
            return View(requestType);
        }

        // POST: RequestTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RequestType requestType = await db.RequestTypes.FindAsync(id);
            db.RequestTypes.Remove(requestType);
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
