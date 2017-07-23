using QuranX.DomainClasses.Models;

namespace QuranX.Website.ViewModels
{
	public class VerseBrowseViewModel
	{
		public Verse[] Verses { get; set; }
		public VerseNavigator VerseNavigator { get; set; }

		public VerseBrowseViewModel(Verse[] verses, VerseNavigator verseNavigator)
		{
			Verses = verses;
			VerseNavigator = verseNavigator;
		}
	}
}