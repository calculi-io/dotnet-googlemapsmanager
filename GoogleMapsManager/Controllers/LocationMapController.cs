using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using GoogleMapsManager.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleMapsManager.Controllers
{
    public class LocationMapController : Controller
    {
        private IntranetResourcesEntities db = new IntranetResourcesEntities();

        public class GenericListItem
        {
            public string Description;
            public string Prefilter;
        }

        public ActionResult GetLocations([DataSourceRequest] DataSourceRequest request)
        {
            //var data = db.GoogleMapsDatas.Where(l => l.Latitude != null && l.Longitude != null);
            var data = db.GoogleMapsDatas;
            //DataSourceResult result = data.ToDataSourceResult(request);
            //return Json(result, JsonRequestBehavior.AllowGet);
            var o = new object();
            return Json(o, JsonRequestBehavior.AllowGet);
        }

        // GET: LocationMap
        public ActionResult Index()
        {
            return View();
        }

        public List<GenericListItem> GetProductsForList()
        {
            List<GenericListItem> gliList = new List<GenericListItem>();
            /*foreach (GM_Products gmp in db.GM_Products.OrderBy(x => x.ProductDescription))
            {
                GenericListItem gli = new GenericListItem();
                gli.Description = gmp.ProductDescription;
                gli.Prefilter = "type-" + gmp.ProductDescription.ToLower().Replace(" ", "-");
                gliList.Add(gli);
            }*/

            return gliList;
        }

        public List<GenericListItem> GetFacilityTypesForList()
        {
            List<GenericListItem> gliList = new List<GenericListItem>();
            /*foreach (GM_FacilityTypes gmft in db.GM_FacilityTypes.OrderBy(x => x.FacilityTypeDescription))
            {
                GenericListItem gli = new GenericListItem();
                gli.Description = gmft.FacilityTypeDescription;
                gli.Prefilter = "facility-" + gmft.FacilityTypeDescription.ToLower().Replace(" ", "-");
                gliList.Add(gli);
            }*/

            return gliList;
        }

        public List<GenericListItem> GetRegionsForList()
        {
            List<GenericListItem> gliList = new List<GenericListItem>();
            /*foreach (GM_Regions gmr in db.GM_Regions.OrderBy(x => x.RegionName))
            {
                GenericListItem gli = new GenericListItem();
                gli.Description = gmr.RegionName;
                gli.Prefilter = "region-" + gmr.RegionName.ToLower().Replace(" ", "-");
                gliList.Add(gli);
            }*/

            return gliList;
        }
    }
}