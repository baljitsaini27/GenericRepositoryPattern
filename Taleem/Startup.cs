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
using Taleem.Data;
using Taleem.Models;
using Taleem.Services;
using Taleem.BAL.Interfaces;
using Taleem.BAL.Repositories;
using Taleem.Data.TaleemEntities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Taleem.Common;

namespace Taleem
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

            services.AddDbContext<TaleemDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/Login";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = new TimeSpan(30, 0, 0, 0);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie(options =>
            {
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = new TimeSpan(30, 0, 0, 0);
            });


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IUserRepository, UserRepository>();

            //services.AddMvc();

            services.AddMvc(options =>
            {
                //an instant  
                //options.Filters.Add(new loggedInuser());
                //By the type  
                options.Filters.Add(typeof(loggedInuser));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
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

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Dashboard}/{id?}");
            });
            //CreateRolesAndAdminUser(serviceProvider);
        }


        private static void CreateRolesAndAdminUser(IServiceProvider serviceProvider)
        {
            bool IsNewRole = false;
            //string[] roleNames = { "Admin", "EA", "SubAdmin" };
            string[] roleNames = { "SuperAdmin" };

            foreach (string roleName in roleNames)
            {
                IsNewRole = CreateRole(serviceProvider, roleName);
            }

            // Get these value from "appsettings.json" file.
            if (IsNewRole)
            {
                string adminUserEmail = "baljitsaini27@gmail.com";
                string adminPwd = "Admin@123456";
                AddUserToRole(serviceProvider, adminUserEmail, adminPwd, "SuperAdmin");
            }
        }

        /// <summary>
        /// Create a role if not exists.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="roleName">Role Name</param>
        private static bool CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            bool IsNewRole = false;
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
                IsNewRole = true;
            }
            return IsNewRole;
        }

        /// <summary>
        /// Add user to a role if the user exists, otherwise, create the user and adds him to the role.
        /// </summary>
        /// <param name="serviceProvider">Service Provider</param>
        /// <param name="userEmail">User Email</param>
        /// <param name="userPwd">User Password. Used to create the user if not exists.</param>
        /// <param name="roleName">Role Name</param>
        private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail,
            string userPwd, string roleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            Task<ApplicationUser> checkAppUser = userManager.FindByEmailAsync(userEmail);
            checkAppUser.Wait();

            ApplicationUser appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                ApplicationUser newAppUser = new ApplicationUser
                {
                    Email = userEmail,
                    UserName = userEmail,
                    EmailConfirmed = true,
                };

                Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(newAppUser, userPwd);
                taskCreateAppUser.Wait();

                if (taskCreateAppUser.Result.Succeeded)
                {
                    appUser = newAppUser;

                    Users obj = new Users();
                    obj.FirstName = "Baljeet";
                    obj.LastName = "Singh";
                    obj.Email = "baljitsaini27@gmail.com";
                    obj.SchoolId = 1;
                    obj.UserType = (int)clsEnum.enumUserType.SuperAdmin; 
                    obj.Status = (int)clsEnum.enumUserStatus.Active;
                    obj.CreatedDate = DateTime.UtcNow;
                    serviceProvider.GetRequiredService<IUserRepository>().Insert(obj);
                }
            }

            var role = userManager.GetRolesAsync(appUser);
            if (role.Result.Count == 0)
            {
                Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
                newUserRole.Wait();
            }
        }

    }
}
