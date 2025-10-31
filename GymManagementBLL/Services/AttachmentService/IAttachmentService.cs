using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        string? Upload(string FolderName, IFormFile File);
        
        bool Delete(string FileName, string FolderName);
    }
}
