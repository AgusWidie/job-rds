﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace WEB_API_WARRANTY_TSJ.Models
{
    public partial class LogWarranty
    {
        public long Id { get; set; }
        public string LogWarrantyId { get; set; }
        public string ProductCode { get; set; }
        public string Source { get; set; }
        public string SerialCode { get; set; }
        public string ActivationCode { get; set; }
        public string RegistrationCode { get; set; }
        public string QrCode { get; set; }
        public string QrCodeFull { get; set; }
        public DateTime? ActivationAt { get; set; }
        public string ActivationBy { get; set; }
        public DateTime? RegistrationAt { get; set; }
        public string RegistrationBy { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Telephone { get; set; }
        public string Additional1 { get; set; }
        public string Additional2 { get; set; }
        public string Additional3 { get; set; }
        public string Additional4 { get; set; }
        public string Additional5 { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}