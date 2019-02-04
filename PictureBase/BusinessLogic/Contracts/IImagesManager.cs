using PictureBase.Models;

namespace PictureBase.BusinessLogic.Contracts
{
    public interface IImagesManager
    {
        ServiceResponse UploadFile(UploadImageModel model);
    }
}
