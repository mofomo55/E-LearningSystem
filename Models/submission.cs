using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Models
{
  public class submission
  {
    public int SubmissionID { get; set; }

    public Student Student_ID { get; set; }
    public assignment Assigment_ID { get; set; }

    public string Descrbtion { get; set; }

    public string attachment { get; set; }


  }
}
