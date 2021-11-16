using System.Collections.Generic;

namespace RaffleRandomizer.Core
{
	public interface IRaffleService
	{
		IEnumerable<object> GenerateWinners(int count, IEnumerable<object> list, bool randomizeList);
	}
}
