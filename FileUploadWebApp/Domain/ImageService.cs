using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUploadWebApp.Domain.Interfaces;
using FileUploadWebApp.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FileUploadWebApp.Domain
{
    public class ImageService : IImageService
    {
        private readonly ILogger<ImageService> _logger;
        private readonly IImageRepository _imageRepository;

        public ImageService(ILogger<ImageService> logger, IImageRepository imageRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _imageRepository = imageRepository ?? throw new ArgumentNullException(nameof(imageRepository));
        }

        public async Task Upload(IFormFile image, string fileName)
        {
            byte[] imageData = null;
            using (var target = new MemoryStream())
            {
                image.CopyTo(target);
                imageData = target.ToArray();
            }

            await _imageRepository.Upload(imageData, fileName);
        }

        public async Task<IEnumerable<string>> GetNames()
        {
            var names = await _imageRepository.GetNames();
            return names;
        }

        public async Task<IEnumerable<string>> GetNamesAsc()
        {
            var names = await _imageRepository.GetNamesAsc();
            return names;
        }

        public async Task<IEnumerable<string>> GetNamesDes()
        {
            var names = await _imageRepository.GetNamesDes();
            return names;
        }

        public async Task<IEnumerable<string>> SearchImages(string imageName)
        {
            var names = await _imageRepository.SearchImages(imageName);
            return names;
        }


    }
}
