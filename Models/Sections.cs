using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Models
{
  public class Sections
  {
    public int SectionsID { get; set; }

    public string Title { get; set; }

    public Course Course_ID { get; set; }

    public int Percent { get; set; }

  }
}
