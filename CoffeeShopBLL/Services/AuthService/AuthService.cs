using CoffeeShopDAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.AuthService
{
    public class AuthService // Changed from 'internal' to 'public'
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Constructor: receives the managers via Dependency Injection
        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Method 1: REGISTER a new user
        public async Task<IdentityResult> RegisterUserAsync(string username, string email, string password)
        {
            // 1. Create a new ApplicationUser object
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
                // The Address and Cart will be handled separately if needed
            };

            // 2. Use UserManager to create the user and hash the password securely
            // This is the core business logic for registration
            var result = await _userManager.CreateAsync(user, password);

            // 3. Return the result (success or a list of errors)
            return result;
        }

        // Method 2: VALIDATE LOGIN (Password Sign In)
        public async Task<SignInResult> ValidateLoginAsync(string username, string password, bool rememberMe = false)
        {
            // This method handles the core login logic using SignInManager
            var result = await _signInManager.PasswordSignInAsync(
                username,          // The username/email they entered
                password,          // The plain text password they entered
                rememberMe,        // Whether to persist the login cookie
                lockoutOnFailure: false  // Optional: lock account after failed attempts
            );

            // Result can be: Success, Failed, LockedOut, etc.
            return result;
        }

        // (Optional) Method 3: Get User by Name
        public async Task<ApplicationUser> GetUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
    }
}