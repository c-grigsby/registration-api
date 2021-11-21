using System;
using System.Collections.Generic;
using CourseRegistration.Models;

namespace CourseRegistration.Repository
{
  public interface ICourseRepository
  {
    public List<Course> Courses { get; set; }
    public List<CoreGoal> Goals { get; set; }
    public List<CourseOffering> Offerings { get; set; }

    public IEnumerable<Course> GetAllCourses();

    public Course GetCourseByName(string courseName);

    public Course InsertCourse(Course newCourse);

    public int UpdateCourse(Course course);

    public int DeleteCourse(string courseName);
  }
}