using System;
using System.Collections.Generic;
using CourseRegistration.Models;

namespace CourseRegistration.Repository
{
  public interface ICoreGoalsRepository
  {
    public IEnumerable<CoreGoal> GetAllCoreGoals();

    public CoreGoal GetCoreGoalById(string courseName);

    public CoreGoal GetCoreGoalWithCoursesById(string id);

    public IEnumerable<Course> GetCoursesForCoreGoalById(string id);

    public CoreGoal InsertCoreGoal(CoreGoal newCourse);

    public bool UpdateCoreGoal(string goalId, CoreGoal course);

    public bool AddCourseToCoreGoal(string id, Course newCourse);

    public bool DeleteCoreGoal(string courseName);
  }
}
