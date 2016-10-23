using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.Corrections.Suggestions
{
    public abstract class Suggest : ITest
    {
        public abstract void SetLetters(string word);

        protected HashSet<char> letters;
        
        public override List<Idea> Execute(Idea word)
        {
            SetLetters(word.Word);
            return AddLetter(word).ToList();
        }

        private IEnumerable<Idea> AddLetter(Idea word)
        {
            foreach (var c in letters)
            {
                yield return new Idea(word.Word + c, word.Score + Rating);
            }
        }

        public override IEnumerable<Idea> ReturnLetter(char s)
        {
            throw new NotImplementedException();
        }
    }
}
