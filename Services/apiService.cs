using Microsoft.AspNetCore.Mvc;
using Hero_Code_Test.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;


namespace Hero_Code_Test.Services
{
    public class apiService : IService
    {
        private string baseUrl = string.Empty;
        private string apiKey;
        
        private RestClient _client;
        private RestRequest _req;       

        public object AddPaxData(Pax pax)
        {
            string json = JsonConvert.SerializeObject(pax);
            var config = LoadSetting.LoadSettings();
            baseUrl = config.GetSection("BaseUrl").Value;
            apiKey = config.GetSection("apiKey").Value;    
            _client = new RestClient(baseUrl);        
            _req = new RestRequest("pax", Method.POST);
            _req.AddHeader("apiKey", apiKey); 
            _req.AddParameter("application/json", json,ParameterType.RequestBody);
            var response = _client.Execute<PaxOut>(_req);
            return response.Content;
        }

        public object GetPrice(string id, string dateCheckin, string nights)
        {
            var config = LoadSetting.LoadSettings();
            
            baseUrl = config.GetSection("BaseUrl").Value;
            apiKey = config.GetSection("apiKey").Value;    
            _client = new RestClient(baseUrl);
            _req = new RestRequest("productpricing", Method.POST);
            _req.AddHeader("apiKey", apiKey);
            _req.AddQueryParameter("id", id);
            _req.AddQueryParameter("dateCheckIn", dateCheckin);
            _req.AddQueryParameter("nights", nights);           

            var response = _client.Execute<PriceOut>(_req);
            return response.Content;
        }

        public object GetSchedule(string id, string start, string end)
        {
            var config = LoadSetting.LoadSettings();
            
            baseUrl = config.GetSection("BaseUrl").Value;
            apiKey = config.GetSection("apiKey").Value;    
            _client = new RestClient(baseUrl);
            _req = new RestRequest("schedule", Method.GET);
            _req.AddHeader("apiKey", apiKey);
            _req.AddQueryParameter("id", id);
            _req.AddQueryParameter("dateStart", start);
            _req.AddQueryParameter("dateEnd", end);           

            var response = _client.Execute<List<ScheduleOut>>(_req);
            return response.Content;
        }

        public object SearchProduct(string q)
        {
            var config = LoadSetting.LoadSettings();
            
            baseUrl = config.GetSection("BaseUrl").Value;
            apiKey = config.GetSection("apiKey").Value;    
            _client = new RestClient(baseUrl);        
            _req = new RestRequest("Search", Method.GET);
            _req.AddHeader("apiKey", apiKey);  
            _req.AddQueryParameter("q", q);
            _req.AddQueryParameter("cat", "1");
            _req.AddQueryParameter("lat", "12");
            _req.AddQueryParameter("lng", "12");
            _req.AddQueryParameter("rad", "30");

            var response = _client.Execute<List<SearchOut>>(_req);
            return response.Content;
        }
            
            
    }
}
