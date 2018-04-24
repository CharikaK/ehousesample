    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ehouse.Data;
using Ehouse.Models;
using Ehouse.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace Ehouse
{
    public class Startup
    {
        

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {         
                   
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();                     


            // to make sure application is using Https - Global
            // AddMvc is already here
            services.AddMvc(options=> {
                options.Filters.Add(new RequireHttpsAttribute());
            });

            // create dummy users - from Playbook
           var users = new Dictionary<string, string> { { "Charika", "password" }, { "EHouse","admin"} };
            services.AddSingleton<IUserServices>(new DummyUserServices(users));

            //for SignInUser() - from Playbook
            //1. AddAuthentication, 2. AddFacebook 3. AddCookie - this is user to sign in - authentication support ,
            //4. options is AddAuthentication 
            //  to say what athutnetication should be used and when, add configuration call backs
            //  DefaultChallengeScheme - facebook is the challenge to the user
            //  DefaultSignInScheme - defualt signin after Authentication has taken place - using cookies
            //  DefaultAuthenticateScheme - what scheme to be used as defualt when incoming requests - cookie

            services.AddAuthentication(options => {
                options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                
                //options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                
            }).AddCookie(options => {
                options.LoginPath = "/auth/signin";//- call back
            }).AddFacebook(facebookOptions =>
            {
                // this option set up is called set up configuration call back
                facebookOptions.AppId = "204769560288245";
                facebookOptions.AppSecret = "1cec2996f93c649209aee5f33798a841";
            });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //any request over Http get redirect to Https
            app.UseRewriter(new RewriteOptions().AddRedirectToHttps(301, 44390));

            app.UseAuthentication(); // SignIn - authentication    

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            
        }
    }
}
