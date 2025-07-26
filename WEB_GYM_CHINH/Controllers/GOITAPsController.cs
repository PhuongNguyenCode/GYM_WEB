using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEB_GYM_CHINH.Models;
using System.IO;

namespace WEB_GYM_CHINH.Controllers
{
    public class GOITAPsController : Controller
    {
        DBWebGymEntities db = new DBWebGymEntities();

        // GET: GOITAPs
        public ActionResult Index()
        {
            var gOITAPs = db.GOITAPs.Include(g => g.LOAIGOITAP);
            return View(gOITAPs.ToList());
        }

        // GET: GOITAPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOITAP gOITAP = db.GOITAPs.Find(id);
            if (gOITAP == null)
            {
                return HttpNotFound();
            }
            return View(gOITAP);
        }
        [HttpGet]
        // GET: GOITAPs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiGoi = new SelectList(db.LOAIGOITAPs, "MaLoaiGoi", "TenLoaiGoi");
            return View();
        }

        // POST: GOITAPs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGoiTap,TenGoiTap,ImageGoi,MoTa,MaLoaiGoi,GiaTien")] GOITAP gOITAP,
            HttpPostedFileBase ImageGoi)
        {
            if (ModelState.IsValid)
            {
                if (ImageGoi != null)
                {
                    var fileName = Path.GetFileName(ImageGoi.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    gOITAP.ImageGoi = fileName;
                    ImageGoi.SaveAs(path);
                }
                db.GOITAPs.Add(gOITAP);
                db.SaveChanges();
                return RedirectToAction("Index", "GOITAPs"); // Chuyển hướng về trang chính
            }
            ViewBag.MaLoaiGoi = new SelectList(db.LOAIGOITAPs, "MaLoaiGoi", "TenLoaiGoi", gOITAP.MaLoaiGoi);
            return View(gOITAP);
        }

        // GET: GOITAPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOITAP gOITAP = db.GOITAPs.Find(id);
            if (gOITAP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiGoi = new SelectList(db.LOAIGOITAPs, "MaLoaiGoi", "TenLoaiGoi", gOITAP.MaLoaiGoi);
            return View(gOITAP);
        }

        // POST: GOITAPs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGoiTap,TenGoiTap,ImageGoi,MoTa,MaLoaiGoi,GiaTien")] GOITAP gOITAP, HttpPostedFileBase ImageGoi)
        {
            if (ModelState.IsValid)
            {
                var goiTapDb = db.GOITAPs.FirstOrDefault(g => g.MaGoiTap == gOITAP.MaGoiTap);
                if (goiTapDb == null)
                {
                    return HttpNotFound();
                }

                goiTapDb.TenGoiTap = gOITAP.TenGoiTap;
                goiTapDb.MoTa = gOITAP.MoTa;
                goiTapDb.GiaTien = gOITAP.GiaTien;
                goiTapDb.MaLoaiGoi = gOITAP.MaLoaiGoi;

                if (ImageGoi != null && ImageGoi.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(ImageGoi.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    goiTapDb.ImageGoi = fileName;
                    ImageGoi.SaveAs(path);
                }

                db.SaveChanges();
                return RedirectToAction("Index", "GOITAPs"); // Chuyển hướng về trang chính
            }

            ViewBag.MaLoaiGoi = new SelectList(db.LOAIGOITAPs, "MaLoaiGoi", "TenLoaiGoi", gOITAP.MaLoaiGoi);
            return View(gOITAP);
        }

        // GET: GOITAPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GOITAP gOITAP = db.GOITAPs.Find(id);
            if (gOITAP == null)
            {
                return HttpNotFound();
            }
            return View(gOITAP);
        }

        // POST: GOITAPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GOITAP gOITAP = db.GOITAPs.Find(id);
            if (gOITAP != null)
            {
                db.GOITAPs.Remove(gOITAP);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "GOITAPs"); // Chuyển hướng về trang chính
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