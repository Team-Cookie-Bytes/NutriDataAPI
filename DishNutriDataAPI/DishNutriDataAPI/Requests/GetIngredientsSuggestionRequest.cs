using MediatR;

namespace DishNutriDataAPI.Requests
{
    /// <summary>
    /// Class for the request to receive the ingredients suggestions for a given image.
    /// </summary>
    public class GetIngredientsSuggestionRequest : IRequest<List<string>>
    {
        public string Base64File { get; set; }
        public GetIngredientsSuggestionRequest(string base64File) 
        {
            Base64File = base64File;
        }
    }
}
