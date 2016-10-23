using Auto.LetterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.Corrections.ChangeLetter
{
    public class AlternateOneAll : ITest
    {
        public override List<Idea> Execute(Idea word)
        {
            _all = Letters.GetNear.All;

            return GetFirst(word).ToList();
        }

        private HashSet<char> _all;

        public override int Rating
        {
            get
            {
                return 2;
            }
        }

        public IEnumerable<string> GetAlts(char c)
        {
            foreach (var alternate in _all)
            {
                yield return alternate.ToString();
            }
        }

        public List<Idea> GetFirst(Idea idea)
        {
            var returnMe = new List<Idea> { idea };
            returnMe.AddRange(AddLetter(idea, GetAlts));
            return returnMe;
        }

        public override IEnumerable<Idea> ReturnLetter(char s)
        {
            throw new NotImplementedException();
        }
    }
}
