using Auto.Corrections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public class FindWordManager : IFindWordManager
    {
        //private string _word;
        private List<Idea> _combinations;
        private List<Idea> _matches;

        private List<ITest> _tests;

        public FindWordManager()
        {
            _tests = new List<ITest>();
        }

        public void AddTest(ITest test)
        {
            _tests.Add(test);
        }

        public void AddTest(ITest test, int depth)
        {
            if (depth < 1)
            {
                throw new Exception("Not valid depth");
            }
            else
            {
                for (int i = 0; i < depth; i++)
                {
                    _tests.Add(test);
                }
            }

        }

        public List<Idea> Find(string word)
        {
            //_word = word;
            _matches = new List<Idea>();

            var checkCache = FindWord.CheckFinalCache(word);
            if (checkCache.Count > 0)
            {
                _matches = checkCache;
                _matches.PrintSuggestions();
            }
            else
            {
                _combinations = new List<Idea>();
                _combinations.Add(new Idea(word, 0));

                foreach (var test in _tests)
                {
                    if (_matches.Count() < 5)
                    {
                        Logger.Main($"Getting {test.GetType().Name} ({_combinations.Count})");
                        _combinations.AddDistinctRange(_combinations.ExecuteTest(test));
                        _matches.AddDistinctRange(_combinations.FindMatches(word));
                        _matches.PrintSuggestions();
                    }
                }

                FindWord.AddToFinalCache(word, _matches);
            }
            Logger.Main("Done");

            return _matches.OrderBy(x => x.Score).ToList();
        }

        public void PrintAll()
        {
            _combinations.PrintAll("All");
        }
    }
}



//var matches = new List<Idea>();

//var checkCache = FindWord.CheckFinalCache(word);
//                if (checkCache.Count > 0)
//                {
//                    matches = checkCache;
//                    matches.PrintSuggestions();
//                }
//                else
//                {
//                    var f = FindWord.FindUsingTest(word, new Alternate3());

//matches.AddDistinctRange(f.FindMatches(word));
//                    matches.PrintSuggestions();

//                    if (matches.Count() < 5)
//                    {
//                        Logger.Main("getting switches");
//                        f = f.AddTest(new GetSwitched());
//                        matches.AddDistinctRange(f.FindMatches(word));
//                        matches.PrintSuggestions();
//                    }

//                    if (matches.Count() < 5)
//                    {
//                        Logger.Main("getting all combs ignoring common");
//                        var doubles = f.AddTest(new KeepCommonCombinations()); //f.AddKeepingCommonCombs();
//matches.AddDistinctRange(doubles.FindMatches(word));
//                        matches.PrintSuggestions();
//                    }

//                    if (matches.Count() < 5)
//                    {
//                        Logger.Main("getting doubles");
//                        f = f.AddTest(new AddDouble()); // f.AddDoubles();
//                        matches.AddDistinctRange(f.FindMatches(word));
//                        matches.PrintSuggestions();
//                    }

//                    if (matches.Count() < 5)
//                    {
//                        Logger.Main("getting alts");
//                        f = f.AddTest(new Alternate()); //f.AddAllAlts();
//                        matches.AddDistinctRange(f.FindMatches(word));
//                        matches.PrintSuggestions();
//                    }

//                    FindWord.AddToFinalCache(word, matches);
//                }

//                Logger.Main("Done");