using System.Speech.Synthesis;

namespace DeepLTranslator.Services
{
    public class TextToSpeechService : IDisposable
    {
        private readonly SpeechSynthesizer _synthesizer;

        public TextToSpeechService()
        {
            _synthesizer = new SpeechSynthesizer();
            _synthesizer.SetOutputToDefaultAudioDevice();
        }

        public async Task SpeakAsync(string text, string languageCode = "en-US")
        {
            try
            {
                // Mapear códigos de idioma de DeepL a códigos de voz
                var voiceLanguage = MapLanguageCodeToVoice(languageCode);
                
                // Intentar establecer la voz para el idioma
                var voices = _synthesizer.GetInstalledVoices();
                var voice = voices.FirstOrDefault(v => 
                    v.VoiceInfo.Culture.Name.StartsWith(voiceLanguage, StringComparison.OrdinalIgnoreCase));

                if (voice != null)
                {
                    _synthesizer.SelectVoice(voice.VoiceInfo.Name);
                }

                await Task.Run(() => _synthesizer.Speak(text));
            }
            catch (Exception ex)
            {
                throw new Exception($"Text-to-speech failed: {ex.Message}");
            }
        }

        public void Stop()
        {
            _synthesizer.SpeakAsyncCancelAll();
        }

        private string MapLanguageCodeToVoice(string deepLLanguageCode)
        {
            return deepLLanguageCode.ToUpper() switch
            {
                "EN" or "EN-US" or "EN-GB" => "en-US",
                "ES" => "es-ES",
                "FR" => "fr-FR",
                "DE" => "de-DE",
                "IT" => "it-IT",
                "PT" or "PT-PT" or "PT-BR" => "pt-BR",
                "RU" => "ru-RU",
                "JA" => "ja-JP",
                "ZH" => "zh-CN",
                _ => "en-US"
            };
        }

        public void Dispose()
        {
            _synthesizer?.Dispose();
        }
    }
}
