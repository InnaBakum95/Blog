using NewBlogAPI.Components.AdminComponents;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewBlogAPI.Components.BlogArticleComponents
{
    public class BlogArticle
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [DisplayName("Назва статті")]
        [MaxLength(200), MinLength(1)]
        public string NameOfArticle { get; set; }
        [MaxLength(500)]
        public string DiscriptionOfArticle { get; set; }
        public string Image { get; set; }
        [Required]
        public string ArticleText { get; set; }
        public DateTime CreateDate { get; set; }

        [Index]
        [Required]
        public string BlogAdminId { get; set; }
        [ForeignKey("BlogAdminId")]
        public BlogAdmin Author { get; set; }
    }
}