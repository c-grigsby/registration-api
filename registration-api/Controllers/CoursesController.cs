using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CourseRegistration.Models;
using CourseRegistration.Services;

namespace CourseRegistration.Controllers
{
  [ApiController]
  [Route("courses")]
  public class CoursesController : ControllerBase
  {
    private ICourseServices _courseServices;

    public CoursesController(ICourseServices courseServices)
    {
      _courseServices = courseServices;
    }

    [HttpGet]
    public IActionResult GetAllCourses()
    {
      try
      {
        IEnumerable<Course> courses = _courseServices.GetCourses();
        if (courses != null) return Ok(courses);
        else return BadRequest();
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal Server Error");
      }
    }

    [HttpGet("{courseName}", Name = "GetCourse")]
    public IActionResult GetCourseByName(string courseName)
    {
      try
      {
        List<Course> courses = _courseServices.GetCourses();
        foreach (Course c in courses)
        {
          string course = c.Name.ToLower();
          if (course.Equals(courseName.ToLower()))
          {
            return Ok(c);
          }
        }
        return StatusCode(404, "Course not found");
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal Server Error");
      }
    }

    [HttpGet("search/")]
    public IActionResult GetCourseByDept(string dept)
    {
      try
      {
        List<Course> courses = _courseServices.GetCourses();
        List<Course> coursesByDept = new List<Course>();
        foreach (Course c in courses)
        {
          string department = c.Department.ToLower();
          if (department.Equals(dept.ToLower()))
          {
            coursesByDept.Add(c);
          }
        }
        if (coursesByDept.Count() > 0) return Ok(coursesByDept);
        return StatusCode(404, "Course not found");
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal Server Error");
      }
    }

    [HttpGet("goals/{courseName}")]
    public IActionResult GetGoalsByCourse(string courseName)
    {
      try
      {
        List<CoreGoal> goalsMetByCourse = _courseServices.GetGoalsByCourse(courseName);
        if (goalsMetByCourse.Count > 0) return Ok(goalsMetByCourse);
        else return StatusCode(404, "No Core Goals for Course Found");
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal Server Error");
      }
    }
    [HttpGet("{courseName}/offerings")]
    public IActionResult GetCourseOfferingsBySemester(string courseName, string semester)
    {
      try
      {
        List<CourseOffering> courseOfferings = _courseServices.GetCourseOfferingsBySemester(courseName, semester);
        if (courseOfferings.Count > 0) return Ok(courseOfferings);
        else return StatusCode(404, "No Semester Offerings for Course Found");
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal Server Error");
      }
    }

    [HttpPost]
    public IActionResult CreateCourse(Course course)
    {
      try
      {
        _courseServices.AddCourse(course);
        return CreatedAtRoute("GetCourse", new { courseName = course.Name }, course);
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal Server error");
      }
    }

    [HttpPut]
    public IActionResult UpdateCourse(Course course)
    {
      try
      {
        if (_courseServices.UpdateCourse(course)) return StatusCode(200, "Course Updated");
        else return BadRequest();
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal Server error");
      }
    }

    [HttpDelete("{courseName}")]
    public IActionResult DeleteCourse(string courseName)
    {
      try
      {
        if (_courseServices.DeleteCourse(courseName)) return NoContent();
        else return BadRequest();
      }
      catch (Exception e)
      {
        return StatusCode(500, "Internal Server error");
      }
    }
  }
}