using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RaffleRandomizer.Core
{
	/// <summary>
	///Native cryptographically-secure random picker. Uses <seealso cref="RandomNumberGenerator"/> as randomization provider.
	/// </summary>
	/// <typeparam name="T">The list's object type.</typeparam>
	public class RNGCSPPicker<T> : IPicker<T>
	{
		public IEnumerable<T> Pick(int count, IEnumerable<T> list)
		{
			if (list is null || !list.Any()) throw new ArgumentException("List of participants is empty.");
			if (count < 1 || count > list.Count()) throw new ArgumentException($"Winners should be between 1 and {list.Count()}.");
			return randomPick(count, list);
		}

		private T[] randomPick(int count, IEnumerable<T> list)
		{
			var randomizedParticipants = list.ToArray();
			var chosenOnes = new T[count];
			bool[] reference = new bool[list.Count()];

			for (int x = 0; x < count; x++)
			{
				int position = RandomNumberGenerator.GetInt32(0, list.Count());

				while (reference[position])
				{
					position = RandomNumberGenerator.GetInt32(0, list.Count());
				}

				chosenOnes[x] = randomizedParticipants[position];
				reference[position] = true;
			}

			return chosenOnes;
		}
	}
}
