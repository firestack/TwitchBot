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
	[Attributes.Command(accessLevel = Message.EPermissions.TMI, respondsTo = Message.ECommand.LEAVE)]
	public class OnLeave : Classes.Operator
	{
		public override void Invoke()
		{
			var chan = message.command[1].TrimStart('#');
			bot.channels.Remove(chan);

			bot.OnChannelLeave?.Invoke(chan);
			//Console.WriteLine("Left Channel " + message.command[1]);
		}
	}
}
