using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicLibPractice.Models;

namespace MusicLibPractice.Controllers
{
    public class pathsController : Controller
    {
        private MusicLibraryEntities db = new MusicLibraryEntities();

        // GET: paths
        public ActionResult Index()
        {
            return View(db.paths.ToList());
        }

        // GET: paths/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paths paths = db.paths.Find(id);
            if (paths == null)
            {
                return HttpNotFound();
            }
            return View(paths);
        }

        // GET: paths/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: paths/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "path_id,path_name")] paths paths)
        {
            if (ModelState.IsValid)
            {
                db.paths.Add(paths);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paths);
        }

        // GET: paths/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paths paths = db.paths.Find(id);
            if (paths == null)
            {
                return HttpNotFound();
            }
            return View(paths);
        }

        // POST: paths/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "path_id,path_name")] paths paths)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paths).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paths);
        }

        // GET: paths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            paths paths = db.paths.Find(id);
            if (paths == null)
            {
                return HttpNotFound();
            }
            return View(paths);
        }

        // POST: paths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            paths paths = db.paths.Find(id);
            db.paths.Remove(paths);
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
