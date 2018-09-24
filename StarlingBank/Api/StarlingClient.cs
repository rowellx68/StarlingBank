using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using StarlingBank.Entities.SavingsGoals;

namespace StarlingBank.Api
{
    public class StarlingClient : IStarlingClient, IDisposable
    {
        public StarlingClient()
        {
            this.RequestBuilder = new StarlingRequestBuilder();
        }

        private StarlingRequestBuilder RequestBuilder { get; }

        public string BaseUrl { get; set; }

        public string AccessToken { get; set; }

        public void Dispose()
        {
            this.RequestBuilder.Dispose();
        }

        public Task<(HttpStatusCode status, CreateOrUpdateSavingsGoalResponse response)> CreateGoal(SavingsGoalRequest goal)
        {
            var goalId = Guid.NewGuid();

            return this.RequestBuilder
                .WithUrl($"{this.BaseUrl}v1/savings-goals/{goalId}")
                .WithMethod(HttpMethod.Put)
                .WithHeader("Authorization", $"Bearer {this.AccessToken}")
                .WithBody(goal)
                .SendAsync<CreateOrUpdateSavingsGoalResponse>();
        }

        public Task<(HttpStatusCode status, SavingsGoal goal)> GetGoal(Guid goalId)
        {
            return this.RequestBuilder
                .WithUrl($"{this.BaseUrl}v1/savings-goals/{goalId}")
                .WithMethod(HttpMethod.Get)
                .WithHeader("Authorization", $"Bearer {this.AccessToken}")
                .SendAsync<SavingsGoal>();
        }

        public Task<(HttpStatusCode status, CreateOrUpdateSavingsGoalResponse response)> AddMoney(Guid goalId, TopUpRequest topup)
        {
            var transferId = Guid.NewGuid();

            return this.RequestBuilder
                .WithUrl($"{this.BaseUrl}v1/savings-goals/{goalId}/add-money/{transferId}")
                .WithMethod(HttpMethod.Put)
                .WithHeader("Authorization", $"Bearer {this.AccessToken}")
                .WithBody(topup)
                .SendAsync<CreateOrUpdateSavingsGoalResponse>();
        }
    }
}