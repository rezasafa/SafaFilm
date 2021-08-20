using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SafaFilm.Models;

namespace SafaFilm.Controllers
{
    public class SearchController : Controller
    {
        MyContext db = new MyContext();
        // GET: Search
        public ActionResult Index(string q)
        {
            var searchResult = new SearchModel();
            var lfilm = db.films.Where(
                m => m.Title.Contains(q) ||
                m.Body.Contains(q) || m.Actress.Contains(q) ||
                m.Director.Contains(q) || m.Tags.Contains(q)
                                        ).ToList();
            searchResult.LFilms = lfilm;
            return View(searchResult);
        }
    }

    public class SearchModel
    {
        public List<Films> LFilms { get; set; }
    }
}