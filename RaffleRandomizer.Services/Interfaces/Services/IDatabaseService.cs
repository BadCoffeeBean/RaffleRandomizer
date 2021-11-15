﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaffleRandomizer.Core
{
	public interface IDatabaseService
	{
		public Participant GetParticipant(Participant searchCriteria);

		public IEnumerable<Participant> GetParticipants(Participant searchCriteria);

		public Participant GetParticipantById(long id);
		
		public Participant GetParticipantByEmployeeId(long employeeId);

		public IEnumerable<Participant> GetParticipantsByName(string firstName, string middleName, string lastName);

		public IEnumerable<Participant> GetParticipantsByRaffleEligibility(bool? grandPrizeEligible, bool? majorPrizeEligible, bool? minorPrizeEligible);

		public IEnumerable<Participant> GetParticipantsByDate(DateTime hireDate, DateTime resignedDate);
	}
}
