using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stratagemHeroConsole
{
    internal class Score
    {
        internal int points { get; private set; } = 0;
        internal int errors { get; private set; } = 0;
        internal Score() { }

        internal void IncPoints() => points++;
        internal void IncErrors() => errors++;
        internal void AllToZero()
        {
            points = 0;
            errors = 0;
        }
    }
}
