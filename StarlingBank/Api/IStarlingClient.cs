using System;
using System.Net;
using System.Threading.Tasks;
using StarlingBank.Entities.SavingsGoals;

namespace StarlingBank.Api
{
    public interface IStarlingClient
    {
        string BaseUrl { get; set; }

        string AccessToken { get; set; }
        
        // SavingsGoal

        Task<(HttpStatusCode status, CreateOrUpdateSavingsGoalResponse response)> CreateGoal(SavingsGoalRequest goal);

        Task<(HttpStatusCode status, SavingsGoal goal)> GetGoal(Guid goalId);

        Task<(HttpStatusCode status, CreateOrUpdateSavingsGoalResponse response)> AddMoney(Guid goalId, TopUpRequest topup);
    }
}