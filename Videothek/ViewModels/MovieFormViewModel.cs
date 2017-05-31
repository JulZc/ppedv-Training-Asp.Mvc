using System.Collections.Generic;
using VideothekData.Models;

namespace Videothek.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }
        public List<GenreVm> Genres { get; set; }
    }
}