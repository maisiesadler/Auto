using Auto.LetterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.Corrections.Suggestions
{
    public class SuggestAll : Suggest
    {
        public override int Rating
        {
            get
            {
                return 1;
            }
        }
        
        public override void SetLetters(string word)
        {
            letters = Letters.GetNear.All;
        }
    }
}
