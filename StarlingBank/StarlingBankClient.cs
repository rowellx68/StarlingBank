using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using StarlingBank.Entities.SavingsGoals;
using StarlingBank.Extensions;

namespace StarlingBank
{
    public class StarlingBankClient : IStarlingBankClient, IDisposable
    {
        public StarlingBankClient(Uri baseUri, string token)
        {
            this.Client = new HttpClient
            {
                BaseAddress = baseUri,
                DefaultRequestHeaders =
                {
                    Authorization = new AuthenticationHeaderValue("Bearer", token),
                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
                }
            };

            this.Formatter = new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };
        }

        private HttpClient Client { get; }

        private JsonMediaTypeFormatter Formatter { get; }

        #region SavingsGoal

        public async Task<(HttpStatusCode status, CreateOrUpdateSavingsGoalResponse response)> CreateGoal(SavingsGoalRequest goal)
        {
            var goalId = Guid.NewGuid();
            var response = await this.Client.PutAsync($"v1/savings-goals/{goalId}", goal, this.Formatter);

            if (!response.IsSuccessStatusCode)
            {
                return (response.StatusCode, null);
            }

            var result = await response.DeserialiseContent<CreateOrUpdateSavingsGoalResponse>();

            return (response.StatusCode, result);
        }

        public async Task<(HttpStatusCode status, SavingsGoal goal)> GetGoal(Guid goalId)
        {
            var response = await this.Client.GetAsync($"v1/savings-goals/{goalId}");
        
            if (!response.IsSuccessStatusCode)
            {
                return (response.StatusCode, null);
            }

            var goal = await response.DeserialiseContent<SavingsGoal>();

            return (response.StatusCode, goal);
        }

        public async Task<(HttpStatusCode status, CreateOrUpdateSavingsGoalResponse response)> AddMoney(Guid goalId, TopUpRequest topup)
        {
            var transferId = Guid.NewGuid();
            var response = await this.Client.PutAsync($"v1/savings-goals/{goalId}/add-money/{transferId}", topup, this.Formatter);

            if (!response.IsSuccessStatusCode)
            {
                return (response.StatusCode, null);
            }

            var result = await response.DeserialiseContent<CreateOrUpdateSavingsGoalResponse>();

            return (response.StatusCode, result);
        }

        #endregion

        public void Dispose()
        {
            this.Client.Dispose();
        }
    }
}