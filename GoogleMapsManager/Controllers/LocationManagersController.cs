using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using GoogleMapsManager.Domain;

namespace GoogleMapsManager.Controllers
{
    public class LocationManagersController : Controller
    {
        private IntranetResourcesEntities db = new IntranetResourcesEntities();

        // GET: LocationManagers
        public ActionResult Index()
        {
            var gM_LocationManagers = db.GM_LocationManagers.Include(g => g.GM_Roles).Include(g => g.GM_Locations);
            return View(gM_LocationManagers.ToList());
        }

        // GET: LocationManagers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_LocationManagers locationManagers = db.GM_LocationManagers.Find(id);
            if (locationManagers == null)
            {
                return HttpNotFound();
            }
            return View(locationManagers);
        }

        // GET: LocationManagers/Create
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.GM_Roles, "RoleId", "RoleDescription");
            ViewBag.LocationId = new SelectList(db.GM_Locations, "PlantId", "PlantName");
            return View();
        }

        // POST: LocationManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LocationId,Role,ManagerEIN,ManagerName,ManagerTitle,ManagerPhone,ManagerEmail,ManagerDisplayOrder")] GM_LocationManagers locationManagers)
        {
            if (ModelState.IsValid)
            {
                db.GM_LocationManagers.Add(locationManagers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Role = new SelectList(db.GM_Roles, "RoleId", "RoleDescription", locationManagers.Role);
            ViewBag.LocationId = new SelectList(db.GM_Locations, "PlantId", "PlantName", locationManagers.LocationId);
            return View(locationManagers);
        }

        // GET: LocationManagers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_LocationManagers locationManagers = db.GM_LocationManagers.Find(id);
            if (locationManagers == null)
            {
                return HttpNotFound();
            }
            ViewBag.Role = new SelectList(db.GM_Roles, "RoleId", "RoleDescription", locationManagers.Role);
            ViewBag.LocationId = new SelectList(db.GM_Locations, "PlantId", "PlantName", locationManagers.LocationId);
            return View(locationManagers);
        }

        // POST: LocationManagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LocationId,Role,ManagerEIN,ManagerName,ManagerTitle,ManagerPhone,ManagerEmail,ManagerDisplayOrder")] GM_LocationManagers locationManagers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(locationManagers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Role = new SelectList(db.GM_Roles, "RoleId", "RoleDescription", locationManagers.Role);
            ViewBag.LocationId = new SelectList(db.GM_Locations, "PlantId", "PlantName", locationManagers.LocationId);
            return View(locationManagers);
        }

        // GET: LocationManagers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_LocationManagers locationManagers = db.GM_LocationManagers.Find(id);
            if (locationManagers == null)
            {
                return HttpNotFound();
            }
            return View(locationManagers);
        }

        // POST: LocationManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GM_LocationManagers locationManagers = db.GM_LocationManagers.Find(id);
            db.GM_LocationManagers.Remove(locationManagers);
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
