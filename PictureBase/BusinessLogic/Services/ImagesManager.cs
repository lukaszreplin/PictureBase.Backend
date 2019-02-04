using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver;
using PictureBase.BusinessLogic.Contracts;
using PictureBase.Models;
using StackExchange.Redis;
using System;
using System.IO;

namespace PictureBase.BusinessLogic.Services
{
    public class ImagesManager : IImagesManager
    {
        private readonly IHostingEnvironment host;
        private readonly IMongoClient db;
        private readonly ConnectionMultiplexer redis;

        public ImagesManager(IMongoClient db)
        {
            this.db = db;
            redis = ConnectionMultiplexer.Connect("localhost");
        }

        public ServiceResponse UploadFile(UploadImageModel model)
        {
            if (model.File == null) return new ServiceResponse("Brak pliku!");
            if (model.File.Length == 0) return new ServiceResponse("Pusty plik!");
            var uploadFilesPath = Path.Combine(host.WebRootPath, "Uploads");
            if (!Directory.Exists(uploadFilesPath))
                Directory.CreateDirectory(uploadFilesPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);
            var filePath = Path.Combine(uploadFilesPath, fileName);
            var image = new Image()
            {
                Id = Guid.NewGuid(),
                AddedDate = DateTime.Now,
                Description = model.Description,
                Filename = filePath
            };
            var database = db.GetDatabase("imagerepo");
            var collection = database.GetCollection<Image>("Images");
            collection.InsertOne(image);
            return new ServiceResponse();
        }
    }
}
