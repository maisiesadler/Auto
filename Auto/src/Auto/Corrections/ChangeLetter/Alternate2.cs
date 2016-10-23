using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public class Alternate2 : ITest
    {
        public override int Rating
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override List<Idea> Execute(Idea word)
        {
            return GetCombs(word);
        }
        
        public override IEnumerable<Idea> ReturnLetter(char s)
        {
            var keys = new Keys().KeyColl;
            //yield return new Idea(s.ToString(), 0);

            foreach (var thing in s.SelectNear())
            {
                var score = s.GetScore(thing);
                yield return new Idea(thing.ToString(), score);
            }
        }
    }
}
