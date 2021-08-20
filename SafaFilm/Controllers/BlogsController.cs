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
    public class BlogsController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Blogs
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            return View(await db.blogs.ToListAsync());
        }

        // GET: Blogs/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = await db.blogs.FindAsync(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Title,Body,imgpic,dt,tm,users")] Blogs blogs)
        {
            if (ModelState.IsValid)
            {
                blogs.dt = DateTime.Now.ToString();
                blogs.users = User.Identity.Name;
                db.blogs.Add(blogs);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(blogs);
        }

        // GET: Blogs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = await db.blogs.FindAsync(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Title,Body,imgpic,dt,tm,users")] Blogs blogs)
        {
            if (ModelState.IsValid)
            {
                blogs.dt = DateTime.Now.ToString();
                blogs.users = User.Identity.Name;
                db.Entry(blogs).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blogs);
        }

        // GET: Blogs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blogs blogs = await db.blogs.FindAsync(id);
            if (blogs == null)
            {
                return HttpNotFound();
            }
            return View(blogs);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Blogs blogs = await db.blogs.FindAsync(id);
            db.blogs.Remove(blogs);
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
