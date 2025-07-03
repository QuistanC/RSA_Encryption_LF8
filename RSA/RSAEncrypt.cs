
using RSA_Encryption.Utilities;
using System.Numerics;
using System.Text;

namespace RSA_Encryption.RSA;

internal class RSAEncrypt(BigInteger n, BigInteger e)
{
    private BigInteger N = n;
    private BigInteger E = e;
    internal byte[] Encrypt(string filePath)
    {
        byte[] convertedData = ConvertFile(filePath);
        using var fout = new FileStream("cipher.bin", FileMode.Create);
        int blockSize = ((int)N.GetBitLength() - 1) / 8;
        int rawTextByteSize = ((int)N.GetBitLength() + 7) / 8;
        using var ms = new MemoryStream();

        for (int i = 0; i < convertedData.Length; i += blockSize)
        {
            int len = Math.Min(blockSize, convertedData.Length - i);
            byte[] slice = new byte[len];
            Buffer.BlockCopy(convertedData, i, slice, 0, len);

            //From kleinsten zum Größten
            BigInteger m = new BigInteger(slice.Reverse()  
                                        .Append((byte)0)
                                        .ToArray());

            BigInteger c = BigInteger.ModPow(m, E, N);

            byte[] raw = c.ToByteArray();
            byte[] block = new byte[rawTextByteSize];
            Buffer.BlockCopy(raw, 0, block, rawTextByteSize - raw.Length, raw.Length);

            fout.Write(block);
        }

        byte[] cipherBytes = ms.ToArray();
        File.WriteAllBytes("ciper.bin", cipherBytes);
        return cipherBytes;
    }



    private byte[] ConvertFile(string filePath)
    {
        string data = FileReader.ReadFile(filePath);

        return Encoding.UTF8.GetBytes(data);
    }
}
