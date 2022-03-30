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
        object GetSchedule(string id, string start, string end);
        object GetPrice(string id, string dateCheckin, string nights);
    }
}