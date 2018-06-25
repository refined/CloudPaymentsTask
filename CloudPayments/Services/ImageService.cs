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
        public async Task<string> SaveImage(IFormFile formFile, string imageFolder)
        {
            var filePath = imageFolder + "/" + formFile.FileName;
            using (var fileStream = new FileStream("images/" + filePath, FileMode.OpenOrCreate))
            {
                await formFile.CopyToAsync(fileStream);
            }
            return filePath;
        }

        public Task DeleteImage(string imagePath)
        {
            return Task.Run(() =>
            {
                var file = new FileInfo("images/" + imagePath);
                if (file.Exists)
                {
                    file.Delete();
                }
            });
        }
    }
}
