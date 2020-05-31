using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicLibPractice.Models;
using System.Data.Entity;

namespace MusicLibPractice.Controllers
{
    public class QueryBuilderController : Controller
    {



        public class RequestForm
        {
            public string Column { get; set; }
            public string Sign { get; set; }
            public string Value { get; set; }
        }

        [HttpGet]
        public ActionResult Index(string error)
        {
            List<string> cols = new List<string>();
            cols.Add("Name");
            cols.Add("Date");
            cols.Add("Author");
            cols.Add("Country");
            cols.Add("Format");
            cols.Add("Genre");
            cols.Add("Paths");
            cols.Add("Albums");
            cols.Add("Duration");

            List<SelectListItem> columns = new List<SelectListItem>();
            foreach (var a in cols)
            {
                columns.Add(new SelectListItem() { Text = a });
            }

            ViewBag.columns = columns;


            ViewBag.error = error;
            List<SelectListItem> signs = new List<SelectListItem>()
            {
                new SelectListItem(){Text="greater"},
                new SelectListItem(){Text="equal"},
                new SelectListItem(){Text="less"},
            };
            ViewBag.sign = signs;


            return View();

        }
        [HttpPost]
        public ActionResult Index(RequestForm form)
        {
            return RedirectToAction("Table", new {form.Column, form.Sign, form.Value });
        }


        [HttpGet]
        public ActionResult Table(RequestForm form)
        {
            MusicLibraryEntities db = new MusicLibraryEntities();
            var songs = db.songs.Include(s => s.authors).Include(s => s.countries).Include(s => s.formats).Include(s => s.genres).Include(s => s.paths).Include(s => s.albums);

            List<string> cols = new List<string>();
            cols.Add("Name");
            cols.Add("Date");
            cols.Add("Author");
            cols.Add("Country");
            cols.Add("Format");
            cols.Add("Genre");
            cols.Add("Paths");
            cols.Add("Albums");
            cols.Add("Duration");

            List<SelectListItem> columns = new List<SelectListItem>();
            foreach (var a in cols)
            {
                columns.Add(new SelectListItem() { Text = a });
            }

            ViewBag.columns = columns;

            try
            {
                switch (form.Column)
                {
                    case "Name":
                        songs = songs.Where(x => x.song_name == "15 Step");
                        break;

                    case "Author":
                        songs = songs.Where(x => x.authors.author_name == form.Value);

                        break;
                    case "Country":
                        songs = songs.Where(x => x.countries.country_name == form.Value);

                        break;
                    case "Format":
                        songs = songs.Where(x => x.formats.format_name == form.Value);
                        break;
                    case "Albums":
                        songs = songs.Where(x => x.albums.First().album_name == form.Value);

                        break;
                    case "Genre":
                        songs = songs.Where(x => x.genres.genre_name == form.Value);
                        break;
                    case "Paths":
                        songs = songs.Where(x => x.paths.path_name == form.Value);
                        break;
                    case "Date":
                        switch (form.Sign)
                        {
                            case "greater":
                                songs = songs.Where(x => x.song_date > int.Parse(form.Value));
                                break;
                            case "equal":
                                songs = songs.Where(x => x.song_date == int.Parse(form.Value));
                                break;
                            case "less":
                                songs = songs.Where(x => x.song_date < int.Parse(form.Value));
                                break;
                        }
                        break;

                    case "Duration":
                        switch (form.Sign)
                        {
                            case "greater":
                                songs = songs.Where(x => x.duration > int.Parse(form.Value));
                                break;
                            case "equal":
                                songs = songs.Where(x => x.duration == int.Parse(form.Value));
                                break;
                            case "less":
                                songs = songs.Where(x => x.duration < int.Parse(form.Value));
                                break;
                        }
                        break;

                }
            }
            catch (Exception)
            {
                string err = "Неправильный формат данных";
                RedirectToAction("Table", new { err });
            }

            return View(songs.ToList());
        }
    
    }
}