using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateMVC.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateMVC.Models
{
    public class House
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "County cannot contain numbers or special characters.")]
        public string County { get; set; }
        
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Bed field has to be a whole number")]
        public int Bed { get; set; }
        
        [Required]
        [RegularExpression(@"^[0-9]{1,2}(\.5)?", ErrorMessage = "Bath must be a whole number, or end in \".5\".")]
        public int Bath { get; set; }
        
        [Required]
        [Display(Name = "Square Feet")]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Square feet must be a whole number.")]
        public int SquareFeet { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = false)]
        [RegularExpression(@"[0-9]*", ErrorMessage = "Price must be a whole number.")]
        public int Price { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "City must not contain numbers or special characters.")]
        public string City { get; set; }
        
        [Required]
        [RegularExpression(@"[A-Za-z]{2}", ErrorMessage = "State must be two letters.")]
        public string State { get; set; }
        
        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"^[0-9]{5}(-[0-9]{4})?$", ErrorMessage = "Must be a valid zip code.")]
        public string Zip { get; set; }
        
        [DataType(DataType.DateTime)]
        [Display(Name = "Next Open House")]
        public DateTime NextOpenHouse { get; set; }


        //get all counties for county select list
        //get all houses by selected county
        //returns HouseListViewModel
        public async static Task<HouseListViewModel> getHousesByCounty(AppDbContext _context, string county)
        {
            IQueryable<string> countyQuery = from h in _context.House
                                             orderby h.County
                                             select h.County;

            var housesQuery = from h in _context.House
                         select h;

            if(county != null && county != "")
            {
                housesQuery = housesQuery.Where(x => x.County == county);
            }

            var HouseListVM = new HouseListViewModel
            {
                County = new SelectList(await countyQuery.Distinct().ToListAsync()),
                houses = await housesQuery.ToListAsync()
            };

            return HouseListVM;
        }


        //get house by id
        //get all houses by houseId
        //returns HouseViewModel
        public async static Task<HouseViewModel> getHouse(AppDbContext _context, int id)
        {
            //get house query
            var houseQuery = from h in _context.House
                              select h;
            houseQuery = houseQuery.Where(h => h.Id == id);

            //get images query
            var images = from i in _context.Image
                         select i;
            images = images.Where(i => i.HouseId == id);

            var HouseVM = new HouseViewModel
            {
                house = await houseQuery.FirstAsync(),
                Images = await images.ToListAsync()
            };

            return HouseVM;
        }

        //gets a HouseViewModel for every house in the selected county
        public async static Task<HomeIndexViewModel> getHomeIndexViewModel(AppDbContext _context, string county)
        {
            HomeIndexViewModel HomeIndexVM = new HomeIndexViewModel();

            //county query
            IQueryable<string> countyQuery = from h in _context.House
                                             orderby h.County
                                             select h.County;

            //house query
            var housesQuery = from h in _context.House
                              select h;

            //where house county == county
            if(county != null && county != "")
            {
                housesQuery = housesQuery.Where(h => h.County == county);
            }

            //get houses
            var houses = await housesQuery.ToListAsync();

            foreach(House property in houses)
            {
                //get images for house
                var imagesQuery = from i in _context.Image
                             select i;
                imagesQuery = imagesQuery.Where(i => i.HouseId == property.Id);

                //create house view model
                HouseViewModel HouseVM = new HouseViewModel
                {
                    //house = await housesQuery.FirstAsync(),
                    house = property,
                    Images = await imagesQuery.ToListAsync()
                };

                //add house to home index view model
                HomeIndexVM.Houses.Add(HouseVM);

                //add counties to home index view model
                HomeIndexVM.County = new SelectList(await countyQuery.Distinct().ToListAsync());
            }

            return HomeIndexVM;
        }
    }
}
