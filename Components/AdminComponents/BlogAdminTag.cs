using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBlogAPI.Components.AdminComponents
{
    public class BlogAdminTag
    {
        [Index]
        public string AdminId { get; set; }
        [ForeignKey("AdminId")]
        public BlogAdmin TagOwner { get; set; }

        [Key]
        [MaxLength(256)]
        public string TagValue { get; set; }
    }
}