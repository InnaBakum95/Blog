#if !FILES_TO_FILESYSTEM

    using NewBlogAPI.Components.BlogArticleComponents;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

#endif

namespace NewBlogAPI.Components.BlogArticleFilesComponents
{
    /// <summary>
    /// Change <see cref="FILES_TO_FILESYSTEM"/> macro in project settings
    /// to change condition of current entity.
    /// </summary>
    public class BlogArticleFile
    {

#if !FILES_TO_FILESYSTEM

        [Key]
        public long Id { get; set; }

#endif

#if !FILES_TO_FILESYSTEM
        [Required]
#endif
        public string FileName { get; set; }

#if !FILES_TO_FILESYSTEM
        [Required]
#endif
        public string FileType { get; set; }

#if !FILES_TO_FILESYSTEM
        [Required]
#endif

        public byte[] FileContent { get; set; }

#if !FILES_TO_FILESYSTEM

        [Index]
        [Required]
        public long ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public BlogArticle Article { get; set; } 

#endif

    }
}