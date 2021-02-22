using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoogleMapsManager.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GoogleMapsManager.Models
{
    public class LocationDetail
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Plant ID is required.")]
        public int PlantId { get; set; }

        [Required(ErrorMessage = "Plant Name is required.")]
        public string PlantName { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        public int Region { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }

        [Required(ErrorMessage = "Lattitude is required.")]
        public decimal LocationLat { get; set; }

        [Required(ErrorMessage = "Longitude is required.")]
        public decimal LocationLong { get; set; }

        [Required(ErrorMessage = "Product is required.")]
        public IEnumerable<GM_Products> SelectedProducts { get; set; }

        public IEnumerable<GM_LocationManagers> SelectedManagers { get; set; }
        public virtual GM_Regions GM_Regions { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        public SelectList RegionsSelectList { get; set; }
        public SelectList RolesSelectList { get; set; }

        [Required(ErrorMessage = "Product(s) are required.")]
        public SelectList ProductsSelectList { get; set; }
    }
}