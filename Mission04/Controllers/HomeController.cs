using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission04.Models;

// note that I removed unneeded pages such as privacy page
namespace Mission04.Controllers
{
    public class HomeController : Controller
    {

        // reference context file
        private MoviesContext GenContext { get; set; }

        // create home controller
        public HomeController(MoviesContext MovieEntry)
        {
            GenContext = MovieEntry;
        }

        // call index page
        public IActionResult Index()
        {
            return View();
        }

        // call podcast lists
        public IActionResult MyPodcasts()
        {
            return View();
        }

        // call the add movie form
        [HttpGet]
        public IActionResult AddMovie()
        {
            ViewBag.Categories = GenContext.Category.ToList();

            return View();
        }

        // submit the contents (ar) of the form and redirect to a confirmation page
        [HttpPost]
        public IActionResult AddMovie(ApplicationResponse ar)
        {
            if (ModelState.IsValid)
            {
                GenContext.Add(ar);
                GenContext.SaveChanges();
                return View("Confirmation", ar);
            }
            else
            {
                ViewBag.Categories = GenContext.Category.ToList();

                return View(ar);
            }
        }

        // provide a list of all movies (including category names from category table)
        public IActionResult Movies()
        {
            var movies = GenContext.responses
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .ToList();

            return View(movies);
        }

        // call an edit function that reroutes them to a prefield add movie form
        [HttpGet]
        public IActionResult Edit(int movieid)
        {
            ViewBag.Categories = GenContext.Category.ToList();

            var editMovie = GenContext.responses.Single(x => x.MovieID == movieid);

            return View("AddMovie", editMovie);
        }

        // call back the movie list page once finished editing via 'submit'
        // also passes the mv variable containing which record to edit
        [HttpPost]
        public IActionResult Edit(ApplicationResponse mv)
        {
            GenContext.Update(mv);
            GenContext.SaveChanges();
            return RedirectToAction("Movies");
        }

        // call a delete confirmation page
        [HttpGet]
        public IActionResult Delete(int movieid)
        {
            var deleteMovie = GenContext.responses.Single(x => x.MovieID == movieid);

            return View(deleteMovie);
        }

        // call back the movie list pgae once finished deleting via 'delete'
        // also passes the mv variable containing which record to delete
        [HttpPost]
        public IActionResult Delete(ApplicationResponse mv)
        {
            GenContext.responses.Remove(mv);
            GenContext.SaveChanges();
            return RedirectToAction("Movies");
        }
    }
}
