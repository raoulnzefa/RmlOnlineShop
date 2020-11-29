using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RmlOnlineShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RmlOnlineShop.Application.Utils
{
    public class RoleInit
    {
        public static async Task InitUserAsync(
            UserManager<User> userManager,
            IConfiguration config
            )
        {
            if (!userManager.Users.Any())
            {
                var UserName = config["UserInit:Admin:UserName"];
                var Email = config["UserInit:Admin:Email"];
                var password = config["UserInit:Admin:Password"];

                User userAdmin = new User
                {
                    UserName = string.IsNullOrEmpty(UserName) ? "admin" : UserName,
                    Email = string.IsNullOrEmpty(Email) ? "admin@admin.com" : Email
                };

                var res = await userManager.CreateAsync(
                    userAdmin, 
                    string.IsNullOrEmpty(password) ? "password" : password);
                if (res.Succeeded)
                {
                    var AdminClaim = new Claim("Role","Admin");

                    await userManager.AddClaimAsync(userAdmin, AdminClaim);
                }


                UserName = config["UserInit:User:UserName"];
                Email = config["UserInit:User:Email"];
                password = config["UserInit:User:Password"];


                User user = new User
                {
                    UserName = string.IsNullOrEmpty(UserName) ? "user" : UserName,
                    Email = string.IsNullOrEmpty(Email) ? "user@user.com" : Email
                };

                res = await userManager.CreateAsync(
                    user,
                     string.IsNullOrEmpty(password) ? "password" : password);

                if (res.Succeeded)
                {
                    var UserClaim = new Claim("Role", "User");

                    await userManager.AddClaimAsync(user, UserClaim);
                }
            }
        }
    }
}
