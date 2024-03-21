using MediatR;
using Newtonsoft.Json.Linq;
using System.Reflection;
using NutriDataAPI.Models;
using NutriDataAPI.Requests;

namespace NutriDataAPI.Commands
{
    public class GetNutritionalDataCommand : IRequestHandler<GetNutritionalDataRequest, NutritionalData>
    {
        public async Task<NutritionalData> Handle(GetNutritionalDataRequest request, CancellationToken cancellationToken)
        {
            var result = new NutritionalData();
            result.SetAllPropertiesToValue(0);
            
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var ingredientsString = string.Join('+',request.IngredientsWithWeight.Keys);

                    client.BaseAddress = new Uri("https://api.api-ninjas.com/v1/");
                    client.DefaultRequestHeaders.Add("X-Api-Key", Environment.GetEnvironmentVariable("ninja-api-key"));

                    HttpResponseMessage response = await client.GetAsync("nutrition?query="+ingredientsString);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var responseJson = JArray.Parse(responseString);

                        foreach (var ingredientNutriData in responseJson)
                        {
                            string? ingredientName = (string?)ingredientNutriData["name"];
                            if (ingredientName is null)
                            {
                                continue;
                            }
                            foreach (PropertyInfo prop in result.GetType().GetProperties())
                            {
                                decimal? previousValue = (decimal?)(prop.GetValue(result));
                                decimal? ValueOfIngPer100g = (decimal?)ingredientNutriData[prop.Name];

                                if (ValueOfIngPer100g is null || previousValue is null || !request.IngredientsWithWeight.ContainsKey(ingredientName))
                                {
                                    prop.SetValue(result, null);
                                }
                                else
                                {
                                    decimal? newValue;
                                    if (ValueOfIngPer100g == 0)
                                    {
                                        newValue = 0;
                                    }
                                    else
                                    {
                                        newValue = ((decimal) request.IngredientsWithWeight[ingredientName] / 100) * ValueOfIngPer100g;
                                    }
                                    prop.SetValue(result, previousValue + newValue);
                                }
                            }
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
