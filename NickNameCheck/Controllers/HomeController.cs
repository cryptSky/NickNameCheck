using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NickNameCheck.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //using (var db = new UserDB())
            //{
  
            //    var user0 = new User { Name = "Vasya", NickName = "Pupkin" };
            //    var user1 = new User { Name = "Vova", NickName = "Vovkin" };

            //    db.Users.Add(user0);
            //    db.Users.Add(user1);
            //    db.SaveChanges();

            //    // Display all Blogs from the database 
            //    var query = from b in db.Users
            //                orderby b.Name
            //                select b;

            //    Console.WriteLine("All blogs in the database:");
            //    foreach (var item in query)
            //    {
            //        Console.WriteLine(item.Name);
            //    }

            //}

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
