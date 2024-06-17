using LearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.DbContexts
{
  public class DBTeacher
  {

    public static List<Teacher> GetAllTeacher(string connstr)
    {

      List<Teacher> TeacherList = new List<Teacher>();

      string query = "SELECT Te.TeacherID, Te.BirthDay, Us.UserID , Us.UserName, Us.Email FROM teacher as Te" + "\r\n";
      query += "inner join user as Us on Te.User_ID = Us.UserID";
      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
         
            Teacher cms = new Teacher();
            User UserInfo = new User();
          DateTime birthd = new DateTime();
          UserInfo.UserID = Convert.ToInt32(dtRow["UserID"].ToString());
            UserInfo.UserName = dtRow["UserName"].ToString();
            UserInfo.Email = dtRow["Email"].ToString();
            cms.TeacherID = Convert.ToInt32(dtRow["TeacherID"].ToString());
            cms.UserID = UserInfo;
          birthd = (DateTime)dtRow["BirthDay"];
          cms.BirthDay = birthd.ToShortDateString();

            TeacherList.Add(cms);
        }
      }
      return TeacherList;
    }

    public static void AddNewTeacher(string connstr, string UserID, string Birthday)
    {
      string query = "INSERT INTO teacher (User_ID, BirthDay)" + "\r\n";
      query += "VALUES ('" + UserID + "', '" + Birthday + "')" + "\r\n";
      DBUser.RoleUpdate(connstr, "T", UserID);
      DBConnection.SQLExecuteInput(query, connstr);
    }

    

  }
}
