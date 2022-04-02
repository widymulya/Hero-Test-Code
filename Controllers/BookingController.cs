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
       
        private Pax _mPax;

        public BookingController(IConfiguration config, IService service)
        {
            _config = config;
            _service = service;
            _vModel = new ViewModel();
           
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

        
        public IActionResult AddPax(ViewModel model)
        {
            PaxOut paxOut = new PaxOut();
            var o = _service.AddPaxData(model.PaxData);
            paxOut = JsonConvert.DeserializeObject<PaxOut>(o.ToString());
            if (o.ToString().Contains("message"))
            {
                model.errorMsg = o.ToString();              
            }
            else
            {
                model.errorMsg = "";    
                model.paxName = paxOut.first + " " + paxOut.last;
                model.paxId = paxOut.id;
            }

            ModelState.Remove("paxName");  
            ModelState.Remove("paxId");  

            return View("Booking", model);
        }

        
        public IActionResult Search(ViewModel model)
        {
            var o = _service.SearchProduct(model.qSearch);
            List<SearchOut> lsSearch = new List<SearchOut>();
            lsSearch = JsonConvert.DeserializeObject<List<SearchOut>>(o.ToString());   
            model.ListSearch = lsSearch;  
            model.errorMsg = "";           
            ModelState.Remove("ListSearch");
            return View("Booking", model);
            
        }       
        
        public IActionResult GetSchedule(ViewModel model)
        {   
            List<ScheduleOut> schd = new List<ScheduleOut>();
            var o = _service.GetSchedule(model.idSelected, model.dateStart, model.dateEnd);

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
            
               
            return View("Booking", model);
        }

        
        
        public IActionResult GetPrice(ViewModel model)
        {   
            PriceOut price = new PriceOut();
            var o = _service.GetPrice(model.idSelected, model.dateCheckin, model.nights);
            price = JsonConvert.DeserializeObject<PriceOut>(o.ToString());   
            model.Price = price;

            decimal diskon = model.Price.commission / 2;

            model.discountPrice = model.Price.rrp - diskon;
            ModelState.Remove("discountPrice");
            return View("Booking", model);
        }

        
        public IActionResult SubmitBooking(ViewModel model)
        {
            //test
            model.paxName = "sandi gilang";
            model.paxId = "85e41cd8-2a66-4903-9583-70f3f0b7fd8b";

            BookingIn bookData = new BookingIn();
            BookingOut bookResult = new BookingOut();
            BookProduct bookProd = new BookProduct();
            PaxRoom pR = new PaxRoom();
            bookData.name = model.paxName;
            bookProd.productId = model.idSelected;
            bookProd.dateCheckin = model.dateCheckin;
            bookProd.nights = model.nights;
            bookProd.agentReference = "";

            List<string> lsPax = new List<string>();
            lsPax.Add(model.paxId);
            bookProd.paxId = lsPax.ToArray();           

            pR.paxId = model.paxId;
            pR.room = 1;
            List<PaxRoom> lsPaxRoom = new List<PaxRoom>();
            lsPaxRoom.Add(pR);

            bookProd.paxRoomId = lsPaxRoom.ToArray();
            List<BookProduct> ls = new List<BookProduct>();
            ls.Add(bookProd);
            bookData.bookingProducts = ls.ToArray();

            var o = _service.InputBooking(bookData);
            try{
                bookResult = JsonConvert.DeserializeObject<BookingOut>(o.ToString());   
                model.bookingId = bookResult.id;
            }
            catch 
            {
                model.errorMsg = o.ToString();
            }

            ModelState.Remove("bookingId");     

            return View("Booking", model);
        }

        
        public IActionResult SubmitPayment(ViewModel model)
        {
            PaymentIn payIn = new PaymentIn();
            payIn.amount = model.discountPrice;
            payIn.bookingId = model.bookingId;
            payIn.isFinal = true;
            payIn.method = model.paymentMethod;
            payIn.paxId = model.paxId;

            PaymentOut payOut = new PaymentOut();
            int rspCode = 0;
            var o = _service.InputPayment(payIn, out rspCode);
            if (rspCode == 201)
            {
                payOut = JsonConvert.DeserializeObject<PaymentOut>(o.ToString());   
                model.paymentReceipt = payOut.receipt;
                model.ticketUrl = BookingFinalise(model.bookingId);
                ModelState.Remove("ticketUrl");
            }
            else
                model.errorMsg = o.ToString();

            ModelState.Remove("paymentReceipt");
            return View("Booking", model);
        }

        private string BookingFinalise(string bookingId)
        {
            string o = _service.BookingFinalise(bookingId);
            return o;
        }




    }
}