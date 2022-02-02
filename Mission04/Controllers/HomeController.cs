using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission04.Models;

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

        public IActionResult Movies()
        {
            var movies = GenContext.responses
                .Include(x => x.Category)
                .OrderBy(x => x.Title)
                .ToList();

            return View(movies);
        }

        [HttpGet]
        public IActionResult Edit(int movieid)
        {
            ViewBag.Categories = GenContext.Category.ToList();

            var editMovie = GenContext.responses.Single(x => x.MovieID == movieid);

            return View("AddMovie", editMovie);
        }

        [HttpPost]
        public IActionResult Edit(ApplicationResponse mv)
        {
            GenContext.Update(mv);
            GenContext.SaveChanges();
            return RedirectToAction("Movies");
        }

        [HttpGet]
        public IActionResult Delete(int movieid)
        {
            var deleteMovie = GenContext.responses.Single(x => x.MovieID == movieid);

            return View(deleteMovie);
        }

        [HttpPost]
        public IActionResult Delete(ApplicationResponse mv)
        {
            GenContext.responses.Remove(mv);
            GenContext.SaveChanges();
            return RedirectToAction("Movies");
        }
    }
}
