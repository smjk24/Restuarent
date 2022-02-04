using Restuarent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarent.Controllers
{
    public class LoginController : Microsoft.AspNetCore.Mvc.Controller
    {
        public readonly ApplicationDbContext context;
        public LoginController(ApplicationDbContext _db)
        {
            this.context = _db;
        }
        //ApplicationDbContext context = new ApplicationDbContext();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Login data)
        {
            data.Id = context.Logins.Select(x => x.Id).FirstOrDefault();
            if (context.Logins.FirstOrDefault(x => x.UserName.Equals(data.UserName) && x.Password.Equals(data.Password)) != null)
            {
                HttpContext.Session.SetString("UserName", data.UserName);
                HttpContext.Session.SetString("PassWord", data.Password);
                HttpContext.Session.SetInt32("Id", data.Id);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.SetString("UserName", "");
            return RedirectToAction("Index", "Login");
        }
    }
}
