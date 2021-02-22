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
    public class MapLocationsController : Controller
    {
        private IntranetResourcesEntities db = new IntranetResourcesEntities();

        // GET: MapLocations
        public ActionResult Index()
        {
            populateFacilityTypes();
            populateProducts();
            populateRegions();
            return View();
        }

        public ActionResult GetLocations([DataSourceRequest] DataSourceRequest request)
        {
            //var data = db.GoogleMapsDatas.Where(l => l.Latitude != null && l.Longitude != null);
            var data = db.GoogleMapsDatas;
            DataSourceResult result = data.ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, GoogleMapsData location)
        {
            var results = new List<GoogleMapsData>();

            if (location != null && ModelState.IsValid)
            {
                try
                {
                    GoogleMapsData g = new GoogleMapsData()
                    {
                        Address = location.Address,
                        Country = location.Country,
                        FacilityTypes = location.FacilityTypes,
                        HR = location.HR,
                        HREmail = location.HREmail,
                        Latitude = location.Latitude,
                        Locality = location.Locality,
                        Longitude = location.Longitude,
                        Operations = location.Operations,
                        OperationsEmail = location.OperationsEmail,
                        PlantId = location.PlantId,
                        PlantManager = location.PlantManager,
                        PlantManagerEmail = location.PlantManagerEmail,
                        PlantSummary = location.PlantSummary,
                        PostalCode = location.PostalCode,
                        ProductTypes = location.ProductTypes,
                        Region = location.Region,
                        Sales = location.Sales,
                        SalesEmail = location.SalesEmail,
                        Subtitle = location.Subtitle,
                        Suite = location.Suite,
                        Telephone = location.Telephone,
                        Title = location.Title
                    };
                    db.GoogleMapsDatas.Add(g);

                    int success = db.SaveChanges();
                }
                catch
                {
                    
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, GoogleMapsData location)
        {
            if (location != null && ModelState.IsValid)
            {
                GoogleMapsData g = db.GoogleMapsDatas.Find(location.id);
                if (g != null)
                {
                    g.Address = location.Address;
                    g.Country = location.Country;
                    g.FacilityTypes = location.FacilityTypes;
                    g.HR = location.HR;
                    g.HREmail = location.HREmail;
                    g.Latitude = location.Latitude;
                    g.Longitude = location.Longitude;
                    g.Locality = location.Locality;
                    g.Operations = location.Operations;
                    g.OperationsEmail = location.OperationsEmail;
                    g.PlantManager = location.PlantManager;
                    g.PlantManagerEmail = location.PlantManagerEmail;
                    g.PlantSummary = location.PlantSummary;
                    g.PostalCode = location.PostalCode;
                    g.ProductTypes = location.ProductTypes;
                    g.Region = location.Region;
                    g.Sales = location.Sales;
                    g.SalesEmail = location.SalesEmail;
                    g.Subtitle = location.Subtitle;
                    g.Suite = location.Suite;
                    g.Telephone = location.Telephone;
                    g.Title = location.Title;
                }
                db.SaveChanges();
            }

            return Json(new [] {location}.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Destroy([DataSourceRequest] DataSourceRequest request, GoogleMapsData location)
        {

            GoogleMapsData googleMapsData = db.GoogleMapsDatas.Find(location.id);
            db.GoogleMapsDatas.Remove(googleMapsData);

            db.SaveChanges();
            return Json(new [] {location}.ToDataSourceResult(request, ModelState));
        }

        private void populateRegions()
        {
            ViewData["regions"] = db.GM_Regions
                        .Select(r => new RegionsDTO
                        {
                            Region = r.RegionName
                        })
                        .OrderBy(r => r.Region);
        }

        private void populateFacilityTypes()
        {
            ViewData["facilityTypes"] = db.GM_FacilityTypes
                        .Select(f => new FacilityTypeDTO
                        {
                            FacilityTypeDescription = f.FacilityTypeDescription
                        })
                        .OrderBy(f => f.FacilityTypeDescription);
        }

        private void populateProducts()
        {
            ViewData["products"] = db.GM_Products
                .Select(p => new ProductDTO
            {
                ProductTypeDescription = p.ProductDescription
            })
            .OrderBy(p => p.ProductTypeDescription);
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
