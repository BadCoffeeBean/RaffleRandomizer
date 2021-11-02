using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaffleRandomizer.Core
{
	public class RaffleService : IRaffleService
	{
		IConfiguration _configuration = null;
		IRandomizer<object> _randomizer = null;
		IPicker<object> _picker = null;

		public RaffleService(IConfiguration config)
		{
			_configuration = config ?? throw new ArgumentNullException("config");
			_randomizer = config.GetValue<string>("RandomizerMode") == "Default" ? new DefaultRandomizer<object>() : default;
			_picker = config.GetValue<string>("PickMode") == "Default" ? new DefaultPicker<object>() : default;
		}
		
		public IEnumerable<object> GenerateWinners(int count, IEnumerable<object> list)
		{
			return _picker.Pick(count, _randomizer.Randomize(list));
		}
	}
}
