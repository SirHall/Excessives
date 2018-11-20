using System;
using Excessives.LinqE;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Collections.Generic;
using Excessives.BitWisE;

namespace Excessives {
	static class MathE {
		#region Constants

		public const double GOLDENRATIO = 1.6180339887498948482;

		public const double PLASTICNUMBER = 1.32471795724474602596;

		public const double TAU = 6.283185307179586;

		public const double SPEEDOFLIGHT = 299792458;

		public const double PLANCKLENGTH = 1.61622938 * (10 * -35);

		public const double PLANCKTIME = 5.3911613 * (10 ^ -44);

		/// <summary>
		/// Note that this is in Kelvin
		/// </summary>
		public const double PLANCKTEMPERATURE = 1.41680833 * (10 ^ 32);

		public const double PLANCKMASS = 4.341 * (10 ^ -9);

		public const double GRAVITATIONALCONSTANT = 6.6740831 * (10 ^ -11);

		public const double PLANCKSCONSTANT = 6.62 * (10 ^ -34);

		/// <summary>
		/// One light year in metres
		/// </summary>
		public const double LIGHTYEAR = 9460730472580800;

		#endregion

		#region Clamp

		public static T Clamp<T>(T value, T max, T min) where T : System.IComparable<T> {
			return value.CompareTo(max) > 0 ?
				max : value.CompareTo(min) < 0 ?
				min : value;
		}

		#endregion

		#region Add Towards

		#region Overflow

		public static float AddTowardsOverFlow(
			float value1, float value2, float target) {
			return value1 + (value2 * Math.Sign(target - value1));
		}

		#endregion

		#region Obstructed

		public static float AddTowardsObstructed(
			float value1, float value2, float target) {
			float v = value1 + (value2 * Math.Sign(target - value1));

			return
				(value1 < target) //If we were firstly underneath the target
				?
				(//Underneath
				(v > target) //v is larger than the target
					?
					target //Clamp to target
					:
					v //Else return v
			)
				:
				(//Not firstly underneath
				(value1 > target)//If firstly above
					?
					(
					(v < target)//If smaller than target
						?
						target //Clamp to target
						:
						v //Else return v
				)
					:
					( //Not above and not below, must be exactly equal to target
					target
				)
			);

		}

		public static int AddTowardsObstructed(
			int value1, int value2, int target) {
			int v = value1 + (value2 * Math.Sign(target - value1));

			return
				(value1 < target) //If we were firstly underneath the target
				?
				(//Underneath
				(v > target) //v is larger than the target
					?
					target //Clamp to target
					:
					v //Else return v
			)
				:
				(//Not firstly underneath
				(value1 > target)//If firstly above
					?
					(
					(v < target)//If smaller than target
						?
						target //Clamp to target
						:
						v //Else return v
				)
					:
					( //Not above and not below, must be exactly equal to target
					target
				)
			);

		}

		public static double AddTowardsObstructed(
			double value1, double value2, double target) {
			double v = value1 + (value2 * Math.Sign(target - value1));

			return
				(value1 < target) //If we were firstly underneath the target
				?
				(//Underneath
				(v > target) //v is larger than the target
					?
					target //Clamp to target
					:
					v //Else return v
			)
				:
				(//Not firstly underneath
				(value1 > target)//If firstly above
					?
					(
					(v < target)//If smaller than target
						?
						target //Clamp to target
						:
						v //Else return v
				)
					:
					( //Not above and not below, must be exactly equal to target
					target
				)
			);

		}

		#endregion

		#region Rebound

		public static float AddTowardsRebound(
			float value1, float value2, float target) {
			float v = value1 + (value2 * Math.Sign(target - value1));

			float offcut = Math.Abs(v - value1);

			return
				(value1 < target) //If we were firstly underneath the target
				?
				(//Underneath
				(v > target) //v is larger than the target
					?
					target - offcut//Rebound from target
					:
					v //Else return v
			)
				:
				(//Not firstly underneath
				(value1 > target)//If firstly above
					?
					(
					(v < target)//If smaller than target
						?
						target + offcut//Rebound from target
						:
						v //Else return v
				)
					:
					( //Not above and not below, must be exactly equal to target
					target
				)
			);

		}

