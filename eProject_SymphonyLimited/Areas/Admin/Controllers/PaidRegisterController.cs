﻿using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class PaidRegisterController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/PaidRegister
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null, int admissionId = 0)
        {
            int pageSize = 6;
            var PaidRegisters = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg,
                           ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name,
                   Tested = pr.Tested,
                   Passed = pr.Passed
               }).Where(x => x.AdmissionId == admissionId).AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
                        PaidRegisters = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg,
                           ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name,
                   Tested = pr.Tested,
                   Passed = pr.Passed
               }).Where(x => x.EntityId.ToString().Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "Name":
                        PaidRegisters = db.RegisterInfo
               .Join(
                   db.Admission,
                      reg => reg.AdmissionId,
                      ad => ad.EntityId,
                      (reg, ad) => new
                      {
                          reg,
                          ad
                      })
               .Join(
           db.PaidRegister,
              re => re.reg.EntityId,
              pr => pr.RegisterInfoId,
              (re, pr) => new
              PaidRegisterViewModel
              {
                  EntityId = pr.EntityId,
                  RollNumber = pr.RollNumber,
                  Result = pr.Result,
                  Name = re.reg.Name,
                  Phone = re.reg.Phone,
                  Email = re.reg.Email,
                  Comment = re.reg.Comment,
                  CreatedAt = re.reg.CreatedAt,
                  AdmissionId = re.reg.AdmissionId,
                  Admission = re.ad.Name,
                  Tested = pr.Tested,
                  Passed = pr.Passed
              }).Where(x => x.Name.Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "Phone":
                        PaidRegisters = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg,
                           ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name,
                   Tested = pr.Tested,
                   Passed = pr.Passed
               }).Where(x => x.Phone.Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "Email":
                        PaidRegisters = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg,
                           ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name,
                   Tested = pr.Tested,
                   Passed = pr.Passed
               }).Where(x => x.Email.Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "Status":
                        PaidRegisters = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg,
                           ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name,
                   Tested = pr.Tested,
                   Passed = pr.Passed
               }).Where(x => x.Status.ToString().Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "RollNumber":
                        PaidRegisters = db.RegisterInfo
                  .Join(
                      db.Admission,
                         reg => reg.AdmissionId,
                         ad => ad.EntityId,
                         (reg, ad) => new
                         {
                             reg,
                             ad
                         })
                  .Join(
              db.PaidRegister,
                 re => re.reg.EntityId,
                 pr => pr.RegisterInfoId,
                 (re, pr) => new
                 PaidRegisterViewModel
                 {
                     EntityId = pr.EntityId,
                     RollNumber = pr.RollNumber,
                     Result = pr.Result,
                     Name = re.reg.Name,
                     Phone = re.reg.Phone,
                     Email = re.reg.Email,
                     Comment = re.reg.Comment,
                     CreatedAt = re.reg.CreatedAt,
                     AdmissionId = re.reg.AdmissionId,
                     Admission = re.ad.Name,
                     Tested = pr.Tested,
                     Passed = pr.Passed
                 }).Where(x => x.RollNumber.Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "Admission":
                        PaidRegisters = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg,
                           ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name,
                   Tested = pr.Tested,
                   Passed = pr.Passed
               }).Where(x => x.Admission.Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)PaidRegisters.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(PaidRegisters.Skip((page - 1) * pageSize).Take(pageSize));
            return Json(new
            {
                TotalPages = totalPages,
                CurrentPage = page,
                StatusCode = 200,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetByPrId(int prId)
        {
            var prById = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg,
                           ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   EndTime = re.ad.EndTime,
                   Admission = re.ad.Name,
                   Tested = pr.Tested,
                   Passed = pr.Passed
               }).FirstOrDefault(x => x.EntityId == prId);
            string jsonData = JsonConvert.SerializeObject(prById);
            if (prById != null)
            {
                if (prById.Tested == false)
                {
                    return Json(new
                    {
                        StatusCode = 200,
                        Message = "This paid register is not tested"
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (prById.EndTime > DateTime.Now)
                {
                    return Json(new
                    {
                        StatusCode = 200,
                        Message = "Admission is not over yet!"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        StatusCode = 200,
                        Data = jsonData
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                StatusCode = 400,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SetIsTested(int prId)
        {
            var prById = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg,
                           ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   EndTime = re.ad.EndTime,
                   Admission = re.ad.Name,
                   Tested = pr.Tested,
                   Passed = pr.Passed
               }).FirstOrDefault(x => x.EntityId == prId);
            if (prById != null)
            {
                if (prById.EndTime > DateTime.Now)
                {
                    return Json(new
                    {
                        StatusCode = 200,
                        Message = "Admission is not over yet!"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var pr = db.PaidRegister.Find(prId);
                    pr.Tested = true;
                    db.SaveChanges();
                    return Json(new
                    {
                        StatusCode = 200
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                StatusCode = 400
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SetResult(int prId, double result, int admissionId)
        {
            var prById = db.PaidRegister.FirstOrDefault(x => x.EntityId == prId);
            var adById = db.Admission.FirstOrDefault(x => x.EntityId == admissionId);
            if (prById != null)
            {
                if (prById.Tested == false)
                {
                    return Json(new
                    {
                        StatusCode = 200,
                        Message = "This paid register is not tested"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (result < 0)
                    {
                        return Json(new
                        {
                            StatusCode = 200,
                            Message = "Result must be bigger than 0"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (result > adById.MaxMark)
                    {
                        return Json(new
                        {
                            StatusCode = 200,
                            Message = "Result must be smaller than " + adById.MaxMark
                        }, JsonRequestBehavior.AllowGet); ;
                    }
                    else
                    {
                        prById.Result = result;
                        if (adById.PassedMark <= result && result <= adById.MaxMark)
                        {
                            prById.Passed = true;
                            if (db.Student.FirstOrDefault(x => x.RegisterInfoId == prById.RegisterInfoId) == null)
                            {
                                db.Student.Add(new Student
                                {
                                    RegisterInfoId = prById.RegisterInfoId,
                                    Status = false,
                                    ClassId = 0
                                });
                            }
                        }
                        else
                        {
                            prById.Passed = false;
                        }
                        db.SaveChanges();
                        return Json(new
                        {
                            StatusCode = 200
                        }, JsonRequestBehavior.AllowGet);
                    }

                }
            }
            return Json(new
            {
                StatusCode = 400
            }, JsonRequestBehavior.AllowGet);
        }
    }
}