﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoRent.Models;

namespace AutoRent.Controllers
{
    public class CustomerQueriesController : Controller
    {
        private AutoRentContext db = new AutoRentContext();

        public ActionResult Index()
        {
            var customerFavours = db.CustomerFavours.Include(c => c.customer);
            return View(customerFavours.ToList());
        }

        // GET: CustomerQueries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerQuery customerQuery = db.CustomerFavours.Find(id);
            if (customerQuery == null)
            {
                return HttpNotFound();
            }
            return View(customerQuery);
        }

        public ActionResult Create(int? customerId)
        {
            if (customerId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "fullName", customerId);
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CustomerID,rentStartDate,rentDays,favouriteBrand,maxRentPricePerDay")] CustomerQuery customerQuery)
        {
            if (ModelState.IsValid)
            {
                db.CustomerFavours.Add(customerQuery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "fullName", customerQuery.CustomerID);
            return View(customerQuery);
        }

        // GET: CustomerQueries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerQuery customerQuery = db.CustomerFavours.Find(id);
            if (customerQuery == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName", customerQuery.CustomerID);
            return View(customerQuery);
        }

        // POST: CustomerQueries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CustomerID,rentStartDate,rentDays,favouriteBrand,maxRentPricePerDay")] CustomerQuery customerQuery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerQuery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "firstName", customerQuery.CustomerID);
            return View(customerQuery);
        }

        // GET: CustomerQueries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerQuery customerQuery = db.CustomerFavours.Find(id);
            if (customerQuery == null)
            {
                return HttpNotFound();
            }
            return View(customerQuery);
        }

        // POST: CustomerQueries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerQuery customerQuery = db.CustomerFavours.Find(id);
            db.CustomerFavours.Remove(customerQuery);
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
