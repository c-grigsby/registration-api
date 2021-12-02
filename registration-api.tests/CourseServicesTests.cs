using CourseRegistration.Models;
using CourseRegistration.Repository;
using CourseRegistration.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;
using Xunit;

namespace registration_api.tests
{
  public class CourseServicesTests
  {
    [Fact]
    public void GetOfferingsByGoalIdAndSemester_GoalNotFound_ExceptionThrown()
    {
      // Arrange
      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(GetTestCourses());
      mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
            new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
      });
      mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = GetTestCourses().First()
                }
            });
      var courseServices = new CourseServices(mockRepository.Object);
      var goalId = "CG5";
      var semester = "Spring 2021";

      // Act & Assert
      Assert.Throws<Exception>(() => courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester));
    }


    [Fact]
    public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndOneCourseOfferingIsInSemester_OfferingIsReturned()
    {
      // Arrange
      var course = new Course()
      {
        Name = "ARTD 201",
        Title = "graphic design",
        Credits = 3.0,
        Description = "graphic design descr"
      };

      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { course });

      mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
        new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
        });
      mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course
                }
            });

      var goalId = "CG1";
      var semester = "Spring 2021";
      var courseServices = new CourseServices(mockRepository.Object);

      // Act
      var result = courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester);

      // Assert
      var itemInList = Assert.Single(result);

      // Assert.Equal(2, result.Count());
      Assert.Equal(semester, itemInList.Semester);
      Assert.Equal(course.Name, itemInList.TheCourse.Name);
    }
    [Fact]
    public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndMultipleCourseOfferingsAreInSemester_OfferingsAreReturned()
    {
      // Arrange
      var course = new Course()
      {
        Name = "ARTD 201",
        Title = "graphic design",
        Credits = 3.0,
        Description = "graphic design descr"
      };
      var course2 = new Course()
      {
        Name = "ARTS 101",
        Title = "art studio",
        Credits = 3.0,
        Description = "studio descr"
      };

      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { course, course2 });

      mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
        new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
        });
      mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = course2
                }
            });
      var goalId = "CG1";
      var semester = "Spring 2021";
      var courseServices = new CourseServices(mockRepository.Object);

      // Act
      var result = courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester);

      // Assert
      Assert.Equal(2, result.Count());
      Assert.Equal(semester, result[0].Semester);
      Assert.Equal(course.Name, result[0].TheCourse.Name);
      Assert.Equal(semester, result[1].Semester);
      Assert.Equal(course2.Name, result[1].TheCourse.Name);
    }

    [Fact]
    public void GetOfferingsByGoalIdAndSemester_GoalIsFoundAndNoCourseOfferingIsInSemester_EmptyListIsReturned()
    {
      // Arrange
      var course = new Course()
      {
        Name = "ARTD 201",
        Title = "graphic design",
        Credits = 3.0,
        Description = "graphic design descr"
      };

      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { course });

      mockRepository.Setup(m => m.Goals).Returns(new List<CoreGoal>(){
        new CoreGoal() {
                Courses = GetTestCourses(),
                Description = "test",
                Id = "CG1",
                Name = "English Literacy"
            }
        });
      mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2022",
                    TheCourse = course
                },
      });
      var goalId = "CG1";
      var semester = "Spring 2021";
      var courseServices = new CourseServices(mockRepository.Object);

      //Act
      var result = courseServices.GetOfferingsByGoalIdAndSemester(goalId, semester);

      // Assert
      Assert.Equal(0, result.Count());
    }

    [Fact]
    public void getCourses_MultipleCoursesAreFound_ReturnsAllCourses()
    {
      // Arrange
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
      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { c1, c2, c3, c4 });
      var courseServices = new CourseServices(mockRepository.Object);

      // Act 
      var courses = courseServices.GetCourses();

      // Assert 
      Assert.Equal(4, courses.Count());

      Assert.Equal(c1.Name, courses[0].Name);
      Assert.Equal(c1.Title, courses[0].Title);
      Assert.Equal(c1.Credits, courses[0].Credits);
      Assert.Equal(c1.Description, courses[0].Description);
      Assert.Equal(c1.Department, courses[0].Department);

      Assert.Equal(c2.Name, courses[1].Name);
      Assert.Equal(c2.Title, courses[1].Title);
      Assert.Equal(c2.Credits, courses[1].Credits);
      Assert.Equal(c2.Description, courses[1].Description);
      Assert.Equal(c2.Department, courses[1].Department);

      Assert.Equal(c3.Name, courses[2].Name);
      Assert.Equal(c3.Title, courses[2].Title);
      Assert.Equal(c3.Credits, courses[2].Credits);
      Assert.Equal(c3.Description, courses[2].Description);
      Assert.Equal(c3.Department, courses[2].Department);

      Assert.Equal(c4.Name, courses[3].Name);
      Assert.Equal(c4.Title, courses[3].Title);
      Assert.Equal(c4.Credits, courses[3].Credits);
      Assert.Equal(c4.Description, courses[3].Description);
      Assert.Equal(c4.Department, courses[3].Department);
    }

    [Fact]
    public void getCourses_NoCoursesAreFound_ReturnsNoCourses()
    {
      // Arrange
      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { });
      var courseServices = new CourseServices(mockRepository.Object);

      // Act 
      var courses = courseServices.GetCourses();

      // Assert 
      Assert.Equal(0, courses.Count());
    }

    [Fact]
    public void getCourses_SingleCoursesFound_ReturnsSingleCourses()
    {
      // Arrange
      Course c1 = new Course()
      {
        Name = "ARTS 201",
        Title = "Graphic Design",
        Credits = 3.0,
        Description = "More valuable than you might realize",
        Department = "ARTS"
      };
      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { c1 });
      var courseServices = new CourseServices(mockRepository.Object);

      // Act 
      var courses = courseServices.GetCourses();

      // Assert 
      Assert.Equal(1, courses.Count());

      Assert.Equal(c1.Name, courses[0].Name);
      Assert.Equal(c1.Title, courses[0].Title);
      Assert.Equal(c1.Credits, courses[0].Credits);
      Assert.Equal(c1.Description, courses[0].Description);
      Assert.Equal(c1.Department, courses[0].Department);
    }

    [Fact]
    public void GetAllCourseOfferingsBySemester_UserProvidesSemester_ReturnsAvailableCourses()
    {
      // Arrange
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
      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { c1, c2, c3, c4 });
      mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c1
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c2
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Fall 2021",
                    TheCourse = c3
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c4
                }
            });
      var courseServices = new CourseServices(mockRepository.Object);
      var semester = "Spring 2021";

      // Act 
      var courses = courseServices.GetAllCourseOfferingsBySemester(semester);

      // Assert 
      Assert.Equal(3, courses.Count());

      AssemblyLoadEventArgs.Equals(semester, courses[0].Semester);
      AssemblyLoadEventArgs.Equals(semester, courses[1].Semester);
      AssemblyLoadEventArgs.Equals(semester, courses[2].Semester);
    }

    [Theory]
    [InlineData(2, "Spring 2021")]
    [InlineData(2, "Fall 2021")]
    [InlineData(1, "Summer 2021")]
    public void GetAllCourseOfferingsBySemester_UserProvidesMultipleSemesters_ReturnsAvailableCourses(int expectedCourses, string semester)
    {
      // Arrange
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
      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { c1, c2, c3, c4 });
      mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c1
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c2
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Fall 2021",
                    TheCourse = c3
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Fall 2021",
                    TheCourse = c4
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Summer 2021",
                    TheCourse = c4
                }
            });
      var courseServices = new CourseServices(mockRepository.Object);

      // Act 
      var courses = courseServices.GetAllCourseOfferingsBySemester(semester);

      // Assert 
      Assert.Equal(expectedCourses, courses.Count());
      Assert.Equal(semester, courses[0].Semester);
    }

    [Fact]
    public void GetCourseOfferingsBySemesterAndDept_UserGivesSemesterAndDept_ReturnsAvailCourses()
    {
      // Arrange
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
      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { c1, c2, c3, c4 });
      mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c1
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c2
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Fall 2021",
                    TheCourse = c3
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Fall 2021",
                    TheCourse = c4
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Summer 2021",
                    TheCourse = c4
                }
            });
      var courseServices = new CourseServices(mockRepository.Object);
      var semester = "Spring 2021";
      var department = "ARTS";

      // Act 
      var courses = courseServices.GetCourseOfferingsBySemesterAndDept(semester, department);

      // Assert
      Assert.Equal(2, courses.Count());

      AssemblyLoadEventArgs.Equals(semester, courses[0].Semester);
      AssemblyLoadEventArgs.Equals(department, courses[0].TheCourse.Department);
      AssemblyLoadEventArgs.Equals(semester, courses[1].Semester);
      AssemblyLoadEventArgs.Equals(department, courses[1].TheCourse.Department);
    }

    [Fact]
    public void GetCourseOfferingsBySemesterAndDept_UserGivesSemesterAndDept_ReturnsNoAvailCourses()
    {
      // Arrange
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
      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { c1, c2, c3, c4 });
      mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c1
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c2
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Fall 2021",
                    TheCourse = c3
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Fall 2021",
                    TheCourse = c4
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Summer 2021",
                    TheCourse = c4
                }
            });
      var courseServices = new CourseServices(mockRepository.Object);
      var semester = "Spring 2021";
      var department = "CSCI";

      // Act 
      var courses = courseServices.GetCourseOfferingsBySemesterAndDept(semester, department);

      // Assert
      Assert.Equal(0, courses.Count());
    }

    [Theory]
    [InlineData(2, "Spring 2021", "ARTS")]
    [InlineData(1, "Fall 2021", "MATH")]
    [InlineData(1, "Summer 2021", "ENGL")]
    public void GetCourseOfferingsBySemesterAndDept_UserMultipleSemesterAndDept_ReturnsAvailCourses(int expectedCourses, string semester, string department)
    {
      // Arrange
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
      var mockRepository = new Mock<ICourseRepository>();
      mockRepository.Setup(m => m.Courses).Returns(new List<Course> { c1, c2, c3, c4 });
      mockRepository.Setup(m => m.Offerings).Returns(new List<CourseOffering>() {
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c1
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Spring 2021",
                    TheCourse = c2
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Fall 2021",
                    TheCourse = c3
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Fall 2021",
                    TheCourse = c4
                },
                new CourseOffering() {
                    Section = "1",
                    Semester = "Summer 2021",
                    TheCourse = c4
                }
            });
      var courseServices = new CourseServices(mockRepository.Object);

      // Act 
      var courses = courseServices.GetCourseOfferingsBySemesterAndDept(semester, department);

      // Assert
      Assert.Equal(expectedCourses, courses.Count());

      AssemblyLoadEventArgs.Equals(semester, courses[0].Semester);
      AssemblyLoadEventArgs.Equals(department, courses[0].TheCourse.Department);
    }

    private List<Course> GetTestCourses()
    {
      return new List<Course>(){
            new Course()
            {
                Name="ARTD 201",
                Title="graphic design",
                Credits=3.0,
                Description="graphic design descr"
            },
            new Course()
            {
                Name="ARTS 101",
                Title="art studio",
                Credits=3.0,
                Description="studio descr"
            }
      };
    }
  }
}
