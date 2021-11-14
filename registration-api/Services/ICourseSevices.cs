using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using CourseRegistration.Repository;

namespace CourseRegistration.Services
{
  public interface ICourseServices
  {
    public Course AddCourse(Course course);

    public Boolean DeleteCourse(string courseName);

    public Boolean UpdateCourse(Course course);

    public List<CoreGoal> GetGoalsByCourse(string courseName);

    public List<Course> GetCourses();

    public List<CourseOffering> GetAllCourseOfferingsBySemester(String semester);

    public List<CourseOffering> GetCourseOfferingsBySemester(String courseName, String semester);

    public List<CourseOffering> GetCourseOfferingsBySemesterAndDept(String semester, String department);

    public List<CourseOffering> GetOfferingsByGoalIdAndSemester(String theGoalId, String semester);

    public Course GetCourseByName(string courseName);
  }
}