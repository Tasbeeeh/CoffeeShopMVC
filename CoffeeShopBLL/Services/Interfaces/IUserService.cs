using CoffeeShopBLL.ModelVMs.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.Services.Interfaces
{
    public interface IUserService
    {
       Task<IdentityResult> CreateUserAsync(RegisterVM model);
        Task<SignInResult> LoginAsync(LoginVM model);

        Task LogoutAsync();
    }
}
