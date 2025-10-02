using System;
using Boekwinkel.Domein;
using Boekwinkel.Utils;
using System.Collections.Generic;

namespace Boekwinkel
{
    internal class Program
    {
        static readonly List<Boek> _boeken = new();
        static readonly List<Tijdschrift> _tijdschriften = new();

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // demo-items
            _boeken.Add(new Boek("978-XXXXXXXXXX", "Mijn Eigen Boek", "Mijn Uitgever", 19.99m));
            _boeken.Add(new Boek("978-YYYYYYYYYY", "Nog Een Boek", "Andere Uitgever", 24.50m));

            _tijdschriften.Add(new Tijdschrift("977-AAAAAAA001", "Mijn Magazine", "MagPress", 6.95m, Frequentie.Wekelijks));
            _tijdschriften.Add(new Tijdschrift("977-BBBBBBB002", "Tech Weekly", "CodePub", 4.50m, Frequentie.Maandelijks));

            Menu();

        }

        static void Menu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("== Boekwinkel ==");
                Console.WriteLine("[B] Boeken tonen   [T] Tijdschriften tonen");
                Console.WriteLine("[N] Nieuw boek     [M] Nieuw tijdschrift");
                Console.WriteLine("[O] Boek bestellen [A] Tijdschrift abonnement");
                Console.WriteLine("[Q] Stoppen");
                Console.Write("> ");

                var key = Console.ReadKey(intercept: true).Key;
                Console.WriteLine();
                switch (key)
                {
                    case ConsoleKey.B: ToonBoeken(); break;
                    case ConsoleKey.T: ToonTijdschriften(); break;
                    case ConsoleKey.N: NieuwBoek(); break;
                    case ConsoleKey.M: NieuwTijdschrift(); break;
                    case ConsoleKey.O: BestelBoek(); break;
                    case ConsoleKey.A: BestelTijdschrift(); break;
                    case ConsoleKey.Q: return;
                    default: Console.WriteLine("Onbekende keuze."); break;
                }
            }
        }

        static void ToonBoeken()
        {
            if (_boeken.Count == 0) { Console.WriteLine("Geen boeken."); return; }
            for (int i = 0; i < _boeken.Count; i++)
                Console.WriteLine($"{i + 1}. {_boeken[i]}");
        }

        static void ToonTijdschriften()
        {
            if (_tijdschriften.Count == 0) { Console.WriteLine("Geen tijdschriften."); return; }
            for (int i = 0; i < _tijdschriften.Count; i++)
                Console.WriteLine($"{i + 1}. {_tijdschriften[i]}");
        }

        static void NieuwBoek()
        {
            var b = new Boek();
            b.Lees();
            _boeken.Add(b);
            Console.WriteLine("Toegevoegd: " + b);
        }

        static void NieuwTijdschrift()
        {
            var t = new Tijdschrift();
            t.Lees();
            _tijdschriften.Add(t);
            Console.WriteLine("Toegevoegd: " + t);
        }

        static void BestelBoek()
        {
            if (_boeken.Count == 0) { Console.WriteLine("Geen boeken om te bestellen."); return; }
            ToonBoeken();
            int keuze = Input.ReadInt("Kies # boek: ", 1, _boeken.Count);
            int aantal = Input.ReadInt("Aantal: ", 1, 1000);

            var boek = _boeken[keuze - 1];
            var bestelling = new Bestelling<Boek>(boek, aantal);

            bestelling.Besteld += OnBesteld; // event handler
            var res = bestelling.Bestel();   // tuple-return

            Console.WriteLine($"→ Bestelling ID {bestelling.Id} | ISBN={res.Isbn} | Aantal={res.Aantal} | Totaal=€ {res.TotalePrijs:0.00}");
        }

        static void BestelTijdschrift()
        {
            if (_tijdschriften.Count == 0) { Console.WriteLine("Geen tijdschriften om te bestellen."); return; }
            ToonTijdschriften();
            int keuze = Input.ReadInt("Kies # tijdschrift: ", 1, _tijdschriften.Count);
            int aantal = Input.ReadInt("Aantal per verschijning: ", 1, 1000);

            Console.WriteLine("Aboperiode: 1) 1 maand  2) 3 maanden  3) 12 maanden");
            int p = Input.ReadInt("Keuze: ", 1, 3);
            Aboperiode periode = p switch
            {
                1 => Aboperiode.EenMaand,
                2 => Aboperiode.DrieMaanden,
                _ => Aboperiode.TwaalfMaanden
            };

            var ts = _tijdschriften[keuze - 1];
            var bestelling = new Bestelling<Tijdschrift>(ts, aantal, periode);

            bestelling.Besteld += OnBesteld;
            var res = bestelling.Bestel();

            Console.WriteLine($"→ Abonnement ID {bestelling.Id} ({periode}) | ISBN={res.Isbn} | Aantal/verschijning={res.Aantal} | Totaal=€ {res.TotalePrijs:0.00}");
        }

        static void OnBesteld(object? sender, BesteldEventArgs e)
        {
            Console.WriteLine($"[EVENT] Besteld: ID {e.Id}, ISBN {e.Isbn}, aant. {e.Aantal}, totaal € {e.Totaal:0.00}");
        }
    }
}
