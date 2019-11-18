using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Entity;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        UserService userService = new UserService();
        public ActionResult Index()
        {
            var entity = new UserEntity()
            {
                Id = Guid.NewGuid(),
                UserName = "Aaron",
                Phone = "1234567890",
                Maile = "1234567@qq.com",
                Birthday = Convert.ToDateTime("1993-03-03"),
                Address = "广东省深圳市福田区",
                CreateOperator = "admin",
                CreateTime = DateTime.Now
            };
            var insert = userService.Add(entity);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}