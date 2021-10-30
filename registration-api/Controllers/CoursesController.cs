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

    [HttpGet("{courseName}")]
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
  }
}