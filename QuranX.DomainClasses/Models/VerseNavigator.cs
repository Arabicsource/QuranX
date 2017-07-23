using QuranX.DomainClasses.Entities;

namespace QuranX.DomainClasses.Models
{
	public class VerseNavigator
	{
		public Chapter[] Chapters { get; set; }
		public int ChapterNumber { get; set; }
		public int VerseNumber { get; set; }
		public int ForwardChapterNumber { get; set; }
		public int ForwardVerseNumber { get; set; }
		public int BackChapterNumber { get; set; }
		public int BackVerseNumber { get; set; }

		public VerseNavigator() { }

		public VerseNavigator(
			Chapter[] chapters, 
			int chapterNumber, 
			int verseNumber, 
			int forwardChapterNumber, int forwardVerseNumber, 
			int backChapterNumber, int backVerseNumber)
		{
			Chapters = chapters;
			ChapterNumber = chapterNumber;
			VerseNumber = verseNumber;
			ForwardChapterNumber = forwardChapterNumber;
			ForwardVerseNumber = forwardVerseNumber;
			BackChapterNumber = backChapterNumber;
			BackVerseNumber = backVerseNumber;
		}
	}
}