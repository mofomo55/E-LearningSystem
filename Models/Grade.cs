using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Models
{
  public class Grade
  {
    public int GradID { get; set; }
    public assignment Assigment_ID { get; set; }

    public Enrollments enrollment_ID { get; set; }

    public double Score { get; set; }
    public string gradDate { get; set; }
    

    public Sections Section_ID { get; set; }



  }
}
