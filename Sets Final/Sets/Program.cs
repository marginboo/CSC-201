using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sets
{
    class Program
    {
        static void Main(string[] args)
        {
            //Task 2
            GenericList<Int32> GenericInt = new GenericList<int>();

            //Adding values to generic list 
            GenericInt.Add(2);
            GenericInt.Add(6);
            GenericInt.Add(8);

            //GenericInt.Add(2); Test to see if duplicate values can be added to a set, commented out as it throws an exception which is expected
            GenericInt.Remove(2);//Removing a value from the generic list
            
            //Searching for values in the generic list
            GenericInt.FindItem(6);
            GenericInt.FindItem(100);

            Console.WriteLine(GenericInt.ToString());
            Console.WriteLine("\n");
            //Task 2 end 

            GenericList<String> Dictionary = new GenericList<string>();
            GenericList<String> DefinedWords = new GenericList<string>();
            GenericList<String> UnknownWords = new GenericList<string>();
            List<string> PrideAndPrejudice = new List<string>();

            //Reads in words from a text file and adds them  to a generic list
            //PRE: Each line of the text file should contain one word
            void AddWords(GenericList<string> Glistname, string Filepath)
            {
                TextReader UserWords = File.OpenText(Filepath);
                string Userword = "";
                while (true)
                {
                    Userword = UserWords.ReadLine();
                    if (Userword == null) // End of file 
                        break;
                    Glistname.Add(Userword);
                    //  Process words...
                }
                UserWords.Close();
            }
            //POST: Adds each word in specified text file to the specified generic list

            //Reads in words from a text file and adds them  to a generic list
            //PRE: None
            void BooktoList(List<String> ListName, string FilePath)
            {
                string[] delimiters = { ",", ".", "!", "?", ";", ":",
                        " ", "\"", "'", "_", "(", ")",
                        "[", "]", "{", "}", "*", "#",
                        "&", "`", "-","/","“","”"};

                TextReader Book = File.OpenText(FilePath);
                string[] PnP = new string[200000];
                string line = "";
                while (true)
                {
                    line = Book.ReadLine();
                    if (line == null) // End of file 
                        break;
                    PnP = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string items in PnP)
                    {
                        ListName.Add(items.ToLower());//Make all words lower case so that the comparison is accurate
                    }

                    //  Process words...
                }
                Book.Close();
            }
            //POST: Adds each word in specified text file to the specified generic list

            AddWords(Dictionary, @"..\..\..\wordlist");//Populate dictionary list
            AddWords(DefinedWords, @"..\..\..\UserWords.txt");//Populate user word list
            BooktoList(PrideAndPrejudice, @"..\..\..\PrideAndPrejudice.txt");//Populate Pride and Prejudice word list
            
            List<string> distinct = PrideAndPrejudice.Distinct().ToList();//Removes duplicate words

            int count = 0;
            int unknown = 0;
            int n = 0;

            //Search user defined word list and check to see how many words are known
            while (n < DefinedWords.Length())
            {
                if (Dictionary.FindItem(DefinedWords.Get(n)) == true)
                {
                    count++;
                }
                else
                {
                    UnknownWords.Add(DefinedWords.Get(n));//Add unknown word to UnkownWords generic list
                    unknown++;
                }
                n++;
            }
            Console.WriteLine("\n");
            Console.WriteLine(unknown + " unknown words in user defined list");
            Console.WriteLine(count + " known words in user defined list");
            Console.WriteLine("\n");

            //Counter reset
            count = 0;
            unknown = 0;
            n = 0;

            //Search Pride and Prejudice word list and check to see how many words are known
            while (n < distinct.Count())
            {
                if (Dictionary.FindItem(distinct[n]) == true)
                {
                    count++;
                }
                else
                {
                    UnknownWords.Add(distinct[n]);//Add unknown word to UnkownWords generic list
                    unknown++;
                }
                n++;
            }
            Console.WriteLine("\n");
            Console.WriteLine(count + " words found");
            Console.WriteLine(unknown + " words unknown");
            Console.WriteLine("\n");

            Console.WriteLine(UnknownWords.ToString());

            Console.ReadLine();

        }
    }
}
