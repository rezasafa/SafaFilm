using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafaFilm.Models;
using System.IO;

namespace SafaFilm.Controllers
{
    public class FilmsController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Films
        public async Task<ActionResult> Index()
        {
            var films = db.films.Include(f => f.countrys).Include(f => f.styles);
            return View(await films.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Films films = await db.films.FindAsync(id);
            if (films == null)
            {
                return HttpNotFound();
            }
            return View(films);
        }

        // GET: Films/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.countrys, "CountryID", "Title");
            ViewBag.StyleID = new SelectList(db.styles, "StyleID", "Title");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind(Include = "FilmID,imgpic,Title,Body,Tags,Director,Producer,Actress,L480,P480,L720,P720,L1080,P1080,LQ1080,PQ1080,LALL,PALL,SubTitle,Translate,StyleID,CountryID,Status,dt,tm,users")] Films films,HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                var dt = DateTime.Now.Year.ToString() + "/" +
                        DateTime.Now.Month.ToString() + "/" +
                        DateTime.Now.Day.ToString();
                var tm = DateTime.Now.Hour.ToString() + ":" +
                        DateTime.Now.Minute.ToString();
                var dtname = dt.Replace("/", "") + tm.Replace(":", "") +
                        DateTime.Now.Second.ToString() +
                        DateTime.Now.Millisecond.ToString();

                if (upfile != null && upfile.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(upfile.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/img"),
                                           dtname +
                                           pic);

                    upfile.SaveAs(path);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        upfile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                    films.imgpic = dtname + pic;
                }
                films.dt = dt;
                films.tm = tm;
                films.users = User.Identity.Name;
                db.films.Add(films);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.countrys, "CountryID", "Title", films.CountryID);
            ViewBag.StyleID = new SelectList(db.styles, "StyleID", "Title", films.StyleID);
            return View(films);
        }

        // GET: Films/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Films films = await db.films.FindAsync(id);
            if (films == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.countrys, "CountryID", "Title", films.CountryID);
            ViewBag.StyleID = new SelectList(db.styles, "StyleID", "Title", films.StyleID);
            return View(films);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit([Bind(Include = "FilmID,imgpic,Title,Body,Tags,Director,Producer,Actress,L480,P480,L720,P720,L1080,P1080,LQ1080,PQ1080,LALL,PALL,SubTitle,Translate,StyleID,CountryID,Status,dt,tm,users")] Films films, HttpPostedFileBase upfile, string oldimg)
        {
            if (ModelState.IsValid)
            {
                var dt = DateTime.Now.Year.ToString() + "/" +
                        DateTime.Now.Month.ToString() + "/" +
                        DateTime.Now.Day.ToString();
                var tm = DateTime.Now.Hour.ToString() + ":" +
                        DateTime.Now.Minute.ToString();
                var dtname = dt.Replace("/", "") + tm.Replace(":", "") +
                        DateTime.Now.Second.ToString() +
                        DateTime.Now.Millisecond.ToString();

                if (upfile != null && upfile.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(upfile.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/img"),
                                           dtname +
                                           pic);

                    upfile.SaveAs(path);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        upfile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                    films.imgpic = dtname + pic;
                }
                else
                {
                    films.imgpic = oldimg;
                }
                films.dt = dt;
                films.tm = tm;
                films.users = User.Identity.Name;

                db.Entry(films).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.countrys, "CountryID", "Title", films.CountryID);
            ViewBag.StyleID = new SelectList(db.styles, "StyleID", "Title", films.StyleID);
            return View(films);
        }

        // GET: Films/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Films films = await db.films.FindAsync(id);
            if (films == null)
            {
                return HttpNotFound();
            }
            return View(films);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Films films = await db.films.FindAsync(id);
            db.films.Remove(films);
            await db.SaveChangesAsync();
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
