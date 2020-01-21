using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace DotNetDcBot.Modules
{

    public class Ping : ModuleBase<SocketCommandContext>
    {
        public Ping() { }
        [Command("info")]
        public async Task CmdHelpAsync()
        {
            await ReplyAsync("**Commands that will react to**:\n" +
                "`/help` - Short info about commands\n" +
                "`/hi` - Tells you hello\n" +
                "`/ai` - Talk with a brainless being\n" +
                "`/translate` - Translate text in other languages. Usage: `/translate \"Some text\" resultLanguage` or `/translate \"Some text\"`\n" +
                "`/riddle` - tells you a riddle\n" +
                "`/answer` - tells you the answer to a riddle. Usage: `/answer \"I’m tall when I’m young and I’m short when I’m old. What am I?\"`"
                );
        }

        [Command("t")]
        public async Task Translate(string word, string intoLanguage = null)
        {
            if (intoLanguage is null)
                intoLanguage = "english";
            await TranslateAsync(word, intoLanguage);
            
        }

        [Command("hi")]
        public async Task HiToUserAsync()
        {
            await ReplyAsync($"Hello||, {Context.Message.Author.Mention}||!");
        }
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
        [Command("translate")]
        public async Task TranslateAsync(string word, string intoLanguage = null)
        {
            if (intoLanguage is null)
                intoLanguage = "english";
            string fullTransCommand = $"translate {word} to {intoLanguage}";
            IRestResponse response = RequestDataFromAPI(fullTransCommand);
            if (response.IsSuccessful)
            {
                var res = (JObject)JsonConvert.DeserializeObject(response.Content); ;
                var responseStringContent = res["cnt"].ToString();
                await ReplyAsync(responseStringContent.Replace($"{word} ->", "").Replace("(translations by Microsoft translator)", ""));
            }
        }

        public string TranslateString(string word, string lang)
        {
            string fullTransCommand = $"translate {word} to {lang}";
            IRestResponse response = RequestDataFromAPI(fullTransCommand);
            if (response.IsSuccessful)
            {
                var res = (JObject)JsonConvert.DeserializeObject(response.Content); ;
                var responseStringContent = res["cnt"].ToString();
                return responseStringContent;//.Replace($"{word} ->", "").Replace("(translations by Microsoft translator)", "");
            }
            return response.Content;
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
            string fullCommand = "what is the answer to the riddle \"+" + riddle + '"';
            IRestResponse response = RequestDataFromAPI(fullCommand);
            if (response.IsSuccessful)
            {
                var res = (JObject)JsonConvert.DeserializeObject(response.Content); ;
                var responseStringContent = res["cnt"].ToString();
                await ReplyAsync(responseStringContent);
            }
        }


        [Command("exit", true)]
        public async Task ShutdownBot()
        {
            await ReplyAsync("Shutting down...");
            await Task.Delay(500);
            await Context.Client.StopAsync();
            await Task.Delay(5000);
            Environment.Exit(0);

        }

        //create a command that lets the user send an embed to another server(assumning the bot is a member of that group)
        [Command("emb")]
        public async Task SendEmbedAsync()
        {
            var exampleAuthor = new EmbedAuthorBuilder()
           .WithName("WithName")
           .WithIconUrl(Context.Message.Author.GetAvatarUrl());
            //await ReplyAsync("test!");
            var _embed = new EmbedBuilder
            {
                Author = exampleAuthor
            };


            _embed.WithColor(Color.DarkPurple)
                .AddField("name", "object \nvalue");
            await ReplyAsync(embed: _embed.Build());
        }
        
        [Command("served", true)]
        public async Task GetAllGuildChannelsAsync()
        {
            var server = new EmbedAuthorBuilder();
            var _embed = new EmbedBuilder();
            var shard = Context.Client.Rest.GetGuildsAsync();
           // await ReplyAsync(shard.Result.Count.ToString());
            foreach (var conn in shard.Result)
            {
                server.WithName(conn.Name)
                    .WithIconUrl((conn.IconUrl == null) ? Context.Client.CurrentUser.GetDefaultAvatarUrl() : conn.IconUrl);

                _embed.Author = server;
                var guildItem = await conn.GetChannelsAsync();

                foreach (var ch in guildItem)
                {
                    
                    _embed.AddField("||"+ch.Id.ToString()+"||","**"+ch.Name+"**");
                }
                var users = conn.GetUsersAsync().FlattenAsync().Result.GetEnumerator();
                int userCount = 0;
                while(users.MoveNext())
                {
                    if (!(users.Current.IsBot))
                        userCount++;
                }
                _embed.WithColor(Color.Blue)
                    .WithFooter("Members: "+userCount.ToString());
                await ReplyAsync(embed: _embed.Build());
            }
            
            
        }
        [Command("test"),RequireOwner,RequireContext(ContextType.Guild)]
        public async Task Test(string newName = "Bot Test Server")
        {
            
            var conns = await Context.Client.GetConnectionsAsync();
            await ReplyAsync(conns.Count.ToString());

            foreach (var s in conns)
            {
                await ReplyAsync(s.ToString());
            }
        }

        [Command("mention",true)]
        public async Task MentionUser(ulong userId)
        {
            await ReplyAsync($"<@{userId}>");
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
