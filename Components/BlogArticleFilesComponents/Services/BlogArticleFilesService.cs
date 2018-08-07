using NewBlogAPI.Components.BlogArticleFilesComponents.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

#if FILES_TO_FILESYSTEM
    
    using System.IO;
    using System.Web;

#else

    using NewBlogAPI.Data;
    using System.Linq;

#endif

namespace NewBlogAPI.Components.BlogArticleFilesComponents.Services
{
    public class BlogArticleFilesService : IBlogFilesService
    {

#if !FILES_TO_FILESYSTEM

        private readonly BlogDBContext _blogDBContext;

        public BlogArticleFilesService(BlogDBContext inBlogDBContext)
        {
            _blogDBContext = inBlogDBContext;
        }

#endif
        ///<exception cref="SystemException"></exception>
#if FILES_TO_FILESYSTEM
        public async Task<List<string>> GetFilesList(long ArticleId, string inBasePath)
#else
        public async Task<List<string>> GetFilesList(long ArticleId)
#endif
        {
            return await Task.Run(
                    () =>

#if FILES_TO_FILESYSTEM
                    {
                        List<string> res = new List<string>();

                        foreach(string iterator in  Directory.
                                                        EnumerateFiles
                                                        (
                                                            inBasePath + $"\\{ArticleId}"
                                                        ))
                            res.Add(Path.GetFileName(iterator));


                        return res;
                    }
#else
                    _blogDBContext.BlogArticleFiles.
                        Where(record => record.ArticleId == ArticleId).
                        Select(record => record.FileName).
                        ToList()
#endif
                    );
        }

        /// <exception cref="SystemException"></exception>
#if FILES_TO_FILESYSTEM
        public async Task<BlogArticleFile> ExtractFile(long ArticleId, string FileName, string inBasePath)
#else
        public async Task<BlogArticleFile> ExtractFile(long ArticleId, string FileName)
#endif
        {
            return await Task.Run(
                () =>

#if FILES_TO_FILESYSTEM

                    new BlogArticleFile()
                    {
                        FileContent = File.ReadAllBytes(inBasePath + $"\\{ArticleId}\\" + FileName),
                        FileType    = MimeMapping.GetMimeMapping(FileName),
                        FileName    = FileName
                    }
#else

                    _blogDBContext.
                        BlogArticleFiles.
                        SingleOrDefault(record => 
                                        record.ArticleId == ArticleId &&
                                        record.FileName == FileName)
#endif
                    );
        }


        ///<summary>
        ///<remarks>If FILES_TO_FILESYSTEM is not defined - be aware of max_allowed_packet limit in MySQL.</remarks>
        ///</summary>
#if FILES_TO_FILESYSTEM
        public async Task<SystemException> PutFiles(MultipartMemoryStreamProvider inFileStream, long inArticleId, string inBasePath)
        {
            try
            {
                foreach (HttpContent fileInfo in inFileStream.Contents)
                {
                    FileStream fileStream = new FileStream( inBasePath              +
                                                            $"\\{inArticleId}\\"    +
                                                            fileInfo.
                                                                Headers.
                                                                ContentDisposition.
                                                                FileName.
                                                                Replace("\"", string.Empty),

                                                            FileMode.Create);

                    await fileInfo.CopyToAsync(fileStream);

                    fileStream.Close();
                }
            }
            catch (SystemException error)
            {
                return error;
            }

            return null;
        }
#else
        public async Task<SystemException> PutFiles(MultipartMemoryStreamProvider inFileStream, long inArticleId)
        {
            return await Task.Run(
                async () =>
                {
                    try
                    {
                        foreach (HttpContent fileInfo in inFileStream.Contents)
                        {
                            BlogArticleFile fileData = new BlogArticleFile()
                            {
                                ArticleId = inArticleId,
                                FileName = fileInfo.Headers.ContentDisposition.FileName.Replace("\"", string.Empty),
                                FileType = fileInfo.Headers.ContentType.MediaType,
                                FileContent = await fileInfo.ReadAsByteArrayAsync()
                            };

                            _blogDBContext.BlogArticleFiles.Add(fileData);
                            _blogDBContext.SaveChanges();
                        }
                    }
                    catch (SystemException error)
                    {
                        return error;
                    }

                    return null;
                });
        }
#endif

#if FILES_TO_FILESYSTEM
        public async Task<SystemException> DeleteFile(string inFileName, long inArticleId, string inBasePath)
#else
        public async Task<SystemException> DeleteFile(string inFileName, long inArticleId)
#endif
        {
            return await Task.Run(
                () =>
                {
                    try
                    {

#if FILES_TO_FILESYSTEM

                        File.Delete(inBasePath + $"\\{inArticleId}\\" + inFileName);

#else

                        BlogArticleFile oldEntity = _blogDBContext.
                                                    BlogArticleFiles.
                                                    SingleOrDefault(record =>
                                                                    record.ArticleId == inArticleId &&
                                                                    record.FileName == inFileName);

                        _blogDBContext.BlogArticleFiles.Remove(oldEntity);

                        _blogDBContext.SaveChanges();
#endif

                    }
                    catch(SystemException error)
                    {
                        return error;
                    }

                    return null;
                });
        }

        ///<summary>
        ///!!!Need To look throught deleting method!!!
        ///</summary>
#if FILES_TO_FILESYSTEM
        public async Task<SystemException> DeleteArticleFiles(long inArticleId, string inBasePath)
#else
        public async Task<SystemException> DeleteArticleFiles(long inArticleId)
#endif
        {
            return await Task.Run(
                () =>
                {
                    try
                    {

#if FILES_TO_FILESYSTEM

                        foreach (
                                    string filePath in 
                                    Directory.GetFiles
                                    (
                                       inBasePath + $"\\{inArticleId}"
                                    )
                                )
                            File.Delete(filePath);

#else

                        bool innerFunction(BlogArticleFile record)
                        {
                            if(record.ArticleId == inArticleId)
                                _blogDBContext.Entry(record).State = System.Data.Entity.EntityState.Deleted;

                            return false;
                        }

                        _blogDBContext.BlogArticleFiles.Where(innerFunction);

                        _blogDBContext.SaveChanges();

#endif

                    }
                    catch (SystemException error)
                    {
                        return error;
                    }

                    return null;
                });
        }

    }
}