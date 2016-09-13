using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Classes
{
	/// <summary>
	/// Config Component for defining which classes are used
	/// </summary>
	public class BotComponentsConfig
	{
		/// <summary>
		/// 
		/// </summary>
		public void StartBot()
		{
			bot.EH.configAdapter = cfgApt;
			bot.superusers = superUsers;
			bot.Start(cred);
		}

		/// <summary>
		/// 
		/// </summary>
		public BotBase bot = new Bot();

		/// <summary>
		/// 
		/// </summary>
		public Interfaces.IConfigProvider cfgApt = new ConfigAdapters.JsonAdapter();

		/// <summary>
		/// super users
		/// </summary>
		public HashSet<string> superUsers = new HashSet<string>();

		/// <summary>
		/// credentials
		/// </summary>
		public Credentials cred = new Credentials();
	}
}
