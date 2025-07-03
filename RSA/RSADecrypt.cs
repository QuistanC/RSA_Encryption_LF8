
using System.Numerics;
using System.Text;

namespace RSA_Encryption.RSA;

internal class RSADecrypt(BigInteger d, BigInteger n)
{
    private BigInteger D = d;
    private BigInteger N = n;
    private int blockSize;
    internal string Decrypt(byte[] input)
    {
        blockSize = (int)(n.GetBitLength() + 7) / 8;
        using var ms = new MemoryStream();

        for (int i = 0; i < input.Length; i += blockSize)
        {
            int len = Math.Min(blockSize, input.Length - i);
            byte[] block = new byte[len];
            Buffer.BlockCopy(input, i, block, 0, len);

            byte[] blockLE = new byte[len + 1];
            for (int j = 0; j < len; j++)
            {
                blockLE[j] = block[len - 1 - j];
            }
            blockLE[len] = 0;

            BigInteger c = new BigInteger(blockLE);

            BigInteger m = BigInteger.ModPow(c, D, N);

            byte[] plainLE = m.ToByteArray();

            if (plainLE.Length > 1 && plainLE[^1] == 0)
                plainLE = plainLE[..^1];

            byte[] plainBE = plainLE.Reverse().ToArray();

            ms.Write(plainBE);
        }
        return DecodeBytes(ms.ToArray());
    }

    private string DecodeBytes(byte[] decryptedText)
    {
        return Encoding.UTF8.GetString(decryptedText);
    }
}
