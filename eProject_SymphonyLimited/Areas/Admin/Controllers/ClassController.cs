﻿using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class ClassController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
            var classes = db.Class.Join(db.Admission,
                    c => c.AdmissionId,
                    a => a.EntityId,
                    (c, a) => new ClassViewModel
                    {
                        EntityId = c.EntityId,
                        Name = c.Name,
                        QuantityStudent = c.QuantityStudent,
                        AdmissionId = a.EntityId,
                        AdmissionName = a.Name
                    }).AsEnumerable();
            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "Name":
                        classes = db.Class.Join(db.Admission,
                                c => c.AdmissionId,
                                a => a.EntityId,
                                (c, a) => new ClassViewModel
                                {
                                    EntityId = c.EntityId,
                                    Name = c.Name,
                                    QuantityStudent = c.QuantityStudent,
                                    AdmissionId = a.EntityId,
                                    AdmissionName = a.Name
                                }).Where(x => x.Name.Contains(key)).AsEnumerable();
                        break;
                    case "AdmissionName":
                        classes = db.Class.Join(db.Admission,
                                c => c.AdmissionId,
                                a => a.EntityId,
                                (c, a) => new ClassViewModel
                                {
                                    EntityId = c.EntityId,
                                    Name = c.Name,
                                    QuantityStudent = c.QuantityStudent,
                                    AdmissionId = a.EntityId,
                                    AdmissionName = a.Name
                                }).Where(x => x.AdmissionName.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)classes.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(classes.Skip((page - 1) * pageSize).Take(pageSize));
            return Json(new
            {
                TotalPages = totalPages,
                CurrentPage = page,
                StatusCode = 200,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.AdmissionId = new SelectList(db.Admission.AsEnumerable(), "EntityId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Class c)
        {
            ViewBag.AdmissionId = new SelectList(db.Admission.AsEnumerable(), "EntityId", "Name", c.AdmissionId);
            if (ModelState.IsValid)
            {
                var validateName = db.Class.FirstOrDefault(x => x.Name == c.Name);
                if (validateName == null)
                {
                    try
                    {
                        c.QuantityStudent = 0;
                        db.Class.Add(c);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Some thing went wrong while save class!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "Class Name is already exist!");
                }
            }
            return View();
        }

        public ActionResult AddStudent(int id)
        {
            var classById = db.Class.Find(id);
            if (classById != null)
            {
                var maxStudentInClass = db.CoreConfigData.FirstOrDefault(x => x.Code == "maximum_student_in_class");
                var maxStudentIsInt = Int32.TryParse(maxStudentInClass.Value, out int maxStudent);
                if (classById.QuantityStudent >= maxStudent)
                {
                    TempData["ErrorMessage"] = "The class is full of students!";
                    return RedirectToAction("Index");
                }
                ViewBag.ClassId = id;
            }
            else
            {
                ViewBag.ClassId = null;
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddStudent(string studentArrJson, int classId)
        {
            dynamic json = System.Web.Helpers.Json.Decode(studentArrJson);
            var classById = db.Class.Find(classId);
            var maxStudentInClass = db.CoreConfigData.FirstOrDefault(x => x.Code == "maximum_student_in_class");
            var maxStudentIsInt = Int32.TryParse(maxStudentInClass.Value, out int maxStudent);
            var quantityStudent = db.Class.Find(classId).QuantityStudent;
            if (json.Length > 0 && classById != null)
            {
                if (maxStudentIsInt)
                {
                    quantityStudent += json.Length;
                    if (quantityStudent > maxStudent)
                    {
                        return Json(new
                        {
                            StatusCode = 400,
                            Message = "Student In Class must smaller than " + maxStudent + "!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    foreach (var item in json)
                    {
                        var studentById = db.Student.Find(item);
                        if (studentById != null)
                        {
                            studentById.Status = true;
                            studentById.ClassId = classId;
                        }
                    }
                    classById.QuantityStudent = quantityStudent;
                    db.SaveChanges();
                    return Json(new
                    {
                        StatusCode = 200,
                        Message = "Add student to class success!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                StatusCode = 400,
                Message = "Something went wrong while add student to class!"
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowStudent(int id)
        {
            var classById = db.Class.Find(id);
            if (classById != null)
            {
                ViewBag.ClassId = id;
                ViewBag.ClassName = classById.Name;
            }
            else
            {
                ViewBag.ClassId = null;
                ViewBag.ClassName = classById.Name;
            }
            return View();
        }

        public ActionResult GetStudentInClass(int page = 1, string type = null, string key = null, int classId = 0)
        {
            if (classId != 0)
            {
                var _class = db.Class.Find(classId);
                int pageSize = 10;
                var students = db.Student.Join(db.RegisterInfo,
                    st => st.RegisterInfoId,
                    re => re.EntityId,
                    (st, re) => new StudentViewModel
                    {
                        EntityId = st.EntityId,
                        StudentName = re.Name,
                        Phone = re.Phone,
                        Email = re.Email,
                        Status = st.Status,
                        ClassName = _class.Name,
                        ClassId = st.ClassId
                    }).Where(x => x.Status == true && x.ClassId == classId).AsEnumerable();
                if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
                {
                    switch (type)
                    {
                        case "StudentName":
                            students = db.Student.Join(db.RegisterInfo,
                                    st => st.RegisterInfoId,
                                    re => re.EntityId,
                                    (st, re) => new StudentViewModel
                                    {
                                        EntityId = st.EntityId,
                                        StudentName = re.Name,
                                        Phone = re.Phone,
                                        Email = re.Email,
                                        Status = st.Status,
                                        ClassName = _class.Name,
                                        ClassId = st.ClassId
                                    }).Where(x => x.StudentName.Contains(key) && x.Status == true && x.ClassId == classId).AsEnumerable();
                            break;
                        default:
                            break;
                    }
                }
                decimal totalPages = Math.Ceiling((decimal)students.Count() / pageSize);
                string jsonData = JsonConvert.SerializeObject(students.Skip((page - 1) * pageSize).Take(pageSize));
                return Json(new
                {
                    TotalPages = totalPages,
                    CurrentPage = page,
                    StatusCode = 200,
                    Data = jsonData
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                StatusCode = 400,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudentNotInClass(int page = 1, string type = null, string key = null, int classId = 0)
        {
            if (classId != 0)
            {
                var admissionIdByClassId = db.Class.FirstOrDefault(x => x.EntityId == classId).AdmissionId;
                int pageSize = 10;
                var students = db.Student.Join(db.RegisterInfo,
                    st => st.RegisterInfoId,
                    re => re.EntityId,
                    (st, re) => new StudentViewModel
                    {
                        EntityId = st.EntityId,
                        StudentName = re.Name,
                        Status = st.Status,
                        AdmissionId = re.AdmissionId
                    }).Where(x => x.Status == false && x.AdmissionId == admissionIdByClassId).AsEnumerable();
                if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
                {
                    switch (type)
                    {
                        case "Name":
                            students = db.Student.Join(db.RegisterInfo,
                                    st => st.RegisterInfoId,
                                    re => re.EntityId,
                                    (st, re) => new StudentViewModel
                                    {
                                        EntityId = st.EntityId,
                                        StudentName = re.Name,
                                        Status = st.Status,
                                    }).Where(x => x.StudentName.Contains(key) && x.Status == false && x.AdmissionId == admissionIdByClassId).AsEnumerable();
                            break;
                        default:
                            break;
                    }
                }
                decimal totalPages = Math.Ceiling((decimal)students.Count() / pageSize);
                string jsonData = JsonConvert.SerializeObject(students.Skip((page - 1) * pageSize).Take(pageSize));
                return Json(new
                {
                    TotalPages = totalPages,
                    CurrentPage = page,
                    StatusCode = 200,
                    Data = jsonData
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                StatusCode = 400,
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetClassForStudent(int studentId = 0)
        {
            if (studentId != 0)
            {
                var currentStudent = db.Student.Find(studentId);
                if (currentStudent != null)
                {
                    var maxStudentInClass = db.CoreConfigData.FirstOrDefault(x => x.Code == "maximum_student_in_class");
                    var maxStudentIsInt = Int32.TryParse(maxStudentInClass.Value, out int maxStudent);
                    var classByStudentId = db.Class.FirstOrDefault(x => x.EntityId == currentStudent.ClassId);
                    if (maxStudentIsInt && classByStudentId != null)
                    {
                        var classForStudent = db.Class.Where(x => x.AdmissionId == classByStudentId.AdmissionId && x.QuantityStudent < maxStudent && x.EntityId != classByStudentId.EntityId);
                        string jsonData = JsonConvert.SerializeObject(classForStudent);
                        return Json(new
                        {
                            StatusCode = 200,
                            Data = jsonData
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            StatusCode = 400,
                            Message = "Some things went wrong get class for student!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new
            {
                StatusCode = 400,
                Message = "Student Id is not correct!"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ChangeClassForStudent(int classId = 0, int studentId = 0)
        {
            if (classId != 0 && studentId != 0)
            {
                var maxStudentInClass = db.CoreConfigData.FirstOrDefault(x => x.Code == "maximum_student_in_class");
                var maxStudentIsInt = Int32.TryParse(maxStudentInClass.Value, out int maxStudent);
                var classByClassId = db.Class.Find(classId);
                var currentStudent = db.Student.Find(studentId);
                var oldClassByStudentId = db.Class.FirstOrDefault(x => x.EntityId == currentStudent.ClassId);
                oldClassByStudentId = db.Class.Find(oldClassByStudentId.EntityId);
                if (classByClassId != null && currentStudent != null && maxStudentIsInt)
                {
                    try
                    {
                        if (classByClassId.QuantityStudent >= maxStudent)
                        {
                            return Json(new
                            {
                                StatusCode = 400,
                                Message = "Class is full students!"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            currentStudent.ClassId = classId;
                            oldClassByStudentId.QuantityStudent -= 1;
                            classByClassId.QuantityStudent += 1;
                            db.SaveChanges();
                            return Json(new
                            {
                                StatusCode = 200,
                                Message = "Change class for student success!"
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception)
                    {
                        return Json(new
                        {
                            StatusCode = 400,
                            Message = "Some things went wrong while change class for student!"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new
                    {
                        StatusCode = 400,
                        Message = "Some things went wrong while change class for student!"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                StatusCode = 400,
                Message = "Student Id or Class Id is not correct!"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}