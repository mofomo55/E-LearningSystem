using LearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.DbContexts
{
  public class DBAdmin
  {

    public static List<Admin> GetAllAdmins(string connstr)
    {

      List<Admin> AdminList = new List<Admin>();

      string query = "SELECT ad.AdminID , ad.Birthday , Us.UserID , Us.UserName, Us.Email FROM admin as ad" + "\r\n";
      query += "inner join user as Us on ad.User_ID = Us.UserID";
      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {

          Admin cms = new Admin();
          User UserInfo = new User();
          DateTime birthd = new DateTime();
          UserInfo.UserID = Convert.ToInt32(dtRow["UserID"].ToString());
          UserInfo.UserName = dtRow["UserName"].ToString();
          UserInfo.Email = dtRow["Email"].ToString();
          cms.adminID = Convert.ToInt32(dtRow["AdminID"].ToString());
          cms.UserID = UserInfo;
          birthd = (DateTime)dtRow["Birthday"];
          cms.BirthDay = birthd.ToShortDateString();

          AdminList.Add(cms);
        }
      }
      return AdminList;
    }


    public static void AddNewAdmin(string connstr, string UserID, string Birthday)
    {
      string query = "INSERT INTO admin (User_ID, Birthday)" + "\r\n";
      query += "VALUES ('" + UserID + "', '" + Birthday + "')" + "\r\n";
      DBUser.RoleUpdate(connstr, "A", UserID);
      DBConnection.SQLExecuteInput(query, connstr);
    }


  }
}
