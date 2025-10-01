using System.Threading;

namespace Boekwinkel.Services
{
    public static class IdGenerator
    {
        private static int _laatste;
        public static int Next() => Interlocked.Increment(ref _laatste);
    }
}
