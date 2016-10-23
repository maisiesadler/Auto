using Auto.LetterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.Corrections
{
    public class KeepCommonCombinations : ITest
    {
        public override List<Idea> Execute(Idea word)
        {
            commonCombs = new HashSet<string> { "ing", "ea", "ai", "st", "ant", "ation" };

            return RemovePartial(word).ToList();
        }

        private HashSet<string> commonCombs;

        public override int Rating
        {
            get
            {
                return 1;
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
            foreach(var comb in matchingCombs)
            {
                var loc = word.IndexOf(comb);
                var wordWithout = word.Replace(comb, "");
                var combs = GetCombs(new Idea(wordWithout, 0));
                
                foreach(var w in combs)
                {
                    var newWord = w.Word.Insert(loc, comb);
                    w.Word = newWord;
                    yield return w;
                }
            }
        }

        public override IEnumerable<Idea> ReturnLetter(char s)
        {
            var near = Letters.GetNear.Near;

            if (near.ContainsKey(s))
            {
                foreach (var thing in near[s])
                {
                    yield return new Idea(thing.ToString(), Rating);
                }
            }
        }
    }
}
