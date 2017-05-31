using System.Collections.Generic;
using VideothekData.Models;

namespace Videothek.ViewModels
{
    public class CustomerFormViewModel
    {
        public Customer Customer { get; set; }
        public List<MembershipType> Memberships { get; set; }
    }
}