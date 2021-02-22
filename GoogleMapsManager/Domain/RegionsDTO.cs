using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GoogleMapsManager.Models;
using System.ComponentModel.DataAnnotations;

namespace GoogleMapsManager.Domain
{
    public class RegionsDTO
    {
        [UIHint("RegionEditor")]
        public string Region { get; set; }
    }
}