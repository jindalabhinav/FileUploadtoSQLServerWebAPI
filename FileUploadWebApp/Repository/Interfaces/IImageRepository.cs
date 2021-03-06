using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploadWebApp.Repository.Interfaces
{
    public interface IImageRepository
    {
        Task Upload(byte[] image, string fileName);
        Task<IEnumerable<string>> GetNames();
        Task<IEnumerable<string>> GetNamesAsc();
        Task<IEnumerable<string>> GetNamesDes();
        Task<IEnumerable<string>> SearchImages(string imageName);
    }
}
