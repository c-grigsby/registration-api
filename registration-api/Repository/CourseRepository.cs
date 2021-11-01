using System;
using System.Collections.Generic;
using CourseRegistration.Models;

namespace CourseRegistration.Repository
{
  public class CourseRepository : ICourseRepository
  {
    public List<Course> Courses { get; set; }
    public List<CoreGoal> Goals { get; set; }
    public List<CourseOffering> Offerings { get; set; }

    public CourseRepository()
    {
      Courses = new List<Course>();
      Goals = new List<CoreGoal>();
      Offerings = new List<CourseOffering>();

      Course c1 = new Course()
      {
        Name = "ARTS 201",
        Title = "Graphic Design",
        Credits = 3.0,
        Description = "More valuable than you might realize",
        Department = "ARTS"
      };

      Course c2 = new Course()
      {
        Name = "ARTS 101",
        Title = "Art Studio",
        Credits = 3.0,
        Description = "Get your paint on!",
        Department = "ARTS"
      };

      Course c3 = new Course()
      {
        Name = "STAT 201",
        Title = "Stats",
        Credits = 4.0,
        Description = "Where science and math make sense from data",
        Department = "MATH"
      };

      Course c4 = new Course()
      {
        Name = "ENGL 302",
        Title = "Math as a Communication Language",
        Credits = 4.0,
        Description = "A math course for English majors",
        Department = "ENGL"
      };

      Course c5 = new Course()
      {
        Name = "CSCI 330",
        Title = "Systems Analysis & Software Engineering",
        Credits = 3.0,
        Description = "A swell course with a fantastic teacher!",
        Department = "CSCI"
      };
      Courses.Add(c1);
      Courses.Add(c2);
      Courses.Add(c3);
      Courses.Add(c4);
      Courses.Add(c5);

      CourseOffering co1 = new CourseOffering()
      {
        TheCourse = c1,
        Section = "D1",
        Semester = "Spring 2021"
      };

      CourseOffering co2 = new CourseOffering()
      {
        TheCourse = c3,
        Section = "01",
        Semester = "Spring 2021"
      };

      CourseOffering co3 = new CourseOffering()
      {
        TheCourse = c2,
        Section = "01",
        Semester = "Spring 2022"
      };
      CourseOffering co4 = new CourseOffering()
      {
        TheCourse = c5,
        Section = "01",
        Semester = "Fall 2020"
      };
      CourseOffering co5 = new CourseOffering()
      {
        TheCourse = c3,
        Section = "01",
        Semester = "Fall 2020"
      };
      Offerings.Add(co1);
      Offerings.Add(co2);
      Offerings.Add(co3);
      Offerings.Add(co4);
      Offerings.Add(co5);

      CoreGoal cg1 = new CoreGoal()
      {
        Id = "CG1",
        Name = "Artistic Expression",
        Description = "Desc for artistic expression",
        Courses = new List<Course>() { c1, c2 }
      };

      CoreGoal cg2 = new CoreGoal()
      {
        Id = "CG2",
        Name = "Quantitative Literacy",
        Description = "Desc for quantitative literacy",
        Courses = new List<Course>() { c2, c3 }
      };

      CoreGoal cg3 = new CoreGoal()
      {
        Id = "CG3",
        Name = "Effective Communication",
        Description = "Desc for communication",
        Courses = new List<Course>() { c4, c3 }
      };
      Goals.Add(cg1);
      Goals.Add(cg2);
      Goals.Add(cg3);
    }//end constructor
  }
}
