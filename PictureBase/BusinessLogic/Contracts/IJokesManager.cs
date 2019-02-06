using Joker.Models;
using System.Collections.Generic;

namespace Joker.BusinessLogic.Contracts
{
    public interface IJokesManager
    {
        ServiceResponse AddJoke(JokeFromApiModel model);
        List<JokeOut> GetAll();
        JokeOut GetById(string id);
        JokeOut GetByJokeId(string id);
        ServiceResponse AddPlus(string id);
        ServiceResponse AddMinus(string id);
        List<ShortJoke> GetTop10();
    }
}
