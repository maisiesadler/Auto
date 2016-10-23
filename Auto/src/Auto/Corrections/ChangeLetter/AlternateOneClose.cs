using Auto.LetterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public class AlternateOneClose : ITest
    {
        public override List<Idea> Execute(Idea word)
        {
            _near = Letters.GetNear.Near;
            
            return GetFirst(word).ToList();
        }

        private Dictionary<char, HashSet<char>> _near;

        public override int Rating
        {
            get
            {
                return 1;
            }
        }

        public IEnumerable<string> GetAlts(char c)
        {
            if (_near.ContainsKey(c))
            {
                foreach (var alternate in _near[c])
                {
                    yield return alternate.ToString();
                }
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
