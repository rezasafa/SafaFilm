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
    public class CountrysController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Countrys
        public async Task<ActionResult> Index()
        {
            return View(await db.countrys.ToListAsync());
        }

        // GET: Countrys/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countrys countrys = await db.countrys.FindAsync(id);
            if (countrys == null)
            {
                return HttpNotFound();
            }
            return View(countrys);
        }

        // GET: Countrys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Countrys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CountryID,Title,imgpic,dt,tm,users")] Countrys countrys)
        {
            if (ModelState.IsValid)
            {
                countrys.dt = DateTime.Now.ToString();
                countrys.users = User.Identity.Name;
                db.countrys.Add(countrys);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(countrys);
        }

        // GET: Countrys/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countrys countrys = await db.countrys.FindAsync(id);
            if (countrys == null)
            {
                return HttpNotFound();
            }
            return View(countrys);
        }

        // POST: Countrys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CountryID,Title,imgpic,dt,tm,users")] Countrys countrys)
        {
            if (ModelState.IsValid)
            {
                countrys.dt = DateTime.Now.ToString();
                countrys.users = User.Identity.Name;
                db.Entry(countrys).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(countrys);
        }

        // GET: Countrys/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Countrys countrys = await db.countrys.FindAsync(id);
            if (countrys == null)
            {
                return HttpNotFound();
            }
            return View(countrys);
        }

        // POST: Countrys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Countrys countrys = await db.countrys.FindAsync(id);
            db.countrys.Remove(countrys);
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
