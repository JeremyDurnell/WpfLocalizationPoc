using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace WpfApplication1
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			var mainWindowVm = new MainWindowViewModel();

			CultureInfo ci = new CultureInfo(mainWindowVm.SelectedLocale.ToString());
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;
		}

	    protected override void OnStartup(StartupEventArgs e)
	    {
	        FrameworkElement.LanguageProperty.OverrideMetadata(
	            typeof (FrameworkElement),
	            new FrameworkPropertyMetadata(
	                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

	        base.OnStartup(e);
	    }
	}
}
