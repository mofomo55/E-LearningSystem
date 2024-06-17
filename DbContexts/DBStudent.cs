using LearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.DbContexts
{
  public class DBStudent
  {
    public static List<Student> GetAllStudent(string connstr)
    {

      List<Student> StudentList = new List<Student>();

      string query = "SELECT St.StudentID , St.Birthday , Us.UserID , Us.UserName, Us.Email FROM student as St" + "\r\n";
      query += "inner join user as Us on St.User_id = Us.UserID";
      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {

          Student cms = new Student();
          User UserInfo = new User();
          DateTime birthd = new DateTime();
          UserInfo.UserID = Convert.ToInt32(dtRow["UserID"].ToString());
          UserInfo.UserName = dtRow["UserName"].ToString();
          UserInfo.Email = dtRow["Email"].ToString();
          cms.StudentID = Convert.ToInt32(dtRow["StudentID"].ToString());
          cms.UserID = UserInfo;
          birthd = (DateTime)dtRow["Birthday"];
          cms.BirthDay = birthd.ToShortDateString();

          StudentList.Add(cms);
        }
      }
      return StudentList;
    }

    public static void AddNewStudent(string connstr, string UserID, string Birthday)
    {
      string query = "INSERT INTO student (User_id, Birthday)" + "\r\n";
      query += "VALUES ('" + UserID + "', '" + Birthday + "')" + "\r\n";
      DBUser.RoleUpdate(connstr, "S", UserID);
      DBConnection.SQLExecuteInput(query, connstr);
    }


  }
}
