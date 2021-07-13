using System;
using System.Net.Sockets;

using DeviceEmulator;

namespace EasyTCP
{
	internal class DataStream : EasyTCP
	{
		public static Version Ver = new Version(1, 0);

		public DataStream()
		{
			DefinePacket<Data>(0);
		}

		public void Send(Data data) => Send(0, data);

		public void Connect(TcpClient client) => Connect(client, Ver.Major, Ver.Minor);
	}
}
