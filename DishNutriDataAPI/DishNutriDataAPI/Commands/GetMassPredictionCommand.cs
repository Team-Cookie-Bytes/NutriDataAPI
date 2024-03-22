using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using DishNutriDataAPI.Requests;

namespace DishNutriDataAPI.Commands
{
    /// <summary>
    /// Command to predict the mass of given ingredients.
    /// </summary>
    public class GetMassPredictionCommand : IRequestHandler<GetMassPredictionRequest, List<Dictionary<string, object>>>
    {
        /// <summary>
        /// Predicts the mass for the parameters (image and ingredients list).
        /// </summary>
        /// <param name="request">request with parameters for command.</param>
        /// <param name="cancellationToken">cancelation token.</param>
        /// <returns></returns>
        public async Task<List<Dictionary<string, object>>> Handle(GetMassPredictionRequest request, CancellationToken cancellationToken)
        {
            var result = new List<Dictionary<string, object>>();
            
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, Environment.GetEnvironmentVariable("mass-prediction-api-url") + "/mass-prediction");
            var ingredString = "[\""+string.Join("\",\"", request.Ingredients)+"\"]";
            requestMessage.Content = new StringContent($"{{\"base64image\":\"{request.Base64File}\",\"ingredients\":{ingredString}}}", Encoding.UTF8, "application/json");
            var test = $"{{\"base64image\":\"{request.Base64File}\",\"ingredients\":{ingredString}}}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.SendAsync(requestMessage);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var responseJson = JArray.Parse(responseString);
                        foreach (var item in responseJson)
                        {
                            string jsonString = item.ToString();

                            Dictionary<string, object> items = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
                            var ingredient = items["ingredient"];
                            var mass = items["mass"];
                            result.Add(new Dictionary<string, object> { { "ingredient", ingredient }, { "mass", mass } });
                        }
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
