using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
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
			StartConnection();
		}

		private void StartConnection()
		{
			try
			{
				Stream = new DataStream();
				Stream.Connect(new TcpClient("10.0.2.2", 4689));

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
