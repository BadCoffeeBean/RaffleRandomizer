using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
		private readonly IDataService _dataService;
		private readonly RaffleContext _database;

		public RaffleController(
			ILogger<RaffleController> logger,
			IRaffleService raffleService,
			IDataService dataService,
			RaffleContext database)
		{
			_logger = logger;
			_raffleService = raffleService;
			_dataService = dataService;
			_database = database;
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
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Provides a list of winners from a pre-configured database.
		/// </summary>
		/// <param name="count"></param>
		/// <param name="prizeType"></param>
		/// <returns></returns>
		[HttpGet("winners/db")]
		public IActionResult GetWinnersFromDatabase(
			[FromQuery, Required] int count,
			[FromQuery, Required] string prizeType)
		{
			try
			{
				switch (prizeType.ToLowerInvariant())
				{ 
					case "grand":
						return new ObjectResult(_raffleService.GenerateWinners(count, _dataService.GetParticipantsByRaffleEligibility(true, null, null)));
					case "major":
						return new ObjectResult(_raffleService.GenerateWinners(count, _dataService.GetParticipantsByRaffleEligibility(null, true, null)));
					case "minor":
						return new ObjectResult(_raffleService.GenerateWinners(count, _dataService.GetParticipantsByRaffleEligibility(null, null, true)));
					default:
						return BadRequest("Invalid Prize Type. Valid types are: \"grand\", \"major\", and \"minor\".");
				}
			}

			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Provides a list of winners from the participants of a Microsoft Teams meeting. (WIP)
		/// </summary>
		/// <param name="id"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[HttpGet("winners/teams/meeting")]
		public IActionResult GetWinnersFromTeamsMeeting(
			[FromQuery, Required] string meetingId,
			[FromQuery, Required] int count)
		{
			try
			{
				return BadRequest("DEMO");
			}

			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Provides a list of winners from the participants of a Microsoft Teams live event. (WIP)
		/// </summary>
		/// <param name="id"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[HttpGet("winners/teams/event")]
		public IActionResult GetWinnersFromTeamsLiveEvent(
			[FromQuery, Required] string eventId,
			[FromQuery, Required] int count)
		{
			try
			{
				return BadRequest("DEMO");
			}

			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}
	}
}
