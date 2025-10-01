using System;

namespace Boekwinkel.Domein
{
    public class BesteldEventArgs : EventArgs
    {
        public int Id { get; }
        public string Isbn { get; }
        public int Aantal { get; }
        public decimal Totaal { get; }

        public BesteldEventArgs(int id, string isbn, int aantal, decimal totaal)
        {
            Id = id;
            Isbn = isbn;
            Aantal = aantal;
            Totaal = totaal;
        }
    }
}
