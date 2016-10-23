using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.Corrections
{
    public class RemoveDoubles : ITest
    {
        public override int Rating
        {
            get
            {
                return 1;
            }
        }

        public override List<Idea> Execute(Idea word)
        {
            return RemoveDoubleLetters(word).ToList();
        }

        private IEnumerable<Idea> RemoveDoubleLetters(Idea idea)
        {
            var word = idea.Word;
            var score = idea.Score;

            for (int i = 0; i < word.Length - 1; i++)
            {
                if (idea.Word[i] == idea.Word[i + 1])
                {
                    yield return new Idea(idea.Word.Substring(0, i + 1) + idea.Word.Substring(i + 2, word.Length - 2 - i), score + Rating);
                }
            }
        }

        public override IEnumerable<Idea> ReturnLetter(char s)
        {
            throw new NotImplementedException();
        }
    }
}
