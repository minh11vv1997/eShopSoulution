using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Services.Common
{
    public class FileStorageService : IStoregeService
    {
        private readonly string _userContentFolde;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolde = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolde, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            //var path = Path.Combine("home", "ReadMe.txt"); //  "home/ReadMe.txt"
            var fileFath = Path.Combine(_userContentFolde, fileName);
            if (File.Exists(fileFath))
            {
                using (FileStream fs = File.Create(fileFath))
                {
                    await mediaBinaryStream.CopyToAsync(fs);
                }
            }
            using var output = new FileStream(fileFath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }
    }
}