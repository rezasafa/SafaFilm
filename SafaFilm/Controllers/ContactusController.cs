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
    public class ContactusController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Contactus
        public async Task<ActionResult> Index()
        {
            return View(await db.contactus.ToListAsync());
        }

        // GET: Contactus/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactus contactus = await db.contactus.FindAsync(id);
            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }

        // GET: Contactus/Create
        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contactus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Create([Bind(Include = "id,Names,Emails,Messages,dt,tm,Users")] Contactus contactus)
        {
            if (ModelState.IsValid)
            {
                contactus.dt = DateTime.Now.ToString();
                db.contactus.Add(contactus);
                await db.SaveChangesAsync();
                return Redirect("~/Home/Index");
            }

            return View(contactus);
        }

        // GET: Contactus/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactus contactus = await db.contactus.FindAsync(id);
            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }

        // POST: Contactus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Names,Emails,Messages,dt,tm,Users")] Contactus contactus)
        {
            if (ModelState.IsValid)
            {
                contactus.dt = DateTime.Now.ToString();
                contactus.users = User.Identity.Name;
                db.Entry(contactus).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contactus);
        }

        // GET: Contactus/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contactus contactus = await db.contactus.FindAsync(id);
            if (contactus == null)
            {
                return HttpNotFound();
            }
            return View(contactus);
        }

        // POST: Contactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Contactus contactus = await db.contactus.FindAsync(id);
            db.contactus.Remove(contactus);
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
