using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hero_Code_Test.Models;
using Microsoft.Extensions.Configuration;
using Hero_Code_Test.Services;
using Newtonsoft.Json;

namespace Hero_Code_Test.Controllers
{
    public class BookingController : Controller
    {

        private readonly IConfiguration _config;
        private readonly IService _service;
        private ViewModel _vModel;
        private List<SearchOut> _listSearch;
        private Pax _mPax;

        public BookingController(IConfiguration config, IService service)
        {
            _config = config;
            _service = service;
            _vModel = new ViewModel();
            _listSearch = new List<SearchOut>();
            _vModel.ListSearch = _listSearch;
        }
        public IActionResult Booking()
        {
            ViewData["Message"] = "YourBooking Page";
            _mPax = new Pax();
            _vModel.errorMsg = "";
            _vModel.PaxData = _mPax;
            //string baseu = _config.GetValue<string>("BaseUrl");

            return View(_vModel);
        }

        [HttpPost]
        public IActionResult AddPax(ViewModel model)
        {
            PaxOut paxOut = new PaxOut();
            var o = _service.AddPaxData(model.PaxData);
            paxOut = JsonConvert.DeserializeObject<PaxOut>(o.ToString());
            model.errorMsg = "";
            return View("Booking", model);
        }

        [HttpPost]
        public IActionResult Search(ViewModel model)
        {
            var o = _service.SearchProduct(model.qSearch);
            _listSearch = JsonConvert.DeserializeObject<List<SearchOut>>(o.ToString());   
            model.ListSearch = _listSearch;  
            model.errorMsg = "";
            
            _vModel = model;       
            return View("Booking", model);
            
        }       


        [HttpPost]
        public IActionResult GetSchedule(ViewModel model)
        {   
            List<ScheduleOut> schd = new List<ScheduleOut>();
            var o = _service.GetSchedule(model.idSelected, model.dateStart, model.dateStart);

            if (o.ToString().Contains("message"))
            {
                model.errorMsg = o.ToString();
            }
            else
            {
                schd = JsonConvert.DeserializeObject<List<ScheduleOut>>(o.ToString());   
                model.ListSchedule = schd;
                model.errorMsg = "";
            }
            
            _vModel = model;       
            return View("Booking", model);
        }

        
         [HttpPost]
        public IActionResult GetPrice(ViewModel model)
        {   
            PriceOut price = new PriceOut();
            var o = _service.GetPrice(model.idSelected, model.dateCheckin, model.nights);
            price = JsonConvert.DeserializeObject<PriceOut>(o.ToString());   
            model.Price = price;
            model.errorMsg = "";
            
            _vModel = model;       
            return View("Booking", model);
        }
    }
}