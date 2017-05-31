using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideothekData.Models
{
    public class Movie
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Range(1, 20)]
        [Display(Name="Number in Stock")]
        public int NumberInStock { get; set; }

        [Display(Name="Release Date")]
        public DateTime Released { get; set; }

        public List<Genre> Genres { get; set; }
    }
}
