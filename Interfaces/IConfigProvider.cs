using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Interfaces
{
	/// <summary>
	/// Config Adapter for twitch bot
	/// </summary>
	public interface IConfigProvider
	{

		/// <summary>
		/// Serialize All Data
		/// </summary>
		/// <returns>Serialized string</returns>
		string Serialize(Classes.ConfigDict cfg);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="channel">channel config to serialize</param>
		/// <returns>Serialized String</returns>
		string Serialize(Classes.ConfigDict cfg, string channel);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="payload">data to deserialize</param>
		/// <returns>Channel -> (Plugin Name -> Operator Data Object)</returns>
		Classes.ConfigDict Deserialize(string payload);

		/// <summary>
		/// 
		/// </summary>
		/// <returns>Channel -> (Plugin Name -> Operator Data Object)</returns>
		Classes.ConfigDict InitalizeConfig();
	}
}
