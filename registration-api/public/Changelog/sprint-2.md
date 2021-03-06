<img
          src="https://upload.wikimedia.org/wikipedia/en/thumb/e/ef/Coastal_Carolina_Chanticleers_logo.svg/1200px-Coastal_Carolina_Chanticleers_logo.svg.png"
          height="100"
          alt="War Chicken"
        />

# Changelog: Sprint 2

### _v0.0.2_

## Status: Complete

---

In this sprint the project aims to implement the following API endpoints for utilization:

### GetCourses

- Method: GET
- URL: https://localhost:5001/courses
- Description: Returns a list of all courses from the repository
- _implemented_
- task ID: 21111

### GetCourseByName

- Method: GET
- URL: https://localhost:5001/courses/{courseName}
- Description: Returns course details from a course name
- _implemented_
- task ID: 21112

### GetCoursesByDept

- Method: GET
- URL: https://localhost:5001/courses/search?dept={departmentName}
- Description: Returns a list of all courses from a department within the repository
- _implemented_
- task ID: 21113

### CreateCourse

- Method: POST
- URL: https://localhost:5001/courses
- Body: JSON object with the values for {Name, Title, Credits, Description, Department}
- Description: Creates a new course in the repository
- _implemented_
- task ID: 21114

### UpdateCourse

- Method: PUT
- URL: https://localhost:5001/courses
- Body: JSON object with the values for {Name, Title, Credits, Description, Department}
- Description: Updates a course within the repository
- _implemented_
- task ID: 21115

### DeleteCourse

- Method: DELETE
- URL: https://localhost:5001/courses/{courseName}
- Description: Deletes a course from the repository
- _implemented_
- task ID: 21116

### GetGoalsByCourse

- Method: GET
- URL: https://localhost:5001/courses/goals/{courseName}
- Description: Returns all core goals that are met by a course
- _implemented_
- task ID: 21117

### GetCourseOfferingsBySemester

- Method: GET
- URL: https://localhost:5001/courses/{courseName}/offerings?semester={semesterName}
- Description: Returns all offerings for a particular course within a semester
- _implemented_
- task ID: 21118

---

The following exclusions were placed on hold indefinetly

#### User Story Five:

- As a student I want to see all courses that meet a core goal, so that I can plan out my courses over the next few semesters and choose core courses that make sense for me

#### User Story Six:

- As a student I want to find a course that meets two different core goals, so that I can "feed two birds with one seed"...save time by taking one class that will fulfill two requirements

#### User Story Seven:

- As a freshman adviser, I want to see all the core goals which do not have any course offerings
  for a given semester, so that I can work with departments to get some courses offered
  that students can take to meet those goals

---

### Source Code: https://github.com/CCU-Computing/registration-api-fa21-grigsby9
