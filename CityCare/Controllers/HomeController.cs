using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CityCare.Models;
using FORCOUtils;
using FORCOUtils.DALUtils;

namespace CityCare.Controllers
{
    public class HomeController : Controller
    {
        IGenericRepository<CCUser>  fUserRepository = new EntityFrameworkDataRepository<CCUser>(new CityCareEntities());
        IGenericRepository<Report> fReportRepository = new EntityFrameworkDataRepository<Report>(new CityCareEntities());


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(CCUser aUser)
        {
            return RedirectToAction("SignIn");
        }

        public ActionResult SignIn()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SignIn(CCUser aUser)
        {
            return RedirectToAction("Index");
        }
        
        
    }
}