using System;
using System.Collections.Generic;
using System.Linq;

namespace RaffleRandomizer.Core
{
	/// <summary>
	/// Default picker. Takes n items from the list.
	/// </summary>
	/// <typeparam name="T">The list's object type.</typeparam>
	public class DefaultPicker<T> : IPicker<T>
	{
		public IEnumerable<T> Pick(int count, IEnumerable<T> list)
		{
			if (list is null || !list.Any()) throw new ArgumentException("List of participants is empty.");
			if (count < 1 || count > list.Count()) throw new ArgumentException($"Winners should be between 1 and {list.Count()}.");
			return list.Take(count);
		}
	}
}
