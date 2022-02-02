using System;
using Microsoft.EntityFrameworkCore;

namespace Mission04.Models
{
    public class MoviesContext: DbContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options) : base(options)
        {
        }

        // this access the application response
        public DbSet<ApplicationResponse> responses { get; set; }
        public DbSet<Category> Category { get; set; }

        // the following seeds the data with 3 entries
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName="Action/Adventure"},
                new Category { CategoryId = 2, CategoryName="Comedy"},
                new Category { CategoryId = 3, CategoryName="Drama"},
                new Category { CategoryId = 4, CategoryName="Family"},
                new Category { CategoryId = 5, CategoryName="Horror/Suspense"},
                new Category { CategoryId = 6, CategoryName="Miscellaneous"},
                new Category { CategoryId = 7, CategoryName="Television"},
                new Category { CategoryId = 8, CategoryName="VHS"}
            );

            mb.Entity<ApplicationResponse>().HasData(
                new ApplicationResponse
                {
                    MovieID = 1,
                    CategoryId = 5,
                    Title = "I Am Legend",
                    Year = 2007,
                    Director = "Francis Lawrence",
                    Rating = "PG-13",
                    Edited = false,
                    LentTo = "",
                    Notes = ""
                },
                new ApplicationResponse
                {
                    MovieID = 2,
                    CategoryId = 1,
                    Title = "Hacksaw Ridge",
                    Year = 2016,
                    Director = "Mel Gibson",
                    Rating = "R",
                    Edited = false,
                    LentTo = "",
                    Notes = "Such an inspiring movie!"
                },
                new ApplicationResponse
                {
                    MovieID = 3,
                    CategoryId = 2,
                    Title = "Jumanji: The Next Level",
                    Year = 2017,
                    Director = "Jake Kasdan",
                    Rating = "PG-13",
                    Edited = false,
                    LentTo = "",
                    Notes = ""
                }
            );
        }
    }
}
