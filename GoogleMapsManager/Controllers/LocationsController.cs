using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GoogleMapsManager.Domain;
using GoogleMapsManager.Models;

namespace GoogleMapsManager.Controllers
{
    public class LocationsController : Controller
    {
        private IntranetResourcesEntities db = new IntranetResourcesEntities();

        // GET: Locations
        [HttpGet]
        public ActionResult Index()
        {
            List<GM_Locations> locations = (List<GM_Locations>)db.GM_Locations.ToList();

            return View(locations);
        }

        // GET: Locations/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_Locations location = db.GM_Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            
            Models.LocationDetail detail = new LocationDetail()
            {
                Address = location.Address,
                City = location.City,
                Country = location.Country,
                Id = location.Id,
                LocationLat = location.LocationLat,
                LocationLong = location.LocationLong,
                Phone = location.Phone,
                PlantId = location.PlantId,
                PlantName = location.PlantName,
                PostalCode = location.PostalCode,
                Region = location.Region,
                StateOrProvince = location.StateOrProvince,
                GM_Regions = location.GM_Regions
            };

            List<GM_Products> products = new List<GM_Products>();
            foreach(GM_Products p in location.GM_Products)
            {
                products.Add(new GM_Products { ProductId = p.ProductId, ProductDescription = p.ProductDescription });
            }
            detail.SelectedProducts = products;

            List<GM_LocationManagers> managers = new List<GM_LocationManagers>();
            foreach (GM_LocationManagers m in location.GM_LocationManagers)
            {
                managers.Add(new GM_LocationManagers
                {
                    Id = m.Id,
                    LocationId = m.LocationId,
                    ManagerDisplayOrder = m.ManagerDisplayOrder,
                    ManagerEIN = m.ManagerEIN,
                    ManagerEmail = m.ManagerEmail,
                    ManagerName = m.ManagerName,
                    ManagerPhone = m.ManagerPhone,
                    Role = m.Role
                });
            }
            detail.SelectedManagers = managers;

            detail.ProductsSelectList = new SelectList(db.GM_Products, "ProductId", "ProductDescription");
            detail.RolesSelectList = new SelectList(db.GM_Roles, "RoleId", "RoleDescription");

            return View(detail);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            ViewBag.Region = new SelectList(db.GM_Regions, "RegionId", "RegionName");
            ViewBag.FacilityTypes = new SelectList(db.GM_FacilityTypes, "FacilityTypeId", "FacilityTypeDescription");
            ViewBag.Products = new SelectList(db.GM_Products, "ProductId", "ProductDescription");

            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PlantId,PlantName,Region,Address,City,StateOrProvince,PostalCode,Country,Phone,LocationLat,LocationLong")] GM_Locations location)
        {
            if (ModelState.IsValid)
            {
                db.GM_Locations.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Region = new SelectList(db.GM_Regions, "RegionId", "RegionName", location.Region);
            return View(location);
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GM_Locations location = db.GM_Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }

            ViewBag.Region = new SelectList(db.GM_Regions, "RegionId", "RegionName", location.Region);
            ViewBag.FacilityTypes = new SelectList(db.GM_FacilityTypes, "FacilityTypeId", "FacilityTypeDescription");
            ViewBag.Products = new SelectList(db.GM_Products, "ProductId", "ProductDescription");

            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PlantId,PlantName,Region,Address,City,StateOrProvince,PostalCode,Country,Phone,LocationLat,LocationLong")] GM_Locations location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Region = new SelectList(db.GM_Regions, "RegionId", "RegionName");
            ViewBag.FacilityTypes = new SelectList(db.GM_FacilityTypes, "FacilityTypeId", "FacilityTypeDescription");
            ViewBag.Products = new SelectList(db.GM_Products, "ProductId", "ProductDescription");

            

            //string[] selectedProducts = 
            return View(location);
        }

        // GET: Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GM_Locations location = db.GM_Locations.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GM_Locations location = db.GM_Locations.Find(id);
            db.GM_Locations.Remove(location);
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
