using DotNet5.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNet5.API
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {

            //services.AddIdentity<IdentityRole, ApiUser>()
            //    .AddEntityFrameworkStores<DatabaseContext>()
            //    // Replace ApplicationUser with your concrete user class
            //      .AddUserManager<UserManager<ApiUser>>()
            //       //.AddSignInManager<SignInManager<ApiUser>>()
            //    .AddDefaultTokenProviders();

            //services.AddIdentity<IdentityRole, ApiUser>() // </-- here you have to replace `IdenityUser` and `IdentityRole` with `ApplicationUser` and `ApplicationRole` respectively
            //.AddEntityFrameworkStores<DatabaseContext>()
            //.AddDefaultTokenProviders();
            ////Addded form app
            var builder = services.AddIdentityCore<ApiUser>(a => a.User.RequireUniqueEmail = true);
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }
        public static void ConfigureJWT()
        {

        }
    }
}