		public static int AddTowardsRebound(
			int value1, int value2, int target) {
			int v = value1 + (value2 * Math.Sign(target - value1));

			int offcut = Math.Abs(v - value1);

			return
				(value1 < target) //If we were firstly underneath the target
				?
				(//Underneath
				(v > target) //v is larger than the target
					?
					target - offcut//Rebound from target
					:
					v //Else return v
			)
				:
				(//Not firstly underneath
				(value1 > target)//If firstly above
					?
					(
					(v < target)//If smaller than target
						?
						target + offcut//Rebound from target
						:
						v //Else return v
				)
					:
					( //Not above and not below, must be exactly equal to target
					target
				)
			);

		}

		public static double AddTowardsRebound(
			double value1, double value2, double target) {
			double v = value1 + (value2 * Math.Sign(target - value1));

			double offcut = Math.Abs(v - value1);

			return
				(value1 < target) //If we were firstly underneath the target
				?
				(//Underneath
				(v > target) //v is larger than the target
					?
					target - offcut//Rebound from target
					:
					v //Else return v
			)
				:
				(//Not firstly underneath
				(value1 > target)//If firstly above
					?
					(
					(v < target)//If smaller than target
						?
						target + offcut//Rebound from target
						:
						v //Else return v
				)
					:
					( //Not above and not below, must be exactly equal to target
					target
				)
			);

		}


		#endregion

		#endregion

		#region ClampWrap

		//Useful for clamping angles
		////{TODO} Could probably just redo this using modulo
		//public static float ClampWrap(float value, float min, float max)
		//{
		//	if (value < min)
		//		return value + (max * ((value / max) + 1));
		//	if (value > max)
		//		return value - (max * (value / max));
		//	return value;
		//}

		public static float ClampWrap(float value, float min, float max) {
			value = ((value - min) % (max - min)) + min;

			if (value < min)
				value += max;

			return value;
		}

		public static double ClampWrap(double value, double min, double max) {
			value = ((value - min) % (max - min)) + min;

			if (value < min)
				value += max;

			return value;
		}

		public static int ClampWrap(int value, int min, int max) {
			value = ((value - min) % (max - min)) + min;

			if (value < min)
				value += max;

			return value;
		}

		#endregion

		#region ReMap

		public static float ReMap(
			float value,
			float inMin, float inMax,
			float outMin, float outMax
			) {
			return
				(value - inMin) / (outMin - inMin) * (outMax - inMax) + inMax;
		}

		public static double ReMap(
			double value,
			double inMin, double inMax,
			double outMin, double outMax
			) {
			return
				(value - inMin) / (outMin - inMin) * (outMax - inMax) + inMax;
		}

		public static float ReMapClamped(
			float value,
			float inMin, float inMax,
			float outMin, float outMax,
			bool clampIn = true, bool clampOut = true
			) {
			return
				MathE.Clamp(
					ReMap(
						MathE.Clamp(
							value,
							clampIn ? inMin : float.NegativeInfinity,
							clampIn ? inMax : float.PositiveInfinity
							),
						inMin, inMax,
						outMin, outMax
						),
					clampOut ? outMin : float.NegativeInfinity, clampOut ? outMax : float.PositiveInfinity
					);
		}

		public static double ReMapClamped(
			double value,
			double inMin, double inMax,
			double outMin, double outMax,
			bool clampIn = true, bool clampOut = true
			) {
			return
				MathE.Clamp(
					ReMap(
						MathE.Clamp(
							value,
							clampIn ? inMin : double.NegativeInfinity,
							clampIn ? inMax : double.PositiveInfinity
							),
						inMin, inMax,
						outMin, outMax
						),
					clampOut ? outMin : double.NegativeInfinity, clampOut ? outMax : double.PositiveInfinity
					);
		}


