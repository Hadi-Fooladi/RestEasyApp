using System;
using System.Collections.Generic;
using System.Net.Sockets;
using EasyTCP;
using Plugin.LocalNotifications;
using Xamarin.Forms;

namespace RestEasyApp
{
	public partial class MainPage : ContentPage
	{
		private DataStream Stream;
		private const int minHR = 60, maxHR = 100, minRR = 12, maxRR = 20, minSPO2 = 95;
		private bool notificationSent = false;

		public MainPage()
		{
			InitializeComponent();
			RetrieveData();
		}

		protected override void OnAppearing()
		{
			/*var sb = new StringBuilder();
			foreach (var data in Global.Database.AllData)
			{
				sb.AppendLine($"{data.Date}, HR = {data.HR}, RR = {data.RR}, SPO2 = {data.SPO2}, Alarm: {data.Alarm}");
			}

			DisplayAlert("", sb.ToString(), "OK");

			base.OnAppearing();*/
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
			if (Global.Database.DataExists)
			{
				lblHR.Text = $"{Global.Database.GetLatestData.HR} bpm";
				lblRR.Text = $"{Global.Database.GetLatestData.RR} breaths/min";
				lblSPO2.Text = $"{Global.Database.GetLatestData.SPO2}%";

				if (Global.Database.AlarmExists)
					lblAlarm.Text = $"Last alarm: {Format_Time(Global.Database.GetLatestData.Date)}";
			}
		}

		private void SetAlarm()
		{
			var latestData = Global.Database.GetLatestData;
			if (Global.Database.GetLatestData.Alarm)
			{

				lblAlarm.Text = $"Last alarm: {Format_Time(latestData.Date)}";

				if (!notificationSent)
				{
					int id = 0;
					string title = "Exacerbation Alert";
					string body = string.Join(", ", GetLines());

					CrossLocalNotifications.Current.Show(title, body, id);
					notificationSent = true;

					imgAlarm.Source = "AlarmRed.png";


					IEnumerable<string> GetLines()
					{
						var alarm = Global.Database.GetLastAlarm;
						yield return $"Time: {alarm.Date}";
						yield return $"HR: {alarm.HR}";
						yield return $"RR: {alarm.RR}";
						yield return $"SPO2: {alarm.SPO2}";
					}
				}
			}
		}

		private static string Format_Time(DateTime date)
		{
			return $"{date: MMMM dd, yyyy\nhh:mm tt}";
		}

		private void Stream_DataReceived(Data data)
		{
			bool alarm;
			if (data.HR < minHR || data.HR > maxHR || data.RR < minRR || data.RR > maxRR || data.SPO2 < minSPO2)
			{
				alarm = true;
			}
			else
			{
				alarm = false;
				notificationSent = false;
			}

			Global.Database.Add(new DB.Data
			{
				HR = data.HR,
				RR = data.RR,
				SPO2 = data.SPO2,
				Date = DateTime.Now,
				Alarm = alarm
			});

			Device.BeginInvokeOnMainThread(() =>
			{
				lblHR.Text = $"{data.HR} bpm";
				lblRR.Text = $"{data.RR} breaths/min";
				lblSPO2.Text = $"{data.SPO2}%";
				SetAlarm();
			});
		}

		private void BConnect_OnClicked(object sender, EventArgs e)
		{
			StartConnection();
		}

		private void BExacerbation_OnClicked(object sender, EventArgs e)
		{

		}
		private void BAlarm_OnClicked(object sender, EventArgs e)
		{
			Navigation.PushModalAsync(new AlarmPage());
		}
	}
}
