using CashFlow.Web.Models;
using Newtonsoft.Json;
using System.Text;

namespace CashFlow.Web.Services
{
    public class CashFlowApiService
    {
        private readonly HttpClient _httpClient;

        public CashFlowApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EntryViewModel>> GetEntriesAsync()
        {
            var response = await _httpClient.GetAsync("api/entries");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<EntryViewModel>>(content);
        }

        public async Task<EntryViewModel> GetEntryByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/entries/{id}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EntryViewModel>(content);
        }

        public async Task<EntryViewModel> CreateEntryAsync(EntryViewModel entry)
        {
            var jsonContent = JsonConvert.SerializeObject(entry);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/entries", stringContent);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<EntryViewModel>(content);
        }

        public async Task UpdateEntryAsync(Guid id, EntryViewModel entry)
        {
            var jsonContent = JsonConvert.SerializeObject(entry);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/entries/{id}", stringContent);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteEntryAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/entries/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<ReportViewModel> GetDailyBalanceReportAsync(DateTime date)
        
        {
            var response = await _httpClient.GetAsync($"api/reports/dailybalance?startDate={date:yyyy-MM-dd}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var report = JsonConvert.DeserializeObject<ReportViewModel>(content);

            return report;
        }

    }
}
