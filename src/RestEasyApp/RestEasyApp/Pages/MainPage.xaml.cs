using System;
using System.Net.Sockets;

using EasyTCP;
using Xamarin.Forms;

namespace RestEasyApp
{
	public partial class MainPage : ContentPage
	{
		private DataStream Stream;

		public MainPage()
		{
			InitializeComponent();
			Global.Server.Connected += Server_Connected;
		}

		private void Server_Connected(object sender, TcpClient client)
		{
			try
			{
				Stream = new DataStream();
				Stream.Connect(client);

				Stream.DataReceived += Stream_DataReceived;
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", ex.Message, "OK");
			}
		}

		private void Stream_DataReceived(Data data)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				lblHR.Text = $"{data.HR} bpm";
				lblRR.Text = $"{data.RR} breaths/min";
				lblSPO2.Text = $"{data.SPO2}%";
			});
		}

		private void bUse_OnClicked(object sender, EventArgs e)
		{
			DisplayAlert("", "Use and Export Tools", "OK");
		}

		private void bExacerbation_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new ExacerbationPage());
		}
	}
}
