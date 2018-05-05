using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Ehouse.Data.Enitities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        // getting all the properties from the Identity user + below properties
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
        public string Address03 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public Booking booking { get; set; }
        public int Contactnumber  { get; set; }
       


    }
}
