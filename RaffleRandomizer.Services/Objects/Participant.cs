using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RaffleRandomizer.Core
{
    public partial class Participant
    {
        [Key]
        public long ParticipantId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime? LastUpdateUtc { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
        public string Product { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? ResignDate { get; set; }
        public bool? GrandPrizeEligible { get; set; }
        public bool? MajorPrizeEligible { get; set; }
        public bool? MinorPrizeEligible { get; set; }
    }
}
