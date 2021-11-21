using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaffleRandomizer.Core
{
	public class SQLServerDataService : IDataService
	{
		RaffleContext _database = null;

		public SQLServerDataService(RaffleContext context)
		{
			_database = context;
		}

		public Participant GetParticipant(Participant searchCriteria)
		{
			return GetParticipants(searchCriteria).SingleOrDefault();
		}

		public Participant GetParticipantByEmployeeId(long employeeId)
		{
			return _database.Participants.SingleOrDefault(p => p.EmployeeId == employeeId);
		}

		public Participant GetParticipantById(long id)
		{
			return _database.Participants.SingleOrDefault(p => p.ParticipantId == id);
		}

		public IEnumerable<Participant> GetParticipants(Participant searchCriteria)
		{
			if (searchCriteria == null) return default;

			// Reference: http://www.albahari.com/nutshell/predicatebuilder.aspx | https://www.entityframeworktutorial.net/efcore/querying-in-ef-core.aspx
			var predicate = PredicateBuilder.New<Participant>();

			if (searchCriteria.ParticipantId > 0) predicate = predicate.And(p => p.ParticipantId == searchCriteria.ParticipantId);
			if (searchCriteria.EmployeeId > 0) predicate = predicate.And(p => p.EmployeeId == searchCriteria.EmployeeId);
			if (searchCriteria.CreatedUtc != DateTime.MinValue) predicate = predicate.And(p => p.CreatedUtc == searchCriteria.CreatedUtc);
			if (searchCriteria.LastUpdateUtc != default && searchCriteria.LastUpdateUtc != DateTime.MinValue) predicate = predicate.And(p => p.LastUpdateUtc == searchCriteria.LastUpdateUtc);
			if (!string.IsNullOrWhiteSpace(searchCriteria.FirstName)) predicate = predicate.And(p => p.FirstName == searchCriteria.FirstName);
			if (!string.IsNullOrWhiteSpace(searchCriteria.MiddleName)) predicate = predicate.And(p => p.MiddleName == searchCriteria.MiddleName);
			if (!string.IsNullOrWhiteSpace(searchCriteria.LastName)) predicate = predicate.And(p => p.LastName == searchCriteria.LastName);
			if (!string.IsNullOrWhiteSpace(searchCriteria.Team)) predicate = predicate.And(p => p.Team == searchCriteria.Team);
			if (!string.IsNullOrWhiteSpace(searchCriteria.Product)) predicate = predicate.And(p => p.Product == searchCriteria.Product);
			if (searchCriteria.HireDate != default && searchCriteria.HireDate != DateTime.MinValue) predicate = predicate.And(p => p.HireDate == searchCriteria.HireDate);
			if (searchCriteria.ResignDate != default && searchCriteria.ResignDate != DateTime.MinValue) predicate = predicate.And(p => p.ResignDate == searchCriteria.ResignDate);
			if (searchCriteria.GrandPrizeEligible != null) predicate = predicate.And(p => p.GrandPrizeEligible == searchCriteria.GrandPrizeEligible);
			if (searchCriteria.MajorPrizeEligible != null) predicate = predicate.And(p => p.MajorPrizeEligible == searchCriteria.MajorPrizeEligible);
			if (searchCriteria.MinorPrizeEligible != null) predicate = predicate.And(p => p.MinorPrizeEligible == searchCriteria.MinorPrizeEligible);

			return _database.Participants.AsExpandable().Where(predicate).ToList();
		}

		public IEnumerable<Participant> GetParticipantsByDate(DateTime hireDate, DateTime resignedDate)
		{
			if (hireDate == DateTime.MinValue && resignedDate == DateTime.MinValue) return default;

			var predicate = PredicateBuilder.New<Participant>();

			if (hireDate > DateTime.MinValue) predicate = predicate.And(p => p.HireDate >= hireDate);
			if (resignedDate > DateTime.MinValue) predicate = predicate.And(p => p.ResignDate <= resignedDate);

			return _database.Participants.AsExpandable().Where(predicate).ToList();
		}

		public IEnumerable<Participant> GetParticipantsByName(string firstName, string middleName, string lastName)
		{
			if (firstName == null && middleName == null && lastName == null) return default;

			var predicate = PredicateBuilder.New<Participant>();

			if (!string.IsNullOrWhiteSpace(firstName)) predicate = predicate.And(p => p.FirstName == firstName);
			if (!string.IsNullOrWhiteSpace(middleName)) predicate = predicate.And(p => p.MiddleName == middleName);
			if (!string.IsNullOrWhiteSpace(lastName)) predicate = predicate.And(p => p.LastName == lastName);

			return _database.Participants.AsExpandable().Where(predicate).ToList();
		}

		public IEnumerable<Participant> GetParticipantsByRaffleEligibility(bool? grandPrizeEligible, bool? majorPrizeEligible, bool? minorPrizeEligible)
		{
			if (grandPrizeEligible == null && majorPrizeEligible == null && minorPrizeEligible == null) return default;

			var predicate = PredicateBuilder.New<Participant>();

			if (grandPrizeEligible != null) predicate = predicate = predicate.And(p => p.GrandPrizeEligible == grandPrizeEligible);
			if (majorPrizeEligible != null) predicate = predicate = predicate.And(p => p.MajorPrizeEligible == majorPrizeEligible);
			if (minorPrizeEligible != null) predicate = predicate = predicate.And(p => p.MinorPrizeEligible == minorPrizeEligible);

			return _database.Participants.AsExpandable().Where(predicate).ToList();
		}

		public void UpdateParticipant(Participant item)
		{
			_database.Entry(item).State = EntityState.Modified;
			_database.SaveChanges();
		}
	}
}
