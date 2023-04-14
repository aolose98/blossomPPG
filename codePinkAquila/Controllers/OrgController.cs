using System.Diagnostics;
using codePinkAquila.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;


public class OrgController : Controller
{
    private MyContext db;
    // Here we can "inject" our context service into the constructor 
    public OrgController(MyContext context)
    {
        // When our UserController is instantiated, it will fill in db with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        db = context;
    }

    private int? uid
    {
        get
        {
            return HttpContext.Session.GetInt32("uid");
        }
    }

    [HttpGet("codepink/laelynn")]
    public IActionResult Laelynn()
    {
        return View("Laelynn");
    }
}