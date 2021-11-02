using System.Collections.Generic;

namespace RaffleRandomizer.Core
{
	public interface IRandomizer<T>
	{
		IEnumerable<T> Randomize(IEnumerable<T> list);
	}
}
