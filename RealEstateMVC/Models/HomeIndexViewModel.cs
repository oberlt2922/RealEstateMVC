using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RealEstateMVC.Models
{
    public class HomeIndexViewModel
    {
        public List<HouseViewModel> Houses { get; set; }
        public SelectList County { get; set; }


        public HomeIndexViewModel ()
        {
            Houses = new List<HouseViewModel>();
        }
    }
}
