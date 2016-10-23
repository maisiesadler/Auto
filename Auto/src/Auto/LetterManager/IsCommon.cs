using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.LetterManager
{
    public class IsCommon
    {
        private Dictionary<char, List<Letter>> _befores;
        private Dictionary<char, List<Letter>> _afters;

        public Dictionary<char, List<Letter>> Befores { get { return _befores; } }
        public Dictionary<char, List<Letter>> Afters { get { return _afters; } }

        public IsCommon()
        {
            _befores = new Dictionary<char, List<Letter>>();
            _afters = new Dictionary<char, List<Letter>>();

            var words = new Words().words;

            foreach (var word in words)
            {
                if (word.Length > 1)
                {
                    Helper.AddCharToDict(_afters, word[0], word[1]);
                    for (int i = 1; i < word.Length - 2; i++)
                    {
                        Helper.AddCharToDict(_afters, word[i], word[i + 1]);
                        Helper.AddCharToDict(_befores, word[i], word[i - 1]);
                    }

                    Helper.AddCharToDict(_befores, word[word.Length - 1], word[word.Length - 2]);
                }
            }
        }
    }

    internal static class Helper
    {
        public static void Print(Dictionary<char, List<Letter>> dict)
        {
            foreach (var pair in dict)
            {
                Console.WriteLine($"{pair.Key} : " + string.Join(", ", pair.Value));
            }
        }
        
        private static void AddCharToList(List<Letter> list, char toAdd)
        {
            if (list.Count(x => x.Char == toAdd) > 0)
            {
                list.First(x => x.Char == toAdd).Count++;
            }
            else
            {
                list.Add(new Letter { Char = toAdd, Count = 1 });
            }
        }

        public static void AddCharToDict(Dictionary<char, List<Letter>> dict, char addTo, char toAdd)
        {
            var list = GetListFromDict(dict, addTo);
            AddCharToList(list, toAdd);
            SetListToDict(dict, list, addTo);
        }

        public static List<Letter> GetListFromDict(Dictionary<char, List<Letter>> dict, char c)
        {
            if (!dict.ContainsKey(c))
            {
                dict.Add(c, new List<Letter>());
            }

            return dict[c];
        }

        private static void SetListToDict(Dictionary<char, List<Letter>> dict, List<Letter> list, char c)
        {
            dict[c] = list;
        }
    }

    public class Letter
    {
        public char Char { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return Char + ":" + Count;
        }
    }
}
