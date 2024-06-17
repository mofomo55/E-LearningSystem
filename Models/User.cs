using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Models
{
  public class User
  {

    public int UserID { get; set; }

    public string UserName { get; set; }
    public string password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public string RoleID { get; set; }

    public bool ActiveStatus { get; set; }

    public string Picture { get; set; }

  }
}
