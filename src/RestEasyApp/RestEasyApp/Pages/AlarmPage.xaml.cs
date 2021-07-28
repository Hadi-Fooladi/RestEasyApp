using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RestEasyApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmPage : ContentPage
    {
        public AlarmPage()
        {
            InitializeComponent();
			RetrieveData();
        }
		private void RetrieveData()
		{
			if (Global.Database.DataExists)
			{
				if (Global.Database.AlarmExists)
				{
					lblDate.Text = Format_Date(Global.Database.GetLastAlarm.Date.Year,
												Global.Database.GetLastAlarm.Date.Month,
												Global.Database.GetLastAlarm.Date.Day);
					lblTime.Text = Format_Time(Global.Database.GetLastAlarm.Date.Hour,
												Global.Database.GetLastAlarm.Date.Minute);
					lblHR.Text = $"{Global.Database.GetLastAlarm.HR} bpm";
					lblRR.Text = $"{Global.Database.GetLastAlarm.RR} breaths/min";
					lblSPO2.Text = $"{Global.Database.GetLastAlarm.SPO2}%";
				}
			}
		}

		private string Format_Date(int Year, int Month, int Day)
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

			return $"{month} {Day}, {Year}";
		}

		private string Format_Time(int Hour, int Minute)
		{
			int hour;
			string AMorPM;
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

			string zero = "";
			if (Minute < 10)
				zero = "0";

			return $"{hour}:{zero}{Minute} {AMorPM}";
		}

	}
}