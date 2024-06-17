using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.DbContexts
{
  public class DBConnection
  {

    public static void SQLExecuteInput(string Sql, string ConnStr, string parameters = null)
    {
      MySqlConnection conn = new MySqlConnection(ConnStr);
      conn.Open();
      string SQLque = Sql;
      var cmd = new MySqlCommand(SQLque, conn);
       cmd.ExecuteReader();
       conn.Close();  

    }




    public static DataTable SQLExecute(string Sql,string ConnStr, string parameters = null)
    {
     DataTable DataList = new DataTable();
     
      using (MySqlConnection conn = new MySqlConnection(ConnStr)) {
        conn.Open();
        string SQLque = Sql;
        var cmd = new MySqlCommand(SQLque, conn);
        var reader = cmd.ExecuteReader();

        DataList.Load(reader);
        conn.Close();
      } ;
      return DataList;
    }
  }
}
