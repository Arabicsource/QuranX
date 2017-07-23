using QuranX.DomainClasses.Models;
using QuranX.DomainClasses.Services;
using System.Linq;

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
			VerseRange[] result = (
				ChapterRepository.All()
				.SelectMany(chapter =>
					Enumerable.Range(1, chapter.NumberOfVerses)
						.Select(v => new VerseRange(chapter.Number, v, v)
					)
				)
			).ToArray();
			return result;
		}
	}
}
