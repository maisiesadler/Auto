using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.Corrections
{
    public class GetSwitched : ITest
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
            return GetSwitch(word);
        }

        public IEnumerable<string> GetAlts(string swapLetters)
        {
            //yield return swapLetters;
            yield return string.Join("", swapLetters.Reverse());
        }

        public List<Idea> GetSwitch(Idea idea)
        {
            var word = idea.Word;
            var score = idea.Score;

            var combs = AddLetters(idea, 2, GetAlts).ToList();
            combs.Add(idea);

            //combs.PrintAll(idea.Word);

            Logger.Debug(idea.Word + ": " + string.Join(", ", combs.Select(x => x.Word)));

            return combs;
        }

        public override IEnumerable<Idea> ReturnLetter(char s)
        {
            throw new NotImplementedException();
        }
    }
}
