using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using FileUploadWebApp.Repository.Interfaces;
using Microsoft.Extensions.Logging;

namespace FileUploadWebApp.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ILogger<ImageRepository> _logger;
        private readonly IDatabaseConnectionFactory _database;

        public ImageRepository(ILogger<ImageRepository> logger, IDatabaseConnectionFactory database)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task Upload(byte[] image, string fileName)
        {
            using var conn = await _database.CreateConnectionAsync();
            var parameters = new
            {
                ImageName = fileName,
                ImageData = image,
                CreatedOn = DateTime.Now
            };
            _logger.LogInformation($"Upload: Calling Db with Stored Procedure: {ServiceConstants.ImageUploadSP} ...");
            var result = await conn.ExecuteAsync(ServiceConstants.ImageUploadSP, parameters, commandType: CommandType.StoredProcedure);
            _logger.LogInformation($"Upload: Db call successful...");
        }

        public async Task<IEnumerable<string>> GetNames()
        {
            using var conn = await _database.CreateConnectionAsync();
            _logger.LogInformation($"GetNames: Calling Db with Stored Procedure: {ServiceConstants.GetNamesSP} ...");
            var result = await conn.QueryAsync<string>(ServiceConstants.GetNamesSP, commandType: CommandType.StoredProcedure);
            _logger.LogInformation($"GetNames: Db call successful...");
            return result;
        }

        public async Task<IEnumerable<string>> GetNamesAsc()
        {
            using var conn = await _database.CreateConnectionAsync();
            _logger.LogInformation($"GetNamesAsc: Calling Db with Stored Procedure: {ServiceConstants.GetNamesAscSP} ...");
            var result = await conn.QueryAsync<string>(ServiceConstants.GetNamesAscSP, commandType: CommandType.StoredProcedure);
            _logger.LogInformation($"GetNamesAsc: Db call successful...");
            return result;
        }

        public async Task<IEnumerable<string>> GetNamesDes()
        {
            using var conn = await _database.CreateConnectionAsync();
            _logger.LogInformation($"GetNamesDes: Calling Db with Stored Procedure: {ServiceConstants.GetNamesDesSP} ...");
            var result = await conn.QueryAsync<string>(ServiceConstants.GetNamesDesSP, commandType: CommandType.StoredProcedure);
            _logger.LogInformation($"GetNamesDes: Db call successful...");
            return result;
        }

        public async Task<IEnumerable<string>> SearchImages(string keyword)
        {
            using var conn = await _database.CreateConnectionAsync();
            var parameters = new
            {
                @SearchKeyword = keyword
            };
            _logger.LogInformation($"SearchImages: Calling Db with Stored Procedure: {ServiceConstants.SearchImagesSP} ...");
            var result = await conn.QueryAsync<string>(ServiceConstants.SearchImagesSP, parameters, commandType: CommandType.StoredProcedure);
            _logger.LogInformation($"SearchImages: Db call successful...");
            return result;
        }
    }
}
