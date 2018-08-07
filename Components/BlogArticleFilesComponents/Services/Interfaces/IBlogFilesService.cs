using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewBlogAPI.Components.BlogArticleFilesComponents.Services.Interfaces
{
    public interface IBlogFilesService
    {

#if FILES_TO_FILESYSTEM

        Task<SystemException> PutFiles(MultipartMemoryStreamProvider inFileStream, long ArticleId, string inBasePath);

        ///<exception cref="SystemException"></exception>
        Task<List<string>> GetFilesList(long ArticleId, string inBasePath);

        /// <exception cref="SystemException"></exception>
        Task<BlogArticleFile> ExtractFile(long ArticleId, string FileName, string inBasePath);

        Task<SystemException> DeleteFile(string inFileName, long inArticleId, string inBasePath);

        Task<SystemException> DeleteArticleFiles(long inArticleId, string inBasePath);

#else
        Task<SystemException> PutFiles(MultipartMemoryStreamProvider inFileStream, long ArticleId);

        ///<exception cref="SystemException"></exception>
        Task<List<string>> GetFilesList(long ArticleId);

        /// <exception cref="SystemException"></exception>
        Task<BlogArticleFile> ExtractFile(long ArticleId, string FileName);

        Task<SystemException> DeleteFile(string inFileName, long inArticleId);

        Task<SystemException> DeleteArticleFiles(long inArticleId);

#endif

    }
}
