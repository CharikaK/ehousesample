using Ehouse.Data.Enitities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ehouse.Data
{
    public class ApplicationSeeder
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<ApplicationUser> _usermanager;

        public ApplicationSeeder(ApplicationDbContext ctx, IHostingEnvironment hosting, 
            UserManager<ApplicationUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _usermanager = userManager;
        }

        public async Task Seed()
        {
            //12 - 14
            var user = await _usermanager.FindByEmailAsync("customer3@ehouse.com");
            if(user==null)
            {
                user = new ApplicationUser()
                {
                    Firstname = "Charika2",
                    Lastname = "Kiriwandala",
                    Email = "charika2.kiriwandala@ehouse.com",
                    Postcode = "UB60NP",
                    UserName = "charika2.kiriwandala@ehouse.com"
                };
                //14
                var result = await _usermanager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }
        }
        // add the user to Booking - TO DO
    }
}
