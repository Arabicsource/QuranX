using QuranX.DomainClasses.Entities;
using QuranX.DomainClasses.Models;
using QuranX.DomainClasses.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuranX.DomainClasses.Builders
{
	public interface IVerseNavigatorBuilder
	{
		VerseNavigator Build(int chapterNumber, int verseNumber, IEnumerable<VerseRange> availableVerses);
	}

	public class VerseNavigatorBuilder : IVerseNavigatorBuilder
	{
		private readonly IChapterRepository ChapterRepository;

		public VerseNavigatorBuilder(IChapterRepository chapterRepository)
		{
			ChapterRepository = chapterRepository;
		}

		public VerseNavigator Build(int chapterNumber, int verseNumber, IEnumerable<VerseRange> availableVerses)
		{
			if (availableVerses == null)
				throw new ArgumentNullException(nameof(availableVerses));

			List<Decimal> sortedAvailableVerses =
				availableVerses
				.Select(x => x.GetChapterAndFirstVerseAsDecimal())
				.OrderBy(x => x)
				.ToList();

			if (chapterNumber < 1)
				chapterNumber = 1;
			if (chapterNumber > 114)
				chapterNumber = 114;

			Chapter chapter = ChapterRepository.Get(chapterNumber);

			if (verseNumber < 1)
				verseNumber = 1;
			if (verseNumber > chapter.NumberOfVerses)
				verseNumber = chapter.NumberOfVerses;

			decimal currentChapterAndVerse = 
				new VerseRange(chapterNumber, verseNumber, verseNumber).GetChapterAndFirstVerseAsDecimal();
			int indexInSortedList = sortedAvailableVerses.BinarySearch(currentChapterAndVerse);

			// Not in the list, but we have an insert point
			if (indexInSortedList < 0)
			{
				indexInSortedList = ~indexInSortedList; // Make it non-negative
				sortedAvailableVerses.Insert(indexInSortedList, currentChapterAndVerse);
			}

			int previousIndex = (indexInSortedList - 1).Modulo(sortedAvailableVerses.Count);
			var previousVerse = VerseRange.FromDecimalChapterAndVerse(sortedAvailableVerses[previousIndex]);

			int nextIndex = (indexInSortedList + 1).Modulo(sortedAvailableVerses.Count);
			var nextVerse = VerseRange.FromDecimalChapterAndVerse(sortedAvailableVerses[nextIndex]);

			return new VerseNavigator(
				chapters: ChapterRepository.All(),
				chapterNumber: chapterNumber,
				verseNumber: verseNumber,
				urlTemplate: UrlTemplate.Quran,
				forwardChapterNumber: nextVerse.Chapter,
				forwardVerseNumber: nextVerse.FirstVerse,
				backChapterNumber: previousVerse.Chapter,
				backVerseNumber: previousVerse.FirstVerse,
				availableVerses: availableVerses);
		}
	}
}
