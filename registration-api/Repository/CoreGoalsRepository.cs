using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using MySql.Data.MySqlClient;

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
      var statement = "SELECT * FROM CoreGoals WHERE Name=@goalId";
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

    public CoreGoal InsertCoreGoal(CoreGoal newCoreGoal)
    {
      var statement = "INSERT INTO CoreGoals (Id, Name, Description) VALUES (@Id, @Name, @Desciption)";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@Id", newCoreGoal.Id);
      command.Parameters.AddWithValue("@Name", newCoreGoal.Name);
      command.Parameters.AddWithValue("@Description", newCoreGoal.Description);
      var results = command.ExecuteNonQuery();

      if (results == 1) { return newCoreGoal; }
      else return null;
    }

    public int UpdateCoreGoal(string goalId, CoreGoal coreGoal)
    {
      var statement = "UPDATE Courses SET Id=@newId, Name=@newName, Description=@newDescription WHERE Id=@updateId";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@updateId", goalId);
      command.Parameters.AddWithValue("@newId", coreGoal.Id);
      command.Parameters.AddWithValue("@newName", coreGoal.Name);
      command.Parameters.AddWithValue("@newTitle", coreGoal.Description);

      var results = command.ExecuteNonQuery();

      if (results == 1) { return results; }
      else return -1;
    }

    public int DeleteCoreGoal(string goalId)
    {
      var statement = "DELETE FROM CoreGoals WHERE Id=@targetValue";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@targetValue", goalId);

      var results = command.ExecuteNonQuery();

      if (results == 1) { return results; }
      else return -1;
    }
  }
}

