using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MobilityDC.Api.Models.DTO;
using MobilityDC.Models;
using MobilityDC.Models.Commons;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MobilityDC.Data.Services
{
    public class PickDataStore 
    {
        HttpClient _client;

        public PickDataStore()
        {
            _client = new HttpClient();
        }
        
        public async Task<Result> CompleteItemsAsync(object item, string userId)
        {
            var route = string.Format("http://{0}/pick/Complete/{1}/", Constants.ApiUrl, userId);
            var uri = new Uri(route);

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);

            var readContent = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<Result>(readContent);

            return results;

        }

        public async Task<Result> GetItemAsync(object search, string userId)
        {

            var route = string.Format("http://{0}/pick/Search/{1}/", Constants.ApiUrl, userId);
            var uri = new Uri(route);

            var json = JsonConvert.SerializeObject(search);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);

            var readContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(readContent);

            return result;
        }

        public async Task<Result> ExitItemAsync(object task, string userId)
        {

            var route = string.Format("http://{0}/pick/ExitTask/{1}/", Constants.ApiUrl, userId);
            var uri = new Uri(route);

            var json = JsonConvert.SerializeObject(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);

            var readContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(readContent);

            return result;
        }

        public async Task<Result> CompleteStoreItemsAsync(TaskModel item, string userId)
        {
            var route = string.Format("http://{0}/pick/Complete/Store/{1}/", Constants.ApiUrl, userId);
            var uri = new Uri(route);

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(uri, content);

            var readContent = await response.Content.ReadAsStringAsync();
            var results = JsonConvert.DeserializeObject<Result>(readContent);

            return results;

        }
    }
}
