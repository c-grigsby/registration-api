using CourseRegistration.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace CourseRegistration.Repository
{
  public class CoreGoalsRepository : ICoreGoalsRepository
  {
    private MySqlConnection _connection;

    public CoreGoalsRepository()
    {
      DotNetEnv.Env.Load();
      string connectionString = Environment.GetEnvironmentVariable("connectionString");
      _connection = new MySqlConnection(connectionString);
      _connection.Open();
    }
    ~CoreGoalsRepository()
    {
      _connection.Close();
    }

    public IEnumerable<CoreGoal> GetAllCoreGoals()
    {
      var statement = "SELECT * FROM CoreGoals";
      var command = new MySqlCommand(statement, _connection);
      var results = command.ExecuteReader();

      List<CoreGoal> coreGoalList = new List<CoreGoal>();

      while (results.Read())
      {
        CoreGoal coreGoal = new CoreGoal
        {
          Id = (string)results[0],
          Name = (string)results[1],
          Description = (string)results[2],
        };
        coreGoalList.Add(coreGoal);
      }
      results.Close();
      return coreGoalList;
    }

    public CoreGoal GetCoreGoalById(string goalId)
    {
      var statement = "SELECT * FROM CoreGoals WHERE Id=@goalId";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@goalId", goalId);
      var results = command.ExecuteReader();

      if (results.Read())
      {
        CoreGoal coreGoal = new CoreGoal
        {
          Id = (string)results[0],
          Name = (string)results[1],
          Description = (string)results[2],
        };
        results.Close();
        return coreGoal;
      }
      results.Close();
      return null;
    }

    public CoreGoal GetCoreGoalWithCoursesById(string id)
    {
      List<CoreGoalCourses> coreGoalCourses = new List<CoreGoalCourses>();
      CoreGoal coreGoalWithCourses = GetCoreGoalById(id);
      CourseRepository courseRepo = new CourseRepository();
      IEnumerable<Course> courses = courseRepo.GetAllCourses();

      var statement = "SELECT * FROM CoreGoalCourses WHERE GoalId=@goalId";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@goalId", id);
      var results = command.ExecuteReader();

      while (results.Read())
      {
        CoreGoalCourses coreGoal = new CoreGoalCourses
        {
          GoalId = (string)results[0],
          CourseName = (string)results[1],
        };
        coreGoalCourses.Add(coreGoal);
      }
      results.Close();

      foreach (CoreGoalCourses coreGoal in coreGoalCourses)
      {
        foreach (Course course in courses)
        {
          if (course.Name.Equals(coreGoal.CourseName))
          {
            coreGoalWithCourses.Courses.Add(course);
          }
        }
      }
      return coreGoalWithCourses;
    }

    public IEnumerable<Course> GetCoursesForCoreGoalById(string id)
    {
      List<Course> courses = new List<Course>();
      CoreGoal coreGoalWithCourses = GetCoreGoalWithCoursesById(id);

      courses = coreGoalWithCourses.Courses;

      return courses;
    }

    public CoreGoal InsertCoreGoal(CoreGoal newCoreGoal)
    {
      var statement = "INSERT INTO CoreGoals (Id, Name, Description) VALUES (@Id, @Name, @Description)";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@Id", newCoreGoal.Id);
      command.Parameters.AddWithValue("@Name", newCoreGoal.Name);
      command.Parameters.AddWithValue("@Description", newCoreGoal.Description);
      var results = command.ExecuteNonQuery();

      if (results == 1) { return newCoreGoal; }
      else return null;
    }

    public bool UpdateCoreGoal(string goalId, CoreGoal coreGoal)
    {
      var statement = "UPDATE CoreGoals SET Id=@newId, Name=@newName, Description=@newDescription WHERE Id=@updateId";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@updateId", goalId);
      command.Parameters.AddWithValue("@newId", coreGoal.Id);
      command.Parameters.AddWithValue("@newName", coreGoal.Name);
      command.Parameters.AddWithValue("@newDescription", coreGoal.Description);

      var results = command.ExecuteNonQuery();

      if (results == 1) { return true; }
      else return false;
    }

    public bool AddCourseToCoreGoal(string id, Course newCourse)
    {
      var statement = "INSERT INTO CoreGoalCourses (GoalId, CourseName) VALUES (@GoalId, @CourseName)";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@GoalId", id);
      command.Parameters.AddWithValue("@CourseName", newCourse.Name);
      var results = command.ExecuteNonQuery();

      if (results == 1) { return true; }
      else return false;
    }

    public bool DeleteCoreGoal(string goalId)
    {
      var statement = "DELETE FROM CoreGoals WHERE Id=@targetValue";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@targetValue", goalId);
      var results = command.ExecuteNonQuery();

      if (results == 1) { return true; }
      else return false;
    }
  }
}

