using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewBlogAPI.Components.AdminComponents.Services.Interfaces
{
    public interface IBlogAdminsService
    {
        ///<exception cref="System.SystemException"></exception>
        Task<BlogAdmin> CreateAdmin(BlogAdmin inAdmin);

        ///<exception cref="System.SystemException"></exception>
        Task DeleteAdmin(string inAdminId);

        ///<exception cref="System.SystemException"></exception>
        Task UpdateAdmin(BlogAdmin inAdmin);

        ///<exception cref="System.SystemException"></exception>
        Task<BlogAdmin> GetAdmin(string inAdminId);

        ///<exception cref="System.ArgumentNullException"></exception>
        Task<List<BlogAdmin>> GetAllAdmins();

        ///<exception cref="System.SystemException"></exception>
        Task AddTag(BlogAdminTag inTag);

        ///<exception cref="System.SystemException"></exception>
        Task DeleteTag(BlogAdminTag inTag);

        ///<exception cref="System.SystemException"></exception>
        Task<List<string>> DeleteAdminTags(string inAdminId);

        ///<exception cref="System.SystemException"></exception>
        Task<List<string>> DeleteAllTags();

        ///<exception cref="System.ArgumentNullException"></exception>
        Task<List<string>> GetAdminTags(string inAdminId);

        ///<exception cref="System.ArgumentNullException"></exception>
        Task<List<string>> GetAllTags();
    }
}
