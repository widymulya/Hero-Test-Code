using System;
using System.Collections.Generic;

namespace Hero_Code_Test.Models
{
    

    public class ViewModel
    {
        public string qSearch {get;set;}
        public string dateStart {get;set;}
        public string idSelected {get;set;}
        public string SchdSelected {get;set;}
        public string dateCheckin {get;set;}
        public string nights {get;set;}
        public string errorMsg {get;set;}
        public List<SearchOut> ListSearch {get;set;}     
        public Pax PaxData {get;set;}  
        public List<ScheduleOut> ListSchedule {get;set;}

        public PriceOut Price {get;set;}

    }

    public class SearchOut
    {
        public int Id	{get; set;}
        public long Score	{get; set;}
        public string Name	{get; set;}
        public string ImageUrl	{get; set;}
        public string SupplierName {get; set;}
        public string SupplierProductCode	{get; set;}
        public string BranchName	{get; set;}
        public int Category	{get; set;}
        public int DurationInMinutes	{get; set;}
        public int NumberOfNights	{get; set;}
        public string FormattedAddressName	{get; set;}
        public string ShortDescription	{get; set;}
        public string FullDescription	{get; set;}
    }

    public class Pax
    {
        //public string id {get;set;}        
        public string first {get;set;}
        public string last {get;set;}
        public string email {get;set;}
        public string mobile {get;set;}

        //E.164 format mobile telephone number (eg. 447577624125)
        public int age	{get;set;}
        //default: 18
        public string notes	{get;set;}

        //eg. Dietary / Special Requirements
        public string nationalityIso {get;set;}
        public int gender	{get;set;}
        //default: null
        //0-male
        //1-female
        public string externalReference	{get;set;}
        public string address {get;set;}
        public string country {get;set;}
        public string postcode {get;set;}
    }

    public class PaxOut
    {
        public string id {get;set;}
        public string first {get;set;}
        public string last {get;set;}
        public string email {get;set;}
        public string mobile {get;set;}
        public int age	{get;set;}
        public string notes	{get;set;}
        public string nationalityIso {get;set;}
        public int gender	{get;set;}

    }

    public class Payment
    {
        public string bookingId	{get;set;}
        public string paxId	{get;set;}

        public decimal amount {get;set;}
        public int method {get;set;}

        public bool isFinal {get;set;}
    }

    public class Receipt:Payment
    {
        public Payment paymentDetail {get;set;}
        public string receipt {get;set;}  
    }

    public class ScheduleOut
    {
        public string start {get;set;}
        public string end	{get;set;}
        public int id	{get;set;}
        public int scheduleId	{get;set;}
        public int availability	{get;set;}
        public bool available	{get;set;}
        public bool cta	{get;set;}
        public bool ctd	{get;set;}
        public int minStay	{get;set;}
        public int maxStay	{get;set;}
        public bool stopsell	{get;set;}
    }

    public class PriceOut
    {
        public int productId {get;set;}
        public string dateCheckIn {get;set;}
        public string dateCheckOut	{get;set;}
        public decimal rrp {get;set;}


        public decimal commission {get;set;}
        public string currencyIso{get;set;}
    }



}