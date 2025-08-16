using System.Text;
using Newtonsoft.Json;
using DeepLTranslator.Models;

namespace DeepLTranslator.Services
{
    public class DeepLService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BaseUrl = "https://api-free.deepl.com/v2";

        public DeepLService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"DeepL-Auth-Key {_apiKey}");
        }

        public async Task<(string translatedText, string detectedLanguage)> TranslateTextAsync(string text, string targetLanguage, string? sourceLanguage = null)
        {
            try
            {
                var parameters = new List<KeyValuePair<string, string>>
                {
                    new("text", text),
                    new("target_lang", targetLanguage)
                };

                if (!string.IsNullOrEmpty(sourceLanguage))
                {
                    parameters.Add(new("source_lang", sourceLanguage));
                }

                var content = new FormUrlEncodedContent(parameters);
                var response = await _httpClient.PostAsync($"{BaseUrl}/translate", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var translationResponse = JsonConvert.DeserializeObject<DeepLTranslationResponse>(jsonResponse);

                    if (translationResponse?.Translations?.Count > 0)
                    {
                        var translation = translationResponse.Translations[0];
                        return (translation.Text, translation.DetectedSourceLanguage);
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"API Error: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Translation failed: {ex.Message}");
            }

            return (string.Empty, string.Empty);
        }

        public async Task<List<DeepLLanguagesResponse>> GetSupportedLanguagesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}/languages?type=target");
                
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var languages = JsonConvert.DeserializeObject<List<DeepLLanguagesResponse>>(jsonResponse);
                    return languages ?? new List<DeepLLanguagesResponse>();
                }
            }
            catch (Exception ex)
            {
                // En caso de error, devolver idiomas predeterminados
                Console.WriteLine($"Error getting languages: {ex.Message}");
            }

            return GetDefaultLanguages();
        }

        private List<DeepLLanguagesResponse> GetDefaultLanguages()
        {
            return new List<DeepLLanguagesResponse>
            {
                new() { Language = "EN-US", Name = "English (American)" },
                new() { Language = "ES", Name = "Spanish" },
                new() { Language = "FR", Name = "French" },
                new() { Language = "DE", Name = "German" },
                new() { Language = "IT", Name = "Italian" },
                new() { Language = "PT-PT", Name = "Portuguese" },
                new() { Language = "RU", Name = "Russian" },
                new() { Language = "JA", Name = "Japanese" },
                new() { Language = "ZH", Name = "Chinese" }
            };
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
