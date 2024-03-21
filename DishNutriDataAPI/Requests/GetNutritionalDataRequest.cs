using MediatR;
using NutriDataAPI.Models;

namespace NutriDataAPI.Requests
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
