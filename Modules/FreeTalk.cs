using Discord.Commands;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDcBot.Modules
{
    [Group("ft")]
    class FreeTalk : ModuleBase<SocketCommandContext>
    {   
        [Command]
        public async Task TalkAsync([Remainder] string message)
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
            if (response.IsSuccessful)
                await ReplyAsync(response.Content);
        }
    }
}
