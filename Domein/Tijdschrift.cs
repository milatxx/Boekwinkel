using System;

namespace Boekwinkel.Domein
{
    public enum Frequentie { Dagelijks = 1, Wekelijks = 2, Maandelijks = 3 }

    public class Tijdschrift : Boek
    {
        public Frequentie Frequentie { get; set; } = Frequentie.Maandelijks;

        public Tijdschrift() : base() { }

        public Tijdschrift(string isbn, string naam, string uitgever, decimal prijs, Frequentie freq)
            : base(isbn, naam, uitgever, prijs)
        {
            Frequentie = freq;
        }

        public override string ToString()
            => base.ToString() + $" — Verschijnt: {Frequentie}";

        public override void Lees()
        {
            base.Lees();
            Console.WriteLine("Frequentie: 1) Dagelijks  2) Wekelijks  3) Maandelijks");
            Console.Write("Keuze: ");
            if (int.TryParse(Console.ReadLine(), out int k) && k is >= 1 and <= 3)
                Frequentie = (Frequentie)k;
        }
    }
}
