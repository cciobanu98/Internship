using System;
using System.IO;
using System.Text;
using System.Globalization;
namespace GarbageStringDisopose
{
    class Program
    {
        public static void TestEncoding()
        {
            string s;
            Stream fs = File.Open("../../../test", FileMode.OpenOrCreate);
            while ((s = Console.ReadLine()) != "exit")
            {
                byte[] utf32Byte = Encoding.UTF32.GetBytes(s + "\n");
                for (int i = 0; i < utf32Byte.Length; i++)
                    fs.WriteByte(utf32Byte[i]);
            }
            fs.Close();
            byte[] newUTF32 = File.ReadAllBytes("../../../test");
            Console.WriteLine(Encoding.UTF32.GetString(newUTF32));
            File.Delete("../../../test");
        }
        public static void TestStringFormating()
        {
            string s = new string('*', 20);
            string name = "My name";
            string name2 = "my name";
            string format = "{0}\n{2}\n{1}";
            string s2 = string.Format(format, s, s, name);
            string s3 = string.Format(format, s, s, name2);
            Console.WriteLine(s2);
            DateTime dt = new DateTime(2019, 3, 9, 16, 5, 7, 123);
            Console.WriteLine(String.Format("{0:M MM MMM MMMM yyyy yy}", dt));
            Console.WriteLine(String.Equals(s2, s3, StringComparison.InvariantCultureIgnoreCase));
        }
        public static void TestingTimeSpan()
        {
            TimeSpan t = new TimeSpan(20000000000);
            TimeSpan t2 = new TimeSpan(0, 20, 20);
            TimeSpan t3 = TimeSpan.FromMinutes(30);
            Console.WriteLine(t);
            Console.WriteLine(t- t2);
            Console.WriteLine(t3);
            Console.WriteLine(t3 > t);
        }
        public static void TestingDateTime()
        {
            DateTime dt1 = new DateTime(1998, 7, 25, 4, 30, 0, DateTimeKind.Local);
            DateTime dt2 = new DateTime(1999, 3, 19, 10, 0, 0, DateTimeKind.Local);
            Console.WriteLine(dt1);
            DateTime now = DateTime.Now;
            Console.WriteLine(now.ToLongDateString() +" " + now.ToLongTimeString());
            TimeSpan t1 = (now - dt1);
            TimeSpan t2 = (now - dt2);
            Console.WriteLine(t1);
            Console.WriteLine(t2);
            Console.WriteLine((t1 - t2).TotalDays);
        }
        public static void TestingDateTimeOffSet()
        {
            DateTimeOffset dt = new DateTimeOffset(DateTime.Now);
            DateTimeOffset offset = DateTimeOffset.Now;
            DateTimeOffset offset2 = DateTimeOffset.UtcNow;
            Console.WriteLine(dt);
            Console.WriteLine(offset);
            Console.WriteLine(offset2);
            Console.WriteLine(offset - dt);
            Console.WriteLine(dt.LocalDateTime);
        }
        public static void TestingTimeZone()
        {
            TimeZone zone = TimeZone.CurrentTimeZone;
            Console.WriteLine(zone.StandardName);
            Console.WriteLine(zone.DaylightName);
            Console.WriteLine(zone.GetUtcOffset(DateTime.Now));
            //Console.WriteLine(zone.GetDaylightChanges(2019).Start);
            Console.WriteLine(TimeZone.CurrentTimeZone.GetDaylightChanges(DateTime.Now.Year).Start);
        }
        public static void TestingCultureInfo()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            Console.WriteLine(culture.Calendar.AlgorithmType);
            Console.WriteLine(culture.EnglishName);
            Console.WriteLine(culture.DisplayName);
            Console.WriteLine(culture.DateTimeFormat.FullDateTimePattern);
            // CultureInfo mdculture = new CultureInfo(200);
            // Console.WriteLine(mdculture.DisplayName);
            Console.WriteLine(culture.LCID);
        }
        public static void TestingNumberFormating()
        {
            foreach(var c in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                if (c.TwoLetterISOLanguageName != "en") continue;
                NumberFormatInfo format = c.NumberFormat;
                //Console.WriteLine(format.CurrencySymbol);
                Console.WriteLine(format.NumberNegativePattern);
            }
        }
        static void Main(string[] args)
        {
            //TestEncoding();
           // TestStringFormating();
            //TestingTimeSpan();
            //TestingDateTime();
           // TestingDateTimeOffSet();
            //TestingTimeZone();
            //TestingCultureInfo();
           // TestingNumberFormating();
            Console.ReadKey();
        }
    }
}
