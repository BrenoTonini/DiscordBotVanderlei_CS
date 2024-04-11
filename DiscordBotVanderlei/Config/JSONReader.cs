using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotVanderlei.Config
{
    internal class JSONReader
    {
        private string DiscordToken;
        private string Prefix;
        private string OpenAIToken;

        public async Task ReadJSON()
        {
            using (StreamReader sr = new StreamReader("Config.json")) 
            {
                string json = await sr.ReadToEndAsync();
                JSONStructure data  = JsonConvert.DeserializeObject<JSONStructure>(json);

                DiscordToken = data.discord_bot_token;
                Prefix = data.prefix;
                OpenAIToken = data.openai_api_token;
            }
        }

        public string GetDiscordToken() { return DiscordToken; }
        public string GetPrefix() { return Prefix; }
        public string GetOpenAIToken() {  return OpenAIToken; }
    }

    internal sealed class JSONStructure
    {
        public string discord_bot_token;
        public string prefix;
        public string openai_api_token;
    }
}
