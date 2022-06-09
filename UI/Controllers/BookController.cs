using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Abstract.Manager.Projection;
using BLL.Manager;
using UI.Models;

namespace UI.Controllers
{
    public class BookController : Controller
    {

        // GET: Book
        private readonly IBookManager _bookManager = new BookManager();
        private readonly IPublisherManager _publisherManager = new PublisherManager();
        public ActionResult Index()
        {
            var allBooks = _bookManager.GetAll().ToList();
            return View(allBooks);
        }
        public ActionResult HomePage()
        {
            var allBooks = _bookManager.GetAll().ToList();
            return View(allBooks);
        }

        public ActionResult Details(int id)
        {
            if (id == 0 ) RedirectToAction(actionName: "BadRequest",controllerName: "HttpErrors");
            var specificBook = _bookManager.GetByID(id);
            int publisherFKTemp = specificBook.PublisherFK;
            ViewBag.Publisher = _publisherManager.GetByID(publisherFKTemp).Name;
            return View(specificBook);
        }


    }
}