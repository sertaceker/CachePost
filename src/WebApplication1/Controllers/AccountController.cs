using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CachePostRest.Models;

namespace CachePostRest.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<MyIdentityUser> userManager;
        private readonly SignInManager<MyIdentityUser> loginManager;
        private readonly RoleManager<MyIdentityRole> roleManager;

        public AccountController(UserManager<MyIdentityUser> userManager, SignInManager<MyIdentityUser> loginManager, RoleManager<MyIdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.loginManager = loginManager;
            this.roleManager = roleManager;
        }

        [HttpPost]
        public bool Register(AccountRegisterModel model)
        {
            MyIdentityUser user = new MyIdentityUser();
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FullName = model.FullName;
            user.BirthDate = model.BirthDate;


            IdentityResult result = userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                if (!roleManager.RoleExistsAsync("NormalUser").Result)
                {
                    MyIdentityRole role = new MyIdentityRole();
                    role.Name = "NormalUser";
                    role.Description = "Perform normal operations.";
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                    if (!roleResult.Succeeded)
                    {
                        ModelState.AddModelError("",
                         "Error while creating role!");
                    }
                }

                userManager.AddToRoleAsync(user,
                             "NormalUser").Wait();
            }

            return result.Succeeded;
        }
    }
}
