namespace Library.Services
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        public void Download(string fileName)
        {
            var result = this.cloudinary.DownloadPrivate(fileName, true, "txt");
        }

        public async Task<string> UploadAsync(IFormFile file, string fileName, string folder)
        {
            byte[] destinationData;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationData = memoryStream.ToArray();
            }

            UploadResult uploadResult = null;

            using (var memoryStream = new MemoryStream(destinationData))
            {
                RawUploadParams uploadParams = new RawUploadParams
                {
                    Folder = folder,
                    File = new FileDescription(fileName, memoryStream),
                };

                uploadResult = await this.cloudinary.UploadAsync(uploadParams, "auto");
            }

            return uploadResult?.SecureUri.AbsoluteUri;
        }
    }
}
