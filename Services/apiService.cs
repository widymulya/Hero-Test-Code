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
        private string _baseUrl = string.Empty;
        private string _apiKey;
        
        private RestClient _client;
        private RestRequest _req;    

        public apiService()
        {
            var config = LoadSetting.LoadSettings();
            _baseUrl = config.GetSection("baseUrl").Value;
            _apiKey = config.GetSection("apiKey").Value; 
            _client = new RestClient(_baseUrl);            
        }   

        public object AddPaxData(Pax pax)
        {
            string json = JsonConvert.SerializeObject(pax);
                 
            _req = new RestRequest("pax", Method.POST);
            _req.AddHeader("apiKey", _apiKey); 
            _req.AddParameter("application/json", json,ParameterType.RequestBody);
            var response = _client.Execute<PaxOut>(_req);
            return response.Content;
        }

        public string BookingFinalise(string bookingId)
        {
            _req = new RestRequest("bookingfinalise/" + bookingId, Method.GET);
            _req.AddHeader("apiKey", _apiKey);
            var response = _client.Execute(_req);
            return response.Content.ToString();;
        }

        public object GetPrice(int id, string dateCheckin, int nights)
        {
           
           //test
            // id = 25352;
            // dateCheckin = "2022-05-01";
            // nights = 1;

            _req = new RestRequest("productpricing/" +id.ToString()+ "/" +dateCheckin+ "/" +nights.ToString(), Method.POST);
            _req.AddHeader("apiKey", _apiKey);
            var response = _client.Execute<PriceOut>(_req);
            return response.Content;
        }

        public object GetSchedule(int id, string start, string end)
        {
            //test      
            id = 29174;
            _req = new RestRequest("schedule/" + id.ToString() + "/" + start + "/" + end , Method.GET);
            _req.AddHeader("apiKey", _apiKey);                   

            var response = _client.Execute<List<ScheduleOut>>(_req);
            return response.Content;
        }

        public object InputBooking(BookingIn bookData)
        {
            string json = JsonConvert.SerializeObject(bookData);
            _req = new RestRequest("bookings", Method.POST);
            _req.RequestFormat = DataFormat.Json;
            _req.AddHeader("apiKey", _apiKey);  
            _req.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = _client.Execute<List<SearchOut>>(_req);
            HttpStatusCode statusCode = response.StatusCode;
            // int numStatusCode = (int)statusCode;  
            // if (numStatusCode >= 200 && numStatusCode <= 206)
            // {return response.Content;}        
            // else{
            //     response.Content = 
            //     return response.Content;
            // }

             return response.Content;
        }

        public object InputPayment(PaymentIn payIn, out int responseCode)
        {
             string json = JsonConvert.SerializeObject(payIn);
            _req = new RestRequest("payments", Method.POST);
            _req.RequestFormat = DataFormat.Json;
            _req.AddHeader("apiKey", _apiKey);  
            _req.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = _client.Execute<PaymentOut>(_req);
            HttpStatusCode statusCode = response.StatusCode;
            responseCode = (int)statusCode; 
            return response.Content;
        }

        public object SearchProduct(string q)
        {
                 
            _req = new RestRequest("Search", Method.GET);
            _req.RequestFormat = DataFormat.Json;
            _req.AddHeader("apiKey", _apiKey);  
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
