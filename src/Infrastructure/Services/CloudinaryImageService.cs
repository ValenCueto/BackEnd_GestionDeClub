using Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace Infrastructure.Services
{

    public class CloudinaryImageService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryImageService> _logger;

        public CloudinaryImageService(IConfiguration configuration, ILogger<CloudinaryImageService> logger)
        {
            var account = new Account(
                configuration["Cloudinary:CloudName"],
                configuration["Cloudinary:ApiKey"],
                configuration["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
            _logger = logger;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Archivo no válido");

            try
            {
                using var stream = file.OpenReadStream();

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Folder = "club-news", // Organizar por carpetas
                    Transformation = new Transformation()
                        .Width(800).Height(600).Crop("limit") // Optimizar tamaño
                        .Quality("auto:good") // Optimización automática
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult?.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.ToString();
                }

                throw new Exception($"Error en Cloudinary: {uploadResult?.Error?.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading image to Cloudinary");
                throw;
            }
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            try
            {
                var deleteParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deleteParams);
                return result.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting image from Cloudinary");
                return false;
            }
        }
    }
}