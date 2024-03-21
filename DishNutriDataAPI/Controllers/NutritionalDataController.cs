using MediatR;
using Microsoft.AspNetCore.Mvc;
using NutriDataAPI.Models;
using NutriDataAPI.Requests;

namespace NutriDataAPI.Controllers
{
    [ApiController]
    [Route("getNutritionalData")]
    public class NutritionalDataController : ControllerBase
    {
        IMediator mediator;

        public NutritionalDataController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost(Name = "GetNutritionalData")]
        [Produces(typeof(NutritionalData))]
        public async Task<NutritionalData> Post([FromBody] Dictionary<string, int> ingredientsWithWeight)
        {
            return await Task.FromResult(await mediator.Send(new GetNutritionalDataRequest(ingredientsWithWeight)));
        }
    }
}