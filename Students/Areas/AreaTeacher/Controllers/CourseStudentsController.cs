﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Students.Domain.Entities;
using Students.Persistence.DbContexts;
using Students.Services.Repositories;
using Students.Services.Service;

namespace Students.Areas.AreaStudent.Controllers
{
    [Area("AreaTeacher")]
    public class CourseStudentsController : Controller
    {
        private readonly StudentDbContext _context;
        private ICourseStudent courseStudentRepository;
        private ITeacher teacherRepository;
        private ICourse courseRepository;
        private IStudent studentRepository;
        public CourseStudentsController(StudentDbContext context)
        {
            _context = context;
            courseStudentRepository = new CourseStudentRepository(_context);
            teacherRepository = new TeacherRepository(_context);
            //courseRepository = new CourseRepository(_context);
           // studentRepository = new StudentRepository(_context);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.teacherRepository != null)
                {
                    this.teacherRepository.Dispose();
                    this.teacherRepository = null;
                }
            }

            base.Dispose(disposing);
        }


        // GET: AreaStudent/CourseStudents
        public async Task<IActionResult> Index()
        {
           
      
            var t = teacherRepository.GetIdByUserName(GlobalVariable.UserName);
            var uy = 3;

            if (teacherRepository.GetIdByUserName(GlobalVariable.UserName) == 0)
            {
                TempData["notExistProfileIndex"] = " ابتدا پروفایل را تکمیل بفرمایید ";
                return RedirectToAction("Index", "Home", new { area = "AreaTeacher" });

            }

            var SelectedTeacheId= teacherRepository.GetIdByUserName(GlobalVariable.UserName);
            var u = courseStudentRepository.GetCourseStudentByTeacheId(SelectedTeacheId);
            if (courseStudentRepository.GetCourseStudentByTeacheId(SelectedTeacheId) ==null)
            {
                TempData["CourseStudentNull"] = " هیچ کلاسی به شما اختصاص داده نشده است  ";
                return RedirectToAction("Index", "Home", new { area = "AreaTeacher" });
              //  return RedirectToAction("Create");
            }
            return View(courseStudentRepository.GetCourseStudentByTeacheId(SelectedTeacheId));
        }

        //// GET: AreaStudent/CourseStudents/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{


        //    var courseStudent = courseStudentRepository.GetCourseStudentById(id.Value);
        //    if (courseStudent == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(courseStudent);
        //}

        //// GET: AreaStudent/CourseStudents/Create
        //public IActionResult Create()
        //{
        //    ViewData["StudentId"] = new SelectList(studentRepository.GetAllStudents(), "Id", "Name");
        //    ViewData["CourseId"] = new SelectList(courseRepository.GetAllCourses(), "Id", "Title");
        //    return View();
        //}

        //// POST: AreaStudent/CourseStudents/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,CourseId,StudentId")] CourseStudent courseStudent)
        //{
        
        //    if (studentRepository.GetIdByUserName(GlobalVariable.UserName)==0)
        //    {
        //        TempData["notExistProfile"] = " ابتدا پروفایل را تکمیل بفرمایید ";
        //        ViewData["StudentId"] = new SelectList(studentRepository.GetAllStudents(), "Id", "Name", courseStudent.StudentId);
        //        ViewData["CourseId"] = new SelectList(courseRepository.GetAllCourses(), "Id", "Title", courseStudent.CourseId);

        //        return View(courseStudent);

        //    }


        //    var SelectedstudentId = studentRepository.GetIdByUserName(GlobalVariable.UserName);
        //    courseStudent.StudentId = SelectedstudentId;
        //    if (courseStudentRepository.IsExistCourseIdCourseId(courseStudent.CourseId, courseStudent.StudentId))
      
        //    {

        //        TempData["IsExistBefir"] = " این کلاس برای شما قبلا ثبت شده است ";
        //        ViewData["StudentId"] = new SelectList(studentRepository.GetAllStudents(), "Id", "Name", courseStudent.StudentId);
        //        ViewData["CourseId"] = new SelectList(courseRepository.GetAllCourses(), "Id", "Title", courseStudent.CourseId);

        //        return View(courseStudent);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        courseStudentRepository.InsertCourseStudent(courseStudent);
        //        courseStudentRepository.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["StudentId"] = new SelectList(studentRepository.GetAllStudents(), "Id", "Name", courseStudent.StudentId);
        //    ViewData["CourseId"] = new SelectList(courseRepository.GetAllCourses(), "Id", "Title", courseStudent.CourseId);
        //    return View(courseStudent);
        //}

        //// GET: AreaStudent/CourseStudents/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{


        //    var courseStudent = courseStudentRepository.GetCourseStudentById(id.Value);
        //    if (courseStudent == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["StudentId"] = new SelectList(studentRepository.GetStudentByIUserName(GlobalVariable.UserName), "Id", "Name", courseStudent.StudentId);
        //    ViewData["CourseId"] = new SelectList(courseRepository.GetAllCourses(), "Id", "Title", courseStudent.CourseId);
        //    return View(courseStudent);
        //}

        //// POST: AreaStudent/CourseStudents/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,StudentId")] CourseStudent courseStudent)
        //{
        //    var SelectedstudentId = studentRepository.GetIdByUserName(GlobalVariable.UserName);
        //    courseStudent.StudentId = SelectedstudentId;
        //    if (id != courseStudent.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            courseStudentRepository.UpdateCourseStudent(courseStudent);
        //            courseStudentRepository.Save();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
                 
        //                throw;
                    
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["StudentId"] = new SelectList(studentRepository.GetAllStudents(), "Id", "Name", courseStudent.StudentId);
        //    ViewData["CourseId"] = new SelectList(courseRepository.GetAllCourses(), "Id", "Title", courseStudent.CourseId);
        //    return View(courseStudent);
        //}

        //// GET: AreaStudent/CourseStudents/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{


        //    var courseStudent = courseStudentRepository.GetCourseStudentById(id.Value);
        //    if (courseStudent == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(courseStudent);
        //}

        //// POST: AreaStudent/CourseStudents/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{

        //    var courseStudent = courseStudentRepository.GetCourseStudentById(id);
        //    if (courseStudent != null)
        //    {
        //        courseStudentRepository.DeleteCourseStudent(courseStudent);
        //    }

        //    courseStudentRepository.Save();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CourseStudentExists(int id)
        //{
        //  return (_context.CourseStudents?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
