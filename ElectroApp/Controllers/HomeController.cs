﻿
using ElectroApp.DAL;
using ElectroApp.Models;
using ElectroApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM
            {
                IntroSliders = _context.IntroSliders.ToList(),
                Products = _context.Products.Include(p => p.ProductComments).ThenInclude(p => p.AppUser).Include(p => p.Brand).Include(p => p.ProductCategories).
                ThenInclude(pc => pc.Category).Include(p => p.ProductImages).Include(p => p.Campaign).
                Include(p => p.Features).Include(p => p.Specs).ToList()
            };
            return View(homeVM);
        }
        public JsonResult Search()
        {
            List<Product> products = _context.Products.Include(p=>p.ProductImages).ToList();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(products, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value);
        }
    }
}
