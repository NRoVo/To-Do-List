# To-Do-List
A standart starting project for most of beginner level web developers. 

# What
This application is a simple version of task manager. You could set multiple tasks, mark any of it as complete or incomplete, delete any of it. This web application is based on 
standart MVC pattern.

# Features
* ```EF Core``` and ```MS SQL server``` were integrated into the application to store all entities into the database.
* DTO Validation is done with the ```FluentValidation``` library. The validators are covered with unit-tests.
* The API controllers don't contain any code. Instead, they delegate into a mediator. For each request, there is a separate Handler class. The app uses the ```MediatR``` library as a mediator. Every request handler is covered by unit tests.
was used for this purpose. In this application controller role is not about doing actual work, but triggering actual work. Covered by unit tests.
* Paging was initialized in the ```GetTodoListHandler``` class inside of ```Handle``` method bu usind ```Skip()``` and ```Take()``` LINQ methods for avoid loading everything from the DB table.
* A simple frontend view was created by using HTML, CSS, and Razor Pages. Razor Pages call into aforementioned request handlers through a mediator to avoid code duplication.
