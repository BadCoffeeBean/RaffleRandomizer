using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaffleRandomizer.Core
{
	public interface IDataService
	{
		public Participant GetParticipant(Participant searchCriteria);

		public IEnumerable<Participant> GetParticipants(Participant searchCriteria);

		public Participant GetParticipantById(long id);
		
		public Participant GetParticipantByEmployeeId(long employeeId);

		public IEnumerable<Participant> GetParticipantsByName(string firstName, string middleName, string lastName);

		public IEnumerable<Participant> GetParticipantsByRaffleEligibility(bool? grandPrizeEligible, bool? majorPrizeEligible, bool? minorPrizeEligible);

		public IEnumerable<Participant> GetParticipantsByDate(DateTime hireDate, DateTime resignedDate);

		public void UpdateParticipant(Participant item);

		public void UpdateParticipants(IEnumerable<Participant> items);
	}
}
