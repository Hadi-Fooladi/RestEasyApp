using System;
using System.Net.Sockets;

using RestEasyApp;

namespace EasyTCP
{
	internal class DataStream : EasyTCP
	{
		public static Version Ver = new Version(1, 0);

		public DataStream()
		{
			DefinePacket<Data>(0);

			OnData += DataStream_OnData;
		}

		public event Action<Data> DataReceived;

		private void DataStream_OnData(EasyTCP sender, ushort code, object value)
		{
			switch (code)
			{
			case 0:
				var data = (Data)value;
				DataReceived?.Invoke(data);
				break;
			}
		}

		//public void Send(Data data) => Send(0, data);

		public void Connect(TcpClient client) => Connect(client, Ver.Major, Ver.Minor);
	}
}
