using System;
using System.Collections;
using System.Diagnostics;

namespace Pellared.Utils
{
    public static class BitUtils
    {
        public static byte DecimalToBcd(int dec)
        {
            Debug.Assert(dec < 100, "Number is above 99");
            return (byte)(((dec / 10) << 4) + (dec % 10));
        }

        public static int BcdToDecimal(byte bcd)
        {
            Debug.Assert((bcd >> 4) < 10, "High digit is above 9");
            Debug.Assert((bcd % 16) < 10, "Low digit is above 9");
            return ((bcd >> 4) * 10) + bcd % 16;
        }

        public static int BcdToDecimal(byte[] bcd)
        {
            int result = 0;
            int exping = 1;
            foreach (var item in bcd)
            {
                result = (exping * result) + BcdToDecimal(item);
                exping *= 100;
            }

            return result;
        }

        /// <summary>
        /// Creates an array from a BitArray.
        /// </summary>
        /// <remarks>BitArray class: http://msdn.microsoft.com/en-us/library/system.collections.bitarray.aspx </remarks>
        /// <param name="bits">BitArray instance.</param>
        /// <returns>Array of bytes.</returns>
        public static byte[] ToByteArray(this BitArray bits, bool fromOldestBit)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    if (fromOldestBit)
                        bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));
                    else
                        bytes[byteIndex] |= (byte)(1 << bitIndex);

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }

        /// <summary>
        /// Get the bit valie in a byte.
        /// </summary>
        /// <param name="pByte">The byte where the value is encoded.</param>
        /// <param name="bitNo">The number of the bit (zero-based index).</param>
        /// <returns>Value of the bit.</returns>
        public static bool GetBit(this byte pByte, byte bitNo)
        {
            return (pByte & (1 << bitNo)) != 0;
        }

        /// <summary>
        /// Set the bit valie in a byte.
        /// </summary>
        /// <param name="pByte">The byte where the value is encoded.</param>
        /// <param name="bitNo">The number of the bit (zero-based index).</param>
        /// <param name="value">Value of the bit.</param>
        /// <returns>Byte with changed bit.</returns>
        public static byte SetBit(this byte pByte, byte bitNo, bool value)
        {
            byte result;
            if (value)
                result = Convert.ToByte(pByte | (1 << bitNo));
            else
                result = Convert.ToByte(pByte & ~(1 << bitNo));
            ////pByte = result;
            return result;
        }

        /// <summary>
        /// Decodes a value encoded in a byte.
        /// </summary>
        /// <param name="pByte">The byte where the value is encoded.</param>
        /// <param name="youngestBitNo">The number of the youngest bit (zero-based index).</param>
        /// <param name="oldestBitNo">The number of the olderst bit (zero-based index).</param>
        /// <returns>Value encoded in interval of the byte.</returns>
        public static byte GetValue(this byte pByte, byte youngestBitNo, byte oldestBitNo)
        {
            byte value = Convert.ToByte(pByte >> youngestBitNo);

            //byte mask = Convert.ToByte(Pow(2, oldestBitNo - youngestBitNo));
            int count = oldestBitNo - youngestBitNo + 1;
            byte mask = Convert.ToByte((1 << count) - 1);

            value &= mask;

            return value;
        }

        /// <summary>
        /// Get the bit valie in a byte.
        /// </summary>
        /// <param name="pByte">The byte where the value is encoded.</param>
        /// <param name="bitNo">The number of the bit (zero-based index).</param>
        /// <returns>Value of the bit.</returns>
        public static bool GetBit(this byte[] array, int bitNo)
        {
            var mask = 1 << (7 - (bitNo % 8));
            return (array[bitNo / 8] & mask) != 0;
        }

        /// <summary>
        /// Set the bit valie in a byte.
        /// </summary>
        /// <param name="pByte">The byte where the value is encoded.</param>
        /// <param name="bitNo">The number of the bit (zero-based index).</param>
        /// <param name="value">Value of the bit.</param>
        /// <returns>Byte with changed bit.</returns>
        public static void SetBit(this byte[] array, int bitNo, bool value)
        {
            var mask = 1 << (7 - (bitNo % 8));
            if (value)
                array[bitNo / 8] = Convert.ToByte(array[bitNo / 8] | mask);
            else
                array[bitNo / 8] = Convert.ToByte(array[bitNo / 8] & ~mask);
        }

        public static byte[] Cut(this byte[] array, int bitStart, int bitEnd)
        {
            int interval = bitEnd - bitStart + 1;
            byte[] result = new byte[((interval - 1) / 8) + 1];

            int sourceBitIndex = bitStart;
            int displacement = (interval % 8 == 0) ? 0 : 8 - interval % 8;
            for (int i = displacement; i < interval + displacement; i++)
            {
                bool value = array.GetBit(sourceBitIndex);
                result.SetBit(i, value);
                sourceBitIndex++;
            }

            return result;
        }

        public static int GetInteger(this byte[] array, int bitStart, int bitEnd)
        {
            byte[] masked = array.Cut(bitStart, bitEnd);
            int result = BitsToInt32(masked);
            return result;
        }

        private static int BitsToInt32(byte[] masked)
        {
            var array = new byte[4];
            for (int i = 0; i < masked.Length; i++)
            {
                array[i] = masked[masked.Length - i - 1];
            }

            int result = BitConverter.ToInt32(array, 0);
            return result;
        }
    }
}