		#endregion

		#region Derivatives Of Position

		public static double PosFromDerivatives(
			double velocity, double acceleration,
			double jerk, double jounce,
			double time) {
			return //This is why I love the order of operations
				velocity * time
			+
			acceleration * time * time / 2
			+
			jerk * time * time * time / 6
			+
			jounce * time * time * time * time / 24;

		}

		#endregion

		#region Boolean to Num

		public static float ToFloat(this bool boolean) => boolean ? 1f : 0f;

		public static int ToInt(this bool boolean) => boolean ? 1 : 0;

		#endregion

		#region Rounding

		#region Double

		/// <summary>
		/// Rounds to the nearest multiple of 'n'.
		/// </summary>
		/// <param name="val">Value.</param>
		/// <param name="n">N.</param>
		public static double RoundToN(double val, double n)
			=> n * Math.Round(val / n);

		/// <summary>
		/// Rounds up to the nearest multiple of 'n'.
		/// </summary>
		/// <param name="val">Value.</param>
		/// <param name="n">N.</param>
		public static double CeilToN(double val, double n)
			=> n * Math.Ceiling(val / n);

		/// <summary>
		/// Rounds down to the nearest multiple of 'n'.
		/// </summary>
		/// <param name="val">Value.</param>
		/// <param name="n">N.</param>
		public static double FloorToN(double val, double n)
			=> n * Math.Floor(val / n);

		#endregion

		#region Float

		//{TODO} Rewrite these so they don't cast to double
		public static float Round(float val)
			=> (float)Math.Round((double)val);

		public static float Floor(float val)
			=> (float)Math.Floor((double)val);

		public static float Ceil(float val)
			=> (float)Math.Ceiling((double)(val));



		/// <summary>
		/// Rounds to the nearest multiple of 'n'.
		/// </summary>
		/// <param name="val">Value.</param>
		/// <param name="n">N.</param>
		public static float RoundToN(float val, float n)
			=> n * Round(val / n);

		/// <summary>
		/// Rounds up to the nearest multiple of 'n'.
		/// </summary>
		/// <param name="val">Value.</param>
		/// <param name="n">N.</param>
		public static float CeilToN(float val, float n)
			=> n * Ceil(val / n);

		/// <summary>
		/// Rounds down to the nearest multiple of 'n'.
		/// </summary>
		/// <param name="val">Value.</param>
		/// <param name="n">N.</param>
		public static float FloorToN(float val, float n)
			=> n * Floor(val / n);

		#endregion

		#endregion

		#region Lerps

		#region Double

		/// <summary>
		/// Lerp
		/// </summary>
		public static double Lerp(double a, double b, double t)
			=> ((b - a) * t) + a;


		/// <summary>
		/// Quadratic Lerp
		/// </summary>
		public static double QuadLerp(double a, double b, double t)
			=> ((b - a) * t * t) + a;

		/// <summary>
		/// A Sine Lerp
		/// </summary>
		public static double SineLerp(double a, double b, double t)
			=> ((b - a) * Math.Sin(t * Math.PI / 2)) + a;

		//Finds the 't' used to lerp between two numbers
		public static double UnLerp(double a, double b, double lerped) {
			//Lerp function: lerped = ((b-a) * t) + a
			//Solve for t
			//t = (lerped - a) / (b - a)
			return (lerped - a) / (b - a);
		}

		public static double UnQuadLerp(double a, double b, double lerped) {
			//Lerp function: lerped = ((b-a) * t * t) + a
			//Solve for t
			//t = Sqrt((lerped - a) / (b - a))
			return Math.Sqrt((lerped - a) / (b - a));
		}

		#endregion

		#region Float

		/// <summary>
		/// Lerp
		/// </summary>
		public static float Lerp(float a, float b, float t) {
			return ((b - a) * t) + a;
		}


