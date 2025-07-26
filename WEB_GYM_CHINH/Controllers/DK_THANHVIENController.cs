using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEB_GYM_CHINH.Models;

namespace WEB_GYM_CHINH.Controllers
{
    public class DK_THANHVIENController : Controller
    {
        private DBWebGymEntities db = new DBWebGymEntities();

        // GET: DK_THANHVIEN
        public ActionResult Index()
        {
            var dK_THANHVIEN = db.DK_THANHVIEN.Include(d => d.GOITAP);
            return View(dK_THANHVIEN.ToList());
        }

        // GET: DK_THANHVIEN/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DK_THANHVIEN dK_THANHVIEN = db.DK_THANHVIEN.Find(id);
            if (dK_THANHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(dK_THANHVIEN);
        }

        // GET: DK_THANHVIEN/Create
        public ActionResult Create()
        {
            ViewBag.MaGoiTap = new SelectList(db.GOITAPs, "MaGoiTap", "TenGoiTap");
            return View();
        }

        // POST: DK_THANHVIEN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDKTV,HoTen,SDT,Email,NgaySinh,GioiTinh,MaGoiTap,NgayBatDau,NgayKetThuc,SoBuoi,DonGia,ThanhToan")] DK_THANHVIEN dK_THANHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.DK_THANHVIEN.Add(dK_THANHVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGoiTap = new SelectList(db.GOITAPs, "MaGoiTap", "TenGoiTap", dK_THANHVIEN.MaGoiTap);
            return View(dK_THANHVIEN);
        }

        // GET: DK_THANHVIEN/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DK_THANHVIEN dK_THANHVIEN = db.DK_THANHVIEN.Find(id);
            if (dK_THANHVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaGoiTap = new SelectList(db.GOITAPs, "MaGoiTap", "TenGoiTap", dK_THANHVIEN.MaGoiTap);
            return View(dK_THANHVIEN);
        }

        // POST: DK_THANHVIEN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDKTV,HoTen,SDT,Email,NgaySinh,GioiTinh,MaGoiTap,NgayBatDau,NgayKetThuc,SoBuoi,DonGia,ThanhToan")] DK_THANHVIEN dK_THANHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dK_THANHVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaGoiTap = new SelectList(db.GOITAPs, "MaGoiTap", "TenGoiTap", dK_THANHVIEN.MaGoiTap);
            return View(dK_THANHVIEN);
        }

        // GET: DK_THANHVIEN/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DK_THANHVIEN dK_THANHVIEN = db.DK_THANHVIEN.Find(id);
            if (dK_THANHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(dK_THANHVIEN);
        }

        // POST: DK_THANHVIEN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DK_THANHVIEN dK_THANHVIEN = db.DK_THANHVIEN.Find(id);
            db.DK_THANHVIEN.Remove(dK_THANHVIEN);
            db.SaveChanges();
            return RedirectToAction("Index");
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
