using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDcBot.Modules
{
    [Group("setup")]
    public class BotSetup : ModuleBase<SocketCommandContext>
    {/*
        [Command("owner")]
        public async Task SetGroupOwner(ulong newOwId = 0) {
            if (newOwId == 0)
            {
                await ReplyAsync("/setup owner *newOwnerId*  parameter __*newOwnerId*__ is missing.");
                return;
            }
            var id = Context.Guild.Id;
            var guild = Context.Client.GetGuild(id);
            await guild.ModifyAsync(group=>
            {
                group.Owner = guild.GetUser(newOwId);
                group.OwnerId = newOwId;
                group.VerificationLevel = VerificationLevel.High;

            });
        }
        */
    }
}
