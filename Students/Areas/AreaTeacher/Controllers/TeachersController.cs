using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Students.Domain.Entities;
using Students.Models;
using Students.Persistence.DbContexts;
using Students.Services.Repositories;
using Students.Services.Service;

namespace Students.Areas.AreaTeacher.Controllers
{
    [Area("AreaTeacher")]
    public class TeachersController : Controller
    {
        private readonly StudentDbContext _context;
        private ITeacher teacherRepository;
        private ICourseStudent courseStudentRepository;
        private IStudent studentRepository;
        private int GlobalCoursId  ;
        public TeachersController(StudentDbContext context)
        {
            _context = context;
            teacherRepository = new TeacherRepository(_context);
            courseStudentRepository = new CourseStudentRepository(_context);
            studentRepository= new StudentRepository(_context);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.courseStudentRepository != null)
                {
                    this.courseStudentRepository.Dispose();
                    this.courseStudentRepository = null;
                }
                if (this.teacherRepository != null)
                {
                    this.teacherRepository.Dispose();
                    this.teacherRepository = null;
                }
                if (this.studentRepository != null)
                {
                    this.studentRepository.Dispose();
                    this.studentRepository = null;
                }
            }

            base.Dispose(disposing);
        }
        public async Task<IActionResult> Index()
        {
            // return View(teacherRepository.GetAllTeachers());

            // var t = teacherRepository.GetIdByUserName(GlobalVariable.UserName);
            // var uy = 3;

            if (teacherRepository.GetIdByUserName(GlobalVariable.UserName) == 0)
            {
                TempData["notExistProfileIndex"] = " ابتدا پروفایل را تکمیل بفرمایید ";
                return RedirectToAction("Index", "Home", new { area = "AreaTeacher" });

            }

            var SelectedTeacheId = teacherRepository.GetIdByUserName(GlobalVariable.UserName);
            //  var u = courseStudentRepository.GetCourseStudentByTeacheId(SelectedTeacheId);
            if (teacherRepository.GetTeacherById(SelectedTeacheId) == null)
            {
                TempData["CourseStudentNull"] = " هیچ کلاسی به شما اختصاص داده نشده است  ";
                return RedirectToAction("Index", "Home", new { area = "AreaTeacher" });
                //  return RedirectToAction("Create");
            }
            return View(teacherRepository.GetTeacherById(SelectedTeacheId).Courses);
        }


        // GET: AreaTeacher/Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AreaTeacher/Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Name,Mob")] Teacher teacher)
        {
            teacher.UserName = GlobalVariable.UserName;
            if (teacherRepository.IExisitUserName(GlobalVariable.UserName))
            {
                TempData["MessageStudentTeacher"] = "نام کاربری قبلا ثبت شده است و فقط با یوزر admin  قابل ویرایش است ";

                return RedirectToAction("Create", "Teachers", new { area = "AreaTeacher" });
            }

            if (!ModelState.IsValid)
            {
                teacherRepository.InsertTeacher(teacher);
                teacherRepository.Save();
                return RedirectToAction("Index", "Home", new { area = "AreaTeacher" });
                // return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }


        public async Task<IActionResult> Edit(int? id)
        {

            this.GlobalCoursId = id.Value;
            var Students = courseStudentRepository.GetStudentsByCoursId(id.Value);
            if (Students == null)
            {
                return NotFound();
            }
            var MyStudentNomre = Students.Select(p => new StudentNomreViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Age = p.Age,
                NationalCode = p.NationalCode,
                UserName = p.UserName,
                Nomre = "0",
                CourseId=id.Value,
                
            });

             var newList = new List<StudentNomreViewModel>();
            foreach (var item in MyStudentNomre)
            {
                var MCoursStudent = courseStudentRepository.GetCourseStudentIdByStudentIdAndCoursId(item.Id,id.Value);
                  item.Nomre = courseStudentRepository.GetCourseStudentById(MCoursStudent).Nomre;
                newList.Add(item);
            }
            return View(newList);
        }

        // POST: Admin/Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        public async Task<IActionResult> EditNomre(int? id ,string SpecifiedName)
        {


   
            //var studentNomreViewModel = courseStudentRepository.GetCourseStudentById(id.Value , );
             var courseStudent =  courseStudentRepository.GetCourseStudentIdByStudentIdAndCoursId(id.Value, Convert.ToInt32(SpecifiedName));
            var myCoursStudent = courseStudentRepository.GetCourseStudentById(courseStudent);
            var student = studentRepository.GetStudentById(id.Value);
            var studentNomreViewModel = new StudentNomreViewModel()
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                NationalCode = student.NationalCode,
                UserName = student.UserName,
                Nomre = myCoursStudent.Nomre,
                CourseId = Convert.ToInt32(SpecifiedName)
            };



            if (studentNomreViewModel == null)
            {
                return NotFound();
            }

            return View(studentNomreViewModel);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNomre(int id, [Bind("Id,UserName,Name,Mob,Nomre,CourseId")] StudentNomreViewModel studentNomreViewModel)
        {
            var t = 9;
           
            if (id != studentNomreViewModel.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    int myStudentId = studentNomreViewModel.Id;
                    int mycourseId = studentNomreViewModel.CourseId;
                    var updateId = courseStudentRepository.GetCourseStudentIdByStudentIdAndCoursId(myStudentId, mycourseId);
                  //  courseStudentRepository.Dispose();
                    var myCoursStudent = new CourseStudent()
                    {
                        Id = updateId,
                        CourseId = mycourseId,
                        StudentId = myStudentId,
                        Nomre = studentNomreViewModel.Nomre
                    };
                    courseStudentRepository.UpdateCourseStudent(myCoursStudent);
                    courseStudentRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction(nameof(Index));
           
            }
            return View(studentNomreViewModel);
        }




    }
}
