using MediatR;
using Microsoft.AspNetCore.Mvc;
using DishNutriDataAPI.Models;
using DishNutriDataAPI.Requests;

namespace DishNutriDataAPI.Controllers
{
    [ApiController]
    [Route("NutritionalData")]
    public class NutritionalDataController : ControllerBase
    {
        IMediator mediator;

        public NutritionalDataController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "NutritionalData")]
        [Produces(typeof(NutritionalData))]
        public async Task<NutritionalData> Post([FromBody] Dictionary<string, int> ingredientsWithWeight)
        {
            return await Task.FromResult(await mediator.Send(new GetNutritionalDataRequest(ingredientsWithWeight)));
        }
    }
}