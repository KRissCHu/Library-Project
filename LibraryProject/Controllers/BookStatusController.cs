using LibraryProject.Configuration;
using LibraryProject.Filter;
using LibraryProject.ViewModels;
using ProjectEntities;
using ProjectRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    [AuthenticationFilter]
    public class BookStatusController : Controller
    {
        public ActionResult Index()
        {
            BookStatusRepository rep = new BookStatusRepository(AppConfig.ConnectionString);
            List<BookStatus> booksStatus = rep.GetAll();
            return View(booksStatus);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BookStatus bookStatus)
        {
            if (!ModelState.IsValid)
            {
                return View(bookStatus);
            }
            bool IsReader = false;
            BookStatusRepository rep = new BookStatusRepository(AppConfig.ConnectionString);
            ReaderRepository repo = new ReaderRepository(AppConfig.ConnectionString);
            List<Reader> readers = repo.GetAll();

            BookRepository repos = new BookRepository(AppConfig.ConnectionString);
            bool IsTitle = false;
            List<Book> books = repos.GetAll();
            for (int i = 0; i < readers.Count; i++)
            {
                if (readers[i].Id == bookStatus.ReaderId)
                {
                    IsReader = true;
                }
            }
           
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Title == bookStatus.Title)
                {
                    IsTitle = true;
                }
            }

            if (IsTitle == true && IsReader == true)
            {
                rep.Insert(bookStatus);
                return RedirectToAction("Index");
            }
            else if (IsTitle == true && IsReader == false)
            {
                ViewBag.error = "No Reader with that Id !!!";
                return View();
            }
            else if (IsTitle == false && IsReader == true)
            {
                ViewBag.error = "No Book with that Title !!!";
                return View();
            }
            else
            {
                ViewBag.error = "Wrong Title and ReaderId !!!";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            BookStatusRepository repository = new BookStatusRepository(AppConfig.ConnectionString);
            BookStatus model = new BookStatus();

            if (id.HasValue)
            {
                BookStatus bookStatus = repository.GetById(id.Value);
                model.Id = bookStatus.Id;
                model.Title = bookStatus.Title;
                model.ReaderId = bookStatus.ReaderId;
                model.BorrowDate = bookStatus.BorrowDate;
                model.ReturnDate = bookStatus.ReturnDate;
                model.ReturnedDate = bookStatus.ReturnedDate;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BookStatus model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool IsReader = false;
            BookStatusRepository rep = new BookStatusRepository(AppConfig.ConnectionString);
            ReaderRepository repo = new ReaderRepository(AppConfig.ConnectionString);
            List<Reader> readers = repo.GetAll();
            BookStatus bookStatus = new BookStatus();
            for (int i = 0; i < readers.Count; i++)
            {
                if (readers[i].Id == model.ReaderId)
                {
                    IsReader = true;
                }
            }

            BookRepository repos = new BookRepository(AppConfig.ConnectionString);
            bool IsTitle = false;
            List<Book> books = repos.GetAll();

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Title == model.Title)
                {
                    IsTitle = true;
                }
            }

            if (IsTitle == true && IsReader == true)
            {
                bookStatus.Id = model.Id;
                bookStatus.Title = model.Title;
                bookStatus.ReaderId = model.ReaderId;
                bookStatus.BorrowDate = model.BorrowDate;
                bookStatus.ReturnDate = model.ReturnDate;
                bookStatus.ReturnedDate = model.ReturnedDate;
                rep.Save(bookStatus);
                return RedirectToAction("Index");
            }
            else if (IsTitle == true && IsReader == false)
            {
                ViewBag.error = "No Reader with that Id !!!";
                return View();
            }
            else if (IsTitle == false && IsReader == true)
            {
                ViewBag.error = "No Book with that Title !!!";
                return View();
            }
            else
            {
                ViewBag.error = "Wrong Title and ReaderId !!!";
                return View();
            }
        }


        [AuthenticationFilter(RequireAdminRole = true)]
        [HttpGet]
        public ActionResult Delete(int id)
        {

            BookStatusRepository repository = new BookStatusRepository(AppConfig.ConnectionString);
            BookStatus bookStatus = repository.GetById(id);

            return View(bookStatus);
        }

        [HttpPost]
        public ActionResult Delete(BookStatus model)
        {

            BookStatusRepository repository = new BookStatusRepository(AppConfig.ConnectionString);
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
