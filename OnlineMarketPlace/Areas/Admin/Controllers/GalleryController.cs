using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMarket.Models;
using OnlineMarketPlace.Areas.Identity.Data;
using OnlineMarketPlace.Models.AdminViewModels;
using OnlineMarketPlace.Repository;

namespace OnlineMarketPlace.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperVisor")]
    public class GalleryController : Controller
    {
        #region Inject
        //Inject DataBase--Start

        DbRepository<OnlineMarketContext, Subject, int> dbSubject;
        DbRepository<OnlineMarketContext, PhotoGallery, int> dbPhotoGallery;
        public GalleryController
            (
                DbRepository<OnlineMarketContext, Subject, int> _dbSubject,
                DbRepository<OnlineMarketContext, PhotoGallery, int> _dbPhotoGallery
            )
        {
            dbSubject = _dbSubject;
            dbPhotoGallery = _dbPhotoGallery;
        }
        //Inject DataBase--End
        #endregion
        #region Index
        public IActionResult Index()
        {
            return RedirectToAction("ShowPhoto");
        }
        #endregion
        #region Subject
        public IActionResult ShowSubject()
        {
            var dbViewModel = dbSubject.GetInclude(e => e.User);
            return View(dbViewModel);
        }
        public IActionResult InsertSubject()
        {
            return View();
        }
        public IActionResult InsertSubjectConfirm(SubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                Subject subject = new Subject()
                {
                    Name = model.Name,
                    LatinName = model.LatinName,
                    Description = model.Description,
                    Status = model.Status
                };
                dbSubject.Insert(subject);
                TempData["InsertConfirm"] = "با موفقیت ثبت شد";
                return RedirectToAction("ShowSubject");
            }
            return RedirectToAction("InsertSubject");
        }
        public IActionResult DeleteSubject(int Id)
        {
            var status = dbSubject.DeleteById(Id);
            if (status)
            {

            }
            else
            {

            }
            return RedirectToAction("ShowSubject");
        }
        public IActionResult EditSubject(int Id)
        {
            ViewData["Subject"] = dbSubject.FindById(Id);
            return View();
        }
        public IActionResult EditSubjectConfirm(SubjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = dbSubject.FindById(model.Id);
                entity.Name = model.Name;
                entity.LatinName = model.LatinName;
                entity.Description = model.Description;
                entity.Status = model.Status;

                dbSubject.Update(entity);
                TempData["InsertConfirm"] = "با موفقیت ثبت شد";
                return RedirectToAction("ShowSubject");
            }
            return RedirectToAction("InsertSubject");
        }
        #endregion
        #region Photo
        public IActionResult ShowPhoto()
        {
            var dbViewModel = dbPhotoGallery.GetInclude(e => e.User);
            return View(dbViewModel);
        }
        public IActionResult InsertPhoto()
        {
            ViewData["Subject"] = dbSubject.GetAll();
            return View();
        }
        public IActionResult InsertPhotoConfirm(PhotoGalleryViewModel model)
        {
            if (ModelState.IsValid)
            {
                byte[] b = new byte[model.Image.Length];
                model.Image.OpenReadStream().Read(b, 0, (int)model.Image.Length);
                PhotoGallery photoGallery = new PhotoGallery()
                {
                    Name = model.Name,
                    LatinName = model.LatinName,
                    Status = model.Status,
                    Image=b,
                };
                dbPhotoGallery.Insert(photoGallery);
                TempData["InsertConfirm"] = "با موفقیت ثبت شد";
                return RedirectToAction("ShowPhoto");
            }
            return RedirectToAction("InsertPhoto");
        }
        public IActionResult DeletePhoto(int Id)
        {
            var status = dbPhotoGallery.DeleteById(Id);
            if (status)
            {

            }
            else
            {

            }
            return RedirectToAction("ShowPhoto");
        }
        public IActionResult EditPhoto(int Id)
        {
            ViewData["PhotoGallery"] = dbPhotoGallery.FindById(Id);
            ViewData["Subject"] = dbSubject.GetAll();

            return View();
        }
        public IActionResult EditPhotoConfirm(PhotoGalleryViewModel model)
        {
            var entity = dbPhotoGallery.FindById(model.Id);
            byte[] b = null;
            if (model.Image==null)
            {
                ModelState.Remove("Image");
                b = entity.Image;
            }
            else
            {
                b = new byte[model.Image.Length];
                model.Image.OpenReadStream().Read(b, 0, (int)model.Image.Length);
            }
            if (ModelState.IsValid)
            {
                entity.Name = model.Name;
                entity.LatinName = model.LatinName;
                entity.Status = model.Status;
                entity.SubjectId = model.SubjectId;
                entity.Image = b;

                dbPhotoGallery.Update(entity);
                TempData["InsertConfirm"] = "با موفقیت ثبت شد";
                return RedirectToAction("ShowPhoto");
            }
            return RedirectToAction("InsertPhoto");
        }
        #endregion
    }
}