using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Abstract.Manager.Projection;
using BLL.Manager;

namespace UI.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        private readonly IBookManager _bookManager = new BookManager();
        public ActionResult Index()
        {
            var allBooks = _bookManager.GetAll().ToList();
            return View(allBooks);
        }
    }
}