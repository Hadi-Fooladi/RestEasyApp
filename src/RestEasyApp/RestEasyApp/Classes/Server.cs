using System;
using System.Net;
using System.Net.Sockets;

namespace RestEasyApp
{
	internal class Server
	{
		private readonly TcpListener Listener;

		public Server(int port)
		{
			Listener = new TcpListener(IPAddress.Any, port);
			Listener.Start();
			Loop();
		}

		public event EventHandler<TcpClient> Connected;

		private async void Loop()
		{
			for (; ; )
			{
				var client = await Listener.AcceptTcpClientAsync();

				Connected?.Invoke(this, client);
			}
		}
	}
}
