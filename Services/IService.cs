using Microsoft.AspNetCore.Mvc;
using Hero_Code_Test.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Hero_Code_Test.Services
{
    public interface IService
    {
        object SearchProduct(string q);
        object AddPaxData(Models.Pax pax);
        object GetSchedule(int id, string start, string end);
        object GetPrice(int id, string dateCheckin, int nights);

        object InputBooking(Models.BookingIn bookData);
        object InputPayment(Models.PaymentIn payIn, out int responseCode);

        string BookingFinalise(string bookingId);
    }
}