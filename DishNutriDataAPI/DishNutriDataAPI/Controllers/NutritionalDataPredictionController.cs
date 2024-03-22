using DishNutriDataAPI.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DishNutriDataAPI.Models;

namespace DishNutriDataAPI.Controllers
{
    /// <summary>
    /// Provides nutritional data for given images.
    /// </summary>
    [ApiController]
    [Route("nutritional-data")]
    public class NutritionalDataPredictionController
    {
        IMediator mediator;
        
        public NutritionalDataPredictionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Predict nutritional data based on an image and an ingredient list.
        /// </summary>
        /// <param name="fileParameter">an image and an ingredient list.</param>
        /// <returns>the predicted nutritional data.</returns>
        [HttpPost(Name = "NutritionalData")]
        [Produces(typeof(List<string>))]
        public async Task<NutritionalData> NutritionalDataPrediction([FromBody] FileWithIngredientsParameter fileParameter)
        {
            var massPredictions = await mediator.Send(new GetMassPredictionRequest(fileParameter.Base64File, fileParameter.Ingredients));

            return await mediator.Send(new GetNutritionalDataRequest(massPredictions));
        }
    }
}
