namespace DishNutriDataAPI.Models
{
    /// <summary>
    /// Class to properly show that a base64 string describing a file is used as a parameter.
    /// </summary>
    public struct FileParameter
    {
        /// <summary>
        /// base64 encoded file as a string.
        /// </summary>
        public string Base64File { get; set; }
    }
}
