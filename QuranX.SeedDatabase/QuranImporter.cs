using QuranX.DomainClasses.Model;
using QuranX.DomainClasses.ServicesImpl;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

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

        private void ClearData()
        {
            using (var objectSpace = new ObjectSpace())
            {
                Console.WriteLine("Clearing Translators");
                objectSpace.Translators.RemoveRange(objectSpace.Translators);
                Console.WriteLine("Clearing VerseTexts");
                objectSpace.VerseTexts.RemoveRange(objectSpace.VerseTexts);
                objectSpace.SaveChanges();
            }
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
                var xml = XDocument.Load(File.OpenText(Path.Combine(DataFolder, "CorpusQuran.xml")));
                var verses =
                    from chapter in xml.Document.Root.Descendants("chapter")
                    from verse in chapter.Descendants("verse")
                    select new VerseText(
                        chapter: int.Parse(chapter.Attribute("index").Value),
                        verse: int.Parse(verse.Attribute("index").Value),
                        translatorCode: "Arabic",
                        text: verse.Element("arabicText").Value);
                objectSpace.VerseTexts.AddRange(verses);
                objectSpace.SaveChanges();
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

                Console.WriteLine($"Importing VerseTexts: {translatorCode} / {translatorName}");
                var verses =
                    from chapter in xml.Document.Root.Descendants("chapter")
                    from verse in chapter.Descendants("verse")
                    select new VerseText(
                        chapter: int.Parse(chapter.Attribute("index").Value),
                        verse: int.Parse(verse.Attribute("index").Value),
                        translatorCode: translatorName,
                        text: verse.Value);
                objectSpace.VerseTexts.AddRange(verses);
                objectSpace.SaveChanges();
            }
        }
    }
}
