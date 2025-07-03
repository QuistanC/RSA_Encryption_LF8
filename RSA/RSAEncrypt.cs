
using RSA_Encryption.Utilities;
using System.Numerics;
using System.Text;

namespace RSA_Encryption.RSA;

internal class RSAEncrypt(BigInteger n, BigInteger e)
{
    private BigInteger N = n;
    private BigInteger E = e;
    private int blockPlain;
    private int blockCipher;
    internal byte[] Encrypt()
    {
        
        blockCipher = (int)(N.GetBitLength() + 7) / 8;
        blockPlain = blockCipher - 11;
        byte[] convertedData = Encoding.UTF8.GetBytes("hi! This is a test string.");
        using var ms = new MemoryStream();

        for (int i = 0; i < convertedData.Length; i += blockPlain)
        {
            int len = Math.Min(blockPlain, convertedData.Length - i);
            byte[] slice = new byte[len];
            Buffer.BlockCopy(convertedData, i, slice, 0, len);

            //From kleinsten zum Größten
            byte[] padded = RsaHelpersPadding.PadBlock(slice, blockCipher);
            BigInteger m = new BigInteger(padded.Reverse().Append((byte)0).ToArray());

            BigInteger c = BigInteger.ModPow(m, E, N);

            byte[] rawLE = c.ToByteArray();
            byte[] rawBE = rawLE.Reverse().ToArray();
            byte[] block = new byte[blockCipher];
            Buffer.BlockCopy(rawBE, 0, block, blockCipher - rawBE.Length, rawBE.Length);

            ms.Write(block);
        }

        return ms.ToArray();
    }



    private byte[] ConvertFile(string filePath)
    {
        string data = FileReader.ReadFile(filePath);

        return Encoding.UTF8.GetBytes("hi! This is a test string.");
    }
}
