using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StratagemHero
{
    internal class Stratagems
    {
        public static List<Stratagem> stratagems = new List<Stratagem>();
        public static async Task loadStratagems()
        {
            string[] lines = File.ReadAllLines("Stratagems.txt");
            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                string[] parts = line.Split(':');

                string name = parts[0];
                string code = parts[1];

                //Console.WriteLine(name);
                //Console.WriteLine(code);
                stratagems.Add(new Stratagem(code, name));
            }
        }
    }
    internal class Stratagem
    {
        private readonly Dictionary<char, char> keyToSymbol = new Dictionary<char, char>()
        {
            { 'W', '↑' },
            { 'D', '→' },
            { 'S', '↓' },
            { 'A', '←' }
        };
        public char[] keys;
        public char[] symbols;
        public string name;
        internal Stratagem(string _keys, string _name)
        {
            this.keys = _keys.ToCharArray();
            this.symbols = this.keys.Select(key => keyToSymbol[key]).ToArray();
            //Console.WriteLine(new string(this.symbols));
            this.name = _name;
        }
    }
}
