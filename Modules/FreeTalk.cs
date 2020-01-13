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
    
    public class FreeTalk : ModuleBase<SocketCommandContext>
    {
        [Command("ai")]
        public async Task ArtificialInteligenceTalkAsync([Remainder] string message)
        {
            IRestResponse response = RequestDataFromAPI(message);
            if (response.IsSuccessful)
            {
                var res = (JObject)JsonConvert.DeserializeObject(response.Content); ;
                var responseStringContent = res["cnt"].ToString();
                await ReplyAsync(responseStringContent);
            }
        }
        [Command("riddle")]
        public async Task RiddleAsync([Remainder]string remainder = null)
        {
            string fullCommand = $"tell me a riddle";

            IRestResponse response = RequestDataFromAPI(fullCommand);
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
            string fullCommand = "what is the answer to this riddle: +" + riddle ;
            IRestResponse response = RequestDataFromAPI(fullCommand);
            if (response.IsSuccessful)
            {
                var res = (JObject)JsonConvert.DeserializeObject(response.Content); ;
                var responseStringContent = res["cnt"].ToString();
                await ReplyAsync(responseStringContent);
            }
        }
        private IRestResponse RequestDataFromAPI(string fullCommand)
        {
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
            return response;
        }
    }
}
