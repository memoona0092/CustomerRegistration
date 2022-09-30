using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CountryData.Standard;
using CustomerRegistration.Models;
using Microsoft.Ajax.Utilities;

namespace CustomerRegistration.Controllers
{
    [RoutePrefix("Customer")]
    public class CustomerController : Controller
    {
        CustomerDb db =new CustomerDb();

        // GET: Customer
        [Route("")]
        [Route("Index")]
        public ActionResult Index()
        {

            return View(db.customers.ToList());
        }
        [HttpGet]
        public ActionResult AddCustomer()
        {
            List<SelectListItem> Countrydata = CountryList();
            ViewBag.CountryList = Countrydata;
            TempData["Countries"] = Countrydata;
            return View();
        }
        [HttpPost]
        public ActionResult AddCustomer(Customer oCustomer,string CountryList,string ddlRegion)
        {
            try
            {
                ViewBag.CountryList = TempData["Countries"];
                if (ModelState.IsValid)
                { 
                    oCustomer.CountryShortCode = CountryList;
                    oCustomer.SelectedRegionCode = ddlRegion;
                    db.customers.Add(oCustomer);
                    db.SaveChanges();
                }
            }
            catch
             { 
                throw;
             }
           
            ViewBag.CountryList = TempData["Countries"];

            RedirectToAction( "Index");
        }
        //Country short code is use to get regions list
        public JsonResult GetRegionList(string val,string name)
        {
            var helper = new CountryHelper();
            var data = helper.GetCountryData();
            var RegionsData=data.Where(x => x.CountryShortCode == val)
                              .Select(r => r.Regions).FirstOrDefault()
                              .ToList();
           List<SelectListItem> regions = new List<SelectListItem>();
            foreach (var r in RegionsData)
            {
                regions.Add(new SelectListItem
                {
                    Text = r.Name,
                    Value = r.ShortCode

                });
            }
          
            return Json(regions);
        }
        //Return List of Countries
        public List<SelectListItem> CountryList()
        {

            var helper = new CountryHelper();
            var data = helper.GetCountryData();

            var CountriesList = data.Select(x => new Country
            {
                CountryName = x.CountryName,
                CountryShortCode = x.CountryShortCode,
            })
          .ToList();

            List<SelectListItem> Countrydata = new List<SelectListItem>();

            foreach (var c in CountriesList)
            {
                Countrydata.Add(new SelectListItem
                {
                    Text = c.CountryName,
                    Value = c.CountryShortCode,
                });
            }
            return Countrydata;
        }
        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.customers.Find(id);
            db.customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}