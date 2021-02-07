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
    public class ServerTypesController : Controller
    {
        private Data_Models db = new Data_Models();

        // GET: ServerTypes
        public ActionResult Index()
        {
            return View(db.ServerTypes.OrderBy(m => m.Name).ToList());
        }

        // GET: ServerTypes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServerType serverType = db.ServerTypes.Find(id);
            if (serverType == null)
            {
                return HttpNotFound();
            }
            return View(serverType);
        }

        // GET: ServerTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServerTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServerTypeID,Name,Description")] ServerType serverType)
        {
            if (ModelState.IsValid)
            {
                serverType.ServerTypeID = Guid.NewGuid();
                db.ServerTypes.Add(serverType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serverType);
        }

        // GET: ServerTypes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServerType serverType = db.ServerTypes.Find(id);
            if (serverType == null)
            {
                return HttpNotFound();
            }
            return View(serverType);
        }

        // POST: ServerTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServerTypeID,Name,Description")] ServerType serverType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serverType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serverType);
        }

        // GET: ServerTypes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServerType serverType = db.ServerTypes.Find(id);
            if (serverType == null)
            {
                return HttpNotFound();
            }
            return View(serverType);
        }

        // POST: ServerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ServerType serverType = db.ServerTypes.Find(id);
            if (serverType.Links.Count > 0)
            {
                ModelState.AddModelError("", "You cannot delete this server type.  There are related systems and servers.  View them through the Details page and delete the link before deleting this.");
            }
            else
            {
                db.ServerTypes.Remove(serverType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serverType);
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
