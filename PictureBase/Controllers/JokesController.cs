using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PictureBase.BusinessLogic.Contracts;
using PictureBase.Models;

namespace PictureBase.Controllers
{
    [EnableCors("SiteCorsPolicy")]
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
            return Ok(jokesManager.AddJoke(model));
        }

        [Route("AddPlus/{id}")]
        [HttpPost]
        public ActionResult AddPlus(string id)
        {
            var result = jokesManager.AddPlus(id);
            return Ok(result);
        }

        [Route("AddMinus/{id}")]
        [HttpPost]
        public ActionResult AddMinus(string id)
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
