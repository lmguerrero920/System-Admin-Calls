using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsultoriaSAS.Web.Models;
using Newtonsoft.Json;

namespace ConsultoriaSAS.Web.Controllers
{
    public class VisualizeDataController : Controller
    {
    
        public ActionResult ColumnChart()
        {
            return View();
        }
     
        public ActionResult PieChart()
        {
            return View();
        }
 
        public ActionResult LineChart()
        {
            return View();
        }

        public ActionResult VisualizeCallsResult()
        {
            return Json(Result(), JsonRequestBehavior.AllowGet);
        }

        public List<ReportsWeek> Result()
        {
            List<ReportsWeek> stdResult = new List<ReportsWeek>();

           
            stdResult.Add(new ReportsWeek()
            {
                stdName = "Lunes",
                marksObtained = 32
            });
            stdResult.Add(new ReportsWeek()
            {
                stdName = "Martes",
                marksObtained = 21
            });
            stdResult.Add(new ReportsWeek()
            {
                stdName = "Miercoles",
                marksObtained = 14
            });
            stdResult.Add(new ReportsWeek()
            {
                stdName = "Jueves",
                marksObtained = 8
            });
            stdResult.Add(new ReportsWeek()
            {
                stdName = "Viernes",
                marksObtained = 23
            });
            return stdResult;
        }
    }
}