using System;
using System.Net;
using System.Windows;
using System.Net.Sockets;

using EasyTCP;

namespace DeviceEmulator
{
	public partial class MainWindow
	{
		private readonly TcpListener Listener = new TcpListener(IPAddress.Any, 4689);

		private DataStream Stream;

		public MainWindow()
		{
			InitializeComponent();

			Listener.Start();

			Loop();
		}

		private async void Loop()
		{
			for (;;)
			{
				var client = await Listener.AcceptTcpClientAsync();

				MessageBox.Show("Connected");

				Stream = new DataStream();
				Stream.Connect(client);
			}
		}

		private void bSend_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				var data = new Data
				{
					HR = float.Parse(tbHR.Text),
					RR = float.Parse(tbRR.Text),
					SPO2 = float.Parse(tbSPO2.Text)
				};

				Stream?.Send(data);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
