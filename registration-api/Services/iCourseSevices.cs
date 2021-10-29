using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using CourseRegistration.Repository;

namespace CourseRegistration.Services
{
  public interface ICourseServices
  {
    public List<CourseOffering> GetOfferingsByGoalIdAndSemester(String theGoalId, String semester);

    public List<Course> GetCourses();

    public List<CourseOffering> GetCourseOfferingsBySemester(String semester);

    public List<CourseOffering> GetCourseOfferingsBySemesterAndDept(String semester, String department);
  }
}