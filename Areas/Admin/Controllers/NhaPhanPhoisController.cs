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
    public class NhaPhanPhoisController : Controller
    {
        private Model1 db = new Model1();

        // GET: Admin/NhaPhanPhois
        public ActionResult Index()
        {
            return View(db.NhaPhanPhois.ToList());
        }

        // GET: Admin/NhaPhanPhois/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaPhanPhoi nhaPhanPhoi = db.NhaPhanPhois.Find(id);
            if (nhaPhanPhoi == null)
            {
                return HttpNotFound();
            }
            return View(nhaPhanPhoi);
        }

        // GET: Admin/NhaPhanPhois/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NhaPhanPhois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenNPP")] NhaPhanPhoi nhaPhanPhoi)
        {
            nhaPhanPhoi.ID = "NP0" + db.NhaPhanPhois.Count();
            if (ModelState.IsValid)
            {
                db.NhaPhanPhois.Add(nhaPhanPhoi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Có lỗi khi thêm, thử lại");
            return View(nhaPhanPhoi);
        }

        // GET: Admin/NhaPhanPhois/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaPhanPhoi nhaPhanPhoi = db.NhaPhanPhois.Find(id);
            if (nhaPhanPhoi == null)
            {
                return HttpNotFound();
            }
            return View(nhaPhanPhoi);
        }

        // POST: Admin/NhaPhanPhois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenNPP")] NhaPhanPhoi nhaPhanPhoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhaPhanPhoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaPhanPhoi);
        }

        // GET: Admin/NhaPhanPhois/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhaPhanPhoi nhaPhanPhoi = db.NhaPhanPhois.Find(id);
            if (nhaPhanPhoi == null)
            {
                return HttpNotFound();
            }
            return View(nhaPhanPhoi);
        }

        // POST: Admin/NhaPhanPhois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhaPhanPhoi nhaPhanPhoi = db.NhaPhanPhois.Find(id);
            db.NhaPhanPhois.Remove(nhaPhanPhoi);
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
