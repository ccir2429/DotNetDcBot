using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDcBot.Entities
{
    class GuildBotConfig
    {
        public GuildBotConfig() { }
        string nickname { get; set;  }
        SocketRole adminRole { get; set; }
        SocketRole testerRole { get; set; }
        
    }
}
