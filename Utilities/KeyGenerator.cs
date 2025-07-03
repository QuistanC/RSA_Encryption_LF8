
using RSA_Encryption.models;
using System.Numerics;

namespace RSA_Encryption.Utilities;

public static class KeyGenerator
{
    public static KeySet Generate()
    {
        Console.WriteLine("Generating primes...");
        var primePair = new PrimeGenerator().GeneratePrimePair();
        Console.WriteLine($"Found primes: {primePair[0]}, {primePair[1]}");
        if (primePair.Length != 2)
            throw new Exception("Didn't get exactly two primes");

        Console.WriteLine("Generating n...");
        var n = Operations.GenerateModulus(primePair[0], primePair[1]);
        Console.WriteLine($"Found n = {n}");

        Console.WriteLine("Generating carmichael's...");
        var carmichaels = Operations.GenerateCarmichaels(primePair[0], primePair[1]);
        Console.WriteLine($"Found lambda = {carmichaels}");

        Console.WriteLine("Generating e...");
        var e = new BigInteger(2);

        while(Operations.FindGreatestCommonDivisor(e, carmichaels) != 1)
        {
            e++;
            if (e == carmichaels)
                throw new Exception("cannot generate e");
        }
        Console.WriteLine($"found e = {e}");

        Console.WriteLine("Generating d...");
        var d = Operations.GeneratePrivateKey(e, carmichaels);
        if (d <= 0)
            throw new Exception("cannot generate d");
        Console.WriteLine($"Found d = {d}");

        Console.WriteLine(n);
        Console.WriteLine(e);
        Console.WriteLine(d);

        return new(n,e,d);
    }
}
