namespace DishNutriDataAPI.Properties
{
    using System;
    using System.IO;

    public static class DotEnv
    {
        public static void Load()
        {
            if (!File.Exists(".env"))
                return;
            foreach (var line in File.ReadAllLines(".env"))
            {
                string[] parts = line.Split(new[] { '=' }, 2, StringSplitOptions.None);

                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}