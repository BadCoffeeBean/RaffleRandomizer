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

			_randomizer = config.GetValue<string>("RandomizerMode") switch
			{
				"RNGCSP" => new RNGCSPRandomizer<object>(),
				_ => new DefaultRandomizer<object>()
			};

			_picker = config.GetValue<string>("PickMode") switch
			{
				"RNGCSP" => new RNGCSPPicker<object>(),
				_ => new DefaultPicker<object>()
			};
		}
		
		public IEnumerable<object> GenerateWinners(int count, IEnumerable<object> list)
		{
			return _picker.Pick(count, _randomizer.Randomize(list));
		}
	}
}
