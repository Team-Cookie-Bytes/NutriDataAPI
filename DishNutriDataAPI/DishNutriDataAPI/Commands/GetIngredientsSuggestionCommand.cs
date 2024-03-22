using MediatR;
using Newtonsoft.Json.Linq;
using System.Text;
using DishNutriDataAPI.Requests;

namespace DishNutriDataAPI.Commands
{
    /// <summary>
    /// Class to receive the Ingredients Suggestions for a given image.
    /// </summary>
    public class GetIngredientsSuggestionCommand : IRequestHandler<GetIngredientsSuggestionRequest, List<string>>
    {
        /// <summary>
        /// Gets Suggestions for Ingredients in a given image.
        /// </summary>
        /// <param name="request">request for passing the parameters for the command.</param>
        /// <param name="cancellationToken">cancelation token.</param>
        /// <returns>Ingredients suggestions.</returns>
        public async Task<List<string>> Handle(GetIngredientsSuggestionRequest request, CancellationToken cancellationToken)
        {
            var result = new List<string>();
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, Environment.GetEnvironmentVariable("ingredients-suggestion-api-url") + "/ingredients-suggestions");
            requestMessage.Content = new StringContent($"{{\"base64image\":\"{request.Base64File}\"}}", Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.SendAsync(requestMessage);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var responseJson = JArray.Parse(responseString).Select(x => x.ToString());
                        return new List<string>(responseJson);
                    }
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
            return result;
        }
    }
}
