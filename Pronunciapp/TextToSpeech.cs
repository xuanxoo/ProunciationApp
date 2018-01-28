using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pronunciapp
{
    public class TextToSpeech
    {

        private const string GOOGLE_TEXT_TO_SPEECH_URL = "http://translate.google.com/translate_tts?";
        private string client;
        private FileInfo file;
        private string lang;

        public TextToSpeech(string lang)
        {
            this.lang = lang;
        }

        public TextToSpeech(string lang, FileInfo file)
        {
            this.lang = lang;
            this.file = file;
        }

        public async Task Download(string text)
        {
            if (string.IsNullOrEmpty(this.lang))
                throw new Exception("Language doesn't informed");

            if (string.IsNullOrEmpty(text))
                throw new Exception("Text doesn't informed");

            var url = mountUrl(text);
            try
            {
                var response = await CallToGoogle(url);

                Console.WriteLine($"Status: {response.StatusCode}");
                var res = await response.Content.ReadAsStreamAsync();
                using (var stream = new FileStream(this.file.FullName, FileMode.Create, FileAccess.Write, FileShare.None, 20000, true))
                {
                    await res.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                var ms = ex.Message;
            }
        }

        public async Task<Stream> DownloadAsStream(string text)
        {
            if (string.IsNullOrEmpty(this.lang))
                throw new Exception("Language doesn't informed");

            if (string.IsNullOrEmpty(text))
                throw new Exception("Text doesn't informed");

            var url = mountUrl(text);
            try
            {
                var response = await CallToGoogle(url);

                Console.WriteLine($"Status: {response.StatusCode}");
                var res = await response.Content.ReadAsStreamAsync();
                return res;
            }
            catch (Exception ex)
            {
                var ms = ex.Message;
            }
            return null;
        }


        private async Task<HttpResponseMessage> CallToGoogle(string url)
        {

            // Setup request
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url)
            };


            request.Headers.Referrer = new Uri("http://translate.google.com/");
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("audio/mpeg"));
            request.Headers.Add("User-Agent", "stagefright/1.2 (Linux;Android 5.0)");

            var clienth = new System.Net.Http.HttpClient();
            return await clienth.SendAsync(request);

        }

        public string mountUrl(string text)
        {
            return GOOGLE_TEXT_TO_SPEECH_URL + "ie=UTF-8&q=" + text + "&client=tw-ob&tl=" + this.lang;
        }
    }
}
