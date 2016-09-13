﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Message
{

	public class NoTagsMessage : NoTags
	{
		public override string channel { get { return EPermissions.TMI.ToString(); } }
		public override EPermissions permission { get { return isUserMessage ? EPermissions.USER : EPermissions.TMI; } }
	}
}
