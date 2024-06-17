using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Models
{
  public class assignment
  {
    public int assiID { get; set; }

    
    public Course Course_id { get; set; }

    public string title { get; set; }

    public string Descrbtion { get; set; }

    public string StartDate { get; set; }

    public string EndDate { get; set; }
    public double Max_Grading { get; set; }

    public string Typee { get; set; }

    public string Submission_Status { get; set; }


  }
}
