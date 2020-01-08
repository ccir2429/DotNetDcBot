using Discord.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDcBot.Modules
{
    
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("help",true)]
        public async Task CmdHelpAsync()
        {
            await ReplyAsync("**Commands that will react to**:\n" +
                "`//help` - Short info about commands\n"+
                "`//hi` - Tells you hello\n"+
                "`//ai` - Talk with a brainless being\n"+
                "`//translate` - Translate text in other languages. Usage: `//translate \"Some text\" resultLanguage` or `//translate \"Some text\"`\n"
                );
        }

        [Command("hi")]
        public async Task HiToUserAsync()
        {
            await ReplyAsync($"Hello||, {Context.Message.Author.Mention}||!");
        }
        [Command("ai")]
        public async Task ArtificialInteligenceTalkAsync([Remainder] string message)
        {
            var encodedMessage = System.Web.HttpUtility.UrlEncode(message);
            var url = "https://acobot-brainshop-ai-v1.p.rapidapi.com/get?" +
                "bid=178" +
                "&key=sX5A2PcYZbsN5EY6" +
                "&uid=mashape" +
                $"&msg={encodedMessage}";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "acobot-brainshop-ai-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "2883dd0e29msha3d1ec5c6d3ca7cp12b85djsnb1129a6f6def");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful) {
                var res = (JObject)JsonConvert.DeserializeObject(response.Content);;
                var responseStringContent = res["cnt"].ToString();
                await ReplyAsync(responseStringContent);
            }
        }
        [Command("translate")]
        public async Task TalkAsync(string word,string intoLanguage="english")
        {
            string fullTransCommand = $"translate {word} into {intoLanguage}";
            var encodedMessage = System.Web.HttpUtility.UrlEncode(fullTransCommand);
            var url = "https://acobot-brainshop-ai-v1.p.rapidapi.com/get?" +
                "bid=178" +
                "&key=sX5A2PcYZbsN5EY6" +
                "&uid=mashape" +
                $"&msg={encodedMessage}";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "acobot-brainshop-ai-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "2883dd0e29msha3d1ec5c6d3ca7cp12b85djsnb1129a6f6def");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var res = (JObject)JsonConvert.DeserializeObject(response.Content); ;
                var responseStringContent = res["cnt"].ToString();
                await ReplyAsync(responseStringContent.Replace($"{word} ->", "").Replace("(translations by Microsoft translator)", ""));
            }
        }
        [Command("riddle")]
        public async Task RiddleAsync([Remainder]string remainder=null)
        {
            string fullCommand = $"tell me a riddle";
            var encodedMessage = System.Web.HttpUtility.UrlEncode(fullCommand);
            var url = "https://acobot-brainshop-ai-v1.p.rapidapi.com/get?" +
                "bid=178" +
                "&key=sX5A2PcYZbsN5EY6" +
                "&uid=mashape" +
                $"&msg={encodedMessage}";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "acobot-brainshop-ai-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "2883dd0e29msha3d1ec5c6d3ca7cp12b85djsnb1129a6f6def");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var res = (JObject)JsonConvert.DeserializeObject(response.Content); ;
                var responseStringContent = res["cnt"].ToString();
                await ReplyAsync(responseStringContent);
            }
        }
        [Command("answer")]
        public async Task RiddleAnswerAsync(string riddle)
        {
            string fullCommand = "what is the answer to the riddle \"+"+riddle+'"';
            var encodedMessage = System.Web.HttpUtility.UrlEncode(fullCommand);
            var url = "https://acobot-brainshop-ai-v1.p.rapidapi.com/get?" +
                "bid=178" +
                "&key=sX5A2PcYZbsN5EY6" +
                "&uid=mashape" +
                $"&msg={encodedMessage}";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "acobot-brainshop-ai-v1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "2883dd0e29msha3d1ec5c6d3ca7cp12b85djsnb1129a6f6def");
            IRestResponse response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var res = (JObject)JsonConvert.DeserializeObject(response.Content); ;
                var responseStringContent = res["cnt"].ToString();
                await ReplyAsync(responseStringContent);
            }
        }
    }
}
