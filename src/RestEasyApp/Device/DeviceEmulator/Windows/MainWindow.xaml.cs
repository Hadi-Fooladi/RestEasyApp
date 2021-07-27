using System;
using System.Net;
using System.Windows;
using System.Net.Sockets;
using EasyTCP;
using System.Windows.Threading;
using System.ComponentModel;

namespace DeviceEmulator
{
	public partial class MainWindow
	{
		private readonly TcpListener Listener = new TcpListener(IPAddress.Any, 4689);

		private DataStream Stream;

		private readonly DispatcherTimer timer = new DispatcherTimer();

		public MainWindow()
		{
			InitializeComponent();

			// Setting the timer for 1 minute
			timer.Interval = TimeSpan.FromSeconds(1);
			timer.Tick += timer_Tick;

			Listener.Start();

			Loop();
		}

		private void SendData()
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

		public void OnWindowClosing(object sender, CancelEventArgs e)
		{
			// Stop the timer
			if (timer.IsEnabled)
			{
				timer.Stop();
				//MessageBox.Show("Stopping Timer");
			}
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show("Loaded");
		}

		private void timer_Tick(object sender, EventArgs e)
        {
			SendData();
		}

		private async void Loop()
		{
			for (;;)
			{
				var client = await Listener.AcceptTcpClientAsync();

				//MessageBox.Show("Connected");

				// Start the timer
				timer.Start();

				//MessageBox.Show("Starting Timer");

				Stream = new DataStream();
				Stream.Connect(client);
			}
		}
	}
}
