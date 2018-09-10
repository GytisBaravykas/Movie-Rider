using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieRider.Models;

namespace MovieRider.ViewModel
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}
