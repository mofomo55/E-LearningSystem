using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Models
{
  public class Teacher
  {
    public int TeacherID { get; set; }

    public User UserID { get; set; }

    public string BirthDay { get; set; }

  }
}
