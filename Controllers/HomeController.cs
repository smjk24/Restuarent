using Restuarent.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Restuarent.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webhostenvironment;
        private readonly ApplicationDbContext context;
        public HomeController(IWebHostEnvironment IWebHostEnvironment, ApplicationDbContext _db) : base(_db)
        {
            _webhostenvironment = IWebHostEnvironment;
            context = _db;
        }
        //ApplicationDbContext context = new ApplicationDbContext();
        [HttpGet]
        public IActionResult Index(string search,int page=1)
        {
            IEnumerable<Menu> data = context.Menus;
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.Name.Contains(search));
                ViewBag.search = search;
            }
                
            data=data.ToPagedList(page, 3);
            return View(data);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Menu data)
        {
            string uniqueFileName = null;
            try
            {
                if (data.iformfile != null)
                {
                    string uploadFolder = Path.Combine(_webhostenvironment.WebRootPath, "demoImages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + data.iformfile.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    await data.iformfile.CopyToAsync(new FileStream(filePath, FileMode.Create));
                }
                data.Image = uniqueFileName;
                data.CDate = DateTime.Now;
                data.CLoginId = (int)HttpContext.Session.GetInt32("Id");
                await context.Menus.AddAsync(data);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return View(context.Menus.Find(id));
        }
        [HttpPost]
        public IActionResult Edit(Menu data)
        {
            string uniqueFileName = null;
            if (ModelState.IsValid)
            {

                if (data.iformfile != null && data.Image != null)
                {
                    try
                    {
                        string fileName = Path.Combine(_webhostenvironment.WebRootPath, "demoImages") + $@"\{ data.Image}";
                        if ((System.IO.File.Exists(fileName)))
                        {
                            System.IO.File.Delete(fileName);
                        }
                    }
                    catch (Exception e)
                    {
                        System.Threading.Thread.Sleep(100);

                    }
                }
                if (data.iformfile != null)
                {
                    string uploadFolder = Path.Combine(_webhostenvironment.WebRootPath, "demoImages");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + data.iformfile.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    var fs = new FileStream(filePath, FileMode.Create);
                    data.iformfile.CopyToAsync(fs);
                    data.Image = uniqueFileName;
                   // context.Add(data);
                    fs.Close();
                    fs.DisposeAsync();

                }
                else
                {
                    context.Entry(data).Property(s => s.Image).IsModified = false;
                }

                data.MLoginId = (int)HttpContext.Session.GetInt32("Id");
                data.MDate = DateTime.Now;
                context.Entry(data).State = EntityState.Modified;
                context.Entry(data).Property(x => x.CLoginId).IsModified = false;
                context.Entry(data).Property(x => x.CDate).IsModified = false;
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var data = await context.Menus.FindAsync(id);
            try
            {
                string fileName = Path.Combine(_webhostenvironment.WebRootPath, "demoImages") + $@"\{ data.Image}";
                if ((System.IO.File.Exists(fileName)))
                {
                    System.IO.File.Delete(fileName);
                }
                context.Entry(data).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Threading.Thread.Sleep(100);
                throw e;

            }
            return RedirectToAction("Index");
        }
    }
}
