using System;
using Excessives.LinqE;

namespace Excessives.BitWisE {
	static class BitWisE {
		#region Bit Play

		//Good for packing 8 bools (8 bytes) into 1
		public static byte BoolArrayToSingleBinaryByte(bool[] boolArray) {
			if (boolArray.Length == 8)
				return (byte)(
					(boolArray[0] ? 1 : 0) << 7 |
					(boolArray[1] ? 1 : 0) << 6 |
					(boolArray[2] ? 1 : 0) << 5 |
					(boolArray[3] ? 1 : 0) << 4 |
					(boolArray[4] ? 1 : 0) << 3 |
					(boolArray[5] ? 1 : 0) << 2 |
					(boolArray[6] ? 1 : 0) << 1 |
					(boolArray[7] ? 1 : 0)
				);

			byte rtnByte = 0;

			for (int i = 0; i < boolArray.Length; i++) {
				if (i >= 8)
					break;

				rtnByte = (byte)(
					rtnByte | ((boolArray[i] ? 1 : 0) << (7 - i))
					);
			}

			return rtnByte;
		}

		//Unpacks one byte back into an array of 8 bools
		public static bool[] SingleBinaryByteToBool(byte binary) {
			return new bool[] {
				(binary & 1) > 0,
				(binary & 2) > 0,
				(binary & 4) > 0,
				(binary & 8) > 0,
				(binary & 16) > 0,
				(binary & 32) > 0,
				(binary & 64) > 0,
				(binary & 128) > 0
			};
		}

		//Will return a string showing all the byte values for the byte sent in
		public static string BinaryToString(byte _byte) {
			string byteString = "";

			byteString += ((_byte & 128) > 0) ? "1" : "0";
			byteString += ((_byte & 64) > 0) ? "1" : "0";
			byteString += ((_byte & 32) > 0) ? "1" : "0";
			byteString += ((_byte & 16) > 0) ? "1" : "0";
			byteString += ((_byte & 8) > 0) ? "1" : "0";
			byteString += ((_byte & 4) > 0) ? "1" : "0";
			byteString += ((_byte & 2) > 0) ? "1" : "0";
			byteString += ((_byte & 1) > 0) ? "1" : "0";


			return byteString;
		}

		//Same as previous, just allows for entire arrays to be processed in one go
		public static string BinaryToString(byte[] bytes) {
			string byteString = "";

			//Quite a handy extension method I'd say
			bytes.ForEachBack(n => byteString += BinaryToString(n));

			return byteString;
		}

		public static byte StringToBinary(string bitString) {
			byte returnbyte = 0;

			for (byte i = 0; i < 8; i++) {
				if (bitString[i] == '1') {
					returnbyte =
						(byte)(
						returnbyte |
						(128 >> i)
						);
				}
			}

			return returnbyte;
		}

		//Get a bit at any point
		public static bool GetBit(byte bitList, int position) {
			return (bitList & (1 << position)) > 0;
		}

		//Get a bit at any point
		public static bool GetBit(byte[] bitList, ulong position) {
			return
				GetBit(
				bitList[(int)Math.Floor((decimal)position / 8)],
				(int)position % 8
			);
		}

		#endregion

		#region Operations

		#region Crossover

		/// <summary>
		/// Applies byte1 to byte2 using a mask
		/// </summary>
		public static byte Crossover(byte byte1, byte byte2, byte mask) {
			return (byte)(
				(byte1 & mask)
				|
				(byte2 & ~mask)
				);
		}

		public static byte[] Crossover
			(byte[] byte1, byte[] byte2, byte[] mask) {
			byte1.For(
				(n, i) =>
					byte1[i] = Crossover(byte1[i], byte2[i], mask[i])
				);

			return byte1;
		}

		#endregion

		public static byte[] Add(byte[] a, byte[] b) {
			if (a.LongLength != b.LongLength)
				return default(byte[]);

			byte[] added = new byte[a.LongLength];
			return (byte[])added.For((n, i) => added[i] = (byte)(a[i] + b[i]));
		}

		#endregion

		#region Byte Conversion Extension Methods

		#region ToBytes
		public static byte[] ToBytes(this string v) {
			char[] cArray = v.ToCharArray();

			byte[] data = new byte[cArray.LongLength * 2];

			for (int i = 0; i < cArray.Length; i++) {
				Array.Copy(
					BitConverter.GetBytes(cArray[i]), 0,
					data, i * 2,
					2
					);
			}

			//v.For((n, i) => data[i] = BitConverter.GetBytes(n)[0]);


			return data;
		}
		public static byte[] ToBytes(this byte v) { return BitConverter.GetBytes(v); }
		public static byte[] ToBytes(this sbyte v) { return BitConverter.GetBytes(v); }
		public static byte[] ToBytes(this char v) { return BitConverter.GetBytes(v); }
		public static byte[] ToBytes(this ushort v) { return BitConverter.GetBytes(v); }
		public static byte[] ToBytes(this short v) { return BitConverter.GetBytes(v); }
		public static byte[] ToBytes(this uint v) { return BitConverter.GetBytes(v); }
		public static byte[] ToBytes(this int v) { return BitConverter.GetBytes(v); }
		public static byte[] ToBytes(this ulong v) { return BitConverter.GetBytes(v); }
		public static byte[] ToBytes(this long v) { return BitConverter.GetBytes(v); }
		public static byte[] ToBytes(this float v) { return BitConverter.GetBytes(v); }
		public static byte[] ToBytes(this double v) { return BitConverter.GetBytes(v); }

		#endregion

		#region FromBytes

		public static string DecStr(this byte[] v) {
			string str = "";
			for (int i = 0; i < v.Length; i += 2) {
				str += BitConverter.ToChar(v, i);
			}
			return str;
		}
		public static string DecHex(this byte[] v) {
			return BitConverter.ToString(v);
		}
		public static char DecChar(this byte[] v) { return BitConverter.ToChar(v, 0); }
		public static ushort DecUShort(this byte[] v) { return BitConverter.ToUInt16(v, 0); }
		public static short DecShort(this byte[] v) { return BitConverter.ToInt16(v, 0); }
		public static uint DecUInt(this byte[] v) { return BitConverter.ToUInt32(v, 0); }
		public static int DecInt(this byte[] v) { return BitConverter.ToInt32(v, 0); }
		public static ulong DecULong(this byte[] v) { return BitConverter.ToUInt64(v, 0); }
		public static long DecLong(this byte[] v) { return BitConverter.ToInt64(v, 0); }
		public static float DecSingle(this byte[] v) { return BitConverter.ToSingle(v, 0); }
		public static double DecDouble(this byte[] v) { return BitConverter.ToDouble(v, 0); }

		#endregion

		#endregion
	}
}
