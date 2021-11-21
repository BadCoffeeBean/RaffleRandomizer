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
		[HttpPost("winners")]
		public IActionResult GetWinnersFromUserList(
			[FromQuery, Required] int count,
			[FromQuery] bool? randomizeList,
			[FromBody, Required] IEnumerable<object> list)
		{
			try
			{
				return new ObjectResult(_raffleService.GenerateWinners(count, list, randomizeList ?? false));
			}

			catch (Exception e)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
			}
		}

		/// <summary>
		/// Provides a list of winners from a pre-configured database.
		/// </summary>
		[HttpGet("winners/db")]
		public IActionResult GetWinnersFromDatabase(
			[FromQuery, Required] int count,
			[FromQuery, Required] string prizeType,
			[FromQuery] bool? randomizeList,
			[FromQuery] bool? allowMultipleChances)
		{
			try
			{
				switch (prizeType.ToLowerInvariant())
				{ 
					case "grand":
						var winnersGrand = _raffleService.GenerateWinners(
							count,
							_dataService.GetParticipantsByRaffleEligibility(true, true, null),
							randomizeList ?? false);

						if (!allowMultipleChances.GetValueOrDefault())
						{
							foreach (var item in winnersGrand)
							{
								(item as Participant).GrandPrizeEligible = false;
								(item as Participant).LastUpdateUtc = DateTime.UtcNow;
								_dataService.UpdateParticipant(item as Participant);
							}
						}

						return new ObjectResult(winnersGrand);
					case "major":
						var winnersMajor = _raffleService.GenerateWinners(
							count,
							_dataService.GetParticipantsByRaffleEligibility(true, true, null),
							randomizeList ?? false);

						if (!allowMultipleChances.GetValueOrDefault())
						{
							foreach (var item in winnersMajor)
							{
								(item as Participant).MajorPrizeEligible = false;
								(item as Participant).LastUpdateUtc = DateTime.UtcNow;
								_dataService.UpdateParticipant(item as Participant);
							}
						}

						return new ObjectResult(winnersMajor);
					case "minor":
						var winnersMinor = _raffleService.GenerateWinners(
							count,
							_dataService.GetParticipantsByRaffleEligibility(null, null, true),
							randomizeList ?? false);

						if (!allowMultipleChances.GetValueOrDefault())
						{
							foreach (var item in winnersMinor)
							{
								(item as Participant).MinorPrizeEligible = false;
								(item as Participant).LastUpdateUtc = DateTime.UtcNow;
								_dataService.UpdateParticipant(item as Participant);
							}
						}

						return new ObjectResult(winnersMinor);
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
			[FromQuery, Required] int count,
			[FromQuery] bool? randomizeList)
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
			[FromQuery, Required] int count,
			[FromQuery] bool? randomizeList)
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
