using LibraryProject.Configuration;
using LibraryProject.ViewModels.Home;
using ProjectEntities;
using ProjectRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            UserRepository rep = new UserRepository(AppConfig.ConnectionString);
            List<User> users = rep.GetAll();
            List<User> items = new List<User>();
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == model.Username && users[i].Password == model.Password)
                {
                    items.Add(users[i]);
                }
            }
            Session["LoggedUser"] = items.Count > 0 ? items[0] : null;

            if (items.Count <= 0)
                this.ModelState.AddModelError("failedLogin", "Login failed!");

            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction("Index", "Book");
        }
    }
}
