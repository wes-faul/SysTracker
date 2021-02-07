using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SysTracker.Models;

namespace SysTracker.Controllers
{
    public class SystemsController : Controller
    {
        private Data_Models db = new Data_Models();

        // GET: Systems
        public ActionResult Index()
        {
            return View(db.Systems.OrderBy(m => m.Name).ToList());
        }

        // GET: Systems/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Systems systems = db.Systems.Find(id);
            if (systems == null)
            {
                return HttpNotFound();
            }
            return View(systems);
        }

        // GET: Systems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Systems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SystemsID,Name,Description")] Systems systems)
        {
            if (ModelState.IsValid)
            {
                systems.SystemsID = Guid.NewGuid();
                db.Systems.Add(systems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systems);
        }

        // GET: Systems/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Systems systems = db.Systems.Find(id);
            if (systems == null)
            {
                return HttpNotFound();
            }
            return View(systems);
        }

        // POST: Systems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SystemsID,Name,Description")] Systems systems)
        {
            if (ModelState.IsValid)
            {
                db.Entry(systems).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systems);
        }

        // GET: Systems/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Systems systems = db.Systems.Find(id);
            if (systems == null)
            {
                return HttpNotFound();
            }
            return View(systems);
        }

        // POST: Systems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Systems systems = db.Systems.Find(id);
            if (systems.Links.Count > 0)
            {
                ModelState.AddModelError("", "You cannot delete this system.  There are related servers and server types.  View them through the Details page and delete the link before deleting this.");
            }
            else
            {
                db.Systems.Remove(systems);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systems);
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
