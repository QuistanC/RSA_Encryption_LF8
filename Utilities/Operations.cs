using System.Numerics;

namespace RSA_Encryption.Utilities;

public static class Operations
{
    public static BigInteger GenerateModulus(BigInteger primeOne, BigInteger primeTwo)
    {
        return primeTwo * primeOne;
    }
    public static BigInteger GenerateCarmichaels(BigInteger primeOne, BigInteger primeTwo) => FindLowestCommonMultiple(primeOne - 1, primeTwo - 1);


    public static BigInteger GeneratePrivateKey(BigInteger e, BigInteger carmichaels)
    {
        var (gcd, x, y) = ExtendedEuclideanAlgorithmRec(e, carmichaels);
        if (gcd != 1)
            throw new ArgumentException("e and λ(n) are not coprime");
        BigInteger d = x % carmichaels;
        if (d < 0) d += carmichaels;
        while (d <= 0)
        {
            d += carmichaels;
            if (d >= carmichaels)
                throw new Exception("cannot generate d");
        }
        return d;
    }
    // Returns [s, t, r] such that s*a + t*b == gcd(a,b) == r
    //private static IEnumerable<BigInteger> ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b) => ExtendedEuclideanAlgorithmRec([a,b], [1,0], [0,1]);
    private static (BigInteger gcd, BigInteger x, BigInteger y) ExtendedEuclideanAlgorithmRec(BigInteger a, BigInteger b)
    {
        BigInteger oldR = a, r = b;
        BigInteger oldS = 1, s = 0;
        BigInteger oldT = 0, t = 1;

        while (r != 0)
        {
            BigInteger quotient = oldR / r;

            (oldR, r) = (r, oldR - quotient * r);
            (oldS, s) = (s, oldS - quotient * s);
            (oldT, t) = (t, oldT - quotient * t);
        }

        // oldR is gcd, oldS and oldT are Bézout coefficients
        return (oldR, oldS, oldT);
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

}
