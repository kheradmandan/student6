using System;
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
    [Area("AreaStudent")]
    public class CoursesController : Controller
    {
        private readonly StudentDbContext _context;
        private ICourse courseRepository;
        private ICI_FieldCourse cI_FieldCourseRepository;
        private ITeacher teacherRepository;
        public CoursesController(StudentDbContext context)
        {
            _context = context;
            courseRepository = new CourseRepository(_context);
            cI_FieldCourseRepository = new CI_FieldCourseRepository(_context);
            teacherRepository = new TeacherRepository(_context);
        }

        // GET: AreaStudent/Courses
        public async Task<IActionResult> Index()
        {
            
            return View(courseRepository.GetAllCourses());
        }

      
    }
}
