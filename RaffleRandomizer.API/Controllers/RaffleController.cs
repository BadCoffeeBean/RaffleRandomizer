using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaffleRandomizer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaffleRandomizer.API.Controllers
{
	[ApiController]
	[Route("raffle")]
	public class RaffleController : ControllerBase
	{
		private readonly ILogger<RaffleController> _logger;
		private readonly IRaffleService _raffleService;

		public RaffleController(
			ILogger<RaffleController> logger,
			IRaffleService raffleService)
		{
			_logger = logger;
			_raffleService = raffleService;
		}

		[HttpPost("winners")]
		public IActionResult GetWinners(
			[FromQuery] int count,
			[FromBody] IEnumerable<object> list)
		{
			return new ObjectResult(_raffleService.GenerateWinners(count, list));
		}
	}
}
