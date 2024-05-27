using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cinema_Manage.Models;

namespace Cinema_Manage.Areas.Admin.Controllers
{
    public class PhongChieuxController : Controller
    {
        private Model1 db = new Model1();

        // GET: PhongChieux
        public ActionResult Index()
        {
            return View(db.PhongChieux.ToList());
        }

        // GET: PhongChieux/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongChieu phongChieu = db.PhongChieux.Find(id);
            if (phongChieu == null)
            {
                return HttpNotFound();
            }
            return View(phongChieu);
        }

        // GET: PhongChieux/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhongChieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhong,TenPhong")] PhongChieu phongChieu)
        {
            phongChieu.MaPhong = "P00" + (db.PhongChieux.Count() + 1);
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.PhongChieux.Add(phongChieu);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm phim. Vui lòng thử lại sau.");
                    }
                }

            }

            return View(phongChieu);
        }

        // GET: PhongChieux/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongChieu phongChieu = db.PhongChieux.Find(id);
            if (phongChieu == null)
            {
                return HttpNotFound();
            }
            return View(phongChieu);
        }

        // POST: PhongChieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhong,TenPhong")] PhongChieu phongChieu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(phongChieu).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm phim. Vui lòng thử lại sau.");
                }
                
            }
            return View(phongChieu);
        }

        // GET: PhongChieux/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhongChieu phongChieu = db.PhongChieux.Find(id);
            if (phongChieu == null)
            {
                return HttpNotFound();
            }
            return View(phongChieu);
        }

        // POST: PhongChieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhongChieu phongChieu = db.PhongChieux.Find(id);
            db.PhongChieux.Remove(phongChieu);
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
