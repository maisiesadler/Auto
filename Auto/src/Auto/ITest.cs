using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public abstract class ITest
    {
        public abstract int Rating { get; }

        public abstract List<Idea> Execute(Idea word);

        public List<Idea> Execute(List<Idea> words)
        {
            var returnMe = new List<Idea>();
            foreach (var w in words)
            {
                returnMe.AddRange(Execute(w));
            }

            return returnMe;
        }

        public List<Idea> GetCombs(Idea partial)
        {
            var partialW = partial.Word;
            if (partialW.Length == 1)
            {
                return ReturnLetter(partialW[0]).ToList();//.Select(x => new Idea(x.ToString(), 0)).ToList();
            }

            var res = new List<Idea>();
            var end = GetCombs(new Idea(partialW.Remove(0, 1), partial.Score));
            foreach (var e in end)
            {
                foreach (var c in ReturnLetter(partialW[0]))
                {
                    res.Add(new Idea(c.Word + e.Word, c.Score + e.Score));
                    //Console.WriteLine("Added: " + c.Word + e.Word);
                }
            }

            return res;
        }

        public IEnumerable<Idea> AddLetter(Idea idea, Func<char, IEnumerable<string>> alts)
        {
            var word = idea.Word;
            var score = idea.Score;

            foreach (var alternate in alts(word[0]))
            {
                yield return new Idea(alternate + word.Substring(1, word.Length - 1), score + Rating);
            }

            for (int i = 1; i < word.Length - 2; i++)
            {
                foreach (var alternate in alts(word[i]))
                {
                    yield return new Idea(word.Substring(0, i) + alternate + word.Substring(i + 1, word.Length - 1 - i), score + Rating);
                }
            }

            foreach (var alternate in alts(word[word.Length - 1]))
            {
                yield return new Idea(word.Substring(0, word.Length - 1) + alternate, score + Rating);
            }
        }

        public IEnumerable<Idea> AddLetters(Idea idea, int length, Func<string, IEnumerable<string>> alts)
        {
            var word = idea.Word;
            var score = idea.Score;

            if (word.Length > length)
            {
                foreach (var alternate in alts(word.Substring(0, length)))
                {
                    yield return new Idea(alternate + word.Substring(length, word.Length - length), score + Rating);
                }
                
                for (int i = 1; i < word.Length - length; i++)
                {
                    foreach (var alternate in alts(word.Substring(i, length)))
                    {
                        yield return new Idea(word.Substring(0, i) + alternate + word.Substring(i + length, word.Length - length - i), score + Rating);
                    }
                }

                foreach (var alternate in alts(word.Substring(word.Length - length, length)))
                {
                    var last = word.Substring(0, word.Length - length) + alternate;
                    yield return new Idea(last, score + Rating);
                }
            }
        }

        public abstract IEnumerable<Idea> ReturnLetter(char s);
    }
}
