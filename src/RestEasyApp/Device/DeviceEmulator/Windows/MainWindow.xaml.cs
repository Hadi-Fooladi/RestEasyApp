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
		private DataStream Stream;

		private readonly DispatcherTimer timer = new DispatcherTimer();

		public MainWindow()
		{
			InitializeComponent();

			timer.Interval = TimeSpan.FromMinutes(1);
			timer.Tick += timer_Tick;
		}

		public void OnWindowClosing(object sender, CancelEventArgs e)
		{
			// Stop the timer
			if (timer.IsEnabled)
			{
				timer.Stop();
				MessageBox.Show("Stopping Timer");
			}
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Loaded");
		}

		private void timer_Tick(object sender, EventArgs e)
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

		private void bConnect_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				var client = new TcpClient("127.0.0.1", 4689);

				Stream = new DataStream();
				Stream.Connect(client);

				timer.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

	}
}
