using LibraryProject.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectRepositories;
using ProjectEntities;
using LibraryProject.Filter;
using System.Text.RegularExpressions;

namespace LibraryProject.Controllers
{
    [AuthenticationFilter]
    public class BookController : Controller
    {

        public ActionResult Index()
        {
            BookRepository rep = new BookRepository(AppConfig.ConnectionString);
            List<Book> books = rep.GetAll();
            return View(books);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            BookRepository rep = new BookRepository(AppConfig.ConnectionString);
            if (Regex.IsMatch(book.Author, @"^[a-zA-Z]+$") == true && book.Available>=0)
            {
                rep.Insert(book);
                return RedirectToAction("Index");
            }
            else if (Regex.IsMatch(book.Author, @"^[a-zA-Z]+$") == false && book.Available >= 0)
            {
                ViewBag.error = "Wrong Author !!!";
                return View();
            }
            else if (Regex.IsMatch(book.Author, @"^[a-zA-Z]+$") == true && book.Available < 0)
            {
                ViewBag.error = "Wrong Available Number !!!";
                return View();
            }
            else 
            {
                ViewBag.error = "Wrong Author and Available Numer !!!";
                return View();
            }

        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            BookRepository repository = new BookRepository(AppConfig.ConnectionString);
            Book model = new Book();


            if (id.HasValue)
            {
                Book book = repository.GetById(id.Value);
                model.Id = book.Id;
                model.ISBN = book.ISBN;
                model.Title = book.Title;
                model.Author = book.Author;
                model.Publisher = book.Publisher;
                model.PubDate = book.PubDate;
                model.Available = book.Available;

            }

            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(Book model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            BookRepository repository = new BookRepository(AppConfig.ConnectionString);

            if (Regex.IsMatch(model.Author, @"^[a-zA-Z]+$") == true && model.Available >= 0)
            {
                Book book = new Book();
                book.Id = model.Id;
                book.ISBN = model.ISBN;
                book.Title = model.Title;
                book.Author = model.Author;
                book.Publisher = model.Publisher;
                book.PubDate = model.PubDate;
                book.Available = model.Available;            
                repository.Save(book);
                return RedirectToAction("Index");
            }
            else if (Regex.IsMatch(model.Author, @"^[a-zA-Z]+$") == false && model.Available >= 0)
            {
                ViewBag.error = "Wrong Author !!!";
                return View();
            }
            else if (Regex.IsMatch(model.Author, @"^[a-zA-Z]+$") == true && model.Available < 0)
            {
                ViewBag.error = "Wrong Available Number !!!";
                return View();
            }
            else
            {
                ViewBag.error = "Wrong Author and Available Numer !!!";
                return View();
            }

        }

        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Delete(int id)
        {

            BookRepository repository = new BookRepository(AppConfig.ConnectionString);

            Book book = repository.GetById(id);

            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(Book model)
        {

            BookRepository repository = new BookRepository(AppConfig.ConnectionString);
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
