using NewBlogAPI.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using NewBlogAPI.Components.AdminComponents.Services.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using NewBlogAPI.Models;
using Microsoft.AspNet.Identity;

namespace NewBlogAPI.Components.AdminComponents.Services
{
    public class BlogAdminsService : IBlogAdminsService
    {
        private readonly BlogDBContext _blogDBContext;
        private readonly ApplicationUserManager _applicationUserManager;

        public BlogAdminsService(BlogDBContext inBlogDBContext)
        {
            _blogDBContext = inBlogDBContext;
            _applicationUserManager = new ApplicationUserManager(new UserStore<BlogAdmin>());
        }

        ///<summary>In PasswordHash field gonna be real password.</summary>
        ///<exception cref="System.SystemException"></exception>
        public async Task<BlogAdmin> CreateAdmin(BlogAdmin inAdmin)
        {
            await _applicationUserManager.CreateAsync(inAdmin, inAdmin.PasswordHash);
            IdentityResult res = await _applicationUserManager.AddToRoleAsync(inAdmin.Id, RolesConstants.ROLE_ADMIN);

            inAdmin = _blogDBContext.Users.Add(inAdmin);

            await _blogDBContext.SaveChangesAsync();

            return inAdmin;
        }

        ///<exception cref="System.SystemException"></exception>
        public async Task DeleteAdmin(string inAdminId)
        {
            BlogAdmin user            = await _applicationUserManager.FindByIdAsync(inAdminId);
            IList<string>   rolesForUser    = await _applicationUserManager.GetRolesAsync(inAdminId);

            user.Logins.ToList().ForEach(
                async login => await _applicationUserManager.
                                     RemoveLoginAsync(login.UserId, 
                                                      new UserLoginInfo(login.LoginProvider, 
                                                                        login.ProviderKey)));
            
            if(rolesForUser.Count() > 0)
                rolesForUser.ToList().ForEach(
                    async role => await _applicationUserManager.
                                        RemoveFromRoleAsync(inAdminId, role));

            await _applicationUserManager.DeleteAsync(user);
            await _blogDBContext.SaveChangesAsync();
        }

        ///<exception cref="System.SystemException"></exception>
        public async Task UpdateAdmin(BlogAdmin inAdmin)
        {
            BlogAdmin old       = await _applicationUserManager.FindByIdAsync(inAdmin.Id);

            old.UserName        = inAdmin.UserName;
            old.Email           = inAdmin.Email;
            old.PhoneNumber     = inAdmin.PhoneNumber;
            old.PasswordHash    = inAdmin.PasswordHash;
            old.Image           = inAdmin.Image;
            old.Contacts        = inAdmin.Contacts;

            await _applicationUserManager.UpdateAsync(old);
            await _blogDBContext.SaveChangesAsync();
        }

        ///<exception cref="System.SystemException"></exception>
        public async Task<BlogAdmin> GetAdmin(string inAdminId)
        {
            return await _applicationUserManager.FindByIdAsync(inAdminId);
        }

        ///<exception cref="System.ArgumentNullException"></exception>
        public async Task<List<BlogAdmin>> GetAllAdmins()
        {
            return await Task.Run(
                () =>
                    _applicationUserManager.
                        Users.
                        ToList());
        }

        //***       Tags

        ///<exception cref="System.SystemException"></exception>
        public async Task AddTag(BlogAdminTag inTag)
        {
            await Task.Run(
                () =>
                {
                    _blogDBContext.BlogAdminsTags.Add(inTag);
                    _blogDBContext.SaveChanges();
                });
        }

        ///<exception cref="System.SystemException"></exception>
        public async Task DeleteTag(BlogAdminTag inTag)
        {
            await Task.Run(
                () =>
                {
                    _blogDBContext.
                        BlogAdminsTags.
                        Remove(_blogDBContext.
                                BlogAdminsTags.
                                Attach(inTag));

                    _blogDBContext.SaveChanges();
                });
        }

        ///<exception cref="System.SystemException"></exception>
        public async Task<List<string>> DeleteAdminTags(string inAdminId)
        {
            return await Task.Run(
                () =>
                {
                    List<string> result =   _blogDBContext.
                                            BlogAdminsTags.
                                            RemoveRange(_blogDBContext.
                                                            BlogAdminsTags.
                                                            Where(record => record.AdminId == inAdminId)).
                                            Select(record => record.TagValue).
                                            ToList();

                    _blogDBContext.SaveChanges();

                    return result;
                });
        }

        ///<exception cref="System.SystemException"></exception>
        public async Task<List<string>> DeleteAllTags()
        {
            return await Task.Run(
               () =>
               {
                   List<string> result = _blogDBContext.
                                           BlogAdminsTags.
                                           RemoveRange(_blogDBContext.
                                                           BlogAdminsTags).
                                           Select(record => record.TagValue).
                                           ToList();

                   _blogDBContext.SaveChanges();

                   return result;
               });
        }

        ///<exception cref="System.ArgumentNullException"></exception>
        public async Task<List<string>> GetAdminTags(string inAdminId)
        {
            return await Task.Run(
                () =>
                    _blogDBContext.
                        BlogAdminsTags.
                        Where(record => record.AdminId == inAdminId).
                        Select(record => record.TagValue).
                        ToList());
        }

        ///<exception cref="System.ArgumentNullException"></exception>
        public async Task<List<string>> GetAllTags()
        {
            return await Task.Run(
                () =>
                _blogDBContext.
                    BlogAdminsTags.
                    Select(record => record.TagValue).
                    ToList());
        }
    }
}