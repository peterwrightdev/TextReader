using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TextReader
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please supply the fully quantified path and name of the file to read");

            string filename = Console.ReadLine();

            Console.WriteLine("How many of the most frequent words do you want to know about? (default is 10)");

            if (!int.TryParse(Console.ReadLine(), out int returnCount))
            {
                returnCount = 10;
            }

            try
            {
                List<KeyValuePair<string, int>> orderedWordCount = Program.GetOrderedWordCount(filename);
                
                Console.WriteLine(string.Format("{0} most frequent words are;", returnCount));

                for (int i = 0; i < returnCount; i++)
                {
                    Console.WriteLine(orderedWordCount[i].Key + ": " + orderedWordCount[i].Value.ToString());
                }
            }
            catch
            {
                Console.WriteLine(string.Format("Failed to read file {0}. Please use either include the full path of the file or ensure the file is in {1};", filename, Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)));
            }
        }

        public static List<KeyValuePair<string, int>> GetOrderedWordCount(string filename)
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string[] words = reader.ReadToEnd().Split(' ');
                reader.Close();

                // leverage Dictionary for handling of key uniqueness and lookup.
                SafeDictionary<string> wordCount = new SafeDictionary<string>();

                foreach (string word in words)
                {
                    wordCount.SafeIncrement(word);
                }

                // Order dictionary and convert to KeyValuePair as Dictionaries are not explicitly ordered.
                return wordCount.OrderByDescending(x => x.Value).ToList();
            }
        }
    }

    // Define extension class on Dictionary to handle method to safely increment our count. If nothing for the key exists, initialise it.
    // if a key value pair exists for the key, incrememnt the value by 1.
    public class SafeDictionary<TKey> : Dictionary<TKey, int>
    {
        public void SafeIncrement(TKey keyToIncrement)
        {
            if (this.ContainsKey(keyToIncrement))
            {
                this[keyToIncrement]++;
            }
            else
            {
                this.Add(keyToIncrement, 1);
            }
        }
    }
}
