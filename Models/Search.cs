using System;
using System.Collections.Generic;

namespace Hero_Code_Test.Models
{
    

    public class ViewModel
    {
        public string qSearch {get;set;}
        public string paxId {get;set;}
        public string paxName {get;set;}
        public string dateStart {get;set;}
        public string dateEnd {get;set;}
        public int idSelected {get;set;}
        public string SchdSelected {get;set;}
        public string dateCheckin {get;set;}
        public int nights {get;set;}
        public string errorMsg {get;set;}
        public decimal discountPrice {get;set;}
        public string bookingId {get;set;}
        public int paymentMethod {get;set;}
        public string paymentReceipt {get;set;}

        public string ticketUrl {get;set;}

        public List<SearchOut> ListSearch {get;set;}     
        public Pax PaxData {get;set;}  
        public List<ScheduleOut> ListSchedule {get;set;}

        public PriceOut Price {get;set;}

    }

    public class SearchOut
    {
        public int Id	{get; set;}
        public decimal Score	{get; set;}
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

   
    public class ScheduleOut
    {
        public string start {get;set;}
        public string end	{get;set;}
        public Nullable<int> id	{get;set;}
        public string productId {get;set;}
        public int scheduleId	{get;set;}
        public int availability	{get;set;}
        public Nullable<bool> available	{get;set;}
        public Nullable<bool> cta	{get;set;}
        public Nullable<bool> ctd	{get;set;}
        public Nullable<int> minStay	{get;set;}
        public Nullable<int> maxStay	{get;set;}
        //public bool stopsell	{get;set;}
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

    public class BookingIn
    {
        public string name {get;set;}
       
        public BookProduct[] bookingProducts{get;set;}


    }

    public class BookingOut
    {
        public string id {get;set;}
        public string created {get;set;}
        public decimal rrp	{get;set;}
        public decimal paid {get;set;}
        public decimal payable {get;set;}
        public decimal commission {get;set;}
        public decimal adjustment {get;set;}
        public int status {get;set;}
        public BookProduct[] bookingProducts{get;set;}
    }

    public class BookProduct
    {
        public int productId {get;set;}
        public string dateCheckin {get;set;}
        //public List<string> paxId {get;set;}
        public string[] paxId {get;set;}
        public string agentReference {get;set;}
        public int nights {get;set;}
        // public List<PaxRoom> paxRoomId {get;set;}
        public PaxRoom[] paxRoomId {get;set;}
    }


    public class PaxRoom 
    {
        public string paxId {get;set;}
        public int room {get;set;}
    }

    public class PaymentIn
    {
        public string bookingId {get;set;}
        public string paxId	{get;set;}
        public decimal amount {get;set;}
        public int method {get;set;}
        // 1- Cash
        // 2- Credit Card
        // 3- Bank Transfer
        // 4- FOC
        public bool isFinal {get;set;}
    }

    public class PaymentOut:PaymentIn
    {
        public string receipt {get;set;}
       
    }



}