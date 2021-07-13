using System;
using System.Windows;

namespace DeviceEmulator
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void bSend_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				var data = new Data
				{
					HR = float.Parse(tbHR.Text),
					RR = float.Parse(tbRR.Text),
					SPO2 = float.Parse(tbSPO2.Text)
				};

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
