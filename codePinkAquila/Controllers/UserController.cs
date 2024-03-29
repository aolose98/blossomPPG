using System.Diagnostics;
using codePinkAquila.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;


public class UserController : Controller
{
    private MyContext db;
    // Here we can "inject" our context service into the constructor 
    public UserController(MyContext context)
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

    [HttpGet("")]
    public IActionResult Home()
    {
        if (HttpContext.Session.GetInt32("uid") != null)
        {
            return RedirectToAction("CodePink", "Product");
        }
        return View("Landing");
    }


    [HttpGet("/welcome")]
    public IActionResult LogReg()
    {
        if (HttpContext.Session.GetInt32("uid") != null)
        {
            return RedirectToAction("CodePink", "Product");
        }
        else
        {
            return View("LogReg");
        }
    }

    [HttpPost("/register")]
    public IActionResult Register(User newUser)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine("I am here 1");
            return View("LogReg");
        }
        else
        {
            PasswordHasher<User> hash = new PasswordHasher<User>(); // This creates a new instance of the password hasher so that we can use it on the next line
            newUser.Password = hash.HashPassword(newUser, newUser.Password);
            // let newUser.Password equal a hashed version of the password
            db.Users.Add(newUser);
            db.SaveChanges();

            HttpContext.Session.SetInt32("uid", newUser.UserId);
            HttpContext.Session.SetString("name", newUser.FirstName + " " + newUser.LastName);
            Console.WriteLine("I am here 2");
            return RedirectToAction("CodePink", "Product");
        }
    }

    [HttpPost("/login")]
    public IActionResult Login(LoginUser userSubmission)
    {
        if (!ModelState.IsValid)
        {
            return View("LogReg");
        }

        // If initial ModelState is valid, query for a user with the provided email        
        User? userInDb = db.Users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);
        // If no user exists with the provided email        
        if (userInDb == null)
        {
            // Add an error to ModelState and return to View!            
            ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
            return View("LogReg");
        }
        // Otherwise, we have a user, now we need to check their password                 
        // Initialize hasher object        
        PasswordHasher<LoginUser> hashbrowns = new PasswordHasher<LoginUser>();
        // Verify provided password against hash stored in db        
        var result = hashbrowns.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);                                    // Result can be compared to 0 for failure        
        if (result == 0)
        {
            ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
            return View("LogReg");
        }

        // Handle success (this should route to an internal page)  
        HttpContext.Session.SetInt32("uid", userInDb.UserId);

        return RedirectToAction("CodePink", "Product");
    }

    [HttpGet("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Home");
    }
}