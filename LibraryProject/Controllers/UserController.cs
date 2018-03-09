using LibraryProject.Configuration;
using ProjectEntities;
using ProjectRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryProject.Filter;
using System.Text.RegularExpressions;

namespace LibraryProject.Controllers
{
    [AuthenticationFilter(RequireAdminRole = true)]
    public class UserController : Controller
    {

        public ActionResult Index()
        {
            UserRepository rep = new UserRepository(AppConfig.ConnectionString);
            List<User> users = rep.GetAll();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            UserRepository rep = new UserRepository(AppConfig.ConnectionString);
            if (Regex.IsMatch(user.FirstName, @"^[a-zA-Z]+$") == true && Regex.IsMatch(user.FamilyName, @"^[a-zA-Z]+$") == true)
            {
                rep.Insert(user);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Wrong Name !!!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            UserRepository repository = new UserRepository(AppConfig.ConnectionString);
            User model = new User();


            if (id.HasValue)
            {
                User user = repository.GetById(id.Value);
                model.Id = user.Id;
                model.Username = user.Username;
                model.Password = user.Password;
                model.FirstName = user.FirstName;
                model.FamilyName = user.FamilyName;
                model.Authority = user.Authority;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(User model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UserRepository repository = new UserRepository(AppConfig.ConnectionString);
            User user = new User();
            if (Regex.IsMatch(model.FirstName, @"^[a-zA-Z]+$") == true && Regex.IsMatch(model.FamilyName, @"^[a-zA-Z]+$") == true)
            {
                user.Id = model.Id;
                user.Username = model.Username;
                user.Password = model.Password;
                user.FirstName = model.FirstName;
                user.FamilyName = model.FamilyName;
                user.Authority = model.Authority;
                repository.Save(user);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Wrong Name !!!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            UserRepository repository = new UserRepository(AppConfig.ConnectionString);
            User user = repository.GetById(id);

            return View(user);
        }

        [HttpPost]
        public ActionResult Delete(User model)
        {

            UserRepository repository = new UserRepository(AppConfig.ConnectionString);
            if (model.Id.ToString() != String.Empty)
            {
                repository.Delete(model.Id);
            }


            return RedirectToAction("Index");
        }

        public ActionResult Users()
        {
            return RedirectToAction("Index", "User");
        }

        public ActionResult Books()
        {
            return RedirectToAction("Index", "Book");
        }

        public ActionResult Readers()
        {
            return RedirectToAction("Index", "Reader");
        }

        public ActionResult BookStatus()
        {
            return RedirectToAction("Index", "BookStatus");
        }
    }

}

