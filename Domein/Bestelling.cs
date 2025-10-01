using System;
using Boekwinkel.Services;

namespace Boekwinkel.Domein
{
    public enum Aboperiode { EenMaand = 1, DrieMaanden = 3, TwaalfMaanden = 12 }

    public class Bestelling<T> where T : Boek
    {
        private int _id;
        public int Id
        {
            get => _id;
            private set => _id = (value == 0) ? IdGenerator.Next() : value; // unieke toekenning
        }

        public T Item { get; }
        public DateTime Datum { get; }
        public int Aantal { get; set; }
        public Aboperiode? Periode { get; set; } // voor tijdschriften

        public Bestelling(T item, int aantal, Aboperiode? periode = null)
        {
            Id = 0;
            Item = item;
            Aantal = Math.Max(1, aantal);
            Periode = periode;
            Datum = DateTime.Now;
        }

        // Tuple: (ISBN, Aantal, TotalePrijs)
        public (string Isbn, int Aantal, decimal TotalePrijs) Bestel()
        {
            decimal totaal = Item.Prijs * Aantal;

            if (Item is Tijdschrift ts && Periode.HasValue)
            {
                int perMaand = ts.Frequentie switch
                {
                    Frequentie.Dagelijks => 30,
                    Frequentie.Wekelijks => 4,
                    _ => 1
                };
                int maanden = (int)Periode.Value;
                int verschijningen = perMaand * maanden;
                totaal = ts.Prijs * Aantal * verschijningen;
            }

            return (Item.Isbn, Aantal, decimal.Round(totaal, 2));
        }
    }
}
