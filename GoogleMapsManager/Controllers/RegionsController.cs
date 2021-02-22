using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoogleMapsManager.Domain;

namespace GoogleMapsManager.Controllers
{
    public class RegionsController : Controller
    {
        private IntranetResourcesEntities db = new IntranetResourcesEntities();

        // GET: Regions
        public ActionResult Index()
        {
            return View(db.GM_Regions.ToList());
        }

        // GET: Regions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_Regions regions = db.GM_Regions.Find(id);
            if (regions == null)
            {
                return HttpNotFound();
            }
            return View(regions);
        }

        // GET: Regions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Regions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegionId,RegionName")] GM_Regions regions)
        {
            if (ModelState.IsValid)
            {
                db.GM_Regions.Add(regions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(regions);
        }

        // GET: Regions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_Regions regions = db.GM_Regions.Find(id);
            if (regions == null)
            {
                return HttpNotFound();
            }
            return View(regions);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegionId,RegionName")] GM_Regions regions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(regions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(regions);
        }

        // GET: Regions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_Regions regions = db.GM_Regions.Find(id);
            if (regions == null)
            {
                return HttpNotFound();
            }
            return View(regions);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GM_Regions regions = db.GM_Regions.Find(id);
            db.GM_Regions.Remove(regions);
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
