using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WpfApplication1
{
	public sealed class Locale
	{
		static Locale(){}

		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Type is immutable")]
		public static readonly Locale enUS = new Locale("en", "US");
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Type is immutable")]
		public static readonly Locale frFR = new Locale("fr", "FR");

		private Locale(string language, string region)
		{
			Language = language;
			Region = region;
		}

		public string Language { get; private set; }
		public string Region { get; private set; }

		public override string ToString()
		{
			return Language.ToLowerInvariant() + "-" + Region.ToUpperInvariant();
		}

		public static IEnumerable<Locale> GetSupportedLocales()
		{
			yield return enUS;
			yield return frFR;
		}

		public static Locale FromString(string locale)
		{
			switch (locale)
			{
				case "en-US":
					return enUS;
				case "fr-FR":
					return frFR;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
