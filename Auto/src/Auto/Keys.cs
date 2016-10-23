using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public class Keys
    {
        public List<Key> KeyColl { get; set; }

        public Keys()
        {
            KeyColl = new List<Key>();
            KeyColl.AddRange(KeyHelper.GetRange(0, new[] { 'z', 'x', 'c', 'v', 'b', 'n', 'm' }, 1));
            KeyColl.AddRange(KeyHelper.GetRange(1, new[] { 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l' }, 0.5));
            KeyColl.AddRange(KeyHelper.GetRange(2, new[] { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p' }, 0));
        }
    }

    public static class KeyHelper
    {
        public static IEnumerable<Key> GetRange(int row, char[] vals, double offset = 0)
        {
            for(int i = 0; i < vals.Length; i++)
            {
                yield return new Key(vals[i], row, i + offset);
            }
        }

        public static double GetScore(this char c, Key key2)
        {
            var keys = new Keys().KeyColl;
            Key key1 = keys.First(x => x.Value == c);
            var x2 = Math.Pow(key1.X - key2.X, 2);
            var y2 = Math.Pow(key1.Y - key2.Y, 2);
            return Math.Sqrt(x2 + y2);
        }

        public static double GetScore(this char c, char c2)
        {
            var keys = new Keys().KeyColl;
            Key key1 = keys.First(x => x.Value == c);
            Key key2 = keys.First(x => x.Value == c2);
            var x2 = Math.Pow(key1.X, key2.X);
            var y2 = Math.Pow(key1.Y, key2.Y);
            return Math.Sqrt(x2 + y2);
        }

        public static Dictionary<char, IEnumerable<char>> cache = new Dictionary<char, IEnumerable<char>>();

        public static IEnumerable<char> SelectNear(this char c)
        {
            if (cache.ContainsKey(c))
                return cache[c];

            Console.WriteLine("Selecting near for " + c);
            var keys = new Keys().KeyColl;
            Key key1 = keys.First(x => x.Value == c);
            var got = keys.Where(x => x.Value.GetScore(key1) < 2);

            cache.Add(c, got.Select(x => x.Value));

            Console.WriteLine("Got: " + string.Join(Environment.NewLine, got.Select(x => x.Value + ": " + Math.Round(x.Value.GetScore(c), 2))));

            return got.Select(x => x.Value);
        }
    }

    public class Key
    {
        public char Value { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }

        public Key(char value, double x, double y)
        {
            Value = value;
            X = x;
            Y = y;
        }
    }
}
