using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TwitchBot.Util;

namespace TwitchBot.Classes
{
	// TODO: Disable Builtin commands via C#
	/// <summary>
	/// 
	/// </summary>
	public class EventHandler
	{
		/// <summary>
		///  Simple Constructor
		/// </summary>
		public EventHandler()
		{

		}

		/// <summary>
		/// Parent
		/// </summary>
		public BotBase bot;

		/// <summary>
		/// Automatically initalized
		/// </summary>
		public Interfaces.IConfigProvider configAdapter;


		/// <summary>
		/// Plugins in the current context
		/// </summary>
		public Dictionary<Message.ECommand, List<Tuple<Operator, Attributes.Command>>> plugins = new Dictionary<Message.ECommand, List<Tuple<Operator, Attributes.Command>>>();

		/// <summary>
		/// 
		/// </summary>
		public List<Tuple<Operator, Attributes.Command>> operators = new List<Tuple<Operator, Attributes.Command>>();

		/// <summary>
		/// 
		/// </summary>
		public ConfigDict channelConfigs = new ConfigDict();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		public void InvokePlugins(Message.Message msg)
		{
			foreach(var plugin in plugins[msg.action])
			{
				var methodAttribute = plugin.Item2;
				if (methodAttribute.respondsTo.HasFlag(msg.action) && msg.permission.CompareTo(methodAttribute.accessLevel) >= 0)
				{
					if(plugin.Item1.CanExecute(msg, methodAttribute.command))
					{
						LoadConfig(msg.channel, plugin.Item1);
						plugin.Item1.message = msg;
						plugin.Item1.Invoke();
					}
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void Load()
		{
			LoadPlugins();
			CreateConfigs();

			bot.OnChannelJoin += UpdateConfig;
		}

		/// <summary>
		/// 
		/// </summary>
		public void LoadPlugins()
		{
			foreach(var op in Helpers.GetTypesInAssembly<Operator>())
			{
				var attr = op.GetCustomAttributes(typeof(Attributes.Command), true) as Attributes.Command[];
				var pe = op.GetCustomAttributes(typeof(Attributes.PluginEnabled), true) as Attributes.PluginEnabled[];

				if(pe.First().enabled == false)
				{
					continue;
				}

				if (attr != null)
				{
					Operator cache = null;
					foreach(Message.ECommand flag in attr.First().respondsTo.GetEnumsInBitFlag())
					{

						if(!plugins.ContainsKey(flag))
						{
							plugins[flag] = new List<Tuple<Operator, Attributes.Command>>();
						}
						if (cache == null)
						{
							cache = Activator.CreateInstance(op) as Operator;
						}
						
						plugins[flag].Add(Tuple.Create(cache, attr.First()));
						
					}

					operators.Add(Tuple.Create(cache, attr.First()));

					cache.Setup(bot);
					cache = null;
				}

			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void CreateConfigs()
		{
			var exampleConfig = new PluginDict();
			foreach(var ob in operators)
			{
				exampleConfig[ob.Item1.GetType().Name] = ob.Item1;
			}

			foreach(var channel in bot.channels)
			{
				channelConfigs[channel] = exampleConfig;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="channel"></param>
		public void UpdateConfig(string channel)
		{
			if (channelConfigs.ContainsKey(channel))
			{
				// Save Data
			}
			else
			{
				var exampleConfig = new PluginDict();
				foreach (var ob in operators)
				{
					exampleConfig[ob.Item1.GetType().Name] = ob.Item1;
				}
				channelConfigs[channel] = exampleConfig;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="channel"></param>
		/// <param name="op"></param>
		public void LoadConfig(string channel, Operator op)
		{
			if (!channelConfigs.ContainsKey(channel))
			{
				UpdateConfig(channel);
			}

			op.CopyConfig(channelConfigs[channel][op.GetType().Name]);
		}
	}
}
