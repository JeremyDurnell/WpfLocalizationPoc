using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

                    if (ApplicationDeployment.IsNetworkDeployed)
                    {
                        string startLink = Directory.GetFiles(
                            Environment.GetFolderPath(Environment.SpecialFolder.Programs), "WpfApplication1.appref-ms",
                            SearchOption.AllDirectories).FirstOrDefault();

                        if (startLink != null)
                        {
                            Process.Start(startLink);
                        }
                        else
                        {
                            MessageBox.Show(
                                "Can't find ClickOnce start link. Please manually restart the application.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    else
                    {
                        Process.Start(Application.ResourceAssembly.Location);
                    }

					Application.Current.Shutdown();
				}
			}
		}

	    public DateTime Now
	    {
	        get { return DateTime.Now; }
	    }

        public decimal Money { get { return 1000000m; } }
	}
}
