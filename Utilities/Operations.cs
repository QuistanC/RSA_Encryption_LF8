using System.Numerics;

namespace RSA_Encryption.Utilities;

public static class Operations
{
    public static BigInteger GenerateModulus(BigInteger primeOne, BigInteger primeTwo)
    {
        return primeTwo * primeOne;
    }

    public static BigInteger GenerateEulers(BigInteger primeOne, BigInteger primeTwo)
    {
        return (primeOne-1)*(primeTwo-1);
    }
    public static BigInteger GenerateCarmichaels(BigInteger primeOne, BigInteger primeTwo) => FindLowestCommonMultiple(primeOne - 1, primeTwo - 1);

    //viel zu langsam, müssen sinnvollen algorithmus implementieren
    public static BigInteger GeneratePrivateKey(BigInteger e, BigInteger carmichaels)
    {
        var extendedAlgoSols = ExtendedEuclideanAlgorithm(e, carmichaels);
        var d = extendedAlgoSols.First();
        while (d <= 0)
        {
            d += carmichaels;
            if (d >= carmichaels)
                throw new Exception("cannot generate d");
        }
        return d;
    }
    // Returns [s, t, r] such that s*a + t*b == gcd(a,b) == r
    private static IEnumerable<BigInteger> ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b) => ExtendedEuclideanAlgorithmRec([a,b], [1,0], [0,1]);
    private static IEnumerable<BigInteger> ExtendedEuclideanAlgorithmRec(List<BigInteger> r, List<BigInteger> s, List<BigInteger> t)
    {
        if (r.Count != s.Count || s.Count != t.Count)
            throw new ArgumentException("Lists should be same length");
        var currentI = r.Count - 1;
        var q = r[currentI - 1] / r[currentI];
        r.Add(r[currentI - 1] - q * r[currentI]);
        s.Add(s[currentI - 1] - q * s[currentI]);
        t.Add(t[currentI - 1] - q * t[currentI]);

        if (r.Last() == 0)
            return [s[currentI], t[currentI], r[currentI]];

        return ExtendedEuclideanAlgorithmRec(r, s, t);
    }
    public static BigInteger FindGreatestCommonDivisor(BigInteger primeOne, BigInteger primeTwo)
    {
        if (primeOne > primeTwo)
        {
            while (primeTwo != 0)
            {
                BigInteger temp = primeTwo;
                primeTwo = primeOne % primeTwo;
                primeOne = temp;
            }

            return BigInteger.Abs(primeOne);
        }
        if (primeOne < primeTwo)
        {
            while (primeOne != 0)
            {
                BigInteger temp = primeOne;
                primeOne = primeTwo % primeOne;
                primeTwo = temp;
            }
            return BigInteger.Abs(primeTwo);
        }

        return primeOne;

    }
    public static BigInteger FindLowestCommonMultiple(BigInteger n1, BigInteger n2) => n1 * n2 / FindGreatestCommonDivisor(n1, n2);
    

    public static BigInteger ModInverse(BigInteger publicExponent, BigInteger totient)
    {
        if (totient <= 0)
            throw new ArgumentException("Totient must be positive.", nameof(totient));

        if (FindGreatestCommonDivisor(publicExponent, totient) != 1)
            throw new InvalidOperationException("e und φ(n) sind nicht teilerfremd.");

        BigInteger d = 0, newT = 1;
        BigInteger r = totient, newR = publicExponent;

        while (newR != 0)
        {
            BigInteger q = r / newR;
            (d, newT) = (newT, d - q * newT);
            (r, newR) = (newR, r - q * newR);
        }

        if (d < 0) d += totient;
        return d;
    }

    private static bool checkIfPrime(BigInteger primeOne, BigInteger primeTwo)
    {
        return FindGreatestCommonDivisor(primeOne, primeTwo) == 1;
    }

   

}
