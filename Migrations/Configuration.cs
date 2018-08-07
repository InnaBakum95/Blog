using NewBlogAPI.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using NewBlogAPI.Models;
using System.Data.Entity.Migrations;
using System.Linq;
using NewBlogAPI.Components.AdminComponents;
using System;

namespace NewBlogAPI.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BlogDBContext>
    {
        private readonly ApplicationUserManager _applicationUserManager;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
            _applicationUserManager = new ApplicationUserManager(new UserStore<BlogAdmin>());
        }

        protected override void Seed(BlogDBContext context)
        {
            if(!context.Roles.Any())
            {
                foreach(string iterator in RolesConstants.GetAllRoles())
                    context.Roles.Add(  new IdentityRole
                                        {
                                            Name = iterator
                                        });
            }

            if (!context.Users.Any())
            {
                var user = new BlogAdmin
                {
                    Email = "ibakum95@gmail.com",
                    PhoneNumber = "",
                    PhoneNumberConfirmed = false,
                    EmailConfirmed = false,
                    UserName = "ibakum95@gmail.com",
                    TwoFactorEnabled = false,
                    LockoutEnabled = false
                };
                _applicationUserManager.CreateAsync(user, "Administrator");
                context.Users.AddOrUpdate(user);
                context.SaveChanges();
                var test = _applicationUserManager.AddToRolesAsync(user.Id, RolesConstants.ROLE_SUPER_ADMIN);
                Console.WriteLine(""+ test.Result);
                context.SaveChanges();
            }
        }
    }
}
