using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RealEstateMVC.Models
{
    public class HouseListViewModel
    {
        public SelectList County { get; set; }
        public List<House> houses { get; set; }
    }
}
