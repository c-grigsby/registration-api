using System;
using System.Collections.Generic;
using CourseRegistration.Models;

namespace CourseRegistration.Repository
{
  public interface ICoreGoalsRepository
  {
    public IEnumerable<CoreGoal> GetAllCoreGoals();

    public CoreGoal GetCoreGoalById(string courseName);

    public CoreGoal InsertCoreGoal(CoreGoal newCourse);

    public int UpdateCoreGoal(string goalId, CoreGoal course);

    public int DeleteCoreGoal(string courseName);
  }
}
