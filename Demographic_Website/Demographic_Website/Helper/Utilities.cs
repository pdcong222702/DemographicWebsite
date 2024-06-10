using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Text;

namespace Demographic_Website.Helper
{
    public class Utilities
    {
        public static async Task<string> Upload(IWebHostEnvironment webHostEnvironment, List<IFormFile> file, string pathName)
        {
            try
            {
                string fileNames = "";
                long size = file.Sum(f => f.Length);
                var allowedExtension = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var filePaths = new List<string>();
                foreach (var item in file)
                {
                    var fileExtension = Path.GetExtension(item.FileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(fileExtension) || !allowedExtension.Contains(fileExtension))
                    {
                        return null;
                    }

                    if (item.Length > 0)
                    {
                        var uploadFolderPath = Path.Combine(webHostEnvironment.WebRootPath, "images", pathName);
                        Directory.CreateDirectory(uploadFolderPath);
                        var fileName = Path.GetRandomFileName() + fileExtension;
                        var filePath = Path.Combine(uploadFolderPath, fileName);
                        filePaths.Add(filePath);
                        fileNames = fileName;
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await item.CopyToAsync(stream);
                        }
                    }
                }
                return fileNames;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
