using System.Collections.Generic;
using VideothekData.Models;

namespace Videothek.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}