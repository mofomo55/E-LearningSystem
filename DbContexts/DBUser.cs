using LearningSystem.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.DbContexts
{
   
  public class DBUser
  {
    
   


    public static List<User> GetAllUsers(string connstr, string URole = null)
     {

      List<User> UserList = new List<User>();

        string query = "SELECT * FROM user where Role <>" + "'" + URole + "'";
        var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0){
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          User cms = new User();

               cms.UserID =  Convert.ToInt32(dtRow["UserID"].ToString()) ;
              cms.UserName = dtRow["UserName"].ToString();
               cms.FirstName = dtRow["FirstName"].ToString();
               cms.LastName = dtRow["LastName"].ToString();
              cms.Email = dtRow["Email"].ToString();
              cms.Role = dtRow["Role"].ToString();
               cms.ActiveStatus = Convert.ToBoolean(dtRow["Active_Status"]);
              cms.Picture = dtRow["personalPic"].ToString();
               UserList.Add(cms);


        }


      }
   
      return UserList;

    }

   
    public List<User> GetUserToValidation(string UserName, string connstr)
    {
      List<User> UserList = new List<User>();
     
      string query = "SELECT * FROM user where UserName =" + "'" + UserName + "'";
      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if(GetInformation.Rows.Count > 0)
      {
        var row = GetInformation.Rows[0];
        User cms = new User();
        string UserRole;
        cms.UserID = Convert.ToInt32(row["UserID"].ToString());
        string UserID = row["UserID"].ToString();
        cms.UserName = row["UserName"].ToString();
        cms.password = row["password"].ToString();
        cms.Role = row["Role"].ToString();
        UserRole = row["Role"].ToString();
        cms.RoleID = GetUserRoleID(connstr, UserRole, UserID);
        UserList.Add(cms);
      }
     
      return UserList;
    }


    public string GetUserRoleID( string connstr, string Role, string User_ID)
    {

      string RoleID;
       if(Role == "A"){
        string query = "SELECT * FROM admin where User_ID =" + "'" + User_ID + "'";
        var GetInformation = DBConnection.SQLExecute(query, connstr);
        if (GetInformation.Rows.Count > 0)
        {
          var row = GetInformation.Rows[0];
          RoleID = row["AdminID"].ToString();
          return RoleID;
        }
        
      }

      
      if (Role == "T")
      {
        string query = "SELECT * FROM teacher where User_ID =" + "'" + User_ID + "'";
        var GetInformation = DBConnection.SQLExecute(query, connstr);
        if (GetInformation.Rows.Count > 0)
        {
          var row = GetInformation.Rows[0];
          RoleID = row["TeacherID"].ToString();
          return RoleID;
        }

      }

      if (Role == "S")
      {
        string query = "SELECT * FROM student where User_id =" + "'" + User_ID + "'";
        var GetInformation = DBConnection.SQLExecute(query, connstr);
        if (GetInformation.Rows.Count > 0)
        {
          var row = GetInformation.Rows[0];
          RoleID = row["StudentID"].ToString();
          return RoleID;
        }

      }

      return "";
    }








    public static List<User> GetUserByID(string UserID, string connstr)
    {
      List<User> UserList = new List<User>();
      string query = "SELECT * FROM user where UserID =" + "'" + UserID + "'";
      
      var GetInformation = DBConnection.SQLExecute(query, connstr);

      if (GetInformation.Rows.Count > 0)
      {
        var row = GetInformation.Rows[0];
        User cms = new User();
        cms.UserID = Convert.ToInt32(row["UserID"].ToString());
        cms.UserName = row["UserName"].ToString();
        cms.FirstName = row["FirstName"].ToString();
        cms.LastName = row["LastName"].ToString();
        cms.Email = row["Email"].ToString();
        cms.Role = row["Role"].ToString();
        cms.ActiveStatus = Convert.ToBoolean(row["Active_Status"]);
        cms.Picture = row["personalPic"].ToString();
        UserList.Add(cms);

      }
       
      
        
      
      
      return UserList;
    }


    public static void AddNewUser( string connstr, string UserName, string FirstName, string LastName, string email, string Role, bool Active_Status, string personalPic, string password)
    {
      
      string query = "INSERT INTO user (UserName, FirstName, LastName,email,Role,Active_Status,personalPic,password)" + "\r\n";
      query += "VALUES ('" + UserName + "', '" + FirstName + "', '" + LastName + "', '" + email +  "','" + Role + "'," + Active_Status + ",'" + personalPic + "','" + password + "')" + "\r\n";
      DBConnection.SQLExecuteInput(query, connstr);
    }
      

    public static void DeleteUser(string connstr,string UserID)
    {
      string query = "DELETE FROM user where UserID =" + "'" + UserID + "'";
      DBConnection.SQLExecuteInput(query, connstr);
    }

    public static void RoleUpdate(string connstr,string Role, string UserID)
    {
      string query = "UPDATE user SET Role =" + "'" + Role + "'" + "WHERE UserID =" + "'" + UserID + "'";
      DBConnection.SQLExecuteInput(query, connstr);
    }

  }




}
