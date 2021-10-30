using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using CourseRegistration.Repository;

namespace CourseRegistration.Services
{
  public class CourseServices : ICourseServices
  {
    private CourseRepository repo = new CourseRepository();

    private readonly ICourseRepository _repo;

    // Default Constructor
    public CourseServices()
    {
      _repo = new CourseRepository();
    }
    // Constructor for ICourseRepository obj
    public CourseServices(ICourseRepository courseRepo)
    {
      _repo = courseRepo;
    }
    /*
    * GetOfferingsByGoalIdAndSemester - returns a list course offerings that meets core goals
    */
    public List<CourseOffering> GetOfferingsByGoalIdAndSemester(String theGoalId, String semester)
    {
      List<CoreGoal> theGoals = _repo.Goals;
      List<CourseOffering> theOfferings = _repo.Offerings;
      CoreGoal theGoal = null;
      List<CourseOffering> courseOfferingsThatMeetGoal = new List<CourseOffering>();

      foreach (CoreGoal cg in theGoals)
      {
        if (cg.Id.Equals(theGoalId))
        {
          theGoal = cg; break;
        }
      }
      if (theGoal == null) throw new Exception("Didn't find the goal");

      foreach (CourseOffering c in theOfferings)
      {
        if (c.Semester.Equals(semester)
           && theGoal.Courses.Contains(c.TheCourse))
        {
          courseOfferingsThatMeetGoal.Add(c);
        }
      }
      return courseOfferingsThatMeetGoal;
    }

    /*
    * GetCourses - returns all courses
    */
    public List<Course> GetCourses()
    {
      List<Course> courses = _repo.Courses;
      return courses;
    }

    /*
    * AddCourse - adds a new course to the repository
    */
    public void AddCourse(Course course)
    {
      List<Course> courses = _repo.Courses;
      courses.Add(course);
    }

    /*
    * UpdateCourse - updates a course in the repository, returns true if successful
    */
    public Boolean UpdateCourse(Course course)
    {
      List<Course> courses = _repo.Courses;
      foreach (Course c in courses)
      {
        string courseName = c.Name.ToLower();
        if (courseName.Equals(course.Name.ToLower()))
        {
          c.Name = course.Name;
          c.Title = course.Title;
          c.Credits = course.Credits;
          c.Description = course.Description;
          c.Department = course.Department;
          return true;
        }
      }
      return false;
    }
    /*
    * DeleteCourse - removes a course in the repository, returns true if successful
    */
    public Boolean DeleteCourse(string courseName)
    {
      List<Course> courses = _repo.Courses;
      foreach (Course course in courses)
      {
        string repoCourseName = course.Name.ToLower();
        if (repoCourseName.Equals(courseName.ToLower()))
        {
          courses.Remove(course);
          return true;
        }
      }
      return false;
    }

    /*
     * GetCourseOfferingsBySemester - returns all course offerings by user selected semester
     */
    public List<CourseOffering> GetCourseOfferingsBySemester(String semester)
    {
      String userSemester = semester.ToLower();
      String courseSemester;
      List<CourseOffering> courseOfferings = _repo.Offerings;
      List<CourseOffering> courseOfferingsBySemester = new List<CourseOffering>();

      foreach (CourseOffering course in courseOfferings)
      {
        courseSemester = course.Semester.ToLower();

        if (courseSemester.Equals(userSemester))
        {
          courseOfferingsBySemester.Add(course);
        }
      }
      return courseOfferingsBySemester;
    }

    /*
     * GetCourseOfferingsBySemesterAndDept - returns all course offerings by user selected semester & dept
     */
    public List<CourseOffering> GetCourseOfferingsBySemesterAndDept(String semester, String department)
    {
      List<CourseOffering> courseOfferingsBySemesterAndDept = new List<CourseOffering>();
      List<CourseOffering> semesterOfferings = GetCourseOfferingsBySemester(semester);

      foreach (CourseOffering c in semesterOfferings)
      {
        if (c.TheCourse.Department.Equals(department))
        {
          courseOfferingsBySemesterAndDept.Add(c);
        }
      }
      return courseOfferingsBySemesterAndDept;
    }

    /* User Story Five */

    /* User Story Six */

    /* User Story Seven */
  }
}
