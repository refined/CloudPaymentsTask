using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CloudPayments.Services
{
    public interface IImageService
    {
        Task<string> SaveImage(IFormFile formFile, string imageFolder);
        //Task GetImage(string imagePath);
        Task DeleteImage(string imagePath);
    }

    public class ImageService : IImageService
    {
        private const string IMAGE_FOLDER = "wwwroot/images/";

        public async Task<string> SaveImage(IFormFile formFile, string imageFolder)
        {
            var filePath = imageFolder + "/" + formFile.FileName;
            if (!new DirectoryInfo(IMAGE_FOLDER + imageFolder).Exists)
            {
                Directory.CreateDirectory(IMAGE_FOLDER + imageFolder);
            }
            using (var fileStream = new FileStream(IMAGE_FOLDER + filePath, FileMode.OpenOrCreate))
            {
                await formFile.CopyToAsync(fileStream);
            }
            return filePath;
        }

        public Task DeleteImage(string imagePath)
        {
            return Task.Run(() =>
            {
                var file = new FileInfo(IMAGE_FOLDER + imagePath);
                if (file.Exists)
                {
                    file.Delete();
                }
            });
        }
    }
}
