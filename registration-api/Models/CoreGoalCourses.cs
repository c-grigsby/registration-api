using System;

namespace CourseRegistration.Models
{
  public class CoreGoalCourses : IComparable<CoreGoalCourses>
  {
    public string GoalId { get; set; }
    public string CourseName { get; set; }

    public override String ToString()
    {
      return $"{GoalId}: {CourseName}";
    }

    public int CompareTo(CoreGoalCourses other)
    {
      return this.GoalId.CompareTo(other.GoalId);
    }
  }
}
