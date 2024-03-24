using MediatR;
using DishNutriDataAPI.Models;

namespace DishNutriDataAPI.Requests
{
    /// <summary>
    /// Request to get nutritional data for a given image with ingredients list.
    /// </summary>
    public class GetNutritionalDataRequest : IRequest<NutritionalData>
    {
        public List<Dictionary<string, object>> IngredientsWithWeight { get; }
        public GetNutritionalDataRequest(List<Dictionary<string, object>> ingredientsWithWeight)
        {
            IngredientsWithWeight = ingredientsWithWeight;
        }
    }
}
