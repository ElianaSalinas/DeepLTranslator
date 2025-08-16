using DeepLTranslator.Models;

namespace DeepLTranslator.Services
{
    public static class LanguageService
    {
        public static List<LanguageInfo> GetLanguagesWithFlags()
        {
            return new List<LanguageInfo>
            {
                new() { Code = "EN-US", Name = "English (US)", FlagEmoji = "🇺🇸" },
                new() { Code = "EN-GB", Name = "English (UK)", FlagEmoji = "🇬🇧" },
                new() { Code = "ES", Name = "Spanish", FlagEmoji = "🇪🇸" },
                new() { Code = "FR", Name = "French", FlagEmoji = "🇫🇷" },
                new() { Code = "DE", Name = "German", FlagEmoji = "🇩🇪" },
                new() { Code = "IT", Name = "Italian", FlagEmoji = "🇮🇹" },
                new() { Code = "PT-PT", Name = "Portuguese", FlagEmoji = "🇵🇹" },
                new() { Code = "PT-BR", Name = "Portuguese (Brazil)", FlagEmoji = "🇧🇷" },
                new() { Code = "RU", Name = "Russian", FlagEmoji = "🇷🇺" },
                new() { Code = "JA", Name = "Japanese", FlagEmoji = "🇯🇵" },
                new() { Code = "ZH", Name = "Chinese", FlagEmoji = "🇨🇳" },
                new() { Code = "KO", Name = "Korean", FlagEmoji = "🇰🇷" },
                new() { Code = "NL", Name = "Dutch", FlagEmoji = "🇳🇱" },
                new() { Code = "PL", Name = "Polish", FlagEmoji = "🇵🇱" },
                new() { Code = "SV", Name = "Swedish", FlagEmoji = "🇸🇪" },
                new() { Code = "DA", Name = "Danish", FlagEmoji = "🇩🇰" },
                new() { Code = "NO", Name = "Norwegian", FlagEmoji = "🇳🇴" },
                new() { Code = "FI", Name = "Finnish", FlagEmoji = "🇫🇮" }
            };
        }

        public static string GetLanguageName(string languageCode)
        {
            var language = GetLanguagesWithFlags().FirstOrDefault(l => 
                l.Code.Equals(languageCode, StringComparison.OrdinalIgnoreCase));
            return language?.Name ?? languageCode;
        }

        public static string GetLanguageFlag(string languageCode)
        {
            var language = GetLanguagesWithFlags().FirstOrDefault(l => 
                l.Code.Equals(languageCode, StringComparison.OrdinalIgnoreCase));
            return language?.FlagEmoji ?? "🌐";
        }
    }
}
