using PictureBase.BusinessLogic.Contracts;
using StackExchange.Redis;

namespace PictureBase.BusinessLogic.Services
{
    public class ImagesManager : IImagesManager
    {
        public string Test()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            return redis.IsConnected.ToString();
        }
    }
}
