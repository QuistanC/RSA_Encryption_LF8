
using System.Numerics;

namespace RSA_Encryption;

internal class PrimeGenerator
{
    internal BigInteger[] GeneratePrimePair()
    {
        var PrimeList = new List<BigInteger>();
        var random = new Random();

        while (PrimeList.Count < 2)
        {
            var bytes = new Byte[128];
            random.NextBytes(bytes);
            var candidate = BitConverter.ToUInt128(bytes);
            var parsedCandidate = Math.Abs((long)candidate % ((long)Math.Pow(10,19)) + (long)Math.Pow(10, 19)-1);
            if (IsPrime(parsedCandidate))
                PrimeList.Add(parsedCandidate);
        }

        return [.. PrimeList];
    }

    private bool IsPrime(long candidate)
    {
        var border = (long)Math.Sqrt(candidate);

        for (long i = 2; i < border + 1; i++)
            if (candidate % i == 0)
            { 
                return false; 
            }

        return true;
    }
}
