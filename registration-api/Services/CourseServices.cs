using System;
using System.Collections.Generic;
using CourseRegistration.Models;
using CourseRegistration.Repository;
using System.Linq;

namespace CourseRegistration.Services
{
  public class CourseServices : ICourseServices
  {
    private CourseRepository repo = new CourseRepository();

    private readonly ICourseRepository _repo;

    /*
    * Default Constructor
    */
    public CourseServices()
    {
      _repo = new CourseRepository();
    }
    /*
    * Constructor for ICourseRepository
    */
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
      List<Course> courses = _repo.GetAllCourses().ToList<Course>();
      return courses;
    }

    /*
    * AddCourse - adds a new course to the repository
    */
    public Course AddCourse(Course course)
    {
      Course c = _repo.InsertCourse(course);
      return c;
    }

    /*
    * UpdateCourse - updates a course in the repository, returns true if successful
    */
    public Boolean UpdateCourse(Course course)
    {
      int result = _repo.UpdateCourse(course);
      if (result == 1) { return true; }
      return false;
    }
    /*
    * DeleteCourse - removes a course in the repository, returns true if successful
    */
    public Boolean DeleteCourse(string courseName)
    {
      int result = _repo.DeleteCourse(courseName); 
      if (result == 1) { return true; }
      return false;
    }

    /*
     * GetAllCourseOfferingsBySemester - returns all course offerings by user selected semester
     */
    public List<CourseOffering> GetAllCourseOfferingsBySemester(String semester)
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
     * GetCourseOfferingsBySemester - returns offerings for a particular course within a semester
     */
    public List<CourseOffering> GetCourseOfferingsBySemester(String courseName, String semester)
    {
      String courseSemester;
      String course_name;
      List<CourseOffering> courseOfferings = _repo.Offerings;
      List<CourseOffering> courseOfferingsBySemesterAndCourse = new List<CourseOffering>();

      foreach (CourseOffering course in courseOfferings)
      {
        courseSemester = course.Semester.ToLower();
        course_name = course.TheCourse.Name.ToLower();

        if (courseSemester.Equals(semester.ToLower()) && course_name.Equals(courseName.ToLower()))
        {
          courseOfferingsBySemesterAndCourse.Add(course);
        }
      }
      return courseOfferingsBySemesterAndCourse;
    }

    /*
     * GetCourseOfferingsBySemesterAndDept - returns all course offerings by user selected semester & dept
     */
    public List<CourseOffering> GetCourseOfferingsBySemesterAndDept(String semester, String department)
    {
      List<CourseOffering> courseOfferingsBySemesterAndDept = new List<CourseOffering>();
      List<CourseOffering> semesterOfferings = GetAllCourseOfferingsBySemester(semester);

      foreach (CourseOffering c in semesterOfferings)
      {
        if (c.TheCourse.Department.Equals(department))
        {
          courseOfferingsBySemesterAndDept.Add(c);
        }
      }
      return courseOfferingsBySemesterAndDept;
    }

    /*
    * GetGoalsByCourse - returns a list acamdeic goals that course fulfills
    */
    public List<CoreGoal> GetGoalsByCourse(string courseName)
    {
      List<CoreGoal> theGoals = _repo.Goals;
      List<CoreGoal> goalsMetByCourse = new List<CoreGoal>();

      foreach (CoreGoal cg in theGoals)
      {
        foreach (Course c in cg.Courses)
        {
          string cName = c.Name.ToLower();
          if (cName.Equals(courseName.ToLower()))
          {
            goalsMetByCourse.Add(cg);
          }
        }
      }
      return goalsMetByCourse;
    }

    public Course GetCourseByName(string courseName)
    {
      return _repo.GetCourseByName(courseName);
    }
  }
}
