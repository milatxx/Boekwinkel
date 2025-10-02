using System;

namespace Boekwinkel.Utils
{
    public static class Input
    {
        public static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                var s = Console.ReadLine();
                if (int.TryParse(s, out int v) && v >= min && v <= max) return v;
                Console.WriteLine($"Voer een getal tussen {min} en {max} in.");
            }
        }
    }
}
