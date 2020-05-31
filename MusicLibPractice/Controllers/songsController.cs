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
    public class songsController : Controller
    {
        private MusicLibraryEntities db = new MusicLibraryEntities();

        // GET: songs
        public ActionResult Index()
        {
            var songs = db.songs.Include(s => s.authors).Include(s => s.countries).Include(s => s.formats).Include(s => s.genres).Include(s => s.paths).Include(s => s.albums);
            return View(songs.ToList());
        }

        // GET: songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            songs songs = db.songs.Find(id);
            if (songs == null)
            {
                return HttpNotFound();
            }
            return View(songs);
        }

        // GET: songs/Create
        public ActionResult Create()
        {
            ViewBag.author_id = new SelectList(db.authors, "author_id", "author_name");
            ViewBag.country_id = new SelectList(db.countries, "country_id", "country_name");
            ViewBag.format_id = new SelectList(db.formats, "format_id", "format_name");
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre_name");
            ViewBag.path_id = new SelectList(db.paths, "path_id", "path_name");
            ViewBag.album_id = new SelectList(db.paths, "path_id", "path_name");

            return View();
        }

        // POST: songs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "song_id,format_id,path_id,genre_id,author_id,duration,song_name,song_date,country_id")] songs songs)
        {
            if (ModelState.IsValid)
            {
                db.songs.Add(songs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.author_id = new SelectList(db.authors, "author_id", "author_name", songs.author_id);
            ViewBag.country_id = new SelectList(db.countries, "country_id", "country_name", songs.country_id);
            ViewBag.format_id = new SelectList(db.formats, "format_id", "format_name", songs.format_id);
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre_name", songs.genre_id);
            ViewBag.path_id = new SelectList(db.paths, "path_id", "path_name", songs.path_id);
            return View(songs);
        }

        // GET: songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            songs songs = db.songs.Find(id);
            if (songs == null)
            {
                return HttpNotFound();
            }
            ViewBag.author_id = new SelectList(db.authors, "author_id", "author_name", songs.author_id);
            ViewBag.country_id = new SelectList(db.countries, "country_id", "country_name", songs.country_id);
            ViewBag.format_id = new SelectList(db.formats, "format_id", "format_name", songs.format_id);
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre_name", songs.genre_id);
            ViewBag.path_id = new SelectList(db.paths, "path_id", "path_name", songs.path_id);
            ViewBag.aaa = 1;
            return View(songs);
        }

        // POST: songs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "song_id,format_id,path_id,genre_id,author_id,duration,song_name,song_date,country_id")] songs songs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(songs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.author_id = new SelectList(db.authors, "author_id", "author_name", songs.author_id);
            ViewBag.country_id = new SelectList(db.countries, "country_id", "country_name", songs.country_id);
            ViewBag.format_id = new SelectList(db.formats, "format_id", "format_name", songs.format_id);
            ViewBag.genre_id = new SelectList(db.genres, "genre_id", "genre_name", songs.genre_id);
            ViewBag.path_id = new SelectList(db.paths, "path_id", "path_name", songs.path_id);
            return View(songs);
        }

        // GET: songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            songs songs = db.songs.Find(id);
            if (songs == null)
            {
                return HttpNotFound();
            }
            return View(songs);
        }

        // POST: songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            songs songs = db.songs.Find(id);
            db.songs.Remove(songs);
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
