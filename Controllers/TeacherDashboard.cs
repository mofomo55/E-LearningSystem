using LearningSystem.DbContexts;
using LearningSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Controllers
{
  [Route("api/Teacher")]
  [ApiController]
  public class TeacherDashboard : ControllerBase
  {
    private IConfiguration _config;
    private string ConnStr;

    public TeacherDashboard(IConfiguration Configuration)
    {
      _config = Configuration;
      ConnStr = _config.GetConnectionString("MySqlConnection");
    }

    [HttpPost]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Courses")]
    public IActionResult GetAllCoursesForTeacher([FromBody] Teacher TeacherID)
    {
      var AllCourses = DBCourse.GetAllCoursesForOneTeacher(ConnStr, TeacherID.TeacherID.ToString());
      return Ok(AllCourses);
    }

    [HttpGet]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Assigment")]
    public IActionResult GetAssigmentForTeacher()
    {
      var AllAssigment = DBCourse.GetAssigmentForTeacher(ConnStr);
      return Ok(AllAssigment);
    }

    [HttpGet]
   [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Sections")]
    public IActionResult GetSectionsForTeacher()
    {
      var AllSections = DBCourse.GetSectionsForTeacher(ConnStr);
      return Ok(AllSections);
    }

    [HttpGet]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("GradesKeys")]
    public IActionResult GetGradesKeys()
    {
      var AllGradesKeys = DBCourse.GetGradeKeys(ConnStr);
      return Ok(AllGradesKeys);
    }

    [HttpGet]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Submission")]
    public IActionResult GetSubmission()
    {
      var AllSubmission = DBCourse.GetAllSubmission(ConnStr);
      return Ok(AllSubmission);
    }

    [HttpPost]
    [Route("FileUpload")]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
      if (file == null || file.Length == 0)
      {
        return BadRequest("File not provided or empty.");
      }

      // You can process the file here, for example, save it to the server.
      // Ensure to validate and sanitize the uploaded file data.

      // Example: Save the file to a specific directory
      var filePath = Path.Combine("Attachment", file.FileName);

      using (var stream = new FileStream(filePath, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }

      return Ok(new { FilePath = filePath });
    }

    [HttpGet("{fileName}")]
    public IActionResult DownloadFile(string fileName)
    {
      // Construct the full path to the file on your server
      var filePath = Path.Combine("Attachment", fileName);

      if (System.IO.File.Exists(filePath))
      {
        // Read the file into a byte array
        var fileBytes = System.IO.File.ReadAllBytes(filePath);

        // Determine the file's content type (MIME type)
        var contentType = "application/octet-stream"; // You may want to set the appropriate content type based on the file type.

        // Send the file as a response with the appropriate headers
        return File(fileBytes, contentType, fileName);
      }
      else
      {
        return NotFound(); // Return a 404 Not Found response if the file doesn't exist.
      }
    }

    [HttpGet]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Contents")]
    public IActionResult GetFilesContent(int AssigmentID)
    {
      var FilesContent = DBCourse.GetContentFiles(ConnStr, AssigmentID.ToString());
      return Ok(FilesContent);
    }

    [HttpPost]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("AddFile")]
    public IActionResult SetFile([FromBody] Content Content)
    {
       DBCourse.AddFileAttachemnt(ConnStr, Content.Assigment_ID.assiID.ToString(), Content.attachment.ToString());
      return Ok("The file uploaded!!!!!!!");
    }


    [HttpPost]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("updateAssigmentiInContent")]
    public IActionResult UpdateDescrbtionFile([FromBody] assignment Content)
    {
      DBCourse.UpdateInContent(ConnStr, Content.assiID.ToString(), Content.Descrbtion.ToString());
      return Ok("The file uploaded!!!!!!!");
    }

    [HttpPost]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("NewAssigment")]
    public IActionResult AddAssigment([FromBody] assignment Assigment)
    {
      DBCourse.AddNewAssigment(ConnStr, Assigment.Course_id.courseID.ToString(), Assigment.title,Assigment.StartDate,Assigment.EndDate,Assigment.Max_Grading.ToString(),Assigment.Typee,"0");
      return Ok("The file uploaded!!!!!!!");
    }


    [HttpPost]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("NewSection")]
    public IActionResult AddSection([FromBody] Sections Section)
    {
      DBCourse.AddNewGradeSection(ConnStr, Section.Title, Section.Course_ID.courseID.ToString(), Section.Percent.ToString());
      return Ok("The file uploaded!!!!!!!");
    }

    [HttpPost]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("AssigmentForSelectList")]
    public IActionResult GetAllAssigmentForList([FromBody] assignment Assigment)
    {
    var AssigmentSelect =  DBCourse.GetAssigmentForSelectList(ConnStr, Assigment.Course_id.courseID.ToString());
      return Ok(AssigmentSelect);
    }


    [HttpPost]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("NewGradeKey")]
    public IActionResult AddGradeKey([FromBody] GradesKey GradeKey)
    {
      DBCourse.AddNewGradeKey(ConnStr, GradeKey.Section_ID.SectionsID.ToString(), GradeKey.Assigment_ID.assiID.ToString(), GradeKey.gDate);
      return Ok("The file uploaded!!!!!!!");
    }

    [HttpPost]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("enrollmentForGrade")]
    public IActionResult GetEnrollmentForGrade([FromBody] Enrollments enroll)
    {
      var enrollGrade = DBenrollments.GetEnrollmentsByCourseID(ConnStr, enroll.Course_id.courseID.ToString());
      return Ok(enrollGrade);
    }


    [HttpPost]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("AssigmentById")]
    public IActionResult GetAssigmentById([FromBody] assignment Assigment)
    {
      var AssigmentByID = DBCourse.GetAssigmentById(ConnStr, Assigment.assiID.ToString());
      return Ok(AssigmentByID);
    }

    [HttpPost]
    [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("NewGrade")]
    public IActionResult SetNewGrade([FromBody] Grade Grade)
    {
       DBCourse.AddNewGrade(ConnStr, Grade.Assigment_ID.assiID.ToString(), Grade.enrollment_ID.EnrollmentsID.ToString(), Grade.Score.ToString(),Grade.gradDate,Grade.Section_ID.SectionsID.ToString());
      return Ok("Grade Has beed added!!!!!!");
    }
  }
}
