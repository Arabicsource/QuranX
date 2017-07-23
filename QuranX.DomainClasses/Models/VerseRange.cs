using System;

namespace QuranX.DomainClasses.Models
{
	public class VerseRange : IComparable, IComparable<VerseRange>, IEquatable<VerseRange>
	{
		public int Chapter { get; private set; }
		public int FirstVerse { get; private set; }
		public int LastVerse { get; private set; }

		public VerseRange(int chapter, int firstVerse, int lastVerse)
		{
			Chapter = chapter;
			FirstVerse = firstVerse;
			LastVerse = lastVerse;
		}

		public decimal GetChapterAndFirstVerseAsDecimal()
		{ 
			return Chapter + (FirstVerse / 1000m);
		}

		public override bool Equals(object obj)
		{
			return (this as IComparable).CompareTo(obj) == 0;
		}

		public override int GetHashCode()
		{
			return $"{Chapter}.{FirstVerse}".GetHashCode();
		}

		public static bool operator==(VerseRange x, VerseRange y)
		{
			if (Object.Equals(x, null) && Object.Equals(y, null))
				return true;
			if (Object.Equals(x, null))
				return false;
			return x.Equals(y);
		}

		public static bool operator!=(VerseRange x, VerseRange y)
		{
			return !(x == y);
		}

		int IComparable.CompareTo(object obj)
		{
			return (this as IComparable<VerseRange>).CompareTo((VerseRange)obj);
		}

		int IComparable<VerseRange>.CompareTo(VerseRange other)
		{
			if (Object.Equals(other, null))
				return 1;

			if (Chapter < other.Chapter)
				return -1;
			if (Chapter > other.Chapter)
				return 1;
			if (FirstVerse < other.FirstVerse)
				return -1;
			if (FirstVerse > other.FirstVerse)
				return 1;
			return 0;
		}

		bool IEquatable<VerseRange>.Equals(VerseRange other)
		{
			return (this as IComparable<VerseRange>).CompareTo(other) == 0;
		}

		public static VerseRange FromDecimalChapterAndVerse(decimal chapterAndVerse)
		{
			if (chapterAndVerse < 1.001m)
				throw new ArgumentOutOfRangeException(nameof(chapterAndVerse), "Cannot be less than 1.001");

			int chapter = (int)Math.Floor(chapterAndVerse);
			int verse = (int)((chapterAndVerse - chapter) * 1000m);
			return new VerseRange(chapter, verse, verse);
		}
	}
}
