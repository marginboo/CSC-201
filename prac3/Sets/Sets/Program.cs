using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cs2;
using System.Diagnostics;
using System.IO;

namespace Sets
{
    class Program
    {
         static string[] dict, Uwords, unknown;
         static GeneralList<string> general = new  GeneralList<string>();
        static GeneralList<string> unknowns = new GeneralList<string>();
        
        static void Main(string[] args)
        {
            /*GeneralList<string> general = new GeneralList<string>();
            general.Add("man");
            general.Add("boy");
            general.Add("mom");
            general.Add("girl");
            general.Add("mom");
            int man = general.Length();
            Console.WriteLine("" + man);
            int nan = general.Position("mom");
            Console.WriteLine(""+ nan);
            general.Remove("man");
            man = general.Position("boy");
            Console.WriteLine("" +man);
            Console.WriteLine(""+ general.Length());
            Console.WriteLine(""+ general.ToString());
            Console.ReadLine();
            */
            that();
            add();
            Console.WriteLine("" + general.Length());
            
            compare();
            Console.ReadKey();

        }
        static void that()
        {
            string[] delimiters = { ",", ".", "!", "?", ";", ":",
                        " ", "\"", "'", "_", "(", ")",
                        "[", "]", "{", "}", "*", "#",
                        "&", "`", "-" };
            List<string> name = new List<string>();
            TextReader file = File.OpenText(@"C:\Users\g18m6734\Downloads\prac3\wordlist.txt");
            while (true)
            {
                string line = file.ReadLine();
                if (line == " ") // End of file 
                    break;
                string[] words = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                name.Add(words[0]);
                //  Process words...
            }
            dict = name.ToArray();
            file.Close();
        }
        static void add()
        {
            int i = 0;
            while (i< dict.Length)
            {
                general.Add(dict[i]);
                i++;
            }
        }

        static void compare()
        {
            // check if word is in dictionary 
            // print a list of unknown words 
            string[] delimiters = { ",", ".", "!", "?", ";", ":",
                        " ", "\"", "'", "_", "(", ")",
                        "[", "]", "{", "}", "*", "#",
                        "&", "`", "-" };
            List<string> unknwn = new List<string>();
            string[] words = new string[200000];
            TextReader file = File.OpenText(@"C:\Users\g18m6734\Downloads\prac3\PrideAndPrejudice.txt");
            while (true)
            {
                string line = file.ReadLine();
                if (line == null) // End of file 
                    break;
                words = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                foreach (string items in words)
                {
                    if (general.check(items))
                        continue;
                    if (unknwn.Contains(items))
                        continue;
                    unknwn.Add(items.ToLower());//Make all words lower case so that the comparison is accurate

                }
                //  Process words...
            }
            int i = 0;
            while (i < unknwn.Count)
            {
                Console.Write(" " + unknwn[i]);
                i++;
            }

            Console.WriteLine(""+ unknwn.Count);
        }
        
    }
}
