using Restuarent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restuarent.Controllers
{
    public class Controller : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ApplicationDbContext context;
        public Controller(ApplicationDbContext _db)
        {
            context = _db;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var x = HttpContext.Session.GetString("UserName");
                var y = HttpContext.Session.GetString("PassWord");
                var z = context.Logins.FirstOrDefault(s => s.UserName == x && s.Password == y);
                if (z == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new { action = "Index", controller = "Login", area = "" });//RedirectToActionResult("Index", "Login", null);
                    return;
                }
            }
            catch
            {

            }

        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}
