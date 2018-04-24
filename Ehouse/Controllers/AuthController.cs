// ck
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ehouse.Models;
using Ehouse.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Ehouse.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        // this is for SignIn Task
        private IUserServices _userservices;
        public AuthController(IUserServices userServices)
        {
            _userservices = userServices;

        }

        [Route("signin")]
        public IActionResult SignIn() // at the SignIn click on the menu
        {
            return View(new SignInModel());
        }
        
        // for Facebook
        [Route("fbsignIn")]

        public IActionResult FBSignIn()
        {
            // to stop the redirecting loop between the app and FB - { RedirectUri = "/" })
            // return the Challenge authentication for FB (refer startup.cs - authentication)
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }); 
        }

        [Route("signin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInModel model, string returnUrl=null) // At the Button click in the form
            {
            if (ModelState.IsValid)
            {
                // IUserServices interface properties to validate
                // User is an entity in IUserService - using Ehouse.Services;
                User user;
                if (await _userservices.ValidateCredentials(model.Username, model.Password, out user))
                {
                    await SignInUser(user.Usernmame); // Claims 
                    if (returnUrl !=null)
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index","Home");
                }

            }
            return View(model); 
        }

        // play Book
        [Route("signout")]
        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");

        }
        public async Task SignInUser(string username)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, username), //unique identifier
                new Claim("name", username)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", null); //claim type = "name", identity role is null (at this scenario)
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }
    }
}