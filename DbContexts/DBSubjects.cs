using LearningSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.DbContexts
{
  public class DBSubjects
  {
    public static List<Subject> GetAllSubject(string connstr)
    {

      List<Subject> SubjectList = new List<Subject>();

      string query = "SELECT * FROM subjects";
      var GetInformation = DBConnection.SQLExecute(query, connstr);
      if (GetInformation.Rows.Count > 0)
      {
        foreach (DataRow dtRow in GetInformation.Rows)
        {
          Subject cms = new Subject();

          cms.SubID = Convert.ToInt32(dtRow["SubID"].ToString());
          cms.SubjectName = dtRow["Name"].ToString();
          cms.Desc = dtRow["Description"].ToString();
          SubjectList.Add(cms);
        }


      }

      return SubjectList;

    }


    public static void AddNewSubject(string connstr, string SubName, string Description)
    {
      string query = "INSERT INTO subjects (Name, Description)" + "\r\n";
      query += "VALUES ('" + SubName + "', '" + Description + "')" + "\r\n";
      DBConnection.SQLExecuteInput(query, connstr);
    }



    public static void DeleteSubject(string connstr, string SubID)
    {
      string query = "DELETE FROM subjects where SubID =" + "'" + SubID + "'";
      DBConnection.SQLExecuteInput(query, connstr);
    }

  }
}