		/// <summary>
		/// Quadratic Lerp
		/// </summary>
		public static float QuadLerp(float a, float b, float t) {
			return ((b - a) * t * t) + a;
		}

		/// <summary>
		/// A Sine Lerp
		/// </summary>
		public static float SineLerp(float a, float b, float t) {
			return ((b - a) * (float)Math.Sin((double)(t * Math.PI / 2))) + a;
		}

		//Finds the 't' used to lerp between two numbers
		public static float UnLerp(float a, float b, float lerped) {
			//Lerp function: lerped = ((b-a) * t) + a
			//Solve for t
			//t = (lerped - a) / (b - a)

			return (lerped - a) / (b - a);
		}

		public static float UnQuadLerp(float a, float b, float lerped) {
			//Lerp function: lerped = ((b-a) * t * t) + a
			//Solve for t
			//t = Sqrt((lerped - a) / (b - a))
			return (float)Math.Sqrt((double)(lerped - a) / (double)(b - a));
		}

		#endregion

		#endregion

		#region Arrays

		//public static T[] Merge<T>(this T[] array, params T[] addative)
		//{
		//    return array;
		//}

		#endregion

		#region Quick Convert

		/// <summary>
		/// Converts radians to degrees
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static float ToDeg(this float v) {
			return v * 180 / (float)Math.PI;
		}
		/// <summary>
		/// Converts radians to degrees
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static double ToDeg(this double v) {
			return v * 180 / Math.PI;
		}

		/// <summary>
		/// Converts degrees to radians
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static float ToRad(this float v) {
			return v * (float)Math.PI / 180;
		}
		/// <summary>
		/// Converts degrees to radians
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static double ToRad(this double v) {
			return v * Math.PI / 180;
		}

		#endregion
	}

	static class CryptoRand {
		static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

		/// <summary>
		/// Gets an array of random bytes.
		/// </summary>
		/// <returns>The byte array.</returns>
		/// <param name="byteCount">Length of byte array.</param>
		public static byte[] GetBytes(int byteCount) {
			byte[] randBytes = new byte[byteCount];

			rng.GetBytes(randBytes);

			return randBytes;
		}

		public static byte[] GetBytes(byte[] byteArray) {
			rng.GetBytes(byteArray);
			return byteArray;
		}

		/// <summary>
		/// Returns a random double from 0-1
		/// </summary>
		public static double Range() {
			byte[] bytes = new Byte[8];
			rng.GetBytes(bytes);
			//Testing new way to generate random numbers
			//double d = Math.Abs(bytes.DecDouble()) % 1.0;
			var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
			Double d = ul / (Double)(1UL << 53);
			return d;
		}

		public static double Range(double min, double max) {
			return ((max - min) * Range()) + min;
		}

		public static float Range(float min, float max) {
			return ((max - min) * (float)Range()) + min;
		}

		/// <summary>
		/// Picks a random element from the parameters passed.
		/// </summary>
		/// <param name="array">Array.</param>
		/// <typeparam name="T">The type parameter.</typeparam>
		public static T Pick<T>(params T[] array)
			=> array[(int)(array.Length * Range())];

		/// <summary>
		/// Has an n probability of returning a true
		/// </summary>
		/// <returns></returns>
		public static bool Chance(double n)
			=> Range(0, 1) > n;

	}

	/// <summary>
	/// XOR encyption.
	/// NOTE: This is not a cryptographically secure method of encryption,
	/// use at own risk!
	/// </summary>
	public static class CryptoSymmetric {

		public static byte[] Encrypt(byte[] data, byte[] key) {
			return XORArrayWithKey(data, EqualizeKey(data, key));
		}

		public static byte[] Decrypt(byte[] data, byte[] key) {
			return Encrypt(data, key);
		}

