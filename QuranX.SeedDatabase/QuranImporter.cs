using QuranX.DomainClasses.Model;
using QuranX.DomainClasses.ServicesImpl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QuranX.SeedDatabase
{
    public class QuranImporter
    {
        private readonly string DataFolder;
        private readonly ObjectSpace ObjectSpace;

        private QuranImporter(ObjectSpace objectSpace, string dataFolder)
        {
            this.ObjectSpace = objectSpace;
            this.DataFolder = dataFolder;
        }

        public static void Execute(ObjectSpace objectSpace, string dataFolder)
        {
            var instance = new QuranImporter(objectSpace, dataFolder);
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
            Console.WriteLine("Clearing Translators");
            ObjectSpace.Translators.RemoveRange(ObjectSpace.Translators);
            Console.WriteLine("Clearing VerseTexts");
            ObjectSpace.VerseTexts.RemoveRange(ObjectSpace.VerseTexts);
            ObjectSpace.SaveChanges();
        }

        private void ImportArabic()
        {
            Console.WriteLine("Importing VerseTexts: Arabic");
            ObjectSpace.Translators.Add(new Translator(
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
            ObjectSpace.VerseTexts.AddRange(verses);
            ObjectSpace.SaveChanges();
        }

        private void ImportTranslations()
        {
            string sourceFolderPath = Path.Combine(DataFolder, "Translations");
            foreach (string filePath in Directory.GetFiles(sourceFolderPath, "*.xml"))
            {
                var xml = XDocument.Load(File.OpenText(filePath));
                string translatorCode = xml.Document.Root.Element("translatorCode").Value;
                string translatorName = xml.Document.Root.Element("translatorName").Value;
                ObjectSpace.Translators.Add(
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
                ObjectSpace.VerseTexts.AddRange(verses);
                ObjectSpace.SaveChanges();
            }
        }
    }
}
