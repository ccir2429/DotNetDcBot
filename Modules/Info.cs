using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDcBot.Modules
{
    [Group("help")]
    public class Info : ModuleBase<SocketCommandContext>
    {
        [Command,RequireContext(ContextType.Guild)]
        public async Task CommandHelpAsync()
        {
            await ReplyAsync("**Commands that will react to**:\n" +
                "`/help` - Short info about commands\n" +
                "   `/help [cmd]` - Short info about specified *cmd*\n" +
                "`/hi` - Tells you hello\n" +
                "`/ai` - Talk with a brainless being\n" +
                "`/translate` - Translate text in other languages. Usage: `/translate \"Some text\" resultLanguage` or `/translate \"Some text\"`\n" +
                "`/riddle` - tells you a riddle\n" +
                "`/answer` - tells you the answer to a riddle. Usage: `/answer \"I’m tall when I’m young and I’m short when I’m old. What am I?\"`"
                );
        }
        
        [Command("r",true),RequireContext(ContextType.Guild)]
        public async Task ServerStartHelp()
        {
            throw new NotImplementedException();
        }
    }
}
