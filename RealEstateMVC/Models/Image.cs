using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateMVC.Data;

namespace RealEstateMVC.Models
{
    public class Image
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageDataUrl { get; set; }
    }
}
