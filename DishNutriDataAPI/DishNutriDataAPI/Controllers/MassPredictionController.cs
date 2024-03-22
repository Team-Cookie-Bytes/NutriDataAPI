using DishNutriDataAPI.Models;
using DishNutriDataAPI.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DishNutriDataAPI.Controllers
{
    /// <summary>
    /// Predicts the mass of ingredients.
    /// </summary>
    [ApiController]
    [Route("mass-prediction")]
    public class MassPredictionController : ControllerBase
    {
        IMediator mediator;
        
        public MassPredictionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Predicts mass based on a given list of ingredients and an image.
        /// </summary>
        /// <param name="parameters">the image and the list of ingredients.</param>
        /// <returns>the predicted mass for each ingredient.</returns>
        [HttpPost(Name = "MassPrediction")]
        [Produces(typeof(List<Dictionary<string, object>>))]
        public async Task<List<Dictionary<string, object>>> MassPrediction([FromBody] FileWithIngredientsParameter parameters) 
        {            
            return await mediator.Send(new GetMassPredictionRequest(parameters.Base64File, parameters.Ingredients));
        }

    }
}
