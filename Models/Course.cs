using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Models
{
  public class Course
  {
    public int courseID { get; set; }

    public string CourseName { get; set; }
    public string Describtion { get; set; }

    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public Subject Sub_ID { get; set; }

    public Teacher Teacher_ID { get; set; }
    public string LastName { get; set; }

    public institute instID { get; set; }

  }
}
