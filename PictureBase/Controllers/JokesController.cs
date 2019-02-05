using Microsoft.AspNetCore.Mvc;
using PictureBase.BusinessLogic.Contracts;
using PictureBase.Models;

namespace PictureBase.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class JokesController : ControllerBase
    {
        private readonly IJokesManager jokesManager;

        public JokesController(IJokesManager jokesManager)
        {
            this.jokesManager = jokesManager;
        }

        [Route("")]
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(jokesManager.GetAll());
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult Get(string id)
        {
            return Ok(jokesManager.GetById(id));
        }

        [Route("JokeId/{id}")]
        [HttpGet]
        public ActionResult GetByJokeId(string id)
        {
            return Ok(jokesManager.GetByJokeId(id));
        }

        [Route("")]
        [HttpPost]
        public ActionResult Post([FromBody] JokeFromApiModel model)
        {
            var result = jokesManager.AddJoke(model);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Ok(result.Message);
        }

        [Route("AddPlus")]
        [HttpPost]
        public ActionResult AddPlus([FromBody] string id)
        {
            var result = jokesManager.AddPlus(id);
            return Ok(result);
        }

        [Route("AddMinus")]
        [HttpPost]
        public ActionResult AddMinus([FromBody] string id)
        {
            var result = jokesManager.AddMinus(id);
            return Ok(result);
        }

        [Route("Top10")]
        [HttpGet]
        public ActionResult GetTop10()
        {
            return Ok(jokesManager.GetTop10());
        }

    }
}
