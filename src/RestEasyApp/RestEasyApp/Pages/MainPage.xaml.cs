using System;
using System.Net.Sockets;
using System.Text;
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
			RetrieveData();
		}

		protected override void OnAppearing()
		{
			var sb = new StringBuilder();
			foreach (var data in Global.Database.AllData)
			{
				sb.AppendLine($"{data.Date}, HR = {data.HR}, RR = {data.RR}, SPO2 = {data.SPO2}, Alarm: {data.Alarm}");
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
		private void RetrieveData()
		{
			lblHR.Text = $"{Global.Database.GetLatestData.HR} bpm";
			lblRR.Text = $"{Global.Database.GetLatestData.RR} breaths/min";
			lblSPO2.Text = $"{Global.Database.GetLatestData.SPO2}%";

			lblAlarm.Text = Format_Time(Global.Database.GetLastAlarm.Date.Year,
										Global.Database.GetLastAlarm.Date.Month,
										Global.Database.GetLastAlarm.Date.Day,
										Global.Database.GetLastAlarm.Date.Hour,
										Global.Database.GetLastAlarm.Date.Minute);
		}

		private void SetAlarm()
		{
			if(Global.Database.GetLatestData.HR < 60 || Global.Database.GetLatestData.HR > 100 || Global.Database.GetLatestData.RR < 12 || Global.Database.GetLatestData.RR > 20 || Global.Database.GetLatestData.SPO2 < 95)
			{
				lblAlarm.Text = Format_Time(Global.Database.GetLatestData.Date.Year,
											Global.Database.GetLatestData.Date.Month,
											Global.Database.GetLatestData.Date.Day,
											Global.Database.GetLatestData.Date.Hour,
											Global.Database.GetLatestData.Date.Minute);
			}
		}

		private string Format_Time(int Year, int Month, int Day, int Hour, int Minute)
		{
			string month = "";
			switch (Month)
			{
				case 1: month = "January"; break;
				case 2: month = "February"; break;
				case 3: month = "March"; break;
				case 4: month = "April"; break;
				case 5: month = "May"; break;
				case 6: month = "June"; break;
				case 7: month = "July"; break;
				case 8: month = "August"; break;
				case 9: month = "September"; break;
				case 10: month = "October"; break;
				case 11: month = "November"; break;
				case 12: month = "December"; break;
				default: break;
			}

			int hour;
			String AMorPM;
			if (Hour > 12)
			{
				hour = Hour - 12;
				AMorPM = "PM";
			}
			else
			{
				if (Hour == 0) hour = 12;
				else hour = Hour;
				AMorPM = "AM";
			}

			String zero = "";
			if (Minute < 10)
				zero = "0";

			return $"Last alarm: {month} {Day}, {Year}\nat {hour}:{zero}{Minute} {AMorPM}";
		}
		private void Stream_DataReceived(Data data)
		{
			bool alarm;
			if (data.HR < 60 || data.HR > 100 || data.RR < 12 || data.RR > 20 || data.SPO2 < 95)
				alarm = true;
			else
				alarm = false;

			Global.Database.Add(new DB.Data
			{
				HR = data.HR,
				RR = data.RR,
				SPO2 = data.SPO2,
				Date = DateTime.Now,
				Alarm = alarm
			}); ;

			Device.BeginInvokeOnMainThread(() =>
			{
				lblHR.Text = $"{data.HR} bpm";
				lblRR.Text = $"{data.RR} breaths/min";
				lblSPO2.Text = $"{data.SPO2}%";
			});

			SetAlarm();
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
