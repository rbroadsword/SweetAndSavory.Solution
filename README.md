# Divine Treats

#### By: Ryan Broadsword

#### Application that allows the user to create a account, login and out and users that are logged in have the ability to create Flavors and Treats! 

## Technologies Used

* C#
* .Net 5
* REPL
* CSS
* HTML
* SQL
* MySQL Workbench 
* Entity
* Identity


## Description 

Pierre's Sweet and Savory Treats
Pierre is back! He wants you to create a new application to market his sweet and savory treats. This time, he would like you to build an application with user authentication and a many-to-many relationship. Here are the features he wants in the application:

The application should have user authentication. A user should be able to log in and log out. Only logged in users should have create, update and delete functionality. All users should be able to have read functionality.
There should be a many-to-many relationship between Treats and Flavors. A treat can have many flavors (such as sweet, savory, spicy, or creamy) and a flavor can have many treats. For instance, the "sweet" flavor could include chocolate croissants, cheesecake, and so on.
A user should be able to navigate to a splash page that lists all treats and flavors. Users should be able to click on an individual treat or flavor to see all the treats/flavors that belong to it.

## Setup/Installation Requirements

### Clone project
* clone repository
* open in vs code
* open terminal
* in terminal run "dotnet build" to make sure everything is up to date.
* in terminal run "dotnet test" to see test results for functionality. 
* in termianl run "dotnet run" to run the program and follow the prompts given in the console. 
* select localhost:5000/ to launch application in browser.

### Create your database schema and tables! 
* in terminal run "dotnet ef migrations add Initial"
* in terminal run "dotnet ef database update"
* check SQL Workbench and refresh schemas to check that sweetandsavory db was successful. 

### Create appsettings.json
* navigate to project folder in terminal "cd SweetAndSavory"
* In termianl add "touch appsettings.json" 
* in the appsettings.json file add "{
  "ConnectionStrings": {
      "DefaultConnection": "Server=localhost;Port=3306;database=sweetandsavory;uid=root;pwd=[YOUR PASSWORD HERE];"
  }
}
* add your password for SQL where it says pwd=[YOUR PASSWORD HERE] 


## Known Bugs

* Styling WIP


## License

Not currently licensed 

Copyright (c) August 27th 2022, by Ryan Broadsword rbroadsword@gmail.com 