using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FileUploadWebApp.Domain.Interfaces
{
    public interface IImageService
    {
        Task Upload(IFormFile image, string fileName);
        Task<IEnumerable<string>> GetNames();
        Task<IEnumerable<string>> GetNamesAsc();
        Task<IEnumerable<string>> GetNamesDes();
        Task<IEnumerable<string>> SearchImages(string imageName);
    }
}
