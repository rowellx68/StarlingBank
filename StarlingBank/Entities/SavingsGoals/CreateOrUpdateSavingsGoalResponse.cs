using System;
using System.Collections.Generic;
using StarlingBank.Entities.Common;

namespace StarlingBank.Entities.SavingsGoals
{
    public class CreateOrUpdateSavingsGoalResponse
    {
        public Guid SavingsGoalUid { get; set; }

        public bool Success { get; set; }

        public IEnumerable<ErrorDetail> Errors { get; set; }
    }
}