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
    public class LinksController : Controller
    {
        private Data_Models db = new Data_Models();

        // GET: Links
        public ActionResult Index()
        {
            return View(db.Links.OrderBy(m=>m.Server.Name).ThenBy(m=>m.System.Name).ThenBy(m=>m.ServerType.Name).ToList());
        }

        // GET: Links/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(id);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // GET: Links/Create
        public ActionResult Create()
        {
            //populate viewbag for drdopdown lists
            ViewBag.Systems = db.Systems.OrderBy(m => m.Name).ToList();
            ViewBag.Servers = db.Servers.OrderBy(m => m.Name).ToList();
            ViewBag.ServerTypes = db.ServerTypes.OrderBy(m => m.Name).ToList();
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServerID,SystemsID,ServerTypeID")] Link link)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Links.Add(link);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "This is a duplicate");
                }
            }
            //populate viewbag for drdopdown lists
            ViewBag.Systems = db.Systems.OrderBy(m => m.Name).ToList();
            ViewBag.Servers = db.Servers.OrderBy(m => m.Name).ToList();
            ViewBag.ServerTypes = db.ServerTypes.OrderBy(m => m.Name).ToList();
            return View(link);
        }

        // GET: Links/Edit/5
        public ActionResult Edit(Guid serverID, Guid systemID, Guid serverTypeID)
        {
            if (serverID == null || systemID == null || serverTypeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Link link = db.Links.Find(serverID, systemID, serverTypeID);
            if (link == null)
            {
                return HttpNotFound();
            }
            //populate viewbag for drdopdown lists
            ViewBag.Systems = db.Systems.OrderBy(m => m.Name).ToList();
            ViewBag.Servers = db.Servers.OrderBy(m => m.Name).ToList();
            ViewBag.ServerTypes = db.ServerTypes.OrderBy(m => m.Name).ToList();
            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /*public ActionResult Edit([Bind(Include = "ServerID,SystemsID,ServerTypeID")] Link link, Link oldLink)*/
        public ActionResult Edit(Guid serverID, Guid systemsID, Guid serverTypeID, Guid oldServerID, Guid oldSystemsID, Guid oldServerTypeID)
        {
            Link link = db.Links.Find(oldServerID, oldSystemsID, oldServerTypeID);
            //to see if it already exists
            Link testLink = db.Links.Find(serverID, systemsID, serverTypeID);
            if (ModelState.IsValid && testLink == null)
            {
                //EF doesnt like updating composite keys.  So remove the old record and create a new one.
                db.Links.Remove(link);
                Link newLink = new Link
                {
                    ServerID = serverID,
                    SystemsID = systemsID,
                    ServerTypeID = serverTypeID
                };
                db.Links.Add(newLink);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            //throw an error if it already exists
            if (testLink!=null)
            {
                ModelState.AddModelError("", "This is a duplicate");
            }
            //populate viewbag for drdopdown lists
            ViewBag.Systems = db.Systems.OrderBy(m => m.Name).ToList();
            ViewBag.Servers = db.Servers.OrderBy(m => m.Name).ToList();
            ViewBag.ServerTypes = db.ServerTypes.OrderBy(m => m.Name).ToList();
            return View(link);
        }

        // GET: Links/Delete/5
        public ActionResult Delete(Guid serverID, Guid systemID, Guid serverTypeID)
        {
            if (serverID == null || systemID == null || serverTypeID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        
            Link link = db.Links.Find(serverID, systemID, serverTypeID);
            if (link == null)
            {
                return HttpNotFound();
            }
            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid serverID, Guid systemID, Guid serverTypeID)
        {
            Link link = db.Links.Find(serverID, systemID, serverTypeID);
            db.Links.Remove(link);
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
