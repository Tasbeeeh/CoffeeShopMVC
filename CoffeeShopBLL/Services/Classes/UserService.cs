using CoffeeShopBLL.ModelVMs.User;
using CoffeeShopBLL.Services.Interfaces;
using CoffeeShopDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager,
                           SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterVM model)
        {
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Address = new Address
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                }
            };

            return await _userManager.CreateAsync(user, model.Password);
        }
        public async Task<SignInResult> LoginAsync(LoginVM model)
        {
            ApplicationUser? user = await _userManager.FindByNameAsync(model.UserNameOrEmail)
                                       ?? await _userManager.FindByEmailAsync(model.UserNameOrEmail);

            if (user == null)
                return SignInResult.Failed;

            return await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
        }
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }

}
