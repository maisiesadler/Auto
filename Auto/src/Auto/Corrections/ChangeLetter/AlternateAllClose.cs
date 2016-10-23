using Auto.LetterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public class AlternateAllClose : ITest
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
            return GetCombs(word);
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
