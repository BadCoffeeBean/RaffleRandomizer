using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RaffleRandomizer.Core
{
	/// <summary>
	/// Native cryptographically-secure randomizer. Uses <seealso cref="RandomNumberGenerator"/> as randomization provider.
	/// </summary>
	/// <typeparam name="T">The list's object type.</typeparam>
	public class RNGCSPRandomizer<T> : IRandomizer<T>
	{
		public IEnumerable<T> Randomize(IEnumerable<T> list)
		{
			if (list is null || !list.Any()) throw new ArgumentException("List of participants is empty.");
			return getPosition(list);
			
		}

		private T[] getPosition(IEnumerable<T> list)
		{
			var randomList = new T[list.Count()];
			bool[] reference = new bool[list.Count()];

			foreach (var item in list)
			{
				int position = RandomNumberGenerator.GetInt32(0, list.Count());

				while (reference[position])
				{
					position = RandomNumberGenerator.GetInt32(0, list.Count());
				}

				randomList[position] = item;
				reference[position] = true;
			}

			return randomList;
		}
	}
}
