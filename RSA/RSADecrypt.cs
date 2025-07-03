
using System.Numerics;

namespace RSA_Encryption.RSA;

internal class RSADecrypt(BigInteger d, BigInteger n)
{
    private BigInteger D = d;
    private BigInteger N = n;
    internal Byte[] Decrypt(BigInteger[] input)
    {
        var decrypted = new List<byte>();

        foreach (var inputNum in input)
        {
            var newByte = BigInteger.ModPow(inputNum, D, N);
            decrypted.Add((Byte)newByte);
            //var nextByte = new BigInteger(1);
            //for (var i = 0; i < D; i++)
            //    nextByte *= inputNum;

            //decrypted.Add((Byte)(nextByte % N));
        }

        return [.. decrypted];
    }
}
