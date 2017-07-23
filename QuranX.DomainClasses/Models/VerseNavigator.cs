using QuranX.DomainClasses.Entities;
using System.Collections.Generic;
using System.Linq;

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
		public IEnumerable<IGrouping<int, int>> VersesByChapter { get; set; }

		public VerseNavigator() { }

		public VerseNavigator(
			Chapter[] chapters, 
			int chapterNumber, 
			int verseNumber, 
			int forwardChapterNumber, int forwardVerseNumber, 
			int backChapterNumber, int backVerseNumber,
			IEnumerable<VerseRange> availableVerses)
		{
			Chapters = chapters;
			ChapterNumber = chapterNumber;
			VerseNumber = verseNumber;
			ForwardChapterNumber = forwardChapterNumber;
			ForwardVerseNumber = forwardVerseNumber;
			BackChapterNumber = backChapterNumber;
			BackVerseNumber = backVerseNumber;
			VersesByChapter = availableVerses.GroupBy(x => x.Chapter, x => x.FirstVerse);
		}
	}
}