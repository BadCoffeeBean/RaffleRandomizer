using System;
using System.Collections.Generic;
using System.Linq;

namespace RaffleRandomizer.Core
{
	/// <summary>
	/// Default randomizer. Uses System.Random as randomization provider.
	/// </summary>
	/// <typeparam name="T">The list's object type.</typeparam>
	public class DefaultRandomizer<T> : IRandomizer<T>
	{
		public IEnumerable<T> Randomize(IEnumerable<T> list)
		{
			if (list is null || list.Count() < 1) throw new ArgumentException("List of participants is empty.");
			var random = new Random();
			return list.OrderBy(l => random.NextDouble());
		}
	}
}
