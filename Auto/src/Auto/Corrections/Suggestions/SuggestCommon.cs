using Auto.LetterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto.Corrections.Suggestions
{
    public class SuggestCommon : Suggest
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
            letters = new HashSet<char>();

            var l = Letters.GetCommon.Afters[word[word.Length - 1]].OrderByDescending(x => x.Count).ToList();
            for (int i = 0; i < 5; i++)
            {
                letters.Add(l[i].Char);
            }
        }
    }
}
