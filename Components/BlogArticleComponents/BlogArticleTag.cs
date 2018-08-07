using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBlogAPI.Components.BlogArticleComponents
{
    public class BlogArticleTag
    {
        [Index]
        public long BlogArticleId { get; set; }
        [ForeignKey("BlogArticleId")]
        public BlogArticle TagOwner { get; set; }

        [Key]
        [MaxLength(256)]
        public string TagValue { get; set; }
    }
}