		static byte[] EqualizeKey(byte[] data, byte[] key) {
			//We need to repeat the key
			if (key.LongLength < data.LongLength) {
				long keyOriginalLength = key.LongLength;

				byte[] dest = new byte[data.Length];

				for (int i = 0; i < dest.Length; i++) {
					dest[i] = key[i % keyOriginalLength];
				}
				return dest;
			}

			//We need to cut the key down
			if (key.LongLength > data.LongLength) {
				byte[] dest = new byte[data.LongLength];
				Array.Copy(key, 0, dest, 0, data.Length);
				return dest;
			}

			return key;
		}

		static byte[] XORArrayWithKey(byte[] input, byte[] key) {
			byte[] XORd = new byte[input.LongLength];

			for (long i = 0; i < key.LongLength; i++) {
				XORd[i] = (byte)(input[i] ^ key[i]);
			}

			return XORd;
		}
	}

	static class StatementsE {
		/// <summary>
		/// Simply loops a given number of times
		/// </summary>
		/// <param name="cycles"></param>
		/// <param name="action"></param>
		public static void Repeat(ulong cycles, Action action) {
			if (cycles == 0) {
				return;
			}

			for (ulong i = 0; i < cycles; i++) {
				action();
			}
		}

		/// <summary>
		/// A 'foreach' with a 'for' backbone
		/// Look at source for example
		/// </summary>
		public static void ForEach<T>(T[] objects, Action<T> action) {
			for (int i = 0; i < objects.Length; i++) {
				action(objects[i]);
			}

			//Example
			/* ForEach(myArray,
			 * (arrayElement) =>{
			 *      //Do something with arrayElement
			 * });
			 */
		}

		/// <summary>
		/// Times how long the action took to complete and returns that time in milliseconds.
		/// </summary>
		public static long SpeedTest(Action action) {
			Stopwatch watch = Stopwatch.StartNew();
			action();
			watch.Stop();
			return watch.ElapsedMilliseconds;
		}

		/// <summary>
		/// Times how long the action took to complete and returns that time in milliseconds.
		/// </summary>
		public static long PerfTestInvoke(this Action action) {
			Stopwatch watch = Stopwatch.StartNew();
			action();
			watch.Stop();
			return watch.ElapsedMilliseconds;
		}

		public static void IfConditions(
			bool condition1, bool condition2,
			Action only1, Action only2,
			Action both = null, Action neither = null
			) {
			if (condition1 && !condition2 && only1 != null)
				only1();
			if (!condition1 && condition2 && only2 != null)
				only2();
			if (condition1 && condition2 && both != null)
				both();
			if (!condition1 && !condition2 && neither != null)
				neither();
		}

	}

	public class PID {
		public double kP { get; set; }

		public double kI { get; set; }

		public double kD { get; set; }

		public double P { get; set; }

		public double I { get; set; }

		public double D { get; set; }

		public PID(double kP, double kI, double kD) {
			this.kP = kP;
			this.kI = kI;
			this.kD = kD;
		}

		public double Step(double error) {
			I += error; //Integral
			D = P - error; //Derivative
			P = error; //Proportional

			//Return the resultant
			return (P * kP) + (I * kI) + (D * kD);
		}

	}

	public static class ColorHex {
		public static string
			Aqua = "#00ffffff", Black = "#000000ff", Brown = "#a52a2aff",
			DarkBlue = "#0000a0ff", Magenta = "#ff00ffff", Green = "#008000ff",
			Grey = "#808080ff", LightBlue = "#add8e6ff", Lime = "#00ff00ff",
			Maroon = "#800000ff", Navy = "#000080ff", Olive = "#808000ff",
			Orange = "#ffa500ff", Purple = "#800080ff", Red = "#ff0000ff",
			Silver = "#c0c0c0ff", Teal = "#008080ff", White = "#ffffffff",
			Yellow = "#ffff00ff";

	}

	static class ExtensionsE {

		#region Conditions

		public static bool IsNull<T>(this T instance) {
			return instance == null;
		}

		public static bool NotNull<T>(this T instance) {
			return instance != null;
		}


