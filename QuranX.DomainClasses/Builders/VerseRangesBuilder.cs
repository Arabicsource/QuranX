using QuranX.DomainClasses.Models;
using QuranX.DomainClasses.Services;
using System.Linq;
using System;
using System.Collections.Generic;
using QuranX.DomainClasses.Entities;

namespace QuranX.DomainClasses.Builders
{
	public interface IVerseRangesBuilder
	{
		VerseRange[] BuildForQuran();
	}

	public class VerseRangesBuilder : IVerseRangesBuilder
	{
		private static object SyncRoot = new object();
		private readonly IChapterRepository ChapterRepository;
		private static VerseRange[] QuranCachedValue;

		public VerseRangesBuilder(IChapterRepository chapterRepository)
		{
			ChapterRepository = chapterRepository;
		}

		public VerseRange[] BuildForQuran()
		{
			if (QuranCachedValue != null)
				return QuranCachedValue;

			lock (SyncRoot)
			{
				if (QuranCachedValue == null)
					QuranCachedValue = BuildCachedValue();
			}
			return QuranCachedValue;
		}

		private VerseRange[] BuildCachedValue()
		{
			var result = new List<VerseRange>();
			for(int chapterNumber = 1; chapterNumber <= 114; chapterNumber++)
			{
				Chapter chapter = ChapterRepository.Get(chapterNumber);
				VerseRange[] verses =
					Enumerable.Range(1, chapter.NumberOfVerses)
					.Select(x => new VerseRange(chapterNumber, x, x))
					.ToArray();
				result.AddRange(verses);
			}
			return result.ToArray();
		}

		private object List<T>()
		{
			throw new NotImplementedException();
		}
	}
}
