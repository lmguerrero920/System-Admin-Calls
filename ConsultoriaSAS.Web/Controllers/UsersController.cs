using ConsultoriaSAS.Web.Models;
using ConsultoriaSAS.Web.ModelView;
using ConsultoriaSAS.Web.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConsultoriaSAS.Web.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users
        public ActionResult Index()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var usersView = new List<UserView>();
            foreach (var user in users)
            {
                var userView = new UserView
                {
                    Email = user.Email,
                    Name = user.UserName,
                    UserId = user.Id
                };
                usersView.Add(userView);
            }

            return View(usersView);
        }

        /// <summary>
        /// Metodo encargado de generar los privilegios de  usuario
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns>userView</returns>
        public ActionResult Roles(string userID)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var users = userManager.Users.ToList();
            var user = users.Find(x => x.Id == userID);

            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var roles = rolemanager.Roles.ToList();

            var rolesView = new List<RoleView>();

            if (user.Roles != null)
            {
                foreach (var item in user.Roles)
                {
                    var role = roles.Find(x => x.Id == item.RoleId);


                    var roleView = new RoleView
                    {
                        RoleId = role.Id,
                        Name = role.Name
                    };
                    rolesView.Add(roleView);
                }

            }


            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id,
                Roles = rolesView  
            };
            return View(userView);
        }


        public ActionResult AddRole(string userID)
        {
            if (string.IsNullOrEmpty(userID))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var users = userManager.Users.ToList();
            var user = users.Find(x => x.Id == userID);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id
            };
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
        
            var list = rolemanager.Roles.ToList();
            list.Add(new IdentityRole { Id = "", Name = EnvironmentSystem.SeleccionarRol });
            list = list.OrderBy(x => x.Name).ToList();
            ViewBag.RoleID = new SelectList(list, EnvironmentSystem.Id, EnvironmentSystem.Name);

            return View(userView);
        }


        /// <summary>
        /// Metodo encargado de agregar  privilegios a un usuario
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns>userView</returns>
        [HttpPost]
        public ActionResult AddRole(string userID, FormCollection form)
        {
            var roleID = Request[EnvironmentSystem.RoleID];
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var users = userManager.Users.ToList();
            var user = users.Find(x => x.Id == userID);
            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                UserId = user.Id,
            };

            if (string.IsNullOrEmpty(roleID))
            {
                ViewBag.Error = EnvironmentSystem.AggRol; 

                var list = rolemanager.Roles.ToList();
                list.Add(new IdentityRole { Id = "", Name = EnvironmentSystem.SeleccionarRol });
                list = list.OrderBy(x => x.Name).ToList(); 
                ViewBag.RoleID = new SelectList(list, EnvironmentSystem.Id, EnvironmentSystem.Name);
                return View(userView);
            }

            var roles = rolemanager.Roles.ToList();

            var role = roles
               .Find(x => x.Id == roleID);


            if (!userManager.IsInRole(user.Id, role.Name))
            {
                userManager.AddToRole(userID, role.Name);


            }
            var rolesView = new List<RoleView>();


            foreach (var item in user.Roles)
            {
                role = roles.Find(x => x.Id == item.RoleId);


                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }
            userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                Roles = rolesView,
                UserId = user.Id,
            };

            return View(EnvironmentSystem.Roles, userView);


        }


        /// <summary>
        /// Metodo encargado de quitar privilegios a un usuario
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleID"></param>
        /// <returns>userView</returns>
        public ActionResult Delete(string userID, string roleID)
        {
            if (string.IsNullOrEmpty(userID) || string.IsNullOrEmpty(roleID))
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.Users.ToList().Find(x => x.Id == userID);
            var role = rolemanager.Roles.ToList().Find(x => x.Id == roleID);

            if (userManager.IsInRole(user.Id, role.Name))
            {
                userManager.RemoveFromRole(user.Id, role.Name);
            }

            var users = userManager.Users.ToList();
            var roles = rolemanager.Roles.ToList();
            var rolesView = new List<RoleView>();


            foreach (var item in user.Roles)
            {
                role = roles.Find(x => x.Id == item.RoleId);


                var roleView = new RoleView
                {
                    RoleId = role.Id,
                    Name = role.Name
                };
                rolesView.Add(roleView);
            }

            var userView = new UserView
            {
                Email = user.Email,
                Name = user.UserName,
                Roles = rolesView,
                UserId = user.Id
            };
            return View(EnvironmentSystem.Roles, userView);

        }
        /// <summary>
        /// Metodo que cierra la conexión
        /// </summary>
        /// <param name="disposing"></param>
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