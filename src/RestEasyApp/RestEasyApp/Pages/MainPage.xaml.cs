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
		//private int Counter = 0;
		private DataStream Stream;

		public MainPage()
		{
			InitializeComponent();
			//Device.StartTimer(TimeSpan.FromSeconds(1), Count);
		}

		/*private bool Count()
		{
			lblHR.Text = $"{++Counter}";

			return Counter < 10;
		}*/

		private void bExacerbation_OnClicked(object sender, EventArgs e)
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
				lblHR.Text = $"{data.HR}";
				lblRR.Text = $"{data.RR}";
				lblSPO2.Text = $"{data.SPO2}";
			});
		}

		private void bUse_OnClicked(object sender, EventArgs e)
		{
			DisplayAlert("", "Use", "OK");
		}
	}
}
