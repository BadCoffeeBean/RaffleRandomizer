using System.Collections.Generic;

namespace RaffleRandomizer.Core
{
	public interface IPicker<T>
	{
		IEnumerable<T> Pick(int count, IEnumerable<T> list);
	}
}
