using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Classes
{
	/// <summary>
	/// 
	/// </summary>
	public class ConfigDict : Dictionary<string, PluginDict>
	{
	}

	/// <summary>
	/// 
	/// </summary>
	public class PluginDict : Dictionary<string, Operator> { }
}
