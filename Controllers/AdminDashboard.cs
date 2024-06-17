using LearningSystem.DbContexts;
using LearningSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Controllers
{
  [Route("api/Admin")]
  [ApiController]
  public class AdminDashboard : ControllerBase
  {

    private IConfiguration _config;
    private string ConnStr;

    public AdminDashboard(IConfiguration Configuration)
    {
      _config = Configuration;
      ConnStr = _config.GetConnectionString("MySqlConnection");
    }


    [HttpGet]
    [Authorize]
    //[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Users")]
    public IActionResult GetAllUsersList()
    {
      var AllUsers = DBUser.GetAllUsers(ConnStr);
      return Ok(AllUsers);
    }


    [HttpPost]
     [Authorize]
    //[ValidateCustomHeader("AdminAccount", "A")]
    [Route("UsersSelectList")]
    public IActionResult GetUsersForSelectList([FromBody]User UserRole)
    {
      var Role = UserRole.Role;
      var AllUsers = DBUser.GetAllUsers(ConnStr, Role);
      return Ok(AllUsers);
    }


    [HttpPost]
    [Authorize]
    
    [Route("NewUser")]
    public IActionResult NewUser([FromBody] User UserInfo)
    {
      DBUser.AddNewUser(ConnStr, UserInfo.UserName, UserInfo.FirstName, UserInfo.LastName, UserInfo.Email, "S", UserInfo.ActiveStatus, UserInfo.Picture, UserInfo.password);
      return Ok("the new User is added successfully!!!!!!");
    }


    [HttpPost]
      [Authorize]

    [Route("NewSubject")]
    public IActionResult NewSubject([FromBody] Subject UserInfo)
    {
      DBSubjects.AddNewSubject(ConnStr, UserInfo.SubjectName, UserInfo.Desc);
      return Ok("the new Subject is added successfully!!!!!!");
    }



    [HttpPost]
    //  [Authorize]

    [Route("NewTeacher")]
    public IActionResult NewTeacher([FromBody]  Teacher techerInfo)
    {
      DBTeacher.AddNewTeacher(ConnStr, techerInfo.UserID.UserID.ToString(), techerInfo.BirthDay);
      return Ok("the new Teacher is added successfully!!!!!!");
    }

    [HttpPost]
      [Authorize]

    [Route("NewStudent")]
    public IActionResult NewStudent([FromBody] Student StudentInfo)
    {
      DBStudent.AddNewStudent(ConnStr, StudentInfo.UserID.UserID.ToString(), StudentInfo.BirthDay);
      return Ok("the new Student is added successfully!!!!!!");
    }


    [HttpPost]
    //  [Authorize]

    [Route("NewAdmin")]
    public IActionResult NewAdmin([FromBody] Admin AdminInfo)
    {
      DBAdmin.AddNewAdmin(ConnStr, AdminInfo.UserID.UserID.ToString(), AdminInfo.BirthDay);
      return Ok("the new Admin is added successfully!!!!!!");
    }



    [HttpPost]
     [Authorize]

    [Route("NewCourse")]
    public IActionResult NewCourse([FromBody] Course CourseInfo)
    {
      DBCourse.AddNewCourse(ConnStr, CourseInfo.CourseName, CourseInfo.Sub_ID.SubID.ToString(), CourseInfo.Describtion, CourseInfo.Teacher_ID.TeacherID.ToString(), CourseInfo.instID.instID.ToString(), CourseInfo.StartDate, CourseInfo.EndDate);
      return Ok("the new Course is added successfully!!!!!!");
    }



    [HttpPost]
      [Authorize]
    [Route("NewEnrollment")]
    public IActionResult NewEnrollment([FromBody] Enrollments EnroInfo)
    {
      DBenrollments.AddNewEnrollment(ConnStr, EnroInfo.Student_id.StudentID.ToString(), EnroInfo.Course_id.courseID.ToString(), EnroInfo.eDate);
      return Ok("the new Enrollment is added successfully!!!!!!");
    }






    [HttpPost]
      [Authorize]

    [Route("DeleteUser")]
    public IActionResult DeleteUser([FromBody] User UserInfo)
    {
      DBUser.DeleteUser(ConnStr, UserInfo.UserID.ToString());
      return Ok("the User was removed!!!!!!");
    }


    [HttpPost]
    [Authorize]

    [Route("DeleteSubject")]
    public IActionResult DeleteSubject([FromBody] Subject subInfo)
    {
      DBSubjects.DeleteSubject(ConnStr, subInfo.SubID.ToString());
      return Ok("the Subject was removed!!!!!!");
    }





    [HttpGet]
    // [Authorize]
    //[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Teacher")]
    public IActionResult GetAllTeacherList()
    {
      var AllTeacher = DBTeacher.GetAllTeacher(ConnStr);
      return Ok(AllTeacher);
    }

    [HttpGet]
    // [Authorize]
    //[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Student")]
    public IActionResult GetAllStudentList()
    {
      var AllStudent = DBStudent.GetAllStudent(ConnStr);
      return Ok(AllStudent);
    }

    [HttpGet]
    // [Authorize]
    //[ValidateCustomHeader("AdminAccount", "A")]
    [Route("AllAdmin")]
    public IActionResult GetAllAdminList()
    {
      var AllAdmin = DBAdmin.GetAllAdmins(ConnStr);
      return Ok(AllAdmin);
    }

    [HttpGet]
    // [Authorize]
    //[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Courses")]
    public IActionResult GetAllCourses()
    {
      var AllCourses = DBCourse.GetAllCourses(ConnStr);
      return Ok(AllCourses);
    }


    [HttpGet]
    // [Authorize]
    //[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Subjects")]
    public IActionResult GetAllSubjects()
    {
      var AllSub = DBSubjects.GetAllSubject(ConnStr);
      return Ok(AllSub);
    }

    [HttpGet]
    // [Authorize]
    //[ValidateCustomHeader("AdminAccount", "A")]
    [Route("enrollments")]
    public IActionResult Getenrollments()
    {
      var Allenro = DBenrollments.GetAllEnrollments(ConnStr);
      return Ok(Allenro);
    }





  }
}
