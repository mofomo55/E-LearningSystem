using LearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.DbContexts
{
  public class DBenrollments
  {
    public static List<Enrollments> GetAllEnrollments(string connstr)
    {

      List<Enrollments> EnrollmentsList = new List<Enrollments>();

      string query = "SELECT   en.EnrollmentsID, us.UserName , en.Course_ID , en.Student_ID , en.EnroDate , " + "\r\n";
      query += "co.CourseName , sb.Name , en.end_Grading FROM enrollments as en" + "\r\n";
      query += "inner join course as co on en.Course_ID = co.CourseID" + "\r\n";
      query += "inner join subjects as sb on co.Sub_ID = sb.SubID" + "\r\n";
      query += "inner join student as st on en.Student_ID = st.StudentID" + "\r\n";
      query += "inner join user as us on st.User_id = us.UserID";
      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          
          Subject Sub = new Subject();
          User UserInfo = new User();
          Student StudentInfo = new Student();
          Course CourseInfo = new Course();
          Enrollments cms = new Enrollments();
          DateTime eDate = new DateTime();


          UserInfo.UserName = dtRow["UserName"].ToString();
          StudentInfo.UserID = UserInfo;
          StudentInfo.StudentID = Convert.ToInt32(dtRow["Student_ID"].ToString());
          Sub.SubjectName = dtRow["Name"].ToString();
          CourseInfo.Sub_ID = Sub;
          CourseInfo.CourseName = dtRow["CourseName"].ToString();
          CourseInfo.courseID = Convert.ToInt32(dtRow["Course_ID"].ToString());
          cms.Course_id = CourseInfo;
          eDate = (DateTime)dtRow["EnroDate"];
          cms.EnrollmentsID = Convert.ToInt32(dtRow["EnrollmentsID"].ToString());
          //  cms.end_Grading = Convert.ToInt32(dtRow["end_Grading"]); 
          cms.Student_id = StudentInfo;
          cms.eDate = eDate.ToShortDateString();

          EnrollmentsList.Add(cms);
        }
      }
      return EnrollmentsList;
    }





    public static List<Enrollments> GetEnrollmentByStudentID(string connstr,string StudentID)
    {

      List<Enrollments> EnrollmentsList = new List<Enrollments>();

      string query = "SELECT   en.EnrollmentsID, us.UserName , en.Course_ID , en.Student_ID , en.EnroDate , " + "\r\n";
      query += "co.CourseName , sb.Name , en.end_Grading FROM enrollments as en" + "\r\n";
      query += "inner join course as co on en.Course_ID = co.CourseID" + "\r\n";
      query += "inner join subjects as sb on co.Sub_ID = sb.SubID" + "\r\n";
      query += "inner join student as st on en.Student_ID = st.StudentID" + "\r\n";
      query += "inner join user as us on st.User_id = us.UserID where en.Student_ID =" + "'" + StudentID + "'";
      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {

          Subject Sub = new Subject();
          User UserInfo = new User();
          Student StudentInfo = new Student();
          Course CourseInfo = new Course();
          Enrollments cms = new Enrollments();
          DateTime eDate = new DateTime();


          UserInfo.UserName = dtRow["UserName"].ToString();
          StudentInfo.UserID = UserInfo;
          StudentInfo.StudentID = Convert.ToInt32(dtRow["Student_ID"].ToString());
          Sub.SubjectName = dtRow["Name"].ToString();
          CourseInfo.Sub_ID = Sub;
          CourseInfo.CourseName = dtRow["CourseName"].ToString();
          CourseInfo.courseID = Convert.ToInt32(dtRow["Course_ID"].ToString());
          cms.Course_id = CourseInfo;
          eDate = (DateTime)dtRow["EnroDate"];
          cms.EnrollmentsID = Convert.ToInt32(dtRow["EnrollmentsID"].ToString());
          //  cms.end_Grading = Convert.ToInt32(dtRow["end_Grading"]); 
          cms.Student_id = StudentInfo;
          cms.eDate = eDate.ToShortDateString();

          EnrollmentsList.Add(cms);
        }
      }
      return EnrollmentsList;
    }




    public static List<Enrollments> GetEnrollmentsByCourseID(string connstr,string Course_ID)
    {

      List<Enrollments> EnrollmentsList = new List<Enrollments>();

      string query = "SELECT  en.EnrollmentsID, us.UserName , en.Course_ID , en.Student_ID , en.EnroDate , " + "\r\n";
      query += "co.CourseName , sb.Name , en.end_Grading FROM enrollments as en" + "\r\n";
      query += "inner join course as co on en.Course_ID = co.CourseID" + "\r\n";
      query += "inner join subjects as sb on co.Sub_ID = sb.SubID" + "\r\n";
      query += "inner join student as st on en.Student_ID = st.StudentID" + "\r\n";
      query += "inner join user as us on st.User_id = us.UserID where en.Course_ID=" + "'" + Course_ID + "'";
      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {

          Subject Sub = new Subject();
          User UserInfo = new User();
          Student StudentInfo = new Student();
          Course CourseInfo = new Course();
          Enrollments cms = new Enrollments();
          DateTime eDate = new DateTime();


          UserInfo.UserName = dtRow["UserName"].ToString();
          StudentInfo.UserID = UserInfo;
          StudentInfo.StudentID = Convert.ToInt32(dtRow["Student_ID"].ToString());
          Sub.SubjectName = dtRow["Name"].ToString();
          CourseInfo.Sub_ID = Sub;
          CourseInfo.CourseName = dtRow["CourseName"].ToString();
          CourseInfo.courseID = Convert.ToInt32(dtRow["Course_ID"].ToString());
          cms.Course_id = CourseInfo;
          eDate = (DateTime)dtRow["EnroDate"];
          cms.EnrollmentsID = Convert.ToInt32(dtRow["EnrollmentsID"].ToString());
          //  cms.end_Grading = Convert.ToInt32(dtRow["end_Grading"]); 
          cms.Student_id = StudentInfo;
          cms.eDate = eDate.ToShortDateString();

          EnrollmentsList.Add(cms);
        }
      }
      return EnrollmentsList;
    }



    //public static List<Enrollments> GetEnrollmentsByCourseID(string connstr, string Course_ID)
    //{

    //  List<Enrollments> EnrollmentsList = new List<Enrollments>();

    //  string query = "SELECT  en.EnrollmentsID, Gr.GradesID, Gr.Score, Gr.GradeDate, us.UserName, en.Course_ID, en.Student_ID, en.EnroDate , " + "\r\n";
    //  query += "co.CourseName, sb.Name , en.end_Grading FROM enrollments as en" + "\r\n";
    //  query += "inner join course as co on en.Course_ID = co.CourseID" + "\r\n";
    //  query += "inner join subjects as sb on co.Sub_ID = sb.SubID" + "\r\n";
    //  query += "inner join student as st on en.Student_ID = st.StudentID" + "\r\n";
    //  query += "inner join user as us on st.User_id = us.UserID" + "\r\n";
    //  query += "Left join grades as Gr on en.EnrollmentsID = Gr.enrollment_ID where en.Course_ID= 2" + "'" + Course_ID + "'";
    //  var GetInformation = DBConnection.SQLExecute(query, connstr);
    //  if (GetInformation.Rows.Count > 0)
    //  {
    //    foreach (DataRow dtRow in GetInformation.Rows)
    //    {

    //      Subject Sub = new Subject();
    //      User UserInfo = new User();
    //      Student StudentInfo = new Student();
    //      Course CourseInfo = new Course();
    //      Enrollments cms = new Enrollments();
    //      DateTime eDate = new DateTime();


    //      UserInfo.UserName = dtRow["UserName"].ToString();
    //      StudentInfo.UserID = UserInfo;
    //      StudentInfo.StudentID = Convert.ToInt32(dtRow["Student_ID"].ToString());
    //      Sub.SubjectName = dtRow["Name"].ToString();
    //      CourseInfo.Sub_ID = Sub;
    //      CourseInfo.CourseName = dtRow["CourseName"].ToString();
    //      CourseInfo.courseID = Convert.ToInt32(dtRow["Course_ID"].ToString());
    //      cms.Course_id = CourseInfo;
    //      eDate = (DateTime)dtRow["EnroDate"];
    //      cms.EnrollmentsID = Convert.ToInt32(dtRow["EnrollmentsID"].ToString());
    //      //  cms.end_Grading = Convert.ToInt32(dtRow["end_Grading"]); 
    //      cms.Student_id = StudentInfo;
    //      cms.eDate = eDate.ToShortDateString();

    //      EnrollmentsList.Add(cms);
    //    }
    //  }
    //  return EnrollmentsList;
    //}









    public static void AddNewEnrollment(string connstr, string Student_ID, string Course_ID, string EnrollDate)
    {
      string query = "INSERT INTO enrollments (Student_ID , Course_ID , EnroDate)" + "\r\n";
      query += "VALUES ('" + Student_ID + "','" + Course_ID + "','" + EnrollDate + "')";
      DBConnection.SQLExecuteInput(query, connstr);
    }


  }
}
