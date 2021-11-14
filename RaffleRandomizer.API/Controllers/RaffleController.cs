using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RaffleRandomizer.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

		/// <summary>
		/// (DEMO) Provides a list of winners from a user-submitted list.
		/// </summary>
		/// <param name="count"></param>
		/// <param name="list"></param>
		/// <returns></returns>
		[HttpPost("winners")]
		public IActionResult GetWinnersFromUserList(
			[FromQuery, Required] int count,
			[FromBody, Required] IEnumerable<object> list)
		{
			try
			{
				return new ObjectResult(_raffleService.GenerateWinners(count, list));
			}

			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// Provides a list of winners from a pre-configured database. (WIP)
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		[HttpPost("winners/db")]
		public IActionResult GetWinnersFromDatabase(
			[FromQuery, Required] int count)
		{
			try
			{
				return new NotFoundResult();
			}

			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// Provides a list of winners from the participants of a Microsoft Teams meeting. (WIP)
		/// </summary>
		/// <param name="id"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[HttpPost("winners/teams/meeting")]
		public IActionResult GetWinnersFromTeamsMeeting(
			[FromQuery, Required] string meetingId,
			[FromQuery, Required] int count)
		{
			try
			{
				return new NotFoundResult();
			}

			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// <summary>
		/// Provides a list of winners from the participants of a Microsoft Teams live event. (WIP)
		/// </summary>
		/// <param name="id"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[HttpPost("winners/teams/event")]
		public IActionResult GetWinnersFromTeamsLiveEvent(
			[FromQuery, Required] string eventId,
			[FromQuery, Required] int count)
		{
			try
			{
				return new NotFoundResult();
			}

			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
