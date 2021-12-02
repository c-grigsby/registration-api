using CourseRegistration.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System;

namespace CourseRegistration.Repository
{
  public class CourseRepository : ICourseRepository
  {
    public List<Course> Courses { get; set; }
    public List<CoreGoal> Goals { get; set; }
    public List<CourseOffering> Offerings { get; set; }

    private MySqlConnection _connection;

    public CourseRepository()
    {
      DotNetEnv.Env.Load();
      string connectionString = Environment.GetEnvironmentVariable("connectionString");
      _connection = new MySqlConnection(connectionString);
      _connection.Open();
    }
    ~CourseRepository()
    {
      _connection.Close();
    }

    public IEnumerable<Course> GetAllCourses()
    {
      var statement = "SELECT * FROM Courses";
      var command = new MySqlCommand(statement, _connection);
      var results = command.ExecuteReader();

      List<Course> courseList = new List<Course>();

      while (results.Read())
      {
        Course course = new Course
        {
          Name = (string)results[0],
          Title = (string)results[1],
          Credits = (double)results[2],
          Description = (string)results[3],
          Department = (string)results[4]
        };
        courseList.Add(course);
      }
      results.Close();
      return courseList;
    }

    public Course GetCourseByName(string courseName)
    {
      var statement = "SELECT * FROM Courses WHERE Name=@givenName";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@givenName", courseName);
      var results = command.ExecuteReader();

      if (results.Read())
      {
        Course course = new Course
        {
          Name = (string)results[0],
          Title = (string)results[1],
          Credits = (double)results[2],
          Description = (string)results[3],
          Department = (string)results[4]
        };
        results.Close();
        return course;
      }
      results.Close();
      return null;
    }

    public Course InsertCourse(Course newCourse)
    {
      var statement = "INSERT INTO Courses (Name, Title, Credits, Description, Department) VALUES (@Name, @Title, @Credits, @Description, @Department)";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@Name", newCourse.Name);
      command.Parameters.AddWithValue("@Title", newCourse.Title);
      command.Parameters.AddWithValue("@Credits", newCourse.Credits);
      command.Parameters.AddWithValue("@Description", newCourse.Description);
      command.Parameters.AddWithValue("@Department", newCourse.Department);
      var results = command.ExecuteNonQuery();

      if (results == 1) { return newCourse; }
      else return null;
    }

    public int UpdateCourse(Course course)
    {
      var statement = "UPDATE Courses SET Name=@newName, Title=@newTitle, Credits=@newCredits, Description=@newDescription, Department=@newDepartment WHERE Name=@updateName";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@updateName", course.Name);
      command.Parameters.AddWithValue("@newName", course.Name);
      command.Parameters.AddWithValue("@newTitle", course.Title);
      command.Parameters.AddWithValue("@newCredits", course.Credits);
      command.Parameters.AddWithValue("@newDescription", course.Description);
      command.Parameters.AddWithValue("@newDepartment", course.Department);

      var results = command.ExecuteNonQuery();

      if (results == 1) { return results; }
      else return -1;
    }

    public int DeleteCourse(string courseName)
    {
      var statement = "DELETE FROM Courses WHERE Name=@targetValue";
      var command = new MySqlCommand(statement, _connection);
      command.Parameters.AddWithValue("@targetValue", courseName);

      var results = command.ExecuteNonQuery();

      if (results == 1) { return results; }
      else return -1;
    }
  }
}
