using DishNutriDataAPI.Requests;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace DishNutriDataAPI.Commands
{
    public class GetMassPredictionCommand : IRequestHandler<GetMassPredictionRequest, List<Dictionary<string, object>>>
    {
        public async Task<List<Dictionary<string, object>>> Handle(GetMassPredictionRequest request, CancellationToken cancellationToken)
        {
            var result = new List<Dictionary<string, object>>();
            
            // Create a new HttpRequestMessage with the multipart content
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, Environment.GetEnvironmentVariable("mass-prediction-api-url") + "/mass-prediction");
            requestMessage.Content = new StringContent($"{{\"file\":\"{request.Base64File}\",\"ingredients\":{request.Ingredients.ToString()}}}", Encoding.UTF8, "application/json");

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
                            var ingredient = items["irgendient"];
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
