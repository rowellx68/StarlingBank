using StarlingBank.Entities.Common;

namespace StarlingBank.Entities.SavingsGoals
{
    public class SavingsGoalRequest
    {
        public string Name { get; set; }

        public string Currency { get; set; }

        public CurrencyAndAmount Target { get; set; }

        public string Base64EncodedPhoto { get; set; }
    }
}