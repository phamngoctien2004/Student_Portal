//using Application.DTOs.Upload;
//using Application.Exceptions;
//using Application.IServices;
//using Core.Enums;
//using Microsoft.AspNetCore.Hosting;

//namespace Application.Services
//{
//    public class UploadService : IUploadService
//    {
        
//        public async Task<string> Upload(UploadReq file)
//        {   
//            var ext = ValidateFile(file);
//            var pathFile = await CreateFile(file, ext);
//            return pathFile;
//        }

//        private async Task<string> CreateFile(UploadReq file, string ext)
//        {
//            // tìm thư mục cấu /static (java)
//            var webRootPath = _environment.WebRootPath;
//            if (string.IsNullOrEmpty(webRootPath))
//            {
//                webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
//            }

//            // tìm đường dẫn thư mục
//            var folderPath = Path.Combine(webRootPath, "images");
//            if (!Directory.Exists(folderPath))
//            {
//                Directory.CreateDirectory(folderPath);
//            }

//            // tạo tên file
//            var fileName = $"{Guid.NewGuid()}{ext}";
//            var fullPath = Path.Combine(folderPath, fileName);

//            await SaveDisk(file.FileStream, fullPath);

//            return fileName;
//        }
//        private async Task SaveDisk(Stream inputStream, string fullPath)
//        {
//            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
//            {
//                await inputStream.CopyToAsync(stream);
//            }
//        }
//    }
//}
