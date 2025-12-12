using Amazon.S3;
using Amazon.S3.Model;
using Application.DTOs.Upload;
using Application.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class S3UploadService : IUploadService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string? _bucketName;
        private readonly string? _url;
        public S3UploadService(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["S3Settings:BucketName"];
            _url = configuration["AWS:ServiceURL"];
        }
        public async Task<string> Upload(UploadReq file, string nameFolder)
        {
            try
            { 
                string fileName = IUploadService.ValidateFile(file);
                string fileNameUpload = $"{nameFolder}/{fileName}";

                // upload minio
                var putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fileNameUpload, // Tên file trên S3
                    InputStream = file.FileStream,
                    ContentType = file.ContentType,
                };
                await _s3Client.PutObjectAsync(putRequest);
                return $"{_url}/{_bucketName}/{fileNameUpload}";
            }
            catch (AmazonS3Exception ex)
            {
                throw new Exception($"S3 Error: {ex.Message}");
            }
        }
    }
}
