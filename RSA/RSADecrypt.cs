
using RSA_Encryption.Utilities;
using System.Numerics;
using System.Text;

namespace RSA_Encryption.RSA;

internal class RSADecrypt(BigInteger d, BigInteger n)
{
    private BigInteger D = d;
    private BigInteger N = n;
    private int blockCipher;
    internal string Decrypt(byte[] cipher)
    {
        blockCipher = (int)(n.GetBitLength() + 7) / 8;
        using var ms = new MemoryStream();

        for (int i = 0; i < cipher.Length; i += blockCipher)
        {
            byte[] block = new byte[blockCipher];
            Buffer.BlockCopy(cipher, i, block, 0, blockCipher);

            BigInteger c = new BigInteger(block.Reverse().Append((byte)0).ToArray());
            BigInteger m = BigInteger.ModPow(c, D, N);

            byte[] le = m.ToByteArray();
            if (le.Length > 1 && le[^1] == 0)
                le = le[..^1];
            byte[] be = le.Reverse().ToArray();
            if (be.Length < blockCipher)                 
            {
                byte[] tmp = new byte[blockCipher];
                Buffer.BlockCopy(be, 0, tmp,
                                 blockCipher - be.Length,
                                 be.Length);
                be = tmp;
            }
            byte[] slice = RsaHelpersPadding.UnpadBlock(be);

            ms.Write(slice);
        }
        return Encoding.UTF8.GetString(ms.ToArray());
    }
}
