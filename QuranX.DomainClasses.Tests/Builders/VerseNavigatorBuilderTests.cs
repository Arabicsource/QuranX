using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuranX.DomainClasses.Builders;
using QuranX.DomainClasses.Entities;
using QuranX.DomainClasses.Models;
using QuranX.DomainClasses.Services;
using System.Collections.Generic;
using System.Linq;

namespace QuranX.DomainClasses.Tests.Builders
{
	[TestClass]
	public class VerseNavigatorBuilderTests
	{
		VerseRange Verse1;
		VerseRange Verse2;
		VerseRange Verse3;
		VerseRange[] Verses;
		IChapterRepository ChapterRepository;

		[TestInitialize]
		public void SetUp()
		{
			Verse1 = new VerseRange(2, 1, 2);
			Verse2 = new VerseRange(2, 11, 12);
			Verse3 = new VerseRange(2, 21, 22);
			Verses = new VerseRange[] { Verse1, Verse2, Verse3 };

			ChapterRepository = new ChapterRepository();
		}

		[TestMethod]
		public void Build_UsesChapter1_WhenChapterIsLessThan1()
		{
			var builder = new VerseNavigatorBuilder(ChapterRepository);
			VerseNavigator result = builder.Build(0, 0, Verses);
			Assert.AreEqual(1, result.ChapterNumber);
		}

		[TestMethod]
		public void Build_UsesChapter114_WhenChapterIsGreaterThan114()
		{
			var builder = new VerseNavigatorBuilder(ChapterRepository);
			VerseNavigator result = builder.Build(115, 0, Verses);
			Assert.AreEqual(114, result.ChapterNumber);
		}

		[TestMethod]
		public void Build_UsesVerse1_WhenVerseNumberIsLessThan1()
		{
			var builder = new VerseNavigatorBuilder(ChapterRepository);
			VerseNavigator result = builder.Build(1, 0, Verses);
			Assert.AreEqual(1, result.VerseNumber);
		}

		[TestMethod]
		public void Build_UsesLastVerseOfCurrentChapter_WhenVerseIsGreaterThanNumberOfVersesInChapter()
		{
			var builder = new VerseNavigatorBuilder(ChapterRepository);
			VerseNavigator result;
			foreach(int chapterNumber in Enumerable.Range(1, 114))
			{
				var chapter = ChapterRepository.Get(chapterNumber);
				result = builder.Build(chapterNumber, chapter.NumberOfVerses + 1 , Verses);
				Assert.AreEqual(chapter.NumberOfVerses, result.VerseNumber);
			}
		}

		[TestMethod]
		public void Build_SetsBackVerseToPreviousAvailable_WhenNotTheFirstAvailable()
		{
			var builder = new VerseNavigatorBuilder(ChapterRepository);
			VerseNavigator result = builder.Build(Verse2.Chapter, Verse2.FirstVerse, Verses);
			Assert.AreEqual(Verse1.Chapter, result.BackChapterNumber, "BackChapterNumber");
			Assert.AreEqual(Verse1.FirstVerse, result.BackVerseNumber, "BackVerseNumber");

			result = builder.Build(Verse2.Chapter, Verse2.FirstVerse - 1, Verses);
			Assert.AreEqual(Verse1.Chapter, result.BackChapterNumber, "BackChapterNumber");
			Assert.AreEqual(Verse1.FirstVerse, result.BackVerseNumber, "BackVerseNumber");
		}

		[TestMethod]
		public void Build_SetsBackVerseToLastAvailable_WhenOnOrBeforeFirstAvailable()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void Build_SetsNextVerseToNextAvailable_WhenNotTheLastAvailable()
		{
			Assert.Fail();
		}

		[TestMethod]
		public void Build_SetsNextVerseToFirstAvailable_WhenOnOrAfterLastAvailable()
		{
			Assert.Fail();
		}

	}
}
