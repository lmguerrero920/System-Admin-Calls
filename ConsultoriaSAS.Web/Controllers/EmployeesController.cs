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
using Microsoft.AspNet.Identity.Owin;
using ConsultoriaSAS.Web.Models; 
using ConsultoriaSAS.Web.Resources;

namespace ConsultoriaSAS.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private ConsultoriaContext db = new ConsultoriaContext();

        // GET: Employees
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Index()
        {
            var employees = db.Employees.Include(e => e.DocumentType).Include(e => e.WorkStation);
            return View(await employees.ToListAsync());
        }

        // GET: Employees/Details/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        /// <summary>
        /// Metodo generado para  agregar  empleado
        /// </summary>
        /// <returns></returns>
        // GET: Employees/Create
        [Authorize(Roles = "Adviser,Admin")]
        public ActionResult Create()
        {
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, EnvironmentSystem.DocumentTypeId, EnvironmentSystem.Name);
            ViewBag.WorkStationId = new SelectList(db.WorkStations, EnvironmentSystem.WorkStationId, EnvironmentSystem.Name);
            return View();
        }
        
        // POST: Employees/Create 
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Create( Employee model)
        {
            if (ModelState.IsValid)
            {
                //Inyeccion a tabla AspNetUser con cifrado
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);

                string msj = result.Errors.FirstOrDefault();
                //"El nombre"
                if (result.Succeeded == false && msj.StartsWith(EnvironmentSystem.Nombre))
                {
                    ViewBag.ErrorExist = EnvironmentSystem.UserExists;
                }
                if (result.Succeeded)
                {

                    db.Employees.Add(model);
                    await db.SaveChangesAsync();
                    return RedirectToAction(EnvironmentSystem.Index);
                } 
            }

            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, EnvironmentSystem.DocumentTypeId, EnvironmentSystem.Name, model.DocumentTypeId);
            ViewBag.WorkStationId = new SelectList(db.WorkStations, EnvironmentSystem.WorkStationId, EnvironmentSystem.Name, model.WorkStationId);
            return View(model);
        }

        /// <summary>
        /// Metodo generado para  modificar  empleado
        /// </summary>
        /// <returns></returns>
        // GET: Employees/Create
        // GET: Employees/Edit/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, EnvironmentSystem.DocumentTypeId, EnvironmentSystem.Name, employee.DocumentTypeId);
            ViewBag.WorkStationId = new SelectList(db.WorkStations, EnvironmentSystem.WorkStationId, EnvironmentSystem.Name, employee.WorkStationId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(EnvironmentSystem.Index);
            }
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, EnvironmentSystem.DocumentTypeId, EnvironmentSystem.Name, employee.DocumentTypeId);
            ViewBag.WorkStationId = new SelectList(db.WorkStations, EnvironmentSystem.DocumentTypeId, EnvironmentSystem.Name, employee.WorkStationId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        /// <summary>
        /// Metodo generado para  quitar  empleado
        /// </summary>
        /// <returns></returns>
        // GET: Employees/Create
        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Adviser,Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
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

        #region Auxiliar
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

    }
}
