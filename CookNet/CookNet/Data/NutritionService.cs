using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace CookNet.Data
{
    public class NutritionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseURL;

        public NutritionService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseURL = configuration["FoodDataCentral:BaseURL"];
            _apiKey = configuration["FoodDataCentral:APIKey"];
        }

        private static readonly Dictionary<string, double> unitConversions = new Dictionary<string, double>
        {
            // Conversion factors for volume measurements to grams
            { "cup", 240.0 },         // 1 cup = 240 grams (example for water/flour)
            { "cups", 240.0 },
            { "tablespoon", 16.0 },   // 1 tablespoon = 16 grams
            { "tablespoons", 16.0 },
            { "tbsp", 16.0 },
            { "tbsps", 16.0 },
            { "teaspoon", 5.0 },      // 1 teaspoon = 5 grams
            { "teaspoons", 5.0 },
            { "tsp", 5.0 },
            { "tsps", 5.0 },
            { "gram", 1.0 },          // 1 gram = 1 gram (self reference)
            { "grams", 1.0 },          
            { "g", 1.0 },
            { "gs", 1.0 },
            { "ounce", 28.35 },       // 1 ounce = 28.35 grams
            { "ounces", 28.35 },       
            { "oz", 28.35 },
            { "ozs", 28.35 },
            { "pound", 453.59 },      // 1 pound = 453.59 grams
            { "pounds", 453.59 },
            { "lb", 453.59 },
            { "lbs", 453.59 }
            // Add more units as necessary...
        };

        public static double ConvertToGrams(string unit, double quantity)
        {
            if (unitConversions.ContainsKey(unit.ToLower()))
            {
                return quantity * unitConversions[unit.ToLower()];
            }
            else
            {
                throw new ArgumentException($"Unit '{unit}' is not recognized.");
            }
        }

        public static double ConvertToStandardUnit(string unit, double quantity)
        {
            if (unitConversions.ContainsKey(unit.ToLower()))
            {
                return quantity * unitConversions[unit.ToLower()];
            }
            else
            {
                throw new ArgumentException($"Unit '{unit}' is not recognized.");
            }
        }

        public async Task<FoodItem> GetFoodDetailsAsync(string foodName)
        {
            // Step 1: Search for the food
            var searchResponse = await _httpClient.GetAsync($"{_baseURL}/foods/search?query={foodName}&api_key={_apiKey}");
            searchResponse.EnsureSuccessStatusCode();
            var response = await searchResponse.Content.ReadAsStringAsync();
            var foodResponse = JsonSerializer.Deserialize<FoodResponse>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var firstFoodItem = foodResponse?.Foods?.FirstOrDefault();
            return firstFoodItem;
        }
    }

    // FoodSearchResponse model
    public class FoodResponse
    {
        public List<FoodItem> Foods { get; set; }
    }

    public class FoodItem
    {
        public int FdcId { get; set; }
        public string Description { get; set; }
        public double ServingSize { get; set; }
        public string ServingSizeUnit { get; set; }
        public string Ingredients { get; set; }
        public string HouseholdServingFullText { get; set; }
        public List<Nutrients> FoodNutrients { get; set; }
    }

    public class Nutrients
    {
        public int NutrientId { get; set; }
        public string NutrientName { get; set; }
        public string UnitName { get; set; }
        public float Value { get; set; }
    }
}
