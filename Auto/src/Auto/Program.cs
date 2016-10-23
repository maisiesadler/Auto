using Auto.Corrections;
using Auto.Corrections.ChangeLetter;
using Auto.Corrections.Suggestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // var find = new FindWord();

            Console.WriteLine("Go:");
            var word = Console.ReadLine();
            while (word.Length > 0)
            {
                var findWord = new FindWordManager();

                //findWord.AddTest(new AlternateOneClose());
                //findWord.AddTest(new GetSwitched());
                //findWord.AddTest(new RemoveDoubles(), 1);
                //findWord.AddTest(new KeepCommonCombinations());
                //findWord.AddTest(new AddDouble());


                //findWord.AddTest(new KeepCommonCombinationsAll());
                //findWord.AddTest(new AlternateAllClose());
                //findWord.AddTest(new AlternateOneAll());

                findWord.AddTest(new SuggestCommon(), 3);

                findWord.Find(word);
                findWord.PrintAll();

                word = Console.ReadLine();
            }
        }
    }

    public static class MainHelper
    {
        public static string GetStr(this List<Idea> list)
        {
            //list = list.OrderBy(x => x.Score).Take(5).ToList();
            return string.Join(", ", list.Select(x => $"{x.Word}: {x.Score}"));
        }

        public static void PrintSuggestions(this List<Idea> list)
        {
            list = list.OrderBy(x => x.Score).Take(5).ToList();
            Console.WriteLine("Suggestions: " + string.Join(", ", list.Select(x => $"{x.Word}: {x.Score}")));
        }

        public static void PrintAll(this List<Idea> list, string title)
        {
            if (list != null)
            {
                list = list.OrderBy(x => x.Score).ToList();
                Console.WriteLine(title + $": ({list.Count})" + string.Join(", ", list.Select(x => $"{x.Word}: {x.Score}")));
            }
        }

        public static void AddDistinctRange(this List<Idea> list, IEnumerable<Idea> listToAdd)
        {
            foreach (var idea in listToAdd)
            {
                if (list.Count(item => item.Word == idea.Word) == 0 && idea.Score < 5)
                    list.Add(idea);
            }
        }
    }

    //public class IsNear
    //{
    //    public Dictionary<char, List<char>> Near { get; set; }

    //    public IsNear()
    //    {
    //        Near = new Dictionary<char, List<char>>();
    //        Near.Add('a', new List<char> { 'q', 'w', 's', 'x', 'z' });
    //        Near.Add('b', new List<char> { 'v', 'g', 'h', 'n' });
    //        Near.Add('c', new List<char> { 'x', 'd', 'f', 'v' });
    //        Near.Add('d', new List<char> { 's', 'e', 'r', 'f', 'c', 'x' });
    //        Near.Add('e', new List<char> { 'w', 's', 'd', 'r' });
    //        Near.Add('f', new List<char> { 'd', 'r', 't', 'g', 'v', 'c' });
    //        Near.Add('g', new List<char> { 'f', 't', 'y', 'h', 'b', 'v' });
    //        Near.Add('h', new List<char> { 'g', 'y', 'u', 'j', 'n', 'b' });
    //        Near.Add('i', new List<char> { 'u', 'j', 'k', 'l', 'o' });
    //        Near.Add('j', new List<char> { 'h', 'u', 'i', 'k', 'm', 'n' });
    //        Near.Add('k', new List<char> { 'j', 'i', 'o', 'l', 'm' });
    //        Near.Add('l', new List<char> { 'k', 'o', 'p' });
    //        Near.Add('m', new List<char> { 'n', 'j', 'k' });
    //        Near.Add('n', new List<char> { 'b', 'h', 'j', 'm' });
    //        Near.Add('o', new List<char> { 'i', 'k', 'l', 'p' });
    //        Near.Add('p', new List<char> { 'o', 'l' });
    //        Near.Add('q', new List<char> { 'a', 'w' });
    //        Near.Add('r', new List<char> { 'e', 'f', 't' });

    //        Near['a'].Add(']');
    //        Near['a'].Add(']');

    //        var x = Near['a'];


    //    }
    //}
}
