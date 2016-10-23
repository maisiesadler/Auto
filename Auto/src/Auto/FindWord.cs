using Auto.Corrections;
using Auto.LetterManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public static class FindWord
    {
        private static Dictionary<string, List<Idea>> finalCache = new Dictionary<string, List<Idea>>();
        private static Dictionary<string, List<Idea>> mainCache = new Dictionary<string, List<Idea>>();
        private static Dictionary<char, HashSet<char>> isNear = Letters.GetNear.Near;
        private static HashSet<string> words = new Words().words;

        private static void AddToCache(string key, List<Idea> value)
        {
            if (value.Count == 0)
                return;

            if (mainCache.ContainsKey(key))
                mainCache[key].AddRange(value);
            else
            {
                mainCache.Add(key, value);
            }
        }

        public static bool checkMainCache(string word)
        {
            if (mainCache.ContainsKey(word) && mainCache[word].Count(x => x.Score < 2) > 5)
            {
                Logger.Debug($"Cache for {word} is full {string.Join(", ", mainCache[word].Select(x => x.Score < 2).ToString())}");
                return true;
            }

            return false;
        }

        public static List<Idea> CheckFinalCache(string word)
        {
            if (finalCache.ContainsKey(word))
                return finalCache[word];
            else
                return new List<Idea>();
        }

        public static void AddToFinalCache(string key, List<Idea> value)
        {
            if (value.Count == 0)
                return;

            if (finalCache.ContainsKey(key))
                finalCache[key].AddRange(value);
            else
            {
                finalCache.Add(key, value);
            }
        }

        public static bool IsMatch(this string word)
        {
            return words.Contains(word);
        }

        public static IEnumerable<Idea> FindMatches(this string word, Func<string, IEnumerable<Idea>> getIdeas)
        {
            var ideas = getIdeas(word);
            var found = new List<Idea>();

            foreach (var idea in ideas)
            {
                var i = idea.Word;
                if (words.Contains(i))
                {
                    found.Add(idea);
                }
            }

            found = found.OrderBy(x => x.Score).Take(5).ToList();

            AddToCache(word, found);
            return found;
        }

        public static IEnumerable<Idea> FindMatches(this IEnumerable<Idea> ideas, string word)
        {
            var found = new List<Idea>();

            foreach (var idea in ideas)
            {
                var i = idea.Word;
                if (words.Contains(i))
                {
                    found.Add(idea);
                }
            }

            found = found.OrderBy(x => x.Score).Take(5).ToList();

            AddToCache(word, found);
            return found;
        }

        /// <summary>
        /// Uses dictionary of chars
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static IEnumerable<Idea> FindUsing1(this string word)
        {
            if (checkMainCache(word))
            {
                return mainCache[word];
            }

            var maybe = new AlternateAllClose().Execute(new Idea(word, 0));
            return maybe;
        }

        /// <summary>
        /// Swaps one letter at a time for each alternative.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static IEnumerable<Idea> FindUsing3(string word)
        {
            if (checkMainCache(word))
            {
                return mainCache[word];
            }

            var maybe = new AlternateOneClose().Execute(new Idea(word, 0));
            return maybe;
        }

        public static IEnumerable<Idea> FindUsingDoubles(this string word)
        {
            if (checkMainCache(word))
            {
                return mainCache[word];
            }

            var maybe = new AddDouble().Execute(new Idea(word, 0));
            return maybe;
        }

        public static IEnumerable<Idea> FindKeepingCommonCombs(this string word)
        {
            if (checkMainCache(word))
            {
                return mainCache[word];
            }

            var maybe = new KeepCommonCombinations().Execute(new Idea(word, 0));
            return maybe;
        }

        public static IEnumerable<Idea> FindSwitches(this string word)
        {
            if (checkMainCache(word))
            {
                return mainCache[word];
            }

            var maybe = new GetSwitched().Execute(new Idea(word, 0));
            return maybe;
        }

        public static IEnumerable<Idea> FindUsingTest(this Idea idea, ITest test)
        {
            if (checkMainCache(idea.Word))
            {
                return mainCache[idea.Word];
            }

            var maybe = test.Execute(idea);
            return maybe;
        }

        #region AddFromList

        public static List<Idea> ExecuteTest(this IEnumerable<Idea> list, ITest test)
        {
            var returnMe = new List<Idea>();
            foreach (var idea in list)
            {
                returnMe.AddRange(idea.FindUsingTest(test));
            }

            return returnMe;
        }

        public static IEnumerable<Idea> AddAllAlts(this IEnumerable<Idea> list)
        {
            var returnMe = new List<Idea>();
            foreach (var idea in list)
            {
                returnMe.AddRange(idea.Word.FindUsing1());
            }

            return returnMe;
        }

        public static IEnumerable<Idea> AddDoubles(this IEnumerable<Idea> list)
        {
            var returnMe = new List<Idea>();
            foreach (var idea in list)
            {
                returnMe.AddRange(idea.Word.FindUsingDoubles());
            }

            return returnMe;
        }

        public static IEnumerable<Idea> AddKeepingCommonCombs(this IEnumerable<Idea> list)
        {
            var returnMe = new List<Idea>();
            foreach (var idea in list)
            {
                returnMe.AddRange(idea.Word.FindKeepingCommonCombs());
            }

            return returnMe;
        }
        #endregion
    }
}
