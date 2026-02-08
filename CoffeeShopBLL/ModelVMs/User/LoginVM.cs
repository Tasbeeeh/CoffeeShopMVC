using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShopBLL.ModelVMs.User
{

        public class LoginVM
        {
            [Required(ErrorMessage = "Username or Email is required")]
            public string UserNameOrEmail { get; set; } = null!;

            [Required(ErrorMessage = "Password is required")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = null!;

            public bool RememberMe { get; set; } = false;
        }
    
}
