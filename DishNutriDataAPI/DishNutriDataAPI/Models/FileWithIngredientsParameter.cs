namespace DishNutriDataAPI.Models
{
    /// <summary>
    /// Class to properly show that a base64 string describing a file and an ingredient list are used as a parameter.
    /// </summary>
    public struct FileWithIngredientsParameter
    {
        /// <summary>
        /// base64 encoded file as a string.
        /// </summary>
        public string Base64File{ get; set; }
        /// <summary>
        /// List of ingredients where ingredients are represented as strings.
        /// </summary>
        public List<string > Ingredients { get; set;}
    }
}
