# To-Do-List
A standart starting project for most of beginner level web developers. 

# What
This application is a simple version of task manager. You could set multiple tasks, mark any of it as complete or incomplete, delete any of it. This web application is based on 
standart MVC pattern.

# Features
* ```EF Core``` and ```MS SQL server``` were integrated into the application to store all entities into the database.
* Validation for models was integrated by using ```FluentValidation``` framework to ensure that DTO instances will be added in the right view. Covered by unit tests.
* Code inside of menthods of controller was extracted to separate classes Handlers for avoid creating different controllers doing the same thing in the future. The ```MediatR``` library
was used for this purpose. In this application controller role is not about doing actual work, but triggering actual work. Covered by unit tests.
* Paging was initialized in the ```GetTodoListHandler``` class inside of ```Handle``` method bu usind ```Skip()``` and ```Take()``` LINQ methods for avoid loading everything from the DB table.
* A simple frontend view was created by using HTML and CSS. Integration with backend was made by using HTML forms and methods from Razor Pages.
