using DishNutriDataAPI.Models;
using DishNutriDataAPI.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DishNutriDataAPI.Controllers
{
    /// <summary>
    /// Helps identifying ingredients in images (which are base64 encoded).
    /// </summary>
    [ApiController]
    [Route("ingredients-suggestion")]
    public class IngredientsSuggestionController : ControllerBase
    {
        IMediator mediator;

        public IngredientsSuggestionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Suggests a list of ingredients for a given image file.
        /// </summary>
        /// <param name="fileParameter">the image.</param>
        /// <returns>the suggestions, a list of possible ingredients.</returns>
        [HttpPost(Name = "IngredientsSuggestion")]
        [Produces(typeof(List<string>))]
        public async Task<List<string>> IngredientsSuggestion([FromBody] FileParameter fileParameter)
        {
            return await mediator.Send(new GetIngredientsSuggestionRequest(fileParameter.Base64File));
        }
    }
}
