using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Models
{
  public class GradesKey
  {
    public int Grade_KeyID { get; set; }

    public Sections Section_ID { get; set; }
    public assignment Assigment_ID { get; set; }

    public Grade Grade_ID { get; set; }
    public string gDate { get; set; }
    

  }
}
