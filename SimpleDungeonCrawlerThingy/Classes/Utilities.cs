using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDungeonCrawlerThingy
{
    public class Utilities
    {public static ConsoleColor DefaultColour { get; } = Console.ForegroundColor;

        public static void ShiftColour(ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
        }

    }
}
