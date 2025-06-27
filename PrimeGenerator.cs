
namespace RSA_Encryption;

internal class PrimeGenerator
{
    internal long[] GeneratePrimePair()
    {
        var PrimeList = new List<long>();
        var random = new Random();

        while (PrimeList.Count < 2)
        {
            var candidate = (long)random.NextDouble() * Math.Pow(10,10308);
            PrimeList.Add(0);
        }

        return [2, 3];
    }
}
