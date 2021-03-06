using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FileUploadWebApp.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FileUploadWebApp.Controllers
{
    [Route("")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ILogger<ImageUploadController> _logger;

        public ImageUploadController(IImageService imageService, ILogger<ImageUploadController> logger)
        {
            _imageService = imageService ?? throw new ArgumentNullException(nameof(imageService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// This endpoint is used to Upload a file to the database
        /// </summary>
        /// <param name="image">Select the image to upload</param>
        /// <param name="fileName">Enter the filename</param>
        /// <returns></returns>
        [HttpPost("")]
        public async Task<IActionResult> Upload(IFormFile image, string fileName)
        {
            if (image == null)
                return BadRequest("No Image uploaded.");
            if (image.Length == 0)
                return BadRequest("Image uploaded has no content.");
            if (String.IsNullOrWhiteSpace(fileName))
                return BadRequest("FileName is required.");

            _logger.LogInformation($"Request received by Upload, FileName: {fileName}");
            try
            {
                await _imageService.Upload(image, fileName);
                return Ok("Image Saved Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in Upload: {ex}");
                return StatusCode(500, "Internal Server Error.");
            }
        }

        /// <summary>
        /// This enpoint is used to Get the names of all the images uploaded in Db
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IActionResult> GetNames()
        {
            _logger.LogInformation($"Request received by GetNames");
            try
            {
                var imageNames = await _imageService.GetNames();
                if (imageNames != null && imageNames.Count() > 0)
                    return Ok(imageNames);
                else
                    return NotFound("No Images found in Database.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in {MethodInfo.GetCurrentMethod().Name}: {ex}");
                return StatusCode(500, "Internal Server Error.");
            }
        }

        /// <summary>
        /// This enpoint is used to Get the names of all the images in ascending order uploaded in Db
        /// </summary>
        /// <returns></returns>
        [HttpGet("/asc")]
        public async Task<IActionResult> GetNamesAsc()
        {
            _logger.LogInformation($"Request received by GetNamesAsc");
            try
            {
                var imageNames = await _imageService.GetNamesAsc();
                if (imageNames != null && imageNames.Count() > 0)
                    return Ok(imageNames);
                else
                    return NotFound("No Images found in Database.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in GetNamesAsc: {ex}");
                return StatusCode(500, "Internal Server Error.");
            }
        }

        /// <summary>
        /// This enpoint is used to Get the names of all the images in descending order uploaded in Db
        /// </summary>
        /// <returns></returns>
        [HttpGet("/des")]
        public async Task<IActionResult> GetNamesDes()
        {
            _logger.LogInformation($"Request received by GetNamesDes");
            try
            {
                var imageNames = await _imageService.GetNamesDes();
                if (imageNames != null && imageNames.Count() > 0)
                    return Ok(imageNames);
                else
                    return NotFound("No Images found in Database.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in GetNamesDes: {ex}");
                return StatusCode(500, "Internal Server Error.");
            }
        }

        /// <summary>
        /// This enpoint is used to search existing files in Db using the keyword given
        /// </summary>
        /// <param name="keyword">Enter the keyword to be searched in the filenames stored in Db</param>
        /// <returns></returns>
        [HttpPost("/search")]
        public async Task<IActionResult> SearchImage(string keyword)
        {
            if (keyword == String.Empty)
                return BadRequest("Input a keyword to search.");
            _logger.LogInformation($"Request received by SearchImage");
            try
            {
                var imageNames = await _imageService.SearchImages(keyword);
                if (imageNames != null && imageNames.Count() > 0)
                    return Ok(imageNames);
                else
                    return NotFound($"No Images found in Database with keyword: {keyword}.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred in SearchImage: {ex}");
                return StatusCode(500, "Internal Server Error.");
            }
        }
    }
}
