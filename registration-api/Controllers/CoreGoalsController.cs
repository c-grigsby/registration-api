using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseRegistration.Models;
using CourseRegistration.Services;
using Newtonsoft.Json;

namespace CourseRegistration.Controllers
{
  [ApiController]
  [Route("coregoals")]
  public class CoreGoalsController : ControllerBase
  {
    private ICoreGoalServices _coreGoalServices;

    public CoreGoalsController(ICoreGoalServices coreGoalServices)
    {
      _coreGoalServices = coreGoalServices;
    }

    [HttpGet]
    public IActionResult GetAllCoreGoals()
    {
      try
      {
        IEnumerable<CoreGoal> coreGoals = _coreGoalServices.GetAllCoreGoals();
        if (coreGoals != null) return Ok(coreGoals);
        else return BadRequest();
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }

    [HttpGet("{goalId}", Name = "GetGoalById")]
    public IActionResult GetCoreGoalById(string goalId)
    {
      try
      {
        CoreGoal coreGoal = _coreGoalServices.GetCoreGoalById(goalId);
        if (coreGoal != null)
        {
          return Ok(coreGoal);
        }
        return StatusCode(404, "Course not found");
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }

    [HttpGet("{id}/withCourses")]
    public IActionResult GetCoreGoalWithCoursesById(string id)
    {
      try
      {
        CoreGoal coreGoal = _coreGoalServices.GetCoreGoalWithCoursesById(id);
        if (coreGoal.Courses.Count() > 0) return Ok(coreGoal);
        return StatusCode(404, "Courses not found");
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }

    [HttpGet("{goalId}/courses")]
    public IActionResult GetCoursesForCoreGoalById(string goalId)
    {
      try
      {
        List<Course> coursesForCoreGoal = (List<Course>)_coreGoalServices.GetCoursesForCoreGoalById(goalId);
        if (coursesForCoreGoal.Count() > 0) return Ok(coursesForCoreGoal);
        else return StatusCode(404, "No Courses for Core Goal Found");
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }

    [HttpPost]
    public IActionResult InsertCoreGoal(CoreGoal newCoreGoal)
    {
      try
      {
        CoreGoal coreGoal = _coreGoalServices.InsertCoreGoal(newCoreGoal);
        if (coreGoal != null)
        {
          return CreatedAtRoute("GetGoalById", new { goalId = newCoreGoal.Id }, coreGoal);
        }
        return BadRequest();
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }

    [HttpPut("{goalId}")]
    public IActionResult UpdateCoreGoal(string goalId, CoreGoal coreGoal)
    {
      try
      {
        if (_coreGoalServices.UpdateCoreGoal(goalId, coreGoal)) return StatusCode(200, "Core Goal Updated");
        else return BadRequest();
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }

    [HttpPut("{goalId}/courses")]
    public IActionResult AddCourseToCoreGoal(string goalId, Course course)
    {
      try
      {
        if (_coreGoalServices.AddCourseToCoreGoal(goalId, course)) return StatusCode(200, "Course Added to CoreGoalCourses");
        else return BadRequest();
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }

    [HttpDelete("{goalId}")]
    public IActionResult DeleteCourse(string goalId)
    {
      try
      {
        if (_coreGoalServices.DeleteCoreGoal(goalId)) return NoContent();
        else return BadRequest();
      }
      catch (Exception err)
      {
        return StatusCode(500, err);
      }
    }
  }
}