using System.Globalization;

namespace TradingAppMvc.Domain.Utils
{
    public static class FormatTool
    {

        public enum FormatType
        {
            LowerCase,
            UpperCase,
            SnakeCase,
            TitleCase
        }

        public static string ToSnakeCase(string input)
        {
            return string.Concat(input.Select((c, i) => i > 0 && char.IsUpper(c) ? "_" + c.ToString() : c.ToString())).ToLower();
        }

        public static string ToTitleCase(string input)
        {
            // Split the input based on camel case or underscore
            string[] words = input.Split(new[] { '_', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Capitalize the first letter of each word
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[i].ToLower());
            }

            // Join the words back together with a space between them
            return string.Join(" ", words);
        }
    }
}