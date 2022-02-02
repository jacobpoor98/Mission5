using System;
using System.ComponentModel.DataAnnotations;

namespace Mission04.Models
{
    // stores the category Id's and names
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
