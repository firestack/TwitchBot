using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchBot.Classes;
using Newtonsoft.Json;

namespace TwitchBot.ConfigAdapters
{
	/// <summary>
	/// Json Adapter
	/// </summary>
	class JsonAdapter : Interfaces.IConfigProvider
	{
		/// <summary>
		/// 
		/// </summary>
		protected JsonSerializerSettings jsonSettings = new JsonSerializerSettings
		{
			TypeNameHandling = TypeNameHandling.All,
			TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
		};

		public ConfigDict Deserialize(string payload)
		{
			return JsonConvert.DeserializeObject<ConfigDict>(payload, jsonSettings);
		}

		public ConfigDict InitalizeConfig()
		{
			throw new NotImplementedException();
		}


		public string Serialize(ConfigDict cfg)
		{
			return JsonConvert.SerializeObject(cfg, Formatting.None, jsonSettings);
		}

		public string Serialize(ConfigDict cfg, string channel)
		{
			var dict = new ConfigDict();
			dict[channel] = cfg[channel];
			return JsonConvert.SerializeObject(dict, Formatting.None, jsonSettings);
		}
	}
}
