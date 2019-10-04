﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Controllers
{
    public class InvoiceController : Controller
    {
        #region Inject
        //Inject DataBase--Start
        //OnlineMarketContext db;
        readonly UserManager<ApplicationUser> userManager;
        readonly DbRepository<OnlineMarketContext, ProductFeature, int> dbProductFeature;
        readonly DbRepository<OnlineMarketContext, Invoice, int> dbInvoice;
        readonly DbRepository<OnlineMarketContext, InvoiceProduct, int> dbInvoiceProduct;


        public InvoiceController
            (
                UserManager<ApplicationUser> _userManager,
                DbRepository<OnlineMarketContext, ProductFeature, int> _dbProductFeature,
                DbRepository<OnlineMarketContext, Invoice, int> _dbInvoice,
                DbRepository<OnlineMarketContext, InvoiceProduct, int> _dbInvoiceProduct
            )
        {
            userManager = _userManager;
            dbProductFeature = _dbProductFeature;
            dbInvoice = _dbInvoice;
            dbInvoiceProduct = _dbInvoiceProduct;
        }
        //Inject DataBase--End
        #endregion
        #region Index
        //Index--Start
        public IActionResult Index()
        {
            return RedirectToAction("ShowPurchuseCart");
        }
        //Index--End
        #endregion
        #region Purchuse Cart
        //Purchuse Cart--Start
        public int InitializeCart(Invoice invoiceUser, ApplicationUser currentUser)
        {
            int cartId;
            if (invoiceUser == null)
            {
                Invoice invoice = new Invoice()
                {
                    CustomerId = currentUser.Id,
                    RegDateTime = DateTime.Now,
                    IsPaid = false,
                    Status = true,
                    Sent = false,
                    Delivered = false
                };
                cartId = dbInvoice.Insert(invoice);
            }
            else
            {
                cartId = invoiceUser.Id;
            }

            return (cartId);
        }
        public async Task<IActionResult> ShowPurchuseCart()
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            var invoiceUser = dbInvoice.GetAll().Where(e => e.CustomerId == currentUser.Id && e.IsPaid == false).FirstOrDefault();
            int cartId = InitializeCart(invoiceUser, currentUser);
            var dbViewModel = dbInvoiceProduct.GetInclude(e => e.ProductFeature, e => e.ProductFeature.ProductAbstract, e => e.ProductFeature.ProductAbstract.ProductImage, e => e.Invoice).Where(e => e.InvoiceId == cartId).ToList();
            ViewData["totalprice"] = TotalInvoicePrice(cartId);
            return View(dbViewModel);
        }

        public async Task<IActionResult> AddToCart(int productFeatureId)
        {
            var productFeature = dbProductFeature.FindById(productFeatureId);
            if (User.Identity.Name == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            var invoiceUser = dbInvoice.GetAll().Where(e => e.CustomerId == currentUser.Id && e.IsPaid == false).FirstOrDefault();
            int cartId = InitializeCart(invoiceUser, currentUser);
            var invoiceProductUser = dbInvoiceProduct.GetAll().Where(e => e.InvoiceId == cartId && e.ProductFeatureId == productFeatureId).FirstOrDefault();
            if (invoiceProductUser != null)
            {
                return RedirectToAction("ShowPurchuseCart");
            }
            InvoiceProduct invoiceProduct = new InvoiceProduct()
            {
                ProductFeatureId = productFeatureId,
                InvoiceId = cartId,
                Count = 1,
                Status = true,
            };
            dbInvoiceProduct.Insert(invoiceProduct);
            return RedirectToAction("ShowPurchuseCart");
        }
        //Purchuse Cart--End
        #endregion
        #region Count & Price
        public int TotalInvoicePrice(int InvoiceId)
        {
            int sum = 0;
            var entity = dbInvoice.FindById(InvoiceId);
            if (entity != null)
            {
                var d = dbInvoiceProduct.GetInclude(e => e.ProductFeature.ProductAbstract).Where(e => e.InvoiceId == InvoiceId);
                sum =(int) d.Sum(e => e.Count * e.ProductFeature.ProductAbstract.BasePrice).Value;

            }
            return (sum);
        }
        public IActionResult IncrementCount(int InvoiceProductId)
        {
            var entity = dbInvoiceProduct.FindById(InvoiceProductId);
            var id = entity.InvoiceId.GetValueOrDefault();
            decimal totalPrice = TotalInvoicePrice(id);
            if (entity != null)
            {
                entity.Count++;
                try
                {
                    dbInvoiceProduct.Update(entity);
                    totalPrice = TotalInvoicePrice(id);
                    return Json(new { status = true, totalprice = totalPrice });
                }
                catch (Exception)
                {
                   
                }
            }
            return Json(new { status = false, totalprice = totalPrice });
        }
        public IActionResult DecrementCount(int InvoiceProductId)
        {
            var entity = dbInvoiceProduct.FindById(InvoiceProductId);
            var id = entity.InvoiceId.GetValueOrDefault();
            decimal totalPrice = TotalInvoicePrice(id);
            if (entity != null)
            {
                if (entity.Count>1)
                {
                    entity.Count--;
                }
                try
                {
                    dbInvoiceProduct.Update(entity);
                    totalPrice = TotalInvoicePrice(id);
                    return Json(new { status = true, totalprice = totalPrice });
                }
                catch (Exception)
                {

                }
            }
            return Json(new { status = false, totalprice = totalPrice });
        }
        #endregion
        #region Remove From Cart
        public IActionResult RemoveFromCart(int InvoiceProductId)
        {
            var entity = dbInvoiceProduct.FindById(InvoiceProductId);
            if (entity != null)
            {
                try
                {
                    dbInvoiceProduct.DeleteById(InvoiceProductId);
                    var id = entity.InvoiceId.GetValueOrDefault();
                    decimal totalPrice = TotalInvoicePrice(id);
                    return Json(new { status = true, totalprice = totalPrice });

                }
                catch (Exception)
                {
                    return Json(new { status = false, totalprice = 0 });
                }
            }
            return Json(new { status = false, totalprice = 0 });
        }
        #endregion
    }
}