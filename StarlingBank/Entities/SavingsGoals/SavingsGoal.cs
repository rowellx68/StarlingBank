using System;
using StarlingBank.Entities.Common;

namespace StarlingBank.Entities.SavingsGoals
{
    public class SavingsGoal
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public CurrencyAndAmount TotalSaved { get; set; }
    }
}