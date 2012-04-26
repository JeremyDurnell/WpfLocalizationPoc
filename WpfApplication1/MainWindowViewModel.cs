using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using WpfApplication1.Properties;

namespace WpfApplication1
{
	public class MainWindowViewModel
	{
		public IEnumerable<Locale> SupportedLocales
		{
			get { return Locale.GetSupportedLocales(); }
		}

		public Locale SelectedLocale
		{
			get
			{
				return Locale.FromString(Settings.Default.SelectedLocale);
			}
			set
			{
				var dialogResult = MessageBox.Show("In order to change locales, the application must be restarted. Continue?", "Restart Required",
				                MessageBoxButton.OKCancel, MessageBoxImage.Warning);

				if (dialogResult == MessageBoxResult.OK)
				{
					Settings.Default.SelectedLocale = value.ToString();
					Settings.Default.Save();

					Process.Start(Application.ResourceAssembly.Location);
					Application.Current.Shutdown();
				}
			}
		}
	}
}
