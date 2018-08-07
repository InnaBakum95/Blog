using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBlogAPI.Components.BlogArticleComponents.Services
{
    public class BlogArticleComment
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string AuthorName { get; set; }
        [Required]
        public string CommentText { get; set; }

        [Index]
        [Required]
        public long BlogArticleId { get; set; }
        [ForeignKey("BlogArticleId")]
        public BlogArticle Article { get; set; }
    }
}