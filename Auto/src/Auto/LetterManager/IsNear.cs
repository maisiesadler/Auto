using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.LetterManager
{
    public class IsNear
    {
        private Dictionary<char, HashSet<char>> _near;
        private HashSet<char> _all;

        public Dictionary<char, HashSet<char>> Near
        {
            get
            {
                return _near;
            }
        }

        public HashSet<char> All
        {
            get
            {
                return _all;
            }
        }

        public IsNear()
        {
            _near = new Dictionary<char, HashSet<char>>();
            _all = new HashSet<char>();

            _near.AddRelations("qwertyuiop");
            _near.AddRelations("asdfghjkl");
            _near.AddRelations("zxcvbnm");
            _near.AddRelations("qazxswedcvfrtgbnhyujmkiolp");
            _near.AddRelations("pokmnjiuhbvgytfcxdreszawq");

            Logger.Debug(string.Join(Environment.NewLine, _near.Select(x => $"{x.Key}: {string.Join(", ", x.Value)}")));
            
            _all.AddToHashSet("qwertyuiopasdfghjklzxcvbnm");
        }
    }

    public static class IsNearHelper
    {
        public static void AddRelations(this Dictionary<char, HashSet<char>> coll, string stringOfLetters)
        {
            for (int i = 1; i < stringOfLetters.Length; i++)
            {
                coll.AddRelation(stringOfLetters[i - 1], stringOfLetters[i]);
            }
        }

        public static void AddRelation(this Dictionary<char, HashSet<char>> coll, char c1, char c2)
        {
            if (coll.ContainsKey(c1))
            {
                coll[c1].Add(c2);
            }
            else
            {
                coll.Add(c1, new HashSet<char> { c2 });
            }

            if (coll.ContainsKey(c2))
            {
                coll[c2].Add(c1);
            }
            else
            {
                coll.Add(c2, new HashSet<char> { c1 });
            }
        }

        public static void AddToHashSet(this HashSet<char> set, string lettersToAdd)
        {
            foreach(var c in lettersToAdd)
            {
                set.Add(c);
            }
        }
    }
}
