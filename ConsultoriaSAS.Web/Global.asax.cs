using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ConsultoriaSAS.Data.Migrations;
using ConsultoriaSAS.Web.Models;
using ConsultoriaSAS.Web.Resources;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConsultoriaSAS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        ConstantsValue value = new ConstantsValue();
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Data.Context.ConsultoriaContext,
               Data.Migrations.Configuration>());


            ApplicationDbContext db = new ApplicationDbContext(); 
            CreateRoles(db);
            CreateSuperUser(db);
            AddPermisionsToSuperUser(db);
            db.Dispose();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        /// <summary>
        /// Metodo generado para Agregar permisos de SA
        /// </summary>
        /// <param name="db"></param>
        private void AddPermisionsToSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            var user = userManager.FindByName(value.SecretAdmin);

            try
            {
                if (!userManager.IsInRole(user.Id, EnvironmentSystem.RoleView))
                {
                    userManager.AddToRole(user.Id, EnvironmentSystem.RoleView);
                }
                if (!userManager.IsInRole(user.Id, EnvironmentSystem.RoleEdit))
                {
                    userManager.AddToRole(user.Id, EnvironmentSystem.RoleEdit);
                }
                if (!userManager.IsInRole(user.Id, EnvironmentSystem.RoleCreate))
                {
                    userManager.AddToRole(user.Id, EnvironmentSystem.RoleCreate);
                }
                if (!userManager.IsInRole(user.Id, EnvironmentSystem.RoleDelete))
                {
                    userManager.AddToRole(user.Id, EnvironmentSystem.RoleDelete);
                }
                if (!userManager.IsInRole(user.Id, EnvironmentSystem.Admin))
                {
                    userManager.AddToRole(user.Id, EnvironmentSystem.Admin);
                }
                if (!userManager.IsInRole(user.Id, EnvironmentSystem.Adviser))
                {
                    userManager.AddToRole(user.Id, EnvironmentSystem.Adviser);
                }
                if (!userManager.IsInRole(user.Id, EnvironmentSystem.User))
                {
                    userManager.AddToRole(user.Id, EnvironmentSystem.User);
                }
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }




        }
        /// <summary>
        /// Metodo generado para Crear SA
        /// </summary>
        /// <param name="db"></param>
        private void CreateSuperUser(ApplicationDbContext db)
        {
            try
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));


                var user = userManager.FindByName(value.SecretAdmin);
                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = value.SecretAdmin,
                        Email = value.SecretAdmin
                    };
                    userManager.Create(user, value.SecretValue);
                }
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }

        }
        /// <summary>
        /// Metodo generado para crear Roles
        /// </summary>
        /// <param name="db"></param>
        private void CreateRoles(ApplicationDbContext db)
        {
            try
            {
                var rolemanager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                if (!rolemanager.RoleExists(EnvironmentSystem.RoleView))
                {
                    rolemanager.Create(new IdentityRole(EnvironmentSystem.RoleView));
                }

                if (!rolemanager.RoleExists(EnvironmentSystem.RoleEdit))
                {
                    rolemanager.Create(new IdentityRole(EnvironmentSystem.RoleEdit));
                }

                if (!rolemanager.RoleExists(EnvironmentSystem.RoleCreate))
                {
                    rolemanager.Create(new IdentityRole(EnvironmentSystem.RoleCreate));
                }

                if (!rolemanager.RoleExists(EnvironmentSystem.RoleDelete))
                {
                    rolemanager.Create(new IdentityRole(EnvironmentSystem.RoleDelete));
                }

                if (!rolemanager.RoleExists(EnvironmentSystem.Admin))
                {
                    rolemanager.Create(new IdentityRole(EnvironmentSystem.Admin));
                }

                if (!rolemanager.RoleExists(EnvironmentSystem.Adviser))
                {
                    rolemanager.Create(new IdentityRole(EnvironmentSystem.Adviser));
                }

                if (!rolemanager.RoleExists(EnvironmentSystem.User))
                {
                    rolemanager.Create(new IdentityRole(EnvironmentSystem.User));
                }
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }




        }




    }
}
