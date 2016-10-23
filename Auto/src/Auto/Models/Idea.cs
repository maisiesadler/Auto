using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public class Idea
    {
        public Idea(string word, double score)
        {
            Word = word;
            Score = score;
        }
        public string Word { get; set; }
        public double Score { get; set; }

        public override string ToString()
        {
            return Word + ": " + Score;
        }
    }
}
