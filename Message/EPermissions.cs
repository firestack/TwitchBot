using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Message
{
	public enum EPermissions
	{
		/// <summary>
		/// Respond to all messages sent to a channel
		/// </summary>
		ALL,
		/// <summary>
		/// Tier 1: User
		/// </summary>
		USER,
		/// <summary>
		/// Tier 2: Sub
		/// </summary>
		SUBSCRIBER,
		/// <summary>
		/// Tier 3: Mod
		/// </summary>
		MOD,
		/// <summary>
		/// Tier 4: Broadcaster
		/// </summary>
		BROADCASTER,
		/// <summary>
		/// Tier 5: Twitch
		/// </summary>
		TMI,
		/// <summary>
		/// Tier 6: Superusers
		/// </summary>
		SUPERUSER
	};
}
