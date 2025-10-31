using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        private readonly long maxFileSize = 5 * 1024 * 1024; // 5 MB
        private readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

        private readonly IWebHostEnvironment WebHost;

        public AttachmentService(IWebHostEnvironment WebHost)
        {
            this.WebHost = WebHost;
        }
        public string? Upload(string FolderName, IFormFile File)
        {
            try
            {
                if ( FolderName is null ||File is null || File.Length == 0) return null;

                if (File.Length > maxFileSize) return null;

                var extension = Path.GetExtension(File.FileName).ToLower();
                if (!allowedExtensions.Contains(extension)) return null;

                var FolderPath = Path.Combine(WebHost.WebRootPath, "images", FolderName);
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                var FileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(FolderPath, FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    File.CopyTo(fileStream);
                }

                return FileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to upload File to Folder = {FolderName} : {ex}");
                return null;
            }
        }
        public bool Delete(string FileName, string FolderName)
        {
            try
            {
                if (string.IsNullOrEmpty(FileName) || string.IsNullOrEmpty(FolderName))
                    return false;

                var fullPath = Path.Combine(WebHost.WebRootPath, "images", FolderName, FileName);

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to Delete File with name = {FileName} : {ex}");
                return false;
            }
        }

    }
}
