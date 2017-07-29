using System;

namespace QuranX.DomainClasses.Models
{
	public enum UrlTemplate
	{
		Quran
	}

	public static class UrlTemplates
	{
		public static string GetUrl(UrlTemplate template, int chapter, int verse)
		{
			return GetTemplate(template)
				.Replace("{chapter}", chapter + "")
				.Replace("{verse}", verse + "");
		}

		public static string GetTemplate(UrlTemplate template)
		{
			switch(template)
			{
				case UrlTemplate.Quran:
					return "/{chapter}.{verse}";

				default: throw new NotImplementedException(template + "");
			}
		}
	}
}
