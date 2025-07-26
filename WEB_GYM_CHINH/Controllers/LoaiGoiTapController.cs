using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_GYM_CHINH.Models;
namespace WEB_GYM_CHINH.Controllers
{
    public class LoaiGoiTapController : Controller
    {
        // GET: LoaiGoiTap
        DBWebGymEntities database = new DBWebGymEntities();
        public ActionResult Index()
        {
            var loaigoitap = database.LOAIGOITAPs.ToList();
            return View(loaigoitap);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(LOAIGOITAP loaigoitap)
        {
            try
            {
                database.LOAIGOITAPs.Add(loaigoitap);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Lỗi không thể thêm loại gói tập mới");
            }
        }
        public ActionResult Details(int id)
        {
            var loaigoitap = database.LOAIGOITAPs.Where(x => x.MaLoaiGoi == id).FirstOrDefault();
            return View(loaigoitap);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var loaigoitap = database.LOAIGOITAPs.Where(x => x.MaLoaiGoi == id).FirstOrDefault();
            return View(loaigoitap);
        }
        [HttpPost]
        public ActionResult Edit(int id, LOAIGOITAP loaigoitap)
        {
            database.Entry(loaigoitap).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var loaigoitap = database.LOAIGOITAPs.Where(x => x.MaLoaiGoi == id).FirstOrDefault();
            if (loaigoitap == null)
            {
                return HttpNotFound();
            }
            return View(loaigoitap);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var loaigoitap = database.LOAIGOITAPs.Where(x => x.MaLoaiGoi == id).FirstOrDefault();
                database.LOAIGOITAPs.Remove(loaigoitap);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return Content("Lỗi không thể xóa loại gói tập này vì nó có liên kết với các bản ghi khác trong hệ thống.");
            }
        }
    }
}