using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.BuiltinPlugins
{
	/// <summary>
	/// 
	/// </summary>
	[Attributes.Command(accessLevel = Message.EPermissions.TMI, respondsTo = Message.ECommand.JOIN)]
	public class OnJoin : Classes.Operator
	{
		public override void Invoke()
		{
			var chan = message.command[1].TrimStart('#');
			bot.channels.Add(chan);

			bot.OnChannelJoin?.Invoke(chan);
			//Console.WriteLine("Joined Channel " + message.command[1]);
		}
	}
}
