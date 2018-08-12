using System.Collections.Generic;
using System.Linq;
using System;


namespace Excessives.LinqE
{
	/* {TODO}
     * Fix indentations
     * Comment obscure code (Fairly obvious)
     */

	public static class LinqE
	{
		#region Loops

		//Foreach, no return
		public static IEnumerable<TSource> ForEach<TSource>(
			this IEnumerable<TSource> enumerable,
			Action<TSource> action
		)
		{
			for (int i = 0; i < enumerable.Count(); i++)
			{
				action(enumerable.ElementAt(i));
			}
			return enumerable;
		}

		//For, no return
		public static IEnumerable<TSource> For<TSource>(
			this IEnumerable<TSource> enumerable,
			Action<TSource, int> action
		)
		{
			for (int i = 0; i < enumerable.Count(); i++)
			{
				action(enumerable.ElementAt(i), i);
			}
			return enumerable;
		}

		//Foreach, return
		public static IEnumerable<TSource> ForEachR<TSource>(
			this IEnumerable<TSource> enumerable,
			Func<TSource, TSource> action
		)
		{
			for (int i = 0; i < enumerable.Count(); i++)
			{
				enumerable.ToArray()[i] = action(enumerable.ElementAt(i));
			}
			return enumerable.AsEnumerable();
		}

		//For, return
		public static IEnumerable<TSource> ForR<TSource>(
			this IEnumerable<TSource> enumerable,
			Func<TSource, int, TSource> action
		)
		{
			for (int i = 0; i < enumerable.Count(); i++)
			{
				enumerable.ToArray()[i] = action(enumerable.ElementAt(i), i);
			}
			return enumerable.AsEnumerable();
		}

		#endregion

		#region Loops Backward

		//Foreach, no return
		public static IEnumerable<TSource> ForEachBack<TSource>(
			this IEnumerable<TSource> enumerable,
			Action<TSource> action
		)
		{
			for (int i = enumerable.Count() - 1; i >= 0; i--)
			{
				action(enumerable.ElementAt(i));
			}
			return enumerable;
		}

		//For, no return
		public static IEnumerable<TSource> ForBack<TSource>(
			this IEnumerable<TSource> enumerable,
			Action<TSource, int> action
		)
		{
			for (int i = enumerable.Count() - 1; i >= 0; i--)
			{
				action(enumerable.ElementAt(i), i);
			}
			return enumerable;
		}

		//Foreach, return
		public static IEnumerable<TSource> ForEachBack<TSource>(
			this IEnumerable<TSource> enumerable,
			Func<TSource, TSource> action
		)
		{
			for (int i = enumerable.Count() - 1; i >= 0; i--)
			{
				enumerable.ToArray()[i] = action(enumerable.ElementAt(i));
			}
			return enumerable.AsEnumerable();
		}

		//For, return
		public static IEnumerable<TSource> ForBack<TSource>(
			this IEnumerable<TSource> enumerable,
			Func<TSource, int, TSource> action
		)
		{
			for (int i = enumerable.Count() - 1; i >= 0; i--)
			{
				enumerable.ToArray()[i] = action(enumerable.ElementAt(i), i);
			}
			return enumerable.AsEnumerable();
		}

		#endregion

		#region Get Sub Array
		/// <summary>
		/// Creates a sub array from one passed to it
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="enumerable"></param>
		/// <param name="startIndex"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		public static IEnumerable<TSource> SubArray<TSource>(
			this IEnumerable<TSource> enumerable,
			int startIndex, int length
		)
		{
			TSource[] final = new TSource[length];

			Array.Copy(enumerable.ToArray(), startIndex, final, 0, length);

			return final.AsEnumerable();
		}

		public static IEnumerable<TSource> SubArraySmart<TSource>(
			this IEnumerable<TSource> enumerable,
			int startIndex, int cycles, int stepsize = 1
		)
		{
			TSource[] final = new TSource[cycles];

			int currentIndex = startIndex;

			for (int i = 0; i < cycles; i++)
			{
				//{TODO} Allow this to handle negative indexes
				currentIndex =
					MathE.ClampWrap(
						startIndex + (i * stepsize),
						0,
						enumerable.Count()
						);

				//(startIndex + (i * stepsize))
				//%
				//enumerable.Count(); //Allows our copies to wrap around the array

				final[i] = enumerable.ElementAt(currentIndex);
			}

			return final.AsEnumerable();
		}

