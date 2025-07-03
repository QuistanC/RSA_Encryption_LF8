using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSA_Encryption.Utilities
{
    internal class RsaHelpersPadding
    {
        internal static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        // --- PKCS#1 v1.5 padding (ENCRYPT side) -------------------------------
        internal static byte[] PadBlock(byte[] messageSlice, int blockLen)
        {
            int psLen = blockLen - messageSlice.Length - 3;   // 0x00 0x02 PS 0x00
            if (psLen < 8) throw new Exception("message slice too long");

            byte[] ps = new byte[psLen];
            RsaHelpersPadding.Rng.GetBytes(ps);
            for (int i = 0; i < ps.Length; i++)               // PS must be non‑zero
                if (ps[i] == 0) ps[i] = 1;

            using var ms = new MemoryStream(blockLen);
            ms.WriteByte(0x00);
            ms.WriteByte(0x02);
            ms.Write(ps);           // random non‑zero padding
            ms.WriteByte(0x00);
            ms.Write(messageSlice);
            return ms.ToArray();    // big‑endian, blockLen bytes
        }

        // --- PKCS#1 v1.5 unpadding (DECRYPT side) -----------------------------
        internal static byte[] UnpadBlock(byte[] block)
        {
            if (block.Length < 11 || block[0] != 0x00 || block[1] != 0x02)
                throw new Exception("Bad padding header");

            int index = 2;
            while (index < block.Length && block[index] != 0x00)
                index++;

            if (index == block.Length)
                throw new Exception("Padding separator not found");

            return block[(index + 1)..]; // message bytes after padding
        }
    }
}
