using MediatR;
using DishNutriDataAPI.Models;

namespace DishNutriDataAPI.Requests
{
    public class GetNutritionalDataRequest : IRequest<NutritionalData>
    {
        public Dictionary<string, int> IngredientsWithWeight { get; }
        public GetNutritionalDataRequest(Dictionary<string, int> ingredientsWithWeight)
        {
            IngredientsWithWeight = ingredientsWithWeight;
        }
    }
}
