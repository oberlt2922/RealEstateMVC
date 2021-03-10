using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RealEstateMVC.Models
{
    public class HouseViewModel
    {
        public House house { get; set; }
        public List<Image> Images { get; set; }
    }
}
