using SportStore.Application.Common.Interfaces;

namespace SportStore.Application.Services
{
    public class FileUploadService : IFileUploadService
    {
        public void UploadFile(Stream input, string filepath)
        {
            using (FileStream fileStream = new FileStream(filepath, FileMode.Create))
            {
                input.CopyTo(fileStream);
            }
        }
    }
}
