using System;
using System.Globalization;

namespace Boekwinkel.Domein
{
    public class Boek
    {
        public string Isbn { get; set; } = "";
        public string Naam { get; set; } = "";
        public string Uitgever { get; set; } = "";

        private decimal _prijs;
        // Prijs wordt in [5,50] gehouden via setter
        public decimal Prijs
        {
            get => _prijs;
            set
            {
                if (value < 5m) _prijs = 5m;
                else if (value > 50m) _prijs = 50m;
                else _prijs = decimal.Round(value, 2);
            }
        }

        public Boek() { }

        public Boek(string isbn, string naam, string uitgever, decimal prijs)
        {
            Isbn = isbn;
            Naam = naam;
            Uitgever = uitgever;
            Prijs = prijs; // setter bewaakt 5–50
        }

        public override string ToString()
            => $"{Naam} — {Uitgever} — ISBN {Isbn} — € {Prijs:0.00}";

        public virtual void Lees()
        {
            Console.Write("ISBN: "); Isbn = Console.ReadLine() ?? "";
            Console.Write("Naam: "); Naam = Console.ReadLine() ?? "";
            Console.Write("Uitgever: "); Uitgever = Console.ReadLine() ?? "";
            Console.Write("Prijs (5..50): ");
            if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.CurrentCulture, out var p)) p = 5m;
            Prijs = p;
        }
    }
}
