using QuranX.DomainClasses.Model;
using QuranX.DomainClasses.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace QuranX.SeedDatabase
{
    public class HadithImporter
    {
        private readonly string DataFolder;
        private readonly Dictionary<string, string[]> VersesByHadithReference;

        public static void Execute(string dataFolder)
        {
            var instance = new HadithImporter(dataFolder);
            instance.Execute();
        }

        private HadithImporter(string dataFolder)
        {
            this.DataFolder = dataFolder;
            this.VersesByHadithReference = new Dictionary<string, string[]>(StringComparer.InvariantCultureIgnoreCase);
        }

        private void Execute()
        {
            ClearData();
            string folderPath = Path.Combine(DataFolder, "Hadiths");
            foreach (string filePath in Directory.GetFiles(folderPath, "*.xml"))
            {
                XDocument xml;
                HadithCollector collector;
                ImportCollector(filePath, out xml, out collector);
                ImportReferenceDefinitions(xml, collector.Code);
                ImportHadiths(xml, collector.Code);
            }
        }

        private void ImportCollector(string filePath, out XDocument xml, out HadithCollector collector)
        {
            using (var objectSpace = new ObjectSpace())
            {
                Console.WriteLine("Importing hadith: " + Path.GetFileNameWithoutExtension(filePath));
                xml = XDocument.Load(File.OpenText(filePath));
                collector = new HadithCollector(
                    code: xml.Document.Root.Element("code").Value,
                    name: xml.Document.Root.Element("name").Value);
                objectSpace.HadithCollectors.Add(collector);
                objectSpace.SaveChanges();
            }
        }

        private void ImportReferenceDefinitions(XDocument xml, string collectorCode)
        {
            using (var objectSpace = new ObjectSpace())
            {
                foreach (XElement definitionElement in xml.Document.Root.Descendants("referenceDefinition"))
                {
                    string code = definitionElement.Element("code").Value;
                    string name = definitionElement.Element("name").Value;
                    string valuePrefix = definitionElement.Element("valuePrefix")?.Value;
                    bool isPrimary = bool.Parse(definitionElement.Element("isPrimary").Value);
                    string[] keyParts = definitionElement.Descendants("part").Select(x => x.Value).ToArray();
                    var definition = new HadithReferenceDefinition(
                        collectorCode: collectorCode,
                        code: code,
                        name: name,
                        isPrimary: isPrimary,
                        keyPart1Name: keyParts[0],
                        keyPart2Name: keyParts.Length > 1 ? keyParts[1] : null,
                        keyPart3Name: keyParts.Length > 2 ? keyParts[2] : null,
                        valuePrefix: valuePrefix);
                    objectSpace.HadithReferenceDefinitions.Add(definition);
                }
                objectSpace.SaveChanges();
            }
        }

        private void ImportHadiths(XDocument xml, string collectorCode)
        {
            LoadHadithVerseCrossReferences(collectorCode);
            IEnumerable<XElement> hadiths = xml.Document.Root.Descendants("hadith");
            int counter = 0;
            float total = hadiths.Count() / 1f;
            var nextDisplayTime = DateTime.UtcNow.AddSeconds(1);
            foreach (XElement hadithElement in hadiths)
            {
                using (var objectSpace = new ObjectSpace())
                {
                    counter++;
                    if (DateTime.UtcNow > nextDisplayTime)
                    {
                        double percent = Math.Ceiling(counter * 100 / total);
                        Console.WriteLine($"{collectorCode} {counter} of {total} ({percent}%)");
                        nextDisplayTime = DateTime.UtcNow.AddSeconds(1);
                    }
                    List<HadithReference> hadithReferences = ImportReferences(hadithElement);
                    List<HadithVerseReference> verseReferences = ImportVerseReferences(hadithElement, hadithReferences);
                    string arabic =
                        string.Join("\r\n", hadithElement.Element("arabic").Elements("text").Select(x => x.Value));
                    string english =
                        string.Join("\r\n", hadithElement.Element("english").Elements("text").Select(x => x.Value));
                    var hadith = new Hadith(
                        collectorCode: collectorCode,
                        arabic: arabic,
                        english: english,
                        references: hadithReferences,
                        verseReferences: verseReferences);
                    objectSpace.Hadiths.Add(hadith);
                    objectSpace.SaveChanges();
                }
            }
        }

        private List<HadithReference> ImportReferences(XElement hadithElement)
        {
            var references = new List<HadithReference>();
            foreach (XElement referenceElement in hadithElement.Element("references").Elements("reference"))
            {
                var parts = referenceElement.Descendants("part").Select(x => x.Value).ToArray();
                string code = referenceElement.Element("code").Value;
                string suffix = referenceElement.Element("suffix")?.Value;
                string part1 = parts[0];
                string part2 = parts.Length > 1 ? parts[1] : null;
                string part3 = parts.Length > 2 ? parts[2] : null;
                references.Add(new HadithReference(
                    code: code,
                    part1: part1,
                    part2: part2,
                    part3: part3,
                    suffix: suffix));
            }
            return references;
        }

        private List<HadithVerseReference> ImportVerseReferences(XElement hadithElement, List<HadithReference> hadithReferences)
        {
            var alreadyIncludedVerses = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);
            var verseReferences = new List<HadithVerseReference>();
            foreach (XElement verseReferenceElement in hadithElement.Element("verseReferences").Elements("reference"))
            {
                int chapter = int.Parse(verseReferenceElement.Element("chapter").Value);
                int firstVerse = int.Parse(verseReferenceElement.Element("firstVerse").Value);
                int lastVerse = int.Parse(verseReferenceElement.Element("lastVerse").Value);
                verseReferences.Add(new HadithVerseReference(
                    chapter: chapter,
                    firstVerse: firstVerse,
                    lastVerse: lastVerse));
                //Note temporarily that we have already added this verse range
                alreadyIncludedVerses.Add($"{chapter}.{firstVerse}-{lastVerse}");
            }
            verseReferences.AddRange(ParseVerseReferencesFromCrossReferenceFile(hadithReferences, alreadyIncludedVerses));

            return verseReferences;
        }

        private List<HadithVerseReference> ParseVerseReferencesFromCrossReferenceFile(
            List<HadithReference> hadithReferences, 
            HashSet<string> alreadyIncludedVerses)
        {
            var result = new List<HadithVerseReference>();
            var hadithReferenceStrings = new HashSet<string>(
                collection: hadithReferences
                    .Select(x => HadithVerseReferenceToString(x.Code, new string[] { x.Part1, x.Part2, x.Part3 })),
                comparer: StringComparer.CurrentCultureIgnoreCase);

            foreach(string hadithReferenceString in hadithReferenceStrings)
            {
                string[] verseReferenceStrings;
                if (!VersesByHadithReference.TryGetValue(hadithReferenceString, out verseReferenceStrings))
                    continue;

                //Parse the verses from the verseReferenceStrings
                //If not already in alreadyIncludedVerses then add to result and add to alreadyIncludedVerses
                foreach (string verseReferenceString in verseReferenceStrings)
                {
                    string[] parts = verseReferenceString.Split('.');
                    int chapter = int.Parse(parts[0]);
                    parts = parts[1].Split('-');
                    int firstVerse = int.Parse(parts[0]);
                    int lastVerse = parts.Length == 1 ? firstVerse : int.Parse(parts[1]);
                    string formatted = $"{chapter}.{firstVerse}-{lastVerse}";
                    if (!alreadyIncludedVerses.Contains(formatted))
                    {
                        alreadyIncludedVerses.Add(formatted);
                        result.Add(new HadithVerseReference(
                            chapter: chapter,
                            firstVerse: firstVerse,
                            lastVerse: lastVerse));
                    }
                }
            }
            return result;    
        }

        private void LoadHadithVerseCrossReferences(string collectorCode)
        {
            VersesByHadithReference.Clear();
            string filePath = Path.Combine(DataFolder, "HadithXRefs", collectorCode + ".txt");
            if (!File.Exists(filePath))
                return;
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                string[] parts = line.Split('\t');
                string key = parts[0];
                string[] values = parts.Skip(1).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                VersesByHadithReference.Add(key, values);
            }
        }

        private void ClearData()
        {
            using (var objectSpace = new ObjectSpace())
            {
                Console.WriteLine("Clearing HadithCollectors");
                objectSpace.Database.ExecuteSqlCommand("delete from HadithCollectors");
                objectSpace.Database.ExecuteSqlCommand("delete from HadithReferenceDefinitions");
                Console.WriteLine("Clearing Hadiths");
                objectSpace.Database.ExecuteSqlCommand("delete from Hadiths");
                objectSpace.SaveChanges();
            }
        }

        private string HadithVerseReferenceToString(string code, string[] parts)
        {
            return code + "/" + string.Join(".", parts.Where(x => x != null));
        }

    }
}
