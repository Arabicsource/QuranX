using QuranX.DomainClasses.Models;
using System.Collections.Generic;
using System.Linq;

namespace QuranX.DomainClasses.Services
{
    public interface IChapterRepository
    {
        Chapter Get(int number);
        Chapter[] All();
    }

    public class ChapterRepository : IChapterRepository
    {
        private static readonly Dictionary<int, Chapter> Chapters;

        public Chapter[] All()
        {
            return Chapters.Values.OrderBy(x => x.Number).ToArray();
        }

        public Chapter Get(int number)
        {
            return Chapters[number];
        }

        static ChapterRepository()
        {
            Chapters = new Dictionary<int, Chapter>();
            Chapters.Add(1, new Chapter(number: 1, arabicName: "الفاتحة", englishName: "Al Fatiha (The opening)", chronologicalOrder: 5, numberOfVerses: 19));
            Chapters.Add(2, new Chapter(number: 2, arabicName: "البقرة", englishName: "Al Baqara (The cow)", chronologicalOrder: 87, numberOfVerses: 52));
            Chapters.Add(3, new Chapter(number: 3, arabicName: "آل عمران", englishName: "Al Imran (The family of Imran)", chronologicalOrder: 89, numberOfVerses: 20));
            Chapters.Add(4, new Chapter(number: 4, arabicName: "النساء", englishName: "An Nisa (The women)", chronologicalOrder: 92, numberOfVerses: 56));
            Chapters.Add(5, new Chapter(number: 5, arabicName: "المائدة", englishName: "Al Maida (The food)", chronologicalOrder: 112, numberOfVerses: 7));
            Chapters.Add(6, new Chapter(number: 6, arabicName: "الأنعام", englishName: "Al Anam (Cattle)", chronologicalOrder: 55, numberOfVerses: 5));
            Chapters.Add(7, new Chapter(number: 7, arabicName: "الأعراف", englishName: "Al Araf (The heights)", chronologicalOrder: 39, numberOfVerses: 29));
            Chapters.Add(8, new Chapter(number: 8, arabicName: "الأنفال", englishName: "Al Anfal (Spoils of war)", chronologicalOrder: 88, numberOfVerses: 19));
            Chapters.Add(9, new Chapter(number: 9, arabicName: "التوبة", englishName: "Al Tawba (Immunity)", chronologicalOrder: 113, numberOfVerses: 21));
            Chapters.Add(10, new Chapter(number: 10, arabicName: "يونس", englishName: "Yunus (Jonah)", chronologicalOrder: 51, numberOfVerses: 30));
            Chapters.Add(11, new Chapter(number: 11, arabicName: "هود", englishName: "Hud", chronologicalOrder: 52, numberOfVerses: 11));
            Chapters.Add(12, new Chapter(number: 12, arabicName: "يوسف", englishName: "Yusuf (Joseph)", chronologicalOrder: 53, numberOfVerses: 8));
            Chapters.Add(13, new Chapter(number: 13, arabicName: "الرعد", englishName: "Ar Rad (The thunder)", chronologicalOrder: 96, numberOfVerses: 3));
            Chapters.Add(14, new Chapter(number: 14, arabicName: "ابراهيم", englishName: "Ibrahim (Abraham)", chronologicalOrder: 72, numberOfVerses: 11));
            Chapters.Add(15, new Chapter(number: 15, arabicName: "الحجر", englishName: "Al Hijr (The rock)", chronologicalOrder: 54, numberOfVerses: 3));
            Chapters.Add(16, new Chapter(number: 16, arabicName: "النحل", englishName: "An Nahl (The bee)", chronologicalOrder: 70, numberOfVerses: 8));
            Chapters.Add(17, new Chapter(number: 17, arabicName: "الإسراء", englishName: "Bani Israil (The Israelites)", chronologicalOrder: 50, numberOfVerses: 7));
            Chapters.Add(18, new Chapter(number: 18, arabicName: "الكهف", englishName: "Al Kahf (The cave)", chronologicalOrder: 69, numberOfVerses: 6));
            Chapters.Add(19, new Chapter(number: 19, arabicName: "مريم", englishName: "Maryamn (Mary)", chronologicalOrder: 44, numberOfVerses: 5));
            Chapters.Add(20, new Chapter(number: 20, arabicName: "طه", englishName: "Ta-Ha", chronologicalOrder: 45, numberOfVerses: 5));
            Chapters.Add(21, new Chapter(number: 21, arabicName: "الأنبياء", englishName: "Al Anbiya (The prophets)", chronologicalOrder: 73, numberOfVerses: 6));
            Chapters.Add(22, new Chapter(number: 22, arabicName: "الحج", englishName: "Al Hajj (The pilgrimage)", chronologicalOrder: 103, numberOfVerses: 4));
            Chapters.Add(23, new Chapter(number: 23, arabicName: "المؤمنون", englishName: "Al Muminun (The believers)", chronologicalOrder: 74, numberOfVerses: 62));
            Chapters.Add(24, new Chapter(number: 24, arabicName: "النور", englishName: "An Nur (The light)", chronologicalOrder: 102, numberOfVerses: 42));
            Chapters.Add(25, new Chapter(number: 25, arabicName: "الفرقان", englishName: "Al Furqan (The discrimination)", chronologicalOrder: 42, numberOfVerses: 5));
            Chapters.Add(26, new Chapter(number: 26, arabicName: "الشعراء", englishName: "Ash Shuara (The poets)", chronologicalOrder: 47, numberOfVerses: 15));
            Chapters.Add(27, new Chapter(number: 27, arabicName: "النمل", englishName: "An Naml (The ant)", chronologicalOrder: 48, numberOfVerses: 22));
            Chapters.Add(28, new Chapter(number: 28, arabicName: "القصص", englishName: "Al Qasas (The narrative)", chronologicalOrder: 49, numberOfVerses: 8));
            Chapters.Add(29, new Chapter(number: 29, arabicName: "العنكبوت", englishName: "Al Ankabut (The spider)", chronologicalOrder: 85, numberOfVerses: 4));
            Chapters.Add(30, new Chapter(number: 30, arabicName: "الروم", englishName: "Ar Rum (The Romans)", chronologicalOrder: 84, numberOfVerses: 11));
            Chapters.Add(31, new Chapter(number: 31, arabicName: "لقمان", englishName: "Luqman", chronologicalOrder: 57, numberOfVerses: 40));
            Chapters.Add(32, new Chapter(number: 32, arabicName: "السجدة", englishName: "As Sajda (The adoration)", chronologicalOrder: 75, numberOfVerses: 9));
            Chapters.Add(33, new Chapter(number: 33, arabicName: "الأحزاب", englishName: "Al Ahzab (The allies)", chronologicalOrder: 90, numberOfVerses: 50));
            Chapters.Add(34, new Chapter(number: 34, arabicName: "سبإ", englishName: "Al Saba (The Saba)", chronologicalOrder: 58, numberOfVerses: 45));
            Chapters.Add(35, new Chapter(number: 35, arabicName: "فاطر", englishName: "Al Fatir (The angels)", chronologicalOrder: 43, numberOfVerses: 20));
            Chapters.Add(36, new Chapter(number: 36, arabicName: "يس", englishName: "Ya Sin", chronologicalOrder: 41, numberOfVerses: 17));
            Chapters.Add(37, new Chapter(number: 37, arabicName: "الصافات", englishName: "As Saffat (Those who set up ranks)", chronologicalOrder: 56, numberOfVerses: 55));
            Chapters.Add(38, new Chapter(number: 38, arabicName: "ص", englishName: "Saad", chronologicalOrder: 38, numberOfVerses: 88));
            Chapters.Add(39, new Chapter(number: 39, arabicName: "الزمر", englishName: "Az Zumar (The troops)", chronologicalOrder: 59, numberOfVerses: 206));
            Chapters.Add(40, new Chapter(number: 40, arabicName: "غافر", englishName: "Al Ghafir (The forgiver)", chronologicalOrder: 60, numberOfVerses: 28));
            Chapters.Add(41, new Chapter(number: 41, arabicName: "فصلت", englishName: "Fussilat (Explained in detail)", chronologicalOrder: 61, numberOfVerses: 83));
            Chapters.Add(42, new Chapter(number: 42, arabicName: "الشورى", englishName: "Ash Shura (The council)", chronologicalOrder: 62, numberOfVerses: 77));
            Chapters.Add(43, new Chapter(number: 43, arabicName: "الزخرف", englishName: "Az Zukhruf (Gold)", chronologicalOrder: 63, numberOfVerses: 45));
            Chapters.Add(44, new Chapter(number: 44, arabicName: "الدخان", englishName: "Ad Dukhan (Drought)", chronologicalOrder: 64, numberOfVerses: 98));
            Chapters.Add(45, new Chapter(number: 45, arabicName: "الجاثية", englishName: "Al Jathiya (Kneeling)", chronologicalOrder: 65, numberOfVerses: 135));
            Chapters.Add(46, new Chapter(number: 46, arabicName: "الأحقاف", englishName: "Al Ahqaf (The dunes)", chronologicalOrder: 66, numberOfVerses: 96));
            Chapters.Add(47, new Chapter(number: 47, arabicName: "محمد", englishName: "Muhammad", chronologicalOrder: 95, numberOfVerses: 227));
            Chapters.Add(48, new Chapter(number: 48, arabicName: "الفتح", englishName: "Al Fath (Victory)", chronologicalOrder: 111, numberOfVerses: 93));
            Chapters.Add(49, new Chapter(number: 49, arabicName: "الحجرات", englishName: "Al Hujraat (The apartments)", chronologicalOrder: 106, numberOfVerses: 88));
            Chapters.Add(50, new Chapter(number: 50, arabicName: "ق", englishName: "Qaf", chronologicalOrder: 34, numberOfVerses: 111));
            Chapters.Add(51, new Chapter(number: 51, arabicName: "الذاريات", englishName: "Adh Dhariyat (The scatterers)", chronologicalOrder: 67, numberOfVerses: 109));
            Chapters.Add(52, new Chapter(number: 52, arabicName: "الطور", englishName: "At Tur (The mount)", chronologicalOrder: 76, numberOfVerses: 123));
            Chapters.Add(53, new Chapter(number: 53, arabicName: "النجم", englishName: "An Najm (The star)", chronologicalOrder: 23, numberOfVerses: 111));
            Chapters.Add(54, new Chapter(number: 54, arabicName: "القمر", englishName: "Al Qamar (The Moon)", chronologicalOrder: 37, numberOfVerses: 99));
            Chapters.Add(55, new Chapter(number: 55, arabicName: "الرحمن", englishName: "Ar Rahman (The beneficent)", chronologicalOrder: 97, numberOfVerses: 165));
            Chapters.Add(56, new Chapter(number: 56, arabicName: "الواقعة", englishName: "Al Waqia (The event)", chronologicalOrder: 46, numberOfVerses: 182));
            Chapters.Add(57, new Chapter(number: 57, arabicName: "الحديد", englishName: "Al Hadid (Iron)", chronologicalOrder: 94, numberOfVerses: 34));
            Chapters.Add(58, new Chapter(number: 58, arabicName: "المجادلة", englishName: "Al Mujadila (The pleading woman)", chronologicalOrder: 105, numberOfVerses: 54));
            Chapters.Add(59, new Chapter(number: 59, arabicName: "الحشر", englishName: "Al Hashr (Exile)", chronologicalOrder: 101, numberOfVerses: 75));
            Chapters.Add(60, new Chapter(number: 60, arabicName: "الممتحنة", englishName: "Al Mumtahana (The examined woman)", chronologicalOrder: 91, numberOfVerses: 85));
            Chapters.Add(61, new Chapter(number: 61, arabicName: "الصف", englishName: "As Saff (The ranks)", chronologicalOrder: 109, numberOfVerses: 54));
            Chapters.Add(62, new Chapter(number: 62, arabicName: "الجمعة", englishName: "Al Jumua (The congregation)", chronologicalOrder: 110, numberOfVerses: 53));
            Chapters.Add(63, new Chapter(number: 63, arabicName: "المنافقون", englishName: "Al Munafiqun (The hypocrites)", chronologicalOrder: 104, numberOfVerses: 89));
            Chapters.Add(64, new Chapter(number: 64, arabicName: "التغابن", englishName: "At Taghabun (Manifestation of losses)", chronologicalOrder: 108, numberOfVerses: 59));
            Chapters.Add(65, new Chapter(number: 65, arabicName: "الطلاق", englishName: "At Talaq (Divorce)", chronologicalOrder: 99, numberOfVerses: 37));
            Chapters.Add(66, new Chapter(number: 66, arabicName: "التحريم", englishName: "At Tahrim (Prohibition)", chronologicalOrder: 107, numberOfVerses: 35));
            Chapters.Add(67, new Chapter(number: 67, arabicName: "الملك", englishName: "Al Mulk (The kingdom)", chronologicalOrder: 77, numberOfVerses: 60));
            Chapters.Add(68, new Chapter(number: 68, arabicName: "القلم", englishName: "Al Qalam (The pen)", chronologicalOrder: 2, numberOfVerses: 26));
            Chapters.Add(69, new Chapter(number: 69, arabicName: "الحاقة", englishName: "Al Haqqa (The reality)", chronologicalOrder: 78, numberOfVerses: 110));
            Chapters.Add(70, new Chapter(number: 70, arabicName: "المعارج", englishName: "Al Maarij (Ascending stairways)", chronologicalOrder: 79, numberOfVerses: 128));
            Chapters.Add(71, new Chapter(number: 71, arabicName: "نوح", englishName: "Nuh (Noah)", chronologicalOrder: 71, numberOfVerses: 28));
            Chapters.Add(72, new Chapter(number: 72, arabicName: "الجن", englishName: "Al Jinn (The Jinn)", chronologicalOrder: 40, numberOfVerses: 52));
            Chapters.Add(73, new Chapter(number: 73, arabicName: "المزمل", englishName: "Al Muzzammil (The enshrouded one)", chronologicalOrder: 3, numberOfVerses: 112));
            Chapters.Add(74, new Chapter(number: 74, arabicName: "المدثر", englishName: "Al Muddaththir (The cloaked one)", chronologicalOrder: 4, numberOfVerses: 118));
            Chapters.Add(75, new Chapter(number: 75, arabicName: "القيامة", englishName: "Al Qiyama (Resurrection)", chronologicalOrder: 31, numberOfVerses: 30));
            Chapters.Add(76, new Chapter(number: 76, arabicName: "الانسان", englishName: "Al Insan (Man)", chronologicalOrder: 98, numberOfVerses: 49));
            Chapters.Add(77, new Chapter(number: 77, arabicName: "المرسلات", englishName: "Al Mursalat (The emissaries)", chronologicalOrder: 33, numberOfVerses: 30));
            Chapters.Add(78, new Chapter(number: 78, arabicName: "النبإ", englishName: "An Naba (The tidings)", chronologicalOrder: 80, numberOfVerses: 52));
            Chapters.Add(79, new Chapter(number: 79, arabicName: "النازعات", englishName: "An Naziat (Those who yearn)", chronologicalOrder: 81, numberOfVerses: 44));
            Chapters.Add(80, new Chapter(number: 80, arabicName: "عبس", englishName: "Abasa (He frowned)", chronologicalOrder: 24, numberOfVerses: 40));
            Chapters.Add(81, new Chapter(number: 81, arabicName: "التكوير", englishName: "At Takwir (The folding up)", chronologicalOrder: 7, numberOfVerses: 46));
            Chapters.Add(82, new Chapter(number: 82, arabicName: "الإنفطار", englishName: "Al Infitar (The cleaving)", chronologicalOrder: 82, numberOfVerses: 19));
            Chapters.Add(83, new Chapter(number: 83, arabicName: "المطففين", englishName: "Al Mutaffifin (The cheats)", chronologicalOrder: 86, numberOfVerses: 25));
            Chapters.Add(84, new Chapter(number: 84, arabicName: "الإنشقاق", englishName: "Al Isnhiqaq (Splitting open)", chronologicalOrder: 83, numberOfVerses: 60));
            Chapters.Add(85, new Chapter(number: 85, arabicName: "البروج", englishName: "Al Buruj (The stars)", chronologicalOrder: 27, numberOfVerses: 69));
            Chapters.Add(86, new Chapter(number: 86, arabicName: "الطارق", englishName: "At Tariq (The morning star)", chronologicalOrder: 36, numberOfVerses: 36));
            Chapters.Add(87, new Chapter(number: 87, arabicName: "الأعلى", englishName: "Al Ala (The most high)", chronologicalOrder: 8, numberOfVerses: 286));
            Chapters.Add(88, new Chapter(number: 88, arabicName: "الغاشية", englishName: "Al Ghashiya (The overwhelming)", chronologicalOrder: 68, numberOfVerses: 75));
            Chapters.Add(89, new Chapter(number: 89, arabicName: "الفجر", englishName: "Al Fajr (The dawn)", chronologicalOrder: 10, numberOfVerses: 200));
            Chapters.Add(90, new Chapter(number: 90, arabicName: "البلد", englishName: "Al Balad (The city)", chronologicalOrder: 35, numberOfVerses: 73));
            Chapters.Add(91, new Chapter(number: 91, arabicName: "الشمس", englishName: "Ash Shams (The Sun)", chronologicalOrder: 26, numberOfVerses: 13));
            Chapters.Add(92, new Chapter(number: 92, arabicName: "الليل", englishName: "Al Lail (The night)", chronologicalOrder: 9, numberOfVerses: 176));
            Chapters.Add(93, new Chapter(number: 93, arabicName: "الضحى", englishName: "Ad Duhaa (The brightness of day)", chronologicalOrder: 11, numberOfVerses: 8));
            Chapters.Add(94, new Chapter(number: 94, arabicName: "الشرح", englishName: "Al Inshirah (The expansion)", chronologicalOrder: 12, numberOfVerses: 29));
            Chapters.Add(95, new Chapter(number: 95, arabicName: "التين", englishName: "At tin (The fig)", chronologicalOrder: 28, numberOfVerses: 38));
            Chapters.Add(96, new Chapter(number: 96, arabicName: "العلق", englishName: "Al Alaq (The clot)", chronologicalOrder: 1, numberOfVerses: 43));
            Chapters.Add(97, new Chapter(number: 97, arabicName: "القدر", englishName: "Al Qadr (The power)", chronologicalOrder: 25, numberOfVerses: 78));
            Chapters.Add(98, new Chapter(number: 98, arabicName: "البينة", englishName: "Al Bayyina (Clear proof)", chronologicalOrder: 100, numberOfVerses: 31));
            Chapters.Add(99, new Chapter(number: 99, arabicName: "الزلزلة", englishName: "Al Zalzala (The shaking)", chronologicalOrder: 93, numberOfVerses: 12));
            Chapters.Add(100, new Chapter(number: 100, arabicName: "العاديات", englishName: "Al Adiyat (The assaulters)", chronologicalOrder: 14, numberOfVerses: 8));
            Chapters.Add(101, new Chapter(number: 101, arabicName: "القارعة", englishName: "Al Qaria (The calamity)", chronologicalOrder: 30, numberOfVerses: 24));
            Chapters.Add(102, new Chapter(number: 102, arabicName: "التكاثر", englishName: "At Takathur (Abundance of wealth)", chronologicalOrder: 16, numberOfVerses: 64));
            Chapters.Add(103, new Chapter(number: 103, arabicName: "العصر", englishName: "Al Asr (The time)", chronologicalOrder: 13, numberOfVerses: 78));
            Chapters.Add(104, new Chapter(number: 104, arabicName: "الهمزة", englishName: "Al Humaza (The slanderer)", chronologicalOrder: 32, numberOfVerses: 11));
            Chapters.Add(105, new Chapter(number: 105, arabicName: "الفيل", englishName: "Al Fil (The elephant)", chronologicalOrder: 19, numberOfVerses: 22));
            Chapters.Add(106, new Chapter(number: 106, arabicName: "قريش", englishName: "Quraish (The Quraish)", chronologicalOrder: 29, numberOfVerses: 18));
            Chapters.Add(107, new Chapter(number: 107, arabicName: "الماعون", englishName: "Al Maun (Almsgiving)", chronologicalOrder: 17, numberOfVerses: 12));
            Chapters.Add(108, new Chapter(number: 108, arabicName: "الكوثر", englishName: "Al Kauthar (Abundance)", chronologicalOrder: 15, numberOfVerses: 18));
            Chapters.Add(109, new Chapter(number: 109, arabicName: "الكافرون", englishName: "Al Kafirun (The disbelievers)", chronologicalOrder: 18, numberOfVerses: 14));
            Chapters.Add(110, new Chapter(number: 110, arabicName: "النصر", englishName: "An Nasr (Divine support)", chronologicalOrder: 114, numberOfVerses: 11));
            Chapters.Add(111, new Chapter(number: 111, arabicName: "المسد", englishName: "Al Lahab (The flame)", chronologicalOrder: 6, numberOfVerses: 29));
            Chapters.Add(112, new Chapter(number: 112, arabicName: "الإخلاص", englishName: "Al Ikhlas (The unity)", chronologicalOrder: 22, numberOfVerses: 120));
            Chapters.Add(113, new Chapter(number: 113, arabicName: "الفلق", englishName: "Al Falaq (Dawn)", chronologicalOrder: 20, numberOfVerses: 129));
            Chapters.Add(114, new Chapter(number: 114, arabicName: "الناس", englishName: "An Nas (Mankind)", chronologicalOrder: 21, numberOfVerses: 3));
        }
    }
}
