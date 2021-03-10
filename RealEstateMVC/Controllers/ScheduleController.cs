using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateMVC.Data;
using RealEstateMVC.Models;

namespace RealEstateMVC.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly AppDbContext _context;

        public ScheduleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Schedule
        public async Task<IActionResult> Index(string county)
        {
            try
            {
                return View(await House.getHousesByCounty(_context, county));
            }
            catch(Exception e)
            {
                return NotFound();
            }
        }

        // GET: Schedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                return View(await House.getHouse(_context, (int)id));
            }
            catch(Exception e)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Schedule(int id, string name, string email, DateTime Date, DateTime Time)
        {
            var houseQuery = from h in _context.House
                             select h;
            houseQuery = houseQuery.Where(h => h.Id == id);

            var house = await houseQuery.FirstAsync();

            TempData["name"] = name;
            TempData["email"] = email;
            TempData["date"] = Date.ToShortDateString();
            TempData["time"] = Time.ToShortTimeString();

            return View(house);
        }

        private bool HouseExists(int id)
        {
            return _context.House.Any(e => e.Id == id);
        }
    }
}
