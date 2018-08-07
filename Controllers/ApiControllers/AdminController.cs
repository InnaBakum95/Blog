using NewBlogAPI.Components.AdminComponents;
using NewBlogAPI.Components.AdminComponents.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace NewBlogAPI.Controllers.ApiControllers
{
    //[Authorize]
    public class AdminController : ApiController
    {
        private readonly IBlogAdminsService _blogAdminService;

        public AdminController(IBlogAdminsService inBlogAdminService)
        {
            _blogAdminService = inBlogAdminService;
        }

        //***       Admins

        [HttpPost]
        [ResponseType(typeof(BlogAdmin))]
        [Route("Api/Admin/CreateAdmin")]
        public async Task<IHttpActionResult> CreateAdmin(BlogAdmin inAdmin)
        {
            try
            {
                return Ok(await _blogAdminService.CreateAdmin(inAdmin));
            }
            catch (System.SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete]
        [Route("Api/Admin/DeleteAdmin")]
        public async Task<IHttpActionResult> DeleteAdmin(string inAdminId)
        {
            try
            {
                await _blogAdminService.DeleteAdmin(inAdminId);
                return Ok();
            }
            catch (System.SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPatch]
        [Route("Api/Admin/UpdateAdmin")]
        public async Task<IHttpActionResult> UpdateAdmin(BlogAdmin inAdmin)
        {
            try
            {
                await _blogAdminService.UpdateAdmin(inAdmin);
                return Ok();
            }
            catch (System.SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(BlogAdmin))]
        [Route("Api/Admin/GetAdmin")]
        public async Task<IHttpActionResult> GetAdmin(string inAdminId)
        {
            try
            {
                return Ok(await _blogAdminService.GetAdmin(inAdminId));
            }
            catch (System.SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<BlogAdmin>))]
        [Route("Api/Admin/GetAllAdmins")]
        public async Task<IHttpActionResult> GetAllAdmins()
        {
            try
            {
                return Ok(await _blogAdminService.GetAllAdmins());
            }
            catch (System.ArgumentNullException error)
            {
                return BadRequest(error.Message);
            }
        }

        //***       Tags

        [HttpPost]
        [Route("Api/Admin/AddTag")]
        public async Task<IHttpActionResult> AddTag(BlogAdminTag inTag)
        {
            try
            {
                await _blogAdminService.AddTag(inTag);
                return Ok();
            }
            catch (System.SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete]
        [Route("Api/Admin/DeleteTag")]
        public async Task<IHttpActionResult> DeleteTag(BlogAdminTag inTag)
        {
            try
            {
                await _blogAdminService.DeleteTag(inTag);
                return Ok();
            }
            catch (System.SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete]
        [ResponseType(typeof(List<string>))]
        [Route("Api/Admin/DeleteAdminTags")]
        public async Task<IHttpActionResult> DeleteAdminTags(string inAdminId)
        {
            try
            {
                return Ok(await _blogAdminService.DeleteAdminTags(inAdminId));
            }
            catch (System.SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete]
        [ResponseType(typeof(List<string>))]
        [Route("Api/Admin/DeleteAllTags")]
        public async Task<IHttpActionResult> DeleteAllTags()
        {
            try
            {
                return Ok(await _blogAdminService.DeleteAllTags());
            }
            catch (System.SystemException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<string>))]
        [Route("Api/Admin/GetAdminTags")]
        public async Task<IHttpActionResult> GetAdminTags(string inAdminId)
        {
            try
            {
                return Ok(await _blogAdminService.GetAdminTags(inAdminId));
            }
            catch (System.ArgumentNullException error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(List<string>))]
        [Route("Api/Admin/GetAllTags")]
        public async Task<IHttpActionResult> GetAllTags()
        {
            try
            {
                return Ok(await _blogAdminService.GetAllTags());
            }
            catch (System.ArgumentNullException error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
