using Auto.LetterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.Corrections
{
    public class KeepCommonCombinationsAll : ITest
    {
        public KeepCommonCombinationsAll()
        {
            checkedWithout = new HashSet<string>();
            commonCombs = new HashSet<string> { "ing", "ea", "ai", "st", "ant", "ation" };
        }

        public override List<Idea> Execute(Idea word)
        {
            return RemovePartial(word).ToList();
        }

        private HashSet<string> commonCombs;

        public HashSet<string> checkedWithout;

        public override int Rating
        {
            get
            {
                return 2;
            }
        }

        public IEnumerable<string> FindCombs(string word)
        {
            foreach (var comb in commonCombs)
            {
                if (word.Contains(comb))
                    yield return comb;
            }
        }

        public IEnumerable<Idea> RemovePartial(Idea idea)
        {
            var word = idea.Word;

            var matchingCombs = FindCombs(word);

            foreach (var comb in matchingCombs)
            {
                if (checkedWithout.Contains(comb))
                    break;

                checkedWithout.Add(comb);

                var loc = word.IndexOf(comb);
                var wordWithout = word.Replace(comb, "");
                var combs = GetCombs(new Idea(wordWithout, 0));

                foreach (var w in combs)
                {
                    var newWord = w.Word.Insert(loc, comb);
                    w.Word = newWord;
                    yield return w;
                }
            }
        }

        public override IEnumerable<Idea> ReturnLetter(char s)
        {
            var all = Letters.GetNear.All;

            foreach (var thing in all)
            {
                yield return new Idea(thing.ToString(), Rating);
            }
        }
    }
}
