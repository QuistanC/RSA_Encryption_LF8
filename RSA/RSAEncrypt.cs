
using System.Numerics;

namespace RSA_Encryption.RSA;

internal class RSAEncrypt(BigInteger n, BigInteger e)
{
    private BigInteger N = n;
    private BigInteger E = e;
    internal BigInteger[] Encrypt(Byte[] input)
    {
        var encrypted = new List<BigInteger>();

        foreach(var inputByte in input)
        {
            //var nextByte = new BigInteger(1);
            //for (var i = 0; i < E; i++)
            //    nextByte *= inputByte;
            var nextByte = BigInteger.Pow(inputByte, (int)E);
            encrypted.Add(nextByte);
        }

        return [.. encrypted];
    }
}
