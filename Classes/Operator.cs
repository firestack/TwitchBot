using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Classes
{
	/// <summary>
	/// Base class for operating on messages recieved from IRC stream
	/// </summary>
	[Attributes.PluginEnabled(enabled = true, exposed = false)]
	public abstract class Operator
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		public static void CopyConfig(Operator lhs, Operator rhs)
		{
			if(lhs.GetType() != rhs.GetType())
			{
				throw new ArgumentException("Copy config operation on operators failed: Operators are not of the same type");
			}

			var lhsType = lhs.GetType();

			foreach(var member in lhsType.GetFields())
			{
				if(member.GetCustomAttribute<NonSerializedAttribute>() == null)
				{
					member.SetValue(lhs, member.GetValue(rhs));
				}
			}
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="that"></param>
		public void CopyConfig(Operator that)
		{
			CopyConfig(that, this);
		}

		/// <summary>
		/// Enabled flag for operators
		/// </summary>
		public bool enabled = true;

		/// <summary>
		/// The current bot that this plugin belongs too
		/// </summary>
		[NonSerialized]
		public BotBase bot;

		/// <summary>
		/// The current message being worked on by the bot
		/// </summary>
		[NonSerialized]
		public Message.Message message;

		/// <summary>
		/// Funciton invoked for each message which this plugin can execute on
		/// </summary>
		public abstract void Invoke();

		internal void Setup(BotBase bot)
		{
			this.bot = bot;

			Init();
		}

		/// <summary>
		/// Overridable function for initalization per plugin
		/// </summary>
		public virtual void Init()
		{

		}

		/// <summary>
		/// Checks if this plugin can execute
		/// </summary>
		/// <param name="msg">Message to test against</param>
		/// <param name="val">String to compare message with</param>
		/// <returns>If this plugin can execute</returns>
		public virtual bool CanExecute(Message.Message msg, string val)
		{
			if (!enabled)
			{
				return false;
			}

			return (val == null) ? true : msg.message.StartsWith(val);
		}

	}
}
