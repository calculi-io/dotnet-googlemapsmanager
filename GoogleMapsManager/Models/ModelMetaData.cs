using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GoogleMapsManager.Domain
{
    [MetadataType(typeof(LocationsMetadata))]
    public partial class GM_Locations { }

    [MetadataType(typeof(FacilityTypesMetadata))]
    public partial class GM_FacilityTypes { }

    [MetadataType(typeof(ProductsMetadata))]
    public partial class GM_Products { }

    [MetadataType(typeof(RegionsMetadata))]
    public partial class GM_Regions { }

    [MetadataType(typeof(PlantFacilityMetadata))]
    public partial class GM_PlantFacilities { }

    [MetadataType(typeof(RolesMetadata))]
    public partial class GM_Roles { }

    [MetadataType(typeof(GoogleMapsMetadata))]
    public partial class GoogleMapsData { }

    sealed class LocationsMetadata
    {
        [Required]
        [Display(Name = "Plant Id")]
        public int PlantId { get; set; }

        [Required]
        [Display(Name="Plant Name")]
        public string PlantName { get; set; }

        [Required]
        public int Region { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Display(Name="State or Province")]
        public string StateOrProvince { get; set; }

        [Required]
        [Display(Name="Postal Code")]
        public string PostalCode { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [Display(Name="Lattitude")]
        public decimal LocationLat { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        public decimal LocationLong { get; set; }
    }

    sealed class FacilityTypesMetadata
    {
        [Required]
        [Display(Name = "Facility Type")]
        public string FacilityTypeDescription { get; set; }
    }

    sealed class ProductsMetadata
    {
        [Required]
        [Display(Name = "Product Description")]
        public string ProductDescription { get; set; }
    }

    sealed class RegionsMetadata
    {
        [Required]
        [Display(Name = "Region")]
        public string RegionName { get; set; }
    }

    sealed class PlantFacilityMetadata
    {
        [Required]
        [Display(Name = "Plant Id")]
        public int PlantId { get; set; }

        [Required]
        [Display(Name = "Facility Type")]
        public int FacilityTypeId { get; set; }
    }

    sealed class RolesMetadata
    {
        [Required]
        [Display(Name = "Role Description")]
        public string RoleDescription { get; set; }
    }

    sealed class GoogleMapsMetadata
    {
        [Display(Name = "Plant Id")]
        public int PlantId { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Plant Manager")]
        public string PlantManager { get; set; }

        [Display(Name = "Manager Email")]
        public string PlantManagerEmail { get; set; }

        [Display(Name = "Facility Type")]
        public decimal FacilityTypes { get; set; }

        [Display(Name = "Products")]
        public decimal ProductTypes { get; set; }
    }
}