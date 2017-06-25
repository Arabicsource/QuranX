using QuranX.DomainClasses.Model;
using QuranX.DomainClasses.Services;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace QuranX.SeedDatabase
{
    public class QuranImporter
    {
        private readonly string DataFolder;

        private QuranImporter(string dataFolder)
        {
            this.DataFolder = dataFolder;
        }

        public static void Execute(string dataFolder)
        {
            var instance = new QuranImporter(dataFolder);
            instance.Execute();
        }

        public void Execute()
        {
            ClearData();
            ImportArabic();
            ImportTranslations();
        }

        private void ImportArabic()
        {
            using (var objectSpace = new ObjectSpace())
            {
                Console.WriteLine("Importing VerseTexts: Arabic");
                objectSpace.Translators.Add(new Translator(
                    code: "Arabic",
                    name: "Arabic",
                    displayOrder: 0));
                objectSpace.SaveChanges();
            }
            var xml = XDocument.Load(File.OpenText(Path.Combine(DataFolder, "CorpusQuran.xml")));
            var verseNodes =
                from chapter in xml.Document.Root.Descendants("chapter")
                from verse in chapter.Descendants("verse")
                select new
                {
                    Chapter = int.Parse(chapter.Attribute("index").Value),
                    Verse = int.Parse(verse.Attribute("index").Value),
                    Arabic = verse.Element("arabicText").Value,
                    Node = verse
                };
            int previousChapterNumber = -1;
            int previousVerseNumber = -1;
            foreach (var verseInfo in verseNodes)
            {
                if (verseInfo.Chapter != previousChapterNumber)
                {
                    previousChapterNumber = verseInfo.Chapter;
                    Console.WriteLine(" (Total " + previousVerseNumber + " verses)");
                    Console.Write(previousChapterNumber + ": ");
                }
                previousVerseNumber = verseInfo.Verse;
                if (verseInfo.Verse % 50 == 0)
                    Console.Write(verseInfo.Verse);
                else if (verseInfo.Verse % 5 == 0)
                    Console.Write(".");
                using (var objectSpace = new ObjectSpace())
                {
                    var verse = new VerseText(
                    chapter: verseInfo.Chapter,
                    verse: verseInfo.Verse,
                    translatorCode: "Arabic",
                    text: verseInfo.Arabic);
                    objectSpace.VerseTexts.Add(verse);
                    ImportAnalysisWords(
                        objectSpace: objectSpace,
                        verse: verse,
                        wordNodes: verseInfo.Node.Descendants("word"));
                    objectSpace.SaveChanges();
                }
            }
            Console.WriteLine();
        }

        private void ImportAnalysisWords(
            ObjectSpace objectSpace,
            VerseText verse,
            IEnumerable<XElement> wordNodes)
        {
            foreach (XElement wordNode in wordNodes)
            {
                int index = int.Parse(wordNode.Attribute("index").Value);
                string buckwalter = wordNode.Element("buckwalter").Value;
                string english = wordNode.Element("english").Value;
                var verseAnalysisWord = new VerseAnalysisWord(
                    chapter: verse.Chapter,
                    verse: verse.Verse,
                    index: index,
                    english: english,
                    buckwalter: buckwalter,
                    parts: null);
                objectSpace.VerseAnalysisWords.Add(verseAnalysisWord);
                ImportAnalysisWordParts(
                    verseAnalysisWord: verseAnalysisWord,
                    partNodes: wordNode.Descendants("wordPart"));
            }
        }

        private void ImportAnalysisWordParts(
            VerseAnalysisWord verseAnalysisWord,
            IEnumerable<XElement> partNodes)
        {
            foreach (XElement partNode in partNodes)
            {
                int index = int.Parse(partNode.Attribute("index").Value);
                string type = partNode.Element("type").Value;
                string root = partNode?.Element("root")?.Value;
                string decorators = string.Join("｜",
                    partNode.Descendants("decorator").Select(x => x.Value).ToArray());
                var analysisWordPart = new VerseAnalysisWordPart(
                    index: index,
                    type: type,
                    root: root,
                    decorators: decorators);
                verseAnalysisWord.Parts.Add(analysisWordPart);
            }
        }

        private void ImportTranslations()
        {
            string sourceFolderPath = Path.Combine(DataFolder, "Translations");
            foreach (string filePath in Directory.GetFiles(sourceFolderPath, "*.xml"))
            {
                ImportTranslation(filePath);
            }
        }

        private static void ImportTranslation(string filePath)
        {
            var xml = XDocument.Load(File.OpenText(filePath));
            string translatorCode = xml.Document.Root.Element("translatorCode").Value;
            string translatorName = xml.Document.Root.Element("translatorName").Value;
            using (var objectSpace = new ObjectSpace())
            {
                objectSpace.Translators.Add(
                    new Translator(
                        code: translatorCode,
                        name: translatorName,
                        displayOrder: translatorCode == "Transliteration" ? 999 : 1));
                objectSpace.SaveChanges();
            }

            Console.WriteLine($"Importing VerseTexts: {translatorCode} / {translatorName}");
            var verses =
                from chapter in xml.Document.Root.Descendants("chapter")
                from verse in chapter.Descendants("verse")
                select new VerseText(
                    chapter: int.Parse(chapter.Attribute("index").Value),
                    verse: int.Parse(verse.Attribute("index").Value),
                    translatorCode: translatorName,
                    text: verse.Value);

            for (int i = 1; i <= 114; i++)
            {
                Console.WriteLine($"{translatorCode} chapter {i}");
                using (var objectSpace = new ObjectSpace())
                {
                    objectSpace.VerseTexts.AddRange(verses.Where(x => x.Chapter == i));
                    objectSpace.SaveChanges();
                }
            }
        }

        private void ClearData()
        {
            using (var objectSpace = new ObjectSpace())
            {
                Console.WriteLine("Clearing Chapters");
                objectSpace.Database.ExecuteSqlCommand("delete from Chapters");
                Console.WriteLine("Clearing Translators");
                objectSpace.Database.ExecuteSqlCommand("delete from Translators");
                Console.WriteLine("Clearing VerseTexts");
                objectSpace.Database.ExecuteSqlCommand("delete from VerseTexts");
                Console.WriteLine("Clearing verse analyses");
                objectSpace.Database.ExecuteSqlCommand("delete from VerseAnalysisWords");

                objectSpace.SaveChanges();
            }
        }


    }
}
