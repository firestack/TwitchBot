﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Message
{
	[Flags]
	public enum ECommand : int // 32 Bits of Flags!
	{
		UNKNOWN				= 1 << 0,
		PRIVMSG				= 1 << 1,
		PING				= 1 << 2,
		CLEARCHAT			= 1 << 3,
		USERSTATE			= 1 << 4,
		NUMERIC				= 1 << 5,
		NOTICE				= 1 << 6,
		CAP					= 1 << 7,
		WHISPER				= 1 << 8,
		HOSTTARGET			= 1 << 9,
		RECONNECT			= 1 << 10,
		USERNOTICE			= 1 << 11,
		ROOMSTATE			= 1 << 12,
		JOIN				= 1 << 13,
		LEAVE				= 2 << 14,

		ALL = ~0,
	}

	public static class ECommandExt
	{
		public static EScope GetMessageScope(this ECommand act)
		{
			var ChannelFlags = ECommand.PRIVMSG | ECommand.USERSTATE | ECommand.ROOMSTATE | ECommand.CLEARCHAT | ECommand.NOTICE;


			if (act.HasFlag(ECommand.WHISPER))
			{
				return EScope.USER;
			}
			else if (act.HasFlag(ChannelFlags))
			{
				return EScope.CHANNEL;
			}
			else
			{
				return EScope.GLOBAL;
			}
		}
};
}