		#endregion

		#region Min/Max

		public static TSource Minimum<TSource>(
			this IEnumerable<TSource> enumerable,
			Func<TSource, IComparable> selector
			) where TSource : IComparable<TSource>
		{
			TSource minimum = default(TSource);

			bool assignedMinimum = false; //Used so we don't have to use a 'default' value

			enumerable.ForEach(
				n =>
					{
						if (selector(n).CompareTo(selector(minimum)) < 0 || !assignedMinimum)
						{
							minimum = n;
							assignedMinimum = true;
						}
					}
				);

			return minimum;
		}

		public static TSource Maximum<TSource>(
			this IEnumerable<TSource> enumerable,
			Func<TSource, IComparable> selector
			) where TSource : IComparable<TSource>
		{
			TSource maximum = default(TSource);

			bool assignedMaximum = false; //Used so we don't have to use a 'default' value

			enumerable.ForEach(
				n =>
				{
					if (selector(n).CompareTo(selector(maximum)) > 0 || !assignedMaximum)
					{
						maximum = n;
						assignedMaximum = true;
					}
				}
				);

			return maximum;
		}

		#endregion

		#region Misc

		/// <summary>
		/// Returns the first element in an array that fits a given criteria
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="enumerable"></param>
		/// <param name="selector"></param>
		/// <returns></returns>
		public static TSource First<TSource>(
			this IEnumerable<TSource> enumerable,
			Func<TSource, bool> selector
			)
		{
			using (var enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
					if (selector(enumerator.Current))
						return enumerator.Current;
				return default(TSource);
			}
		}

		/// <summary>
		/// Selects the nth element in an enumerable
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="enumerable"></param>
		/// <param name="n"></param>
		/// <returns></returns>
		public static TSource Nth<TSource>(
			this IEnumerable<TSource> enumerable,
			int n
			)
		{
			using (var enumerator = enumerable.GetEnumerator())
			{
				int i = 0;
				while (enumerator.MoveNext())
				{
					if (i == n)
						return enumerator.Current;
					i++;
				}
				return default(TSource);
			}
		}

		/// <summary>
		/// Finds the index of a given object in an enumerable
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="enumerable"></param>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static int FindIndex<TSource>(
			this IEnumerable<TSource> enumerable,
			TSource instance
			)
		{
			int i = 0;
			var enumerator = enumerable.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Equals(instance))
					return i;
				i++;
			}

			return -1; //Could not find it in the array
		}

		public static IEnumerable<TSource> Swap<TSource>(
			this IEnumerable<TSource> enumerable,
			int index1, int index2
			)
		{
			//{TODO} Speed up! (This is the slowest 'swap' I have ever written...)
			TSource[] array = enumerable.ToArray(); //Yuck!

			TSource tmp;
			tmp = array[index1];
			array[index1] = array[index2];
			array[index2] = array[index1];

			return array.AsEnumerable(); //Yuck! V2.0
		}

		public static IEnumerable<TSource> SetAt<TSource>(
			this IEnumerable<TSource> enumerable,
			TSource value, int index
			)
		{
			if (index >= enumerable.Count())
				throw new Exception("IndexOutOfRangeException"); //{TODO} Finish this

			TSource[] array = enumerable.ToArray();

			array[index] = value;

			return array.AsEnumerable();
		}

		#endregion

		#region Random

		/// <summary>
		/// Randomly picks an element from an enumerable
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <param name="enumerable"></param>
		/// <returns></returns>
		public static TSource Pick<TSource>(
			this IEnumerable<TSource> enumerable
		)
		{
			return CryptoRand.Pick(enumerable.ToArray());
		}

		public static IEnumerable<TSource> Shuffle<TSource>(
			this IEnumerable<TSource> enumerable
			)
		{
			TSource[] newArray = new TSource[enumerable.Count()];

			for (int i = 0; i < newArray.Length; i++)
				newArray[i] =
					enumerable.Where(n => !newArray.Contains(n))
					.Pick();
			return newArray.AsEnumerable();
		}

		#endregion

	}
}
