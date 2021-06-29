using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using RealEstateMVC.Data;
using RealEstateMVC.Models;
using SmartyStreets;
using SmartyStreets.USStreetApi;
using SmartyStreets.USAutocompleteApi;

namespace RealEstateMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index(string County)
        {
            return View(await House.getHousesByCounty(_context, County));
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,County,Price,Bed,Bath,SquareFeet,Address,City,State,Zip")] House house, string date, string time)
        {
            if (ModelState.IsValid && USStreetSingleAddress.ValidateAddress(house.Address, house.City, house.State, house.Zip) == true)
            {
                //capitalize first letter of county
                string firstLetter = house.County.Substring(0, 1).ToUpper();
                house.County = firstLetter + house.County.Substring(1);

                DateTime openHouseDate = Convert.ToDateTime(date + " " + time);
                house.NextOpenHouse = openHouseDate;

                //add house
                _context.Add(house);
                await _context.SaveChangesAsync();

                //add images
                InsertImages(house);
            }
            else
            {
                TempData["ErrorMessage"] = "The property that you entered does not exist.\n" +
                    "Please ensure that the street address, city, state, and zip code you enter are valid.";
                return View(house);
            }
            return RedirectToAction(nameof(Index));
        }

        //GET:Admin/Details
        public async Task<IActionResult> Details(int id)
        {
            return View(await House.getHouse(_context, id));
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await House.getHouse(_context, (int)id));
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,County,Price,Bed,Bath,SquareFeet,Address,City,State,Zip")] House house, string date, string time)
        {
            if (id != house.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && USStreetSingleAddress.ValidateAddress(house.Address, house.City, house.State, house.Zip) == true)
            {
                try
                {

                    //capitalize first letter of county
                    string firstLetter = house.County.Substring(0, 1).ToUpper();
                    house.County = firstLetter + house.County.Substring(1);

                    DateTime openHouseDate = Convert.ToDateTime(date + " " + time);
                    house.NextOpenHouse = openHouseDate;

                    //update house
                    _context.Update(house);
                    await _context.SaveChangesAsync();

                    //add images
                    InsertImages(house);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HouseExists(house.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "The property that you entered does not exist.\n" +
                    "Please ensure that the street address, city, state, and zip code you enter are valid.";
                return View(await House.getHouse(_context, (int)id));
            }
        }

        //POST: Admin/AutoComplete
        [HttpPost]
        public JsonResult AutoComplete(string prefix)
        {
            var authId = Environment.GetEnvironmentVariable("SMARTY_AUTH_ID", EnvironmentVariableTarget.Machine);
            var authToken = Environment.GetEnvironmentVariable("SMARTY_AUTH_TOKEN", EnvironmentVariableTarget.Machine);

            var client = new ClientBuilder(authId, authToken).BuildUsAutocompleteApiClient();
            var lookup = new SmartyStreets.USAutocompleteApi.Lookup(prefix);
            lookup.GeolocateType = "null";
            lookup.MaxSuggestions = 5;

            client.Send(lookup);

            return Json(lookup.Result);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var house = await _context.House
                .FirstOrDefaultAsync(m => m.Id == id);
            if (house == null)
            {
                return NotFound();
            }

            return View(house);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int id, int imageId)
        {
            House house = await _context.House.FindAsync(id);
            Image image = await _context.Image.FindAsync(imageId);
            _context.Image.Remove(image);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = id, house = house });
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //remove hosue
            var house = await _context.House.FindAsync(id);
            _context.House.Remove(house);
            await _context.SaveChangesAsync();

            //remove images
            var imagesQuery = from i in _context.Image
                         select i;
            imagesQuery = imagesQuery.Where(i => i.HouseId == id);
            var images = await imagesQuery.ToListAsync();
            _context.Image.RemoveRange(images);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool HouseExists(int id)
        {
            return _context.House.Any(e => e.Id == id);
        }

        private void InsertImages(House house)
        {
            //add images
            foreach (var file in Request.Form.Files)
            {
                Image img = new Image();
                img.ImageTitle = file.FileName;
                img.HouseId = house.Id;

                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                img.ImageData = ms.ToArray();

                string imageBase64Data = Convert.ToBase64String(img.ImageData);
                string imageDataUrl = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
                img.ImageDataUrl = imageDataUrl;

                ms.Close();
                ms.Dispose();

                _context.Image.Add(img);
                _context.SaveChanges();
            }
        }
    }



    //smartystreets API class
    internal static class USStreetSingleAddress
    {
        public static bool ValidateAddress(string address, string city, string state, string zip)
        {
            var authId = Environment.GetEnvironmentVariable("SMARTY_AUTH_ID", EnvironmentVariableTarget.Machine);
            var authToken = Environment.GetEnvironmentVariable("SMARTY_AUTH_TOKEN", EnvironmentVariableTarget.Machine);

            var client = new ClientBuilder(authId, authToken).BuildUsStreetApiClient();

            var lookup = new SmartyStreets.USStreetApi.Lookup
            {
                Street = address,
                City = city,
                State = state,
                ZipCode = zip,
                MatchStrategy = SmartyStreets.USStreetApi.Lookup.STRICT
            };

            try
            {
                client.Send(lookup);
            }
            catch (SmartyException ex)
            {
                //handle exceptions
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            catch (IOException ex)
            {
                //handle exceptions
                Console.WriteLine(ex.StackTrace);
            }

            var candidates = lookup.Result;

            if (candidates.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
