using CourseRegistration.Models;
using CourseRegistration.Repository;
using System.Collections.Generic;
using System.Linq;
using System;

namespace CourseRegistration.Services
{
  public class CoreGoalServices : ICoreGoalServices
  {
    private CoreGoalsRepository repo = new CoreGoalsRepository();

    private readonly ICoreGoalsRepository _repo;

    /*
    * Default Constructor
    */
    public CoreGoalServices()
    {
      _repo = new CoreGoalsRepository();
    }
    /*
    * Constructor for ICoreGoalsRepository
    */
    public CoreGoalServices(ICoreGoalsRepository courseRepo)
    {
      _repo = courseRepo;
    }

    public IEnumerable<CoreGoal> GetAllCoreGoals()
    {
      return _repo.GetAllCoreGoals();
    }

    public CoreGoal GetCoreGoalById(string courseName)
    {
      return _repo.GetCoreGoalById(courseName);
    }

    public CoreGoal GetCoreGoalWithCoursesById(string id)
    {
      return _repo.GetCoreGoalWithCoursesById(id);
    }

    public IEnumerable<Course> GetCoursesForCoreGoalById(string id)
    {
      return _repo.GetCoursesForCoreGoalById(id);
    }

    public CoreGoal InsertCoreGoal(CoreGoal newCoreGoal)
    {
      return _repo.InsertCoreGoal(newCoreGoal);
    }

    public bool UpdateCoreGoal(string goalId, CoreGoal course)
    {
      return _repo.UpdateCoreGoal(goalId, course);
    }

    public bool AddCourseToCoreGoal(string id, Course newCourse)
    {
      return _repo.AddCourseToCoreGoal(id, newCourse);
    }

    public bool DeleteCoreGoal(string courseName)
    {
      return _repo.DeleteCoreGoal(courseName);
    }
  }
}
