using Application.DTOs.Upload;
using Application.Exceptions;
using Domain.Constants;

namespace Application.IServices
{
    public interface IUploadService
    {
        Task<string> Upload(UploadReq file, string nameFolder);
        public static string ValidateFile(UploadReq file)
        {
            if (file == null || file.FileStream == null)
            {
                throw new AppException(Errors.FileNotEmpty);
            }

            var maxBytes = UploadReq.MaxSize * 1024 * 1024; // 10mb
            if (file.Length > maxBytes)
            {
                throw new AppException(Errors.FileTooLarge);
            }

            var ext = Path.GetExtension(file.FileName).ToLower();
            if (!UploadReq.AllowedExtension.Contains(ext))
            {
                throw new AppException(Errors.FileNotAllowed);
            }
            var fileName = $"{Guid.NewGuid()}{ext}";
            return fileName;
        }
    }
}
