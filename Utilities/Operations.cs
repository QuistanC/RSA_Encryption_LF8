using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RSA_Encryption.Utilities;

public class Operations
{
    public static BigInteger GenerateModulus(BigInteger primeOne, BigInteger primeTwo)
    {
        return primeTwo * primeOne;
    }

    public static BigInteger GenerateEulers(BigInteger primeOne, BigInteger primeTwo)
    {
        return (primeOne-1)*(primeTwo-1);
    }

    public static BigInteger GeneratePrivateKey(BigInteger publicKey, BigInteger eulers)
    {
        throw new NotImplementedException();
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
        else if (primeOne < primeTwo)
        {
            while (primeOne != 0)
            {
                BigInteger temp = primeOne;
                primeOne = primeTwo % primeOne;
                primeTwo = temp;
            }
            return BigInteger.Abs(primeTwo);
        }

        else return primeOne;

    }

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

    

   

}
