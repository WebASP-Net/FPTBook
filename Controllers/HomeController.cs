﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FPTBook.Models;
using FPTBook.Data;
using Microsoft.EntityFrameworkCore;
using FPTBook.Models;

namespace FPTBook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }
    public IActionResult Index(string SearchString = "")
    {
        if (SearchString != "")
        {
            var Products = _db.Products.Include(s => s.Category).Where(x => x.Name.ToUpper().Contains(SearchString.ToUpper()));
            return View(Products.ToList());
        }
    
        IEnumerable<Product> lstPro = _db.Products.ToList();
        return View(lstPro);
    }
    
    public IActionResult Search(string SearchString = "")
    {
        if (!String.IsNullOrEmpty(SearchString))
        {
            var Products = _db.Products.Include(s => s.Category).Where(x => x.Name.ToUpper().Contains(SearchString.ToUpper()));
            return View(Products.ToList());
        }
    
        IEnumerable<Product> lstPro = _db.Products.ToList();
        return View(lstPro);
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
