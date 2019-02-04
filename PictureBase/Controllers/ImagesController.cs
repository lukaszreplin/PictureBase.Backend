using Microsoft.AspNetCore.Mvc;
using PictureBase.BusinessLogic.Contracts;
using PictureBase.Models;
using System.Collections.Generic;

namespace PictureBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesManager imagesManager;

        public ImagesController(IImagesManager imagesManager)
        {
            this.imagesManager = imagesManager;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return new ActionResult<string>("test");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [Route("Upload")]
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult UploadFile(UploadImageModel model)
        {
            var result = imagesManager.UploadFile(model);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Ok(result.Message);
        }
    }
}
