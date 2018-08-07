using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using NewBlogAPI.Components.AdminComponents;
using NewBlogAPI.Data;

namespace NewBlogAPI
{
    // Настройка диспетчера пользователей приложения. UserManager определяется в ASP.NET Identity и используется приложением.
    public class ApplicationUserManager : UserManager<BlogAdmin>
    {
        public ApplicationUserManager(IUserStore<BlogAdmin> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<BlogAdmin>(context.Get<BlogDBContext>()));
            // Настройка логики проверки имен пользователей
            manager.UserValidator = new UserValidator<BlogAdmin>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Настройка логики проверки паролей
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<BlogAdmin>(dataProtectionProvider.Create("ASP.NET Identity"));
            }

            return manager;
        }
    }
}
