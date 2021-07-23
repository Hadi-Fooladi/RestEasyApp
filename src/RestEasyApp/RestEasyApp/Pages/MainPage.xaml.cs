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
		}

		protected override void OnAppearing()
		{

			var sb = new StringBuilder();
			foreach (var data in Global.Database.AllData)
			{
				sb.AppendLine($"{data.Date}: {data.HR}");
			}

			DisplayAlert("", sb.ToString(), "OK");

			base.OnAppearing();

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
			Global.Database.Add(new DB.Data
			{
				HR = data.HR,
				Date = DateTime.Now
			});

			Device.BeginInvokeOnMainThread(() =>
			{
				lblHR.Text = $"{data.HR} bpm";
				lblRR.Text = $"{data.RR} breaths/min";
				lblSPO2.Text = $"{data.SPO2}%";
			});
		}

		private void bConnect_OnClicked(object sender, EventArgs e)
		{
			StartConnection();
		}

		private void bExacerbation_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new ExacerbationPage());
		}
	}
}
