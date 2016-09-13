﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace TwitchBot.Classes
{
	public abstract class MessageSocket
	{

		/// <summary>
		/// Simple Connection Constructor
		/// </summary>
		/// <param name="server"></param>
		/// <param name="port"></param>
		//public MessageSocket()
		//{
		//}

        /// <summary>
        /// The server to connect to 
        /// </summary>
        public string server;

        /// <summary>
        /// Port on server
        /// </summary>
        public int port;
        		
		/// <summary>
		/// Incoming IRC Message Buffer
		/// </summary>
		protected string incoming = String.Empty;

		/// <summary>
		/// Socket Connection Object
		/// </summary>
		protected TcpClient sockConn;

		/// <summary>
		/// Socket Stream Object
		/// </summary>
		protected NetworkStream sockStream;

		/// <summary>
		/// Stream for reading lines from the stream
		/// </summary>
		protected StreamReader sReader;

		/// <summary>
		/// Stream for writing to the socket
		/// </summary>
		protected StreamWriter sWriter;

		/// <summary>
		/// 
		/// </summary>
        public void Connect()
        {
            Disconnect();
            sockConn = new TcpClient(server, port);
            sockStream = sockConn.GetStream();

			

			//var encoding = Encoding.BigEndianUnicode;
			sReader = new StreamReader(sockStream, Encoding.UTF8);
        }

		/// <summary>
		/// 
		/// </summary>
        public virtual void Disconnect()
        {
            if (sockConn != null)
            {
                sockStream.Close();
                sockConn.Close();
            }
        }

		/// <summary>
		/// Send string to server with \r\n at the end in UTF8
		/// </summary>
		/// <param name="message"></param>			
		virtual public void Send(string message)
		{
			
			if (sockConn?.Connected != null)
			{
				var data = Encoding.UTF8.GetBytes(message.TrimEnd('\r', '\n') + "\r\n");
				sockStream.Write(data, 0, data.Length);
			}
		}

		/// <summary>
		/// Recieve Data if connected in UTF8
		/// </summary>
		/// <returns>Data inside buffer at time of calling or after blocking</returns>
		virtual protected string Receive()
		{
			if (sockConn?.Connected != null)
			{
				// On _Data_ Received
				var data = new byte[2048];
				int bytes = sockStream.Read(data, 0, data.Length);
				return Encoding.UTF8.GetString(data, 0, bytes);

			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Read Line from network stream separated by \r\n
		/// </summary>
		/// <returns>Line read from server</returns>
		virtual protected string ReceiveLine()
		{
			return sReader.ReadLine();
		}
	}
}
