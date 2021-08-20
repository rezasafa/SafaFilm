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

namespace SafaFilm.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StylesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Styles
        public async Task<ActionResult> Index()
        {
            return View(await db.styles.ToListAsync());
        }

        // GET: Styles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Styles styles = await db.styles.FindAsync(id);
            if (styles == null)
            {
                return HttpNotFound();
            }
            return View(styles);
        }

        // GET: Styles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Styles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "StyleID,Title,imgpic,dt,tm,users")] Styles styles)
        {
            if (ModelState.IsValid)
            {
                styles.dt = DateTime.Now.ToString();
                styles.users = User.Identity.Name;
                db.styles.Add(styles);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(styles);
        }

        // GET: Styles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Styles styles = await db.styles.FindAsync(id);
            if (styles == null)
            {
                return HttpNotFound();
            }
            return View(styles);
        }

        // POST: Styles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "StyleID,Title,imgpic,dt,tm,users")] Styles styles)
        {
            if (ModelState.IsValid)
            {
                styles.dt = DateTime.Now.ToString();
                styles.users = User.Identity.Name;
                db.Entry(styles).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(styles);
        }

        // GET: Styles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Styles styles = await db.styles.FindAsync(id);
            if (styles == null)
            {
                return HttpNotFound();
            }
            return View(styles);
        }

        // POST: Styles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Styles styles = await db.styles.FindAsync(id);
            db.styles.Remove(styles);
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
