using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RestEasyApp
{
	public partial class MainPage : ContentPage
	{
		//private int Counter = 0;

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
			DisplayAlert("", "Exacerbation", "OK");
		}

		private void bUse_OnClicked(object sender, EventArgs e)
		{
			DisplayAlert("", "Use", "OK");
		}
	}
}
