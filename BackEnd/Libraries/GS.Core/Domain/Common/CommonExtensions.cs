using System;
using System.Text;

namespace GS.Core.Domain.Common
{
    public static class CommonExtensions
    {
        public static byte[] toByteOfGUID(this string inputHex)
        {
            if (String.IsNullOrEmpty(inputHex))
                return null;
            //return Convert.FromBase64String(inputHex);
            byte[] Bytes = new byte[inputHex.Length / 2];
            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
       0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
       0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };

            for (int x = 0, i = 0; i < inputHex.Length; i += 2, x += 1)
            {
                Bytes[x] = (byte)(HexValue[Char.ToUpper(inputHex[i + 0]) - '0'] << 4 |
                                  HexValue[Char.ToUpper(inputHex[i + 1]) - '0']);
            }

            return Bytes;
        }
        public static string toStringOfGUID(this byte[] guid)
        {
            //return Convert.ToBase64String(guid);
            StringBuilder ret = new StringBuilder(guid.Length * 2);
            string HexAlphabet = "0123456789ABCDEF";

            foreach (byte B in guid)
            {
                ret.Append(HexAlphabet[(int)(B >> 4)]);
                ret.Append(HexAlphabet[(int)(B & 0xF)]);
            }

            return ret.ToString();
        }
    }
}
