using LearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.DbContexts
{
  public class DBCourse
  {
    public static List<Course> GetAllCourses(string connstr)
    {

      List<Course> CoursesList = new List<Course>();

      string query = "SELECT co.CourseID , co.CourseName , co.Sub_ID , co.Teacher_Id, sb.Name as Subject , te.User_ID , Us.UserName As Teacher FROM course as co" + "\r\n";
      query += "inner join subjects as sb on co.Sub_ID = sb.SubID" + "\r\n";
      query += "inner join teacher as te on co.Teacher_Id = te.TeacherID" + "\r\n";
      query += "inner join user as Us on te.User_ID = Us.UserID";
      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          Teacher Tea = new Teacher();
          Subject Sub = new Subject();
          User UserInfo = new User();
          Course cms = new Course();
          UserInfo.UserName = dtRow["Teacher"].ToString();
          Tea.UserID = UserInfo;
          Tea.TeacherID = Convert.ToInt32(dtRow["Teacher_Id"].ToString());
          Sub.SubjectName = dtRow["Subject"].ToString();
          cms.Teacher_ID = Tea;
          cms.Sub_ID = Sub;
          cms.courseID = Convert.ToInt32(dtRow["CourseID"].ToString());
          cms.CourseName = dtRow["CourseName"].ToString();

          CoursesList.Add(cms);
        }
      }
      return CoursesList;
    }


    public static List<Course> GetAllCoursesForOneTeacher(string connstr, string TeacherID)
    {

      List<Course> CoursesList = new List<Course>();

      string query = "SELECT co.CourseID , co.CourseName , co.Sub_ID , co.Teacher_Id, sb.Name as Subject , te.User_ID , Us.UserName As Teacher FROM course as co" + "\r\n";
      query += "inner join subjects as sb on co.Sub_ID = sb.SubID" + "\r\n";
      query += "inner join teacher as te on co.Teacher_Id = te.TeacherID" + "\r\n";
      query += "inner join user as Us on te.User_ID = Us.UserID" + "\r\n";
      query += "where co.Teacher_Id =" + "'" + TeacherID + "'";
      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          Teacher Tea = new Teacher();
          Subject Sub = new Subject();
          User UserInfo = new User();
          Course cms = new Course();
          UserInfo.UserName = dtRow["Teacher"].ToString();
          Tea.UserID = UserInfo;
          Tea.TeacherID = Convert.ToInt32(dtRow["Teacher_Id"].ToString());
          Sub.SubjectName = dtRow["Subject"].ToString();
          cms.Teacher_ID = Tea;
          cms.Sub_ID = Sub;
          cms.courseID = Convert.ToInt32(dtRow["CourseID"].ToString());
          cms.CourseName = dtRow["CourseName"].ToString();

          CoursesList.Add(cms);
        }
      }
      return CoursesList;
    }



    public static List<submission> GetAllSubmission(string connstr)
    {

      List<submission> SubmissionList = new List<submission>();

      string query = "SELECT sb.Submission_ContentID, sb.Student_ID, sb.Assigment_ID, sb.attachment, sb.Descrbtion ," + "\r\n";
      query += "co.CourseID, st.StudentID,st.User_id, us.UserName, ass.AssignmentID, ass.Tite, ass.MaxScore, ass.Course_ID  from submission_content as sb" + "\r\n";
      query += "inner join student as st on sb.Student_ID = st.StudentID" + "\r\n";
      query += "inner join assignment as ass on sb.Assigment_ID = ass.AssignmentID" + "\r\n";
      query += "inner join user as us on st.User_id = us.UserID" + "\r\n";
      query += "inner join course as co on ass.Course_ID = co.CourseID";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
         
          User UserInfo = new User();
          Student SutdentInfo = new Student();
          assignment AssigmentInfo = new assignment();
          Course courseInfo = new Course();
         
          submission cms = new submission();
           courseInfo.courseID = Convert.ToInt32(dtRow["CourseID"].ToString());
           UserInfo.UserName = dtRow["UserName"].ToString();
           UserInfo.UserID = Convert.ToInt32(dtRow["User_id"].ToString());
           AssigmentInfo.assiID = Convert.ToInt32(dtRow["AssignmentID"].ToString());
           AssigmentInfo.title = dtRow["Tite"].ToString();
           AssigmentInfo.Course_id = courseInfo;
           SutdentInfo.UserID = UserInfo;
          SutdentInfo.StudentID = Convert.ToInt32(dtRow["StudentID"].ToString());
          cms.SubmissionID = Convert.ToInt32(dtRow["Submission_ContentID"].ToString());
           cms.attachment = dtRow["attachment"].ToString();
          cms.Student_ID = SutdentInfo;
           cms.Assigment_ID = AssigmentInfo;

          SubmissionList.Add(cms);
        }
      }
      return SubmissionList;
    }




    public static List<assignment> GetAssigmentForTeacher(string connstr)
    {

      List<assignment> AssigmentList = new List<assignment>();

      string query = "SELECT AssignmentID,submisson_Status, Course_ID, Tite, Descrbtion, StartDate, DueDate, MaxScore  FROM assignment";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          DateTime StartDate = new DateTime();
          DateTime EndDate = new DateTime();
          assignment cms = new assignment();
          Course courseInfo = new Course();
          cms.assiID = Convert.ToInt32(dtRow["AssignmentID"].ToString());
          courseInfo.courseID = Convert.ToInt32(dtRow["Course_ID"].ToString());
          cms.Course_id = courseInfo;
          if(dtRow["StartDate"] is DateTime)
          {
            StartDate = (DateTime)dtRow["StartDate"];
          }

          if (dtRow["DueDate"] is DateTime)
          {
            EndDate = (DateTime)dtRow["DueDate"];
          }
          
          cms.title = dtRow["Tite"].ToString();
          cms.Descrbtion = dtRow["Descrbtion"].ToString();
          cms.StartDate = StartDate.ToShortDateString();
          cms.EndDate = EndDate.ToShortDateString();
          cms.Submission_Status = dtRow["submisson_Status"].ToString();
          cms.Max_Grading = Convert.ToDouble(dtRow["MaxScore"].ToString());

          AssigmentList.Add(cms);
        }
      }
      return AssigmentList;
    }


    public static List<assignment> GetAssigmentForStudent(string connstr)
    {

      List<assignment> AssigmentList = new List<assignment>();

      string query = "SELECT AssignmentID,submisson_Status, Course_ID, Tite, Descrbtion, StartDate, DueDate, MaxScore  FROM assignment where Typee = 'Task'";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          DateTime StartDate = new DateTime();
          DateTime EndDate = new DateTime();
          assignment cms = new assignment();
          Course courseInfo = new Course();
          cms.assiID = Convert.ToInt32(dtRow["AssignmentID"].ToString());
          courseInfo.courseID = Convert.ToInt32(dtRow["Course_ID"].ToString());
          cms.Course_id = courseInfo;
          if (dtRow["StartDate"] is DateTime)
          {
            StartDate = (DateTime)dtRow["StartDate"];
          }

          if (dtRow["DueDate"] is DateTime)
          {
            EndDate = (DateTime)dtRow["DueDate"];
          }

          cms.title = dtRow["Tite"].ToString();
          cms.Descrbtion = dtRow["Descrbtion"].ToString();
          cms.StartDate = StartDate.ToShortDateString();
          cms.EndDate = EndDate.ToShortDateString();
          cms.Submission_Status = dtRow["submisson_Status"].ToString();
          cms.Max_Grading = Convert.ToDouble(dtRow["MaxScore"].ToString());

          AssigmentList.Add(cms);
        }
      }
      return AssigmentList;
    }

    public static List<assignment> GetAssigmentForSelectList(string connstr, string Course_ID)
    {

      List<assignment> AssigmentList = new List<assignment>();

      string query = "SELECT AssignmentID, Course_ID, Tite, Descrbtion, StartDate, DueDate, MaxScore  FROM assignment where Course_ID=" + "'" + Course_ID + "'";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          DateTime StartDate = new DateTime();
          DateTime EndDate = new DateTime();
          assignment cms = new assignment();
          Course courseInfo = new Course();
          cms.assiID = Convert.ToInt32(dtRow["AssignmentID"].ToString());
          courseInfo.courseID = Convert.ToInt32(dtRow["Course_ID"].ToString());
          cms.Course_id = courseInfo;
          if (dtRow["StartDate"] is DateTime)
          {
            StartDate = (DateTime)dtRow["StartDate"];
          }

          if (dtRow["DueDate"] is DateTime)
          {
            EndDate = (DateTime)dtRow["DueDate"];
          }

          cms.title = dtRow["Tite"].ToString();
          cms.Descrbtion = dtRow["Descrbtion"].ToString();
          cms.StartDate = StartDate.ToShortDateString();
          cms.EndDate = EndDate.ToShortDateString();

          cms.Max_Grading = Convert.ToDouble(dtRow["MaxScore"].ToString());

          AssigmentList.Add(cms);
        }
      }
      return AssigmentList;
    }

    public static List<Content> GetContentFiles(string connstr,string AssigmentID )
    {

      List<Content> ContentFiles = new List<Content>();

      string query = "SELECT Assigment_ContentID, attachment FROM assigment_content where Assigment_ID =" + "'" + AssigmentID + "'";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          Content cms = new Content();
          cms.ContentID = Convert.ToInt32(dtRow["Assigment_ContentID"].ToString());
          cms.attachment = dtRow["attachment"].ToString();
          ContentFiles.Add(cms);
        }
      }
      return ContentFiles;
    }

    public static List<assignment> GetAssigmentById(string connstr, string AssigmentID)
    {

      List<assignment> Assigment = new List<assignment>();

      string query = "SELECT * FROM assignment where AssignmentID =" + "'" + AssigmentID + "'";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          assignment cms = new assignment();
          cms.assiID = Convert.ToInt32(dtRow["AssignmentID"].ToString());
          cms.title = dtRow["Tite"].ToString();
          cms.Max_Grading = Convert.ToDouble(dtRow["MaxScore"].ToString());
          cms.StartDate = dtRow["StartDate"].ToString();
          cms.EndDate = dtRow["DueDate"].ToString();
          cms.Submission_Status = dtRow["submisson_Status"].ToString();
          cms.Descrbtion = dtRow["Descrbtion"].ToString();
          cms.Typee = dtRow["Typee"].ToString();
          Assigment.Add(cms);
        }
      }
      return Assigment;
    }


    public static List<Content> GetsubmissionByAssigmentId(string connstr, string AssigmentID)
    {

      List<Content> submissionContent = new List<Content>();

      string query = "SELECT * FROM submission_content where Assigment_ID =" + "'" + AssigmentID + "'";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          assignment assigmrnt = new assignment();
          Content cms = new Content();
          assigmrnt.assiID = Convert.ToInt32(dtRow["Assigment_ID"].ToString());
          cms.Assigment_ID = assigmrnt;
          cms.Descrbtion = dtRow["Descrbtion"].ToString();
          cms.attachment = dtRow["attachment"].ToString();
          submissionContent.Add(cms);
        }
      }
      return submissionContent;
    }

    public static List<Sections> GetSectionsForTeacher(string connstr)
    {

      List<Sections> SectionsList = new List<Sections>();

      string query = "SELECT * FROM section;";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          
          Sections cms = new Sections();
          Course courseInfo = new Course();
          cms.SectionsID = Convert.ToInt32(dtRow["SectionID"].ToString());
          courseInfo.courseID = Convert.ToInt32(dtRow["Course_ID"].ToString());
          cms.Course_ID = courseInfo;
         
          cms.Title = dtRow["Title"].ToString();
          


          SectionsList.Add(cms);
        }
      }
      return SectionsList;
    }



    public static List<Sections> GetSectionsByCourseID(string connstr,string CourseID)
    {

      List<Sections> SectionsList = new List<Sections>();

      string query = "SELECT * FROM section where Course_ID =" + "'" + CourseID + "'";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {

          Sections cms = new Sections();
          Course courseInfo = new Course();
          cms.SectionsID = Convert.ToInt32(dtRow["SectionID"].ToString());
          courseInfo.courseID = Convert.ToInt32(dtRow["Course_ID"].ToString());
          cms.Course_ID = courseInfo;
          cms.Percent = Convert.ToInt32(dtRow["Percent"].ToString());
          cms.Title = dtRow["Title"].ToString();



          SectionsList.Add(cms);
        }
      }
      return SectionsList;
    }

    public static List<GradesKey> GetGradeKeys(string connstr)
    {

      List<GradesKey> GradesKeyList = new List<GradesKey>();

      string query = "SELECT gk.Grade_KeyID, gk.Section_ID, gk.Assigment_ID, gk.gDate, ass.Tite, ass.MaxScore FROM grade_key as gk" + "\r\n";
      query += "inner join section as sc on  gk.Section_ID = sc.SectionID" + "\r\n";
      query += "inner join assignment as ass on gk.Assigment_ID = ass.AssignmentID";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          DateTime gDate = new DateTime();
          GradesKey cms = new GradesKey();
          Sections Sec = new Sections();
          assignment assigmentInfo = new assignment();
          cms.Grade_KeyID = Convert.ToInt32(dtRow["Grade_KeyID"].ToString());
          assigmentInfo.title = dtRow["Tite"].ToString();
          assigmentInfo.assiID = Convert.ToInt32(dtRow["Assigment_ID"].ToString());
          assigmentInfo.Max_Grading = Convert.ToDouble(dtRow["MaxScore"].ToString());
          Sec.SectionsID = Convert.ToInt32(dtRow["Section_ID"].ToString());
          gDate = (DateTime)dtRow["gDate"];
          cms.gDate = gDate.ToShortDateString();
          cms.Assigment_ID = assigmentInfo;
         cms.Section_ID = Sec;


          GradesKeyList.Add(cms);
        }
      }
      return GradesKeyList;
    }



    public static List<GradesKey> GetGradeForStudent(string connstr)
    {

      List<GradesKey> GradesKeyList = new List<GradesKey>();

      string query = "SELECT gk.Grade_KeyID, gk.Section_ID, gk.Assigment_ID, gk.gDate, ass.Tite, ass.MaxScore," + "\r\n";
      query += "gra.Score, gra.GradeDate, gra.GradesID, sc.Percent FROM grade_key as gk" + "\r\n";
      query += "inner join section as sc on  gk.Section_ID = sc.SectionID" + "\r\n";
      query += "inner join assignment as ass on gk.Assigment_ID = ass.AssignmentID" + "\r\n";
      query += "left join grades as gra on gk.Assigment_ID = gra.Assignment_ID";

      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          DateTime gDate = new DateTime();
          DateTime GradDate = new DateTime();
          GradesKey cms = new GradesKey();
          Sections Sec = new Sections();
          Grade gra = new Grade();
          if (dtRow["GradesID"] is int)
          {
            gra.GradID = Convert.ToInt32(dtRow["GradesID"].ToString());
          }

          if (dtRow["Score"] is double)
          {
            gra.Score = Convert.ToDouble(dtRow["Score"].ToString());
          }

          if (dtRow["GradeDate"] is DateTime)
          {
            GradDate = (DateTime)dtRow["GradeDate"];
          }
          
          gra.gradDate = GradDate.ToShortDateString();
          assignment assigmentInfo = new assignment();
          cms.Grade_KeyID = Convert.ToInt32(dtRow["Grade_KeyID"].ToString());
          assigmentInfo.title = dtRow["Tite"].ToString();
          assigmentInfo.assiID = Convert.ToInt32(dtRow["Assigment_ID"].ToString());
          assigmentInfo.Max_Grading = Convert.ToDouble(dtRow["MaxScore"].ToString());
          Sec.SectionsID = Convert.ToInt32(dtRow["Section_ID"].ToString());
          Sec.Percent = Convert.ToInt32(dtRow["Percent"].ToString());
          gDate = (DateTime)dtRow["gDate"];
          cms.gDate = gDate.ToShortDateString();
          cms.Assigment_ID = assigmentInfo;
          cms.Section_ID = Sec;
          cms.Grade_ID = gra;

          GradesKeyList.Add(cms);
        }
      }
      return GradesKeyList;
    }




    public static void AddNewCourse(string connstr, string courseName, string sub_ID, string Desc, string Teacher_ID, string institut_ID, string StartDate, string EndDate)
    {
      string query = "INSERT INTO course (CourseName , Sub_ID , StartDate , EndDate , Describtion , Teacher_Id , institute_id)" + "\r\n";
      query += "VALUES ('" + courseName + "','" + sub_ID + "','" + StartDate + "','" + EndDate + "','" + Desc + "','" + Teacher_ID + "','" + institut_ID + "')" ;
      DBConnection.SQLExecuteInput(query, connstr);
    }


    public static void AddFileAttachemnt(string connstr, string Assigment_ID, string attachment)
    {
      string query = "INSERT INTO assigment_content (Assigment_ID , attachment )" + "\r\n";
      query += "VALUES ('" + Assigment_ID + "','" + attachment + "')";
      DBConnection.SQLExecuteInput(query, connstr);
    }


    public static void UpdateSubmissionStatus(string connstr,string status_Value, string Assigment_ID)
    {
      string query = "update assignment SET submisson_Status =" + "'" + status_Value + "'" + "\r\n";
      query += "where AssignmentID =" + "'" + Assigment_ID + "'";
      DBConnection.SQLExecuteInput(query, connstr);
    }

    public static void UpdateInContent(string connstr, string Assigment_ID, string Descrbtion)
    {
      string query = "UPDATE assignment SET  Descrbtion='" + Descrbtion + "' WHERE AssignmentID = '" + Assigment_ID + "'" ;
      DBConnection.SQLExecuteInput(query, connstr);
    }

    public static void AddNewAssigment(string connstr, string Course_ID, string Tite, string StartDate, string DueDate, string MaxScore, string Typee, string submission)
    {
      string query = "INSERT INTO assignment (Course_ID, Tite, StartDate, DueDate, MaxScore, Typee,submisson_Status)" + "\r\n";
      query += "VALUES ('" + Course_ID + "','" + Tite + "','" + StartDate + "','" + DueDate + "','" + MaxScore + "','" + Typee + "','" + submission + "')";
      DBConnection.SQLExecuteInput(query, connstr);
    }

    public static void AddNewGradeSection(string connstr, string Tite, string Course_ID, string Percent)
    {
      string query = "INSERT INTO section ( Title, Course_ID, Percent)" + "\r\n";
      query += "VALUES ('" + Tite + "','" + Course_ID + "','" + Percent + "')";
      DBConnection.SQLExecuteInput(query, connstr);
    }


    public static void AddNewGradeKey(string connstr, string Section_ID, string Assigment_ID, string gDate)
    {
      string query = "INSERT INTO grade_key ( Section_ID, Assigment_ID, gDate)" + "\r\n";
      query += "VALUES ('" + Section_ID + "','" + Assigment_ID + "','" + gDate + "')";
      DBConnection.SQLExecuteInput(query, connstr);
    }

    public static void AddNewSubmissionContent(string connstr, string Descrbtion, string attachment, string Assigment_ID)
    {
      string query = "INSERT INTO submission_content (Descrbtion, attachment, Assigment_ID)" + "\r\n";
      query += "VALUES ('" + Descrbtion + "','" + attachment + "','" + Assigment_ID + "')";
      DBConnection.SQLExecuteInput(query, connstr);
    }


    public static void AddNewGrade(string connstr, string Assignment_ID, string enrollment_ID, string Score, string GradeDate, string section_ID)
    {
      string query = "INSERT INTO grades (Assignment_ID, enrollment_ID, Score, GradeDate, section_ID)" + "\r\n";
      query += "VALUES ('" + Assignment_ID + "','" + enrollment_ID + "','" + Score + "','" + GradeDate + "','" + section_ID + "')";
      DBConnection.SQLExecuteInput(query, connstr);
    }
  }
}
