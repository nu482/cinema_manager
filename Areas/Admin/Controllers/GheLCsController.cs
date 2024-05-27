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
    public class GheLCsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/GheLCs
        public ActionResult Index()
        {
            return View(db.GheLCs.ToList());
        }

        // GET: Admin/GheLCs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GheLC gheLC = db.GheLCs.Find(id);
            if (gheLC == null)
            {
                return HttpNotFound();
            }
            return View(gheLC);
        }

        // POST: Admin/GheLCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MaGhe,MaPhong,OderCode,MaLichChieu,LoaiGhe,SoGhe,GiaGhe,TinhTrang")] GheLC gheLC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gheLC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gheLC);
        }

        // GET: Admin/GheLCs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GheLC gheLC = db.GheLCs.Find(id);
            if (gheLC == null)
            {
                return HttpNotFound();
            }
            return View(gheLC);
        }

        // POST: Admin/GheLCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GheLC gheLC = db.GheLCs.Find(id);
            db.GheLCs.Remove(gheLC);
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
