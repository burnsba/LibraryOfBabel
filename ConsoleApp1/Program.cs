using Classification;
using Classification.Analyze;
using Classification.Composition;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            /*
            var p = new Classification.Category.Parser();

            var t1 = p.Parse("ostrich");
            var t2 = p.Parse("asdf");

            int a = 9;
            */

            string path = @"C:\Users\benja\code\LibraryOfBabel\exclude\source\chessmen_of_mars.txt";
            string path2 = @"C:\Users\benja\code\LibraryOfBabel\exclude\source\warlord_of_mars.txt";
            string path3 = @"C:\Users\benja\code\LibraryOfBabel\exclude\source\thuvia,_maid_of_mars.txt";
            var textContent = File.ReadAllText(path);
            textContent += "\n\n\n";
            textContent += File.ReadAllText(path2);
            textContent += "\n\n\n";
            textContent += File.ReadAllText(path3);

            // chessmen of mars, replace asterisk with footnote.
            textContent = textContent.Replace("*", "†");

            var ee = new Engine();
            await ee.Process(textContent);
            var tbc = ee.GetComposition();
            tbc.SetSeed(0x12345678);

            // chessmen of mars: remove footnotes, single quote stand alone punctutation
            tbc.RemoveTokens(WordType.Punctuation, wt =>
            {
                if (wt.Value.Contains("*") || wt.Value.Contains("'"))
                {
                    return true;
                }

                return false;
            });

            var output = tbc.GenerateTextBlockString(500);

            Console.WriteLine(output);
        }
    }
}