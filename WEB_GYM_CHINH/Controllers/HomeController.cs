using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_GYM_CHINH.Models;

namespace WEB_GYM_CHINH.Controllers
{
    public class HomeController : Controller
    {
        DBWebGymEntities db = new DBWebGymEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult TrangChu()
        {
            return View();
        }

        public ActionResult VeChungToi()
        {
            return View();
        }

        public ActionResult DkyGoiTap()
        {
            var goiTapList = db.GOITAPs.ToList();
            return View(goiTapList);
        }

        public ActionResult HTPhongTap()
        {
            return View();
        }

        public ActionResult LienHe()
        {
            return View();
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