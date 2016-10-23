using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.Corrections
{
    public class AddDouble : ITest
    {
        public override List<Idea> Execute(Idea word)
        {
            doubles = new HashSet<char> { 'n', 'e', 't', 'p', 'l', 'g', 'f', 'd', 's' };

            return AddDoubles(word);
        }

        private HashSet<char> doubles;

        public override int Rating
        {
            get
            {
                return 1;
            }
        }

        public IEnumerable<string> GetAlts(char c)
        {
            if (doubles.Contains(c))
            {
                //Logger.Debug("Add double for " + c);
                yield return c + "" + c;
            }
        }

        public List<Idea> AddDoubles(Idea idea)
        {
            var combs = AddLetter(idea, GetAlts);

            Logger.Debug(idea.Word + ": " + string.Join(", ", combs.Select(x => x.Word)));

            return combs.ToList();
        }

        public override IEnumerable<Idea> ReturnLetter(char s)
        {
            throw new NotImplementedException();
        }
    }
}