		public static bool IsNull<T>(this T instance, Action<T> actionIfNull) {
			if (instance == null) {
				if (actionIfNull != null)
					actionIfNull(instance);
				return true;
			}
			return false;
		}

		public static bool NotNull<T>(this T instance, Action<T> actionIfNotNull) {
			if (instance != null) {
				if (actionIfNotNull != null)
					actionIfNotNull(instance);
				return true;
			}
			return false;
		}

		#endregion

		#region Action/Func

		/// <summary>
		/// Invokes the action if it isn't null, if it is null then just ignores it.
		/// </summary>
		/// <param name="action">Action.</param>
		public static void InvokeNull(this Action action) {
			if (action != null) {
				action();
			}
		}

		/// <summary>
		/// Invokes the action if it isn't null, if it is null then invoke the 'ifNull' action
		/// </summary>
		/// <param name="action">Action.</param>
		/// <param name="ifNull">If null.</param>
		public static void InvokeNull(this Action action, Action ifNull) {
			if (action == null) {
				ifNull();
				return;
			} else {
				action();
			}
		}



		#endregion

		#region Console

		public static void Write<T>(this T message) {
			Console.Write(message);
		}
		public static void WriteLine<T>(this T message) {
			Console.WriteLine(message);
		}
		#endregion

		#region Debugging

		#region Printing Enumerables

		public static string ToElementsString<TSource>(
	   this IEnumerable<TSource> enumerable,
	   string splitter = ", ") {
			string str = "";

			using (var enumerator = enumerable.GetEnumerator()) {
				bool putFirst = false;

				while (enumerator.MoveNext()) {
					str +=
						(putFirst ? splitter : "") + //If I already put the first index, add in the splitter
						enumerator.Current.ToString();

					putFirst = true;
				}
			}

			return str;
		}

		public static void WriteArrayElements<TSource>(
	   this IEnumerable<TSource> enumerable,
	   string splitter = ", ") {
			Console.Write(ToElementsString(enumerable, splitter));
		}

		public static void WriteLineArrayElements<TSource>(
	   this IEnumerable<TSource> enumerable,
	   string splitter = ", ") {
			WriteArrayElements(enumerable, splitter);
		}

		#endregion

		#region Printing Dictionaries

		public static string ToElementsString<TSource1, TSource2>(
			this Dictionary<TSource1, TSource2> dict,
			string keyValueSeparator = " => ",
			string elementSeparator = "\n") {
			string returnString = "";

			var dictEnumerator = dict.GetEnumerator();

			while (dictEnumerator.MoveNext()) {
				returnString +=
					elementSeparator +
					dictEnumerator.Current.Key.ToString() +
					keyValueSeparator +
					dictEnumerator.Current.Value.ToString();
			}

			return returnString.Remove(0, elementSeparator.Length);
		}

		/// <summary>
		/// Writes elements to the console
		/// </summary>
		public static void WriteElements<TSource1, TSource2>(
			this Dictionary<TSource1, TSource2> dict,
			string keyValueSeparator = " => ",
			string elementSeparator = "\n") {
			ToElementsString(dict, keyValueSeparator, elementSeparator)
				.WriteLine();
		}
		#endregion

		#endregion
	}

	/// <summary>
	/// Used to store a variable type as a reference type.
	/// Or for some interesting scoping tricks ;)
	/// </summary>
	public class Ref<T> {

		T val;

		public Ref(T value) {
			this.val = value;
		}

		public virtual T Value {
			get { return val; }
			set { val = value; }
		}
	}

	/// <summary>
	/// Can be used to setup a getter and setter of an external
	/// instance of a given object
	/// </summary>
	public class GetSet<T> {
		Func<T> getter;
		Action<T> setter;

		public GetSet
			(
			Func<T> getter,
			Action<T> setter
			) {
			this.getter = getter;
			this.setter = setter;
		}

		public T value {
			get { return getter(); }
			set { setter(value); }
		}
	}
}
