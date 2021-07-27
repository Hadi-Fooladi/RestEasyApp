using Xamarin.Forms;

namespace RestEasyApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			Global.Init();

			MainPage = new MainPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
