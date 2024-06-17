using LearningSystem.DbContexts;
using LearningSystem.Models;
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
  [Route("api/Student")]
  [ApiController]
  public class StudentDashboard : ControllerBase
  {
    private IConfiguration _config;
    private string ConnStr;

    public StudentDashboard(IConfiguration Configuration)
    {
      _config = Configuration;
      ConnStr = _config.GetConnectionString("MySqlConnection");
    }


    [HttpPost]
    //// [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("enrollmentForStudent")]
    public IActionResult GetEnrollmentForStudent([FromBody] Enrollments enroll)
    {
      var enrollGrade = DBenrollments.GetEnrollmentByStudentID(ConnStr, enroll.Student_id.StudentID.ToString());
      return Ok(enrollGrade);
    }

    [HttpPost]
    //// [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("SectionsStudent")]
    public IActionResult GetSectionsForStudent([FromBody] Sections Section)
    {
      var Sections = DBCourse.GetSectionsByCourseID(ConnStr, Section.Course_ID.courseID.ToString());
      return Ok(Sections);
    }

    [HttpPost]
    //// [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("StudentAssigment")]
    public IActionResult GetAssigmentForStudent([FromBody] assignment Assigment)
    {
      var CurrentAssigment = DBCourse.GetAssigmentById(ConnStr, Assigment.assiID.ToString());
      return Ok(CurrentAssigment);
    }


    [HttpPost]
    //// [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("submissioncontent")]
    public IActionResult Getsubmisson([FromBody] assignment Assigment)
    {
      var CurrentAssigment = DBCourse.GetsubmissionByAssigmentId(ConnStr, Assigment.assiID.ToString());
      return Ok(CurrentAssigment);
    }






    [HttpGet]
    //// [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("Assigment")]
    public IActionResult GetAssigmentForTeacher()
    {
      var AllAssigment = DBCourse.GetAssigmentForStudent(ConnStr);
      return Ok(AllAssigment);
    }

    [HttpGet]
    //// [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("GradesStudent")]
    public IActionResult GetGradesForStudent()
    {
      var GradesForStudent = DBCourse.GetGradeForStudent(ConnStr);
      return Ok(GradesForStudent);
    }



    [HttpPost]
    //// [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("ContentAssigment")]
    public IActionResult GetAssigmentcontent([FromBody] assignment Assigment)
    {
      var CurrentAssigment = DBCourse.GetContentFiles(ConnStr, Assigment.assiID.ToString());
      return Ok(CurrentAssigment);
    }


    [HttpPost]
    //// [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("NewSubmissionContent")]
    public IActionResult SetNewSubmission([FromBody] Content submissionContent)
    {
      DBCourse.AddNewSubmissionContent(ConnStr, submissionContent.Descrbtion.ToString(), submissionContent.attachment, submissionContent.Assigment_ID.assiID.ToString());
      return Ok("The file uploaded!!!!!!!");
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
      var filePath = Path.Combine("submission", file.FileName);

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
      var filePath = Path.Combine("submission", fileName);

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

    [HttpPost]
    //// [Authorize]
    ////[ValidateCustomHeader("AdminAccount", "A")]
    [Route("updateStatus")]
    public IActionResult updateSubmissionState([FromBody] assignment submissionContent)
    {
      DBCourse.UpdateSubmissionStatus(ConnStr, submissionContent.Submission_Status, submissionContent.assiID.ToString());
      return Ok("the status already updated!!!!!");
    }



  }
}
