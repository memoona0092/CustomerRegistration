using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace CustomerRegistration.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Customer Name")]
        public string CustName { get; set; }
        [Required]
        [Display(Name ="Father Name")]
        public string CustFatherName { get; set; }
        [Required]
        [Display(Name ="Phone No.")]
        public string CustPhoneNo { get; set; }
        [Display(Name ="Address")]
        public string CustAddress { get; set; }
        [Display (Name ="Country")]
        public string CountryShortCode { get; set; }
        
        [Display(Name ="Region")]
        public string SelectedRegionCode { set; get; }
        [Display(Name ="Postcode")]
        public int PostCode { set; get; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
   public class CustomerDb:DbContext
    { 
        public DbSet<Customer> customers { get; set; }
    }

}