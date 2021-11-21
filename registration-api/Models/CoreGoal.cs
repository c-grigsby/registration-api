using System;
using System.Collections.Generic;
using System.Text;

namespace CourseRegistration.Models
{
  public class CoreGoal : IComparable<CoreGoal>
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Course> Courses { get; set; }

    public CoreGoal () {
      Courses = new List<Course>();
    }

    public override String ToString()
    {
      StringBuilder courseList = new StringBuilder();
      if (Courses != null)
      {
        foreach (Course c in Courses)
        {
          courseList.Append(c.ToString() + ",");
        }
        return $"{Id}-{Name}: {Description} ()\n{courseList.ToString()}\n";
      }
      return $"{Id}-{Name}: {Description}";
    }

    public int CompareTo(CoreGoal other)
    {
      return this.Id.CompareTo(other.Id);
    }
  }
}
