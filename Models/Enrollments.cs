using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Models
{
  public class Enrollments
  {
    public int EnrollmentsID { get; set; }

    public Student Student_id { get; set; }
    public Course Course_id { get; set; }

    public string eDate { get; set; }
    public int end_Grading { get; set; }

  }
}
