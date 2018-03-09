using LibraryProject.Configuration;
using LibraryProject.Filter;
using ProjectEntities;
using ProjectRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class ReaderController : Controller
    {
        [AuthenticationFilter]
        public ActionResult Index()
        {
            ReaderRepository rep = new ReaderRepository(AppConfig.ConnectionString);
            List<Reader> readers = rep.GetAll();
            return View(readers);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Reader reader)
        {
            if (!ModelState.IsValid)
            {
                return View(reader);
            }
            ReaderRepository rep = new ReaderRepository(AppConfig.ConnectionString);
            if (Regex.IsMatch(reader.FirstName, @"^[a-zA-Z]+$") == true && Regex.IsMatch(reader.FamilyName, @"^[a-zA-Z]+$") == true)
            {
                rep.Insert(reader);
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

            ReaderRepository repository = new ReaderRepository(AppConfig.ConnectionString);
            Reader model = new Reader();


            if (id.HasValue)
            {
                Reader reader = repository.GetById(id.Value);
                model.Id = reader.Id;
                model.FirstName = reader.FirstName;
                model.FamilyName = reader.FamilyName;
                model.CardNumber = reader.CardNumber;
                model.ExpCardDate = reader.ExpCardDate;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Reader model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ReaderRepository repository = new ReaderRepository(AppConfig.ConnectionString);
            if (Regex.IsMatch(model.FirstName, @"^[a-zA-Z]+$") == true && Regex.IsMatch(model.FamilyName, @"^[a-zA-Z]+$") == true)
            {
                Reader reader = new Reader();
                reader.Id = model.Id;
                reader.FirstName = model.FirstName;
                reader.FamilyName = model.FamilyName;
                reader.CardNumber = model.CardNumber;
                reader.ExpCardDate = model.ExpCardDate;
                repository.Save(reader);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Wrong Name !!!";
                return View();
            }

        }
        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Delete(int id)
        {

            ReaderRepository repository = new ReaderRepository(AppConfig.ConnectionString);
            Reader reader = repository.GetById(id);

            return View(reader);
        }

        [HttpPost]
        public ActionResult Delete(Reader model)
        {

            ReaderRepository repository = new ReaderRepository(AppConfig.ConnectionString);
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

