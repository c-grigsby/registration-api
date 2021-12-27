<div align="center" markdown="1">

<img
          src="https://upload.wikimedia.org/wikipedia/en/thumb/e/ef/Coastal_Carolina_Chanticleers_logo.svg/1200px-Coastal_Carolina_Chanticleers_logo.svg.png"
          height="140"
          alt="War Chicken"
        />

# Course Registration Back-end

![dotnet](https://img.shields.io/badge/.NET-v5.0-teal)
![language](https://img.shields.io/badge/language-C%23-orange)

This server-side application is the back-end to the course registration application
<br/>
_Coursework for CCU_

</div>

---

## Project Specifications

- Developed within .NET Core web API framework
- REST API to query course information via departments, core goals, & semesters
- Persists data via MySQL
- create, read, update, delete (CRUD) services
- xUnit.net for testing
- Controller, Service, and Repository layers

---

#### This application utilizes a .env file to host environment variables. To configure:

- ##### connectionString={MySQLConnectionString}

---

## Changelog

- [v0.0.1](./registration-api/public/Changelog/sprint-1.md)
- [v0.0.2](./registration-api/public/Changelog/sprint-2.md)
- [v0.0.3](./registration-api/public/Changelog/sprint-3.md)

---

## API References

### GetCourses

- Method: GET
- URL: https://localhost:5001/courses
- Description: Returns a list of all courses from the repository

### GetCourseByName

- Method: GET
- URL: https://localhost:5001/courses/{courseName}
- Description: Returns course details from a course name

### GetCoursesByDept

- Method: GET
- URL: https://localhost:5001/courses/search?dept={departmentName}
- Description: Returns a list of all courses from a department within the repository

### CreateCourse

- Method: POST
- URL: https://localhost:5001/courses
- Body: JSON object with the values for {Name, Title, Credits, Description, Department}
- Description: Creates a new course in the repository

### UpdateCourse

- Method: PUT
- URL: https://localhost:5001/courses
- Body: JSON object with the values for {Name, Title, Credits, Description, Department}
- Description: Updates a course within the repository

### DeleteCourse

- Method: DELETE
- URL: https://localhost:5001/courses/{courseName}
- Description: Deletes a course from the repository

### GetGoalsByCourse

- Method: GET
- URL: https://localhost:5001/courses/goals/{courseName}
- Description: Returns all core goals that are met by a course

### GetCourseOfferingsBySemester

- Method: GET
- URL: https://localhost:5001/courses/{courseName}/offerings?semester={semesterName}
- Description: Returns all offerings for a particular course within a semester

---
