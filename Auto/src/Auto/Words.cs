using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public class Words
    {
        public HashSet<string> words { get; set; }

        public Words()
        {
            words = new HashSet<string>();

            var file = File.ReadAllLines(@"C:\Users\Maisie\Documents\Visual Studio 2015\Projects\Auto\src\Auto\words.txt");
            foreach (var word in file)
            {
                var noSpace = word.Replace(" ", "");
                if (noSpace.Length > 0)
                {
                    words.Add(noSpace);
                }
            }
            //words.Add("ab");
            //words.Add("whf");
            //words.Add("ahf");
            //words.Add("bead");
            //words.Add("beaf");
            //words.Add("wish");
        }
    }

    class NameComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, true);
        }
    }

    public static class WordHelper
    {
        public static IEnumerable<string> Ordered(this IEnumerable<string> list)
        {
            return list.OrderBy(x => x, new NameComparer());
        }

        public static IEnumerable<string> SelectLength(this IEnumerable<string> list, int length)
        {
            foreach(var word in list)
            {
                if (word.Length == length)
                {
                    yield return word;
                }
            }
        }

        public static IEnumerable<string> SelectStartingWith(this IEnumerable<string> list, char letter)
        {
            foreach(var word in list)
            {
                if (word.StartsWithNear(letter))
                {
                    yield return word;
                }
            }
        }

        public static bool StartsWithNear(this string word, char letter)
        {
            foreach(var variation in letter.SelectNear())
            {
                if (word.StartsWith(variation.ToString()))
                    return true;
            }

            return false;
        }
    }
}
