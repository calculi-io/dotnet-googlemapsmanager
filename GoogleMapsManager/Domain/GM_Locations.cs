//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoogleMapsManager.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class GM_Locations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GM_Locations()
        {
            this.GM_LocationManagers = new HashSet<GM_LocationManagers>();
            this.GM_FacilityTypes = new HashSet<GM_FacilityTypes>();
            this.GM_Products = new HashSet<GM_Products>();
        }
    
        public int Id { get; set; }
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public int Region { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string StateOrProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public decimal LocationLat { get; set; }
        public decimal LocationLong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GM_LocationManagers> GM_LocationManagers { get; set; }
        public virtual GM_Regions GM_Regions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GM_FacilityTypes> GM_FacilityTypes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GM_Products> GM_Products { get; set; }
    }
}
