using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Identity
{
    public static class AppIdentitySeed
    {
       public static async Task AppUserSeed(UserManager<AppUser> userManager)
        {

            if (!userManager.Users.Any())
            {


                var User = new AppUser()
                {
                    DisplayName = "Mohamed Salah",
                    Email = "mattar17@gmail.com",
                    UserName = "mattar17",
                    PhoneNumber = "8845845454"
                };

                await userManager.CreateAsync(User,"P@assw0rd");

            }

        }
        }
    }
