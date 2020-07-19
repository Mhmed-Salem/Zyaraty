﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zyarat.Models.RequestResponseInteracting;

namespace Zyarat.Models.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> UploadAsync(IFormFile formFile)
        {
            var unique = Guid.NewGuid() + "_" + formFile.FileName;
            var path = Path.Combine(_environment.WebRootPath, unique);
            try
            {
                await formFile.CopyToAsync(new FileStream(path, FileMode.Create));
                return path;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public FileStream GetFileStream(string path)
        {
            if (!string.IsNullOrEmpty(path) && !File.Exists(path)) return File.OpenRead(path);
            throw new Exception("invalid Path!");
        }

        public void  RemoveFile(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))  File.Delete(path);
        }

        public Task<string> UpdateFileAsync(string oldFilePath, IFormFile file)
        {
            RemoveFile(oldFilePath);
            return UploadAsync(file);
        }
        
    }
}