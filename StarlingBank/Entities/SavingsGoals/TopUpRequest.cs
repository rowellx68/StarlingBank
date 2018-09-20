using StarlingBank.Entities.Common;

namespace StarlingBank.Entities.SavingsGoals
{
    public class TopUpRequest
    {
        public CurrencyAndAmount Amount { get; set; }
    }
}