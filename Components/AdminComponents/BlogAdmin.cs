using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewBlogAPI.Components.AdminComponents
{
    public class BlogAdmin : IdentityUser
    {
        public string Contacts { get; set; }
        public byte[] Image { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<BlogAdmin> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}