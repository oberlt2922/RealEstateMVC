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
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index(string county)
        {
            return View(await House.getHomeIndexViewModel(_context, county));
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return View(await House.getHouse(_context, (int)id));
        }

        private bool HouseExists(int id)
        {
            return _context.House.Any(e => e.Id == id);
        }
    }
}
