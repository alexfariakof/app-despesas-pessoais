# Backend API Restful Despesas Pessoais 

## Introduction

In summary, this project involves an update of the Restful API application created in the current final project, upgrading from .NET Core 3.1 to .NET Core 8.0. Test-Driven Development (TDD) is implemented using XUnit, generating test coverage reports locally with ReportGenerator. CI/CD is implemented with GitActions/Workflows, which perform "Build, Unit Testing, and Static Code Analysis in the cloud using Sonar Cloud." The Restful HATEOAS API documentation is created using Swagger, and the application is dockerized. Document and image storage is done on an Amazon S3 bucket file server, with integration with either MySql Server or Sql Server through the Entity Framework using Migrations to control versioning of entities or tables created or updated during the project's evolution. The application is currently in production on an AWS EC2 server, ensuring high availability.

## Postman Documentation 
This project can be access [Postman Documentation](https://bold-eclipse-872793.postman.co/workspace/local-api-despesas-pessoais~bb08206c-ff0d-44c9-b49e-55339a554a3b/overview)

## Application in Production 
This project can be access at [Production API Restful Despesas Pessoais](http://alexfariakof.com:42535/swagger).

## Application in Development 
This project can be access at [Development API Restful Despesas Pessoais](http://alexfariakof.com:42536/swagger).

![image](https://github.com/alexfariakof/despesas-backend-api-net-core/assets/42475620/c0abe2f5-da31-4907-90dc-bbb06a95d2f3)


## Build

Run `dotnet build -restore ` to build the project. The build artifacts will be stored in the `bin/` directory. 

## Development server without automatically reload

* First way

  Run `dotnet run --project ./despesas-backend-api-net-core`. Navigate to `http://localhost:42535/swagger` or `https://localhost/swagger`.

* Second way

  Run `./run.ps1 ` if using windows, or Run `./run.sh` if using linux. Navigate to `http://localhost:42535/swagger` or `https://localhost/swagger`.

## Development server with automatically reload

Make sure have instaled tool watch, if not Run `dotnet tool install --global dotnet-watch`

* First way

  Run `dotnet watch run --project ./despesas-backend-api-net-core`. Navigate to `http://localhost:42535/swagger` or `https://localhost/swagger`. The application automatically restart it when changes are detected.
  
* Second way

  Run `./run.ps1 -w` if using windows, or Run `./run.sh -w` if using linux. The application will open in default browser and automatically restart it when changes are detected.

## Development server in Docker with Database Localy

Make sure have instaled Docker Engine instaled, if not go to [Install Docker Engine](https://docs.docker.com/engine/install/).

* First way

  Run `docker-compose -f .\docker-compose.database.yml up -d`.  Navigate to `http://localhost:42535/swagger`. 
  
* Second way

  Run `./rundocker.ps1 -local` if using windows, or Run `./rundocker.sh -local` is using linux. The application will open in default browser.

## Development server in Docker without Database Localy 
In this case the application will work correctely only in branch database-in-memory

Make sure have instaled Docker Engine instaled, if not go to [Install Docker Engine](https://docs.docker.com/engine/install/).

* First way

  Run `docker-compose -f .\docker-compose.yml up -d`.  Navigate to `http://localhost:42535/swagger`. 
  
* Second way

  Run  `./rundocker.ps1` if using windows, or Run `./rundocker.sh` if using linux. The application will open in default browser.

  

## Running Unit Tests

Run `dotnet test` to execute the unit tests.

## Running Unit Tests and Generate Report Test Coverage

Make sure have instaled tool ReportGenerator, if not Run `dotnet tool install --global dotnet-reportgenerator-globaltool`

Run  `./generate_coverage_report.ps1` if using windows, or Run `./generate_coverage_report.sh` if using linux. The Report will open in default browser automatically.

[Overview Report Coverage Results](http://alexfariakof.com:42536/coveragereport/index.html) 
![reportTestCoverage](https://github.com/alexfariakof/despesas-backend-api-net-core/assets/42475620/afd1b5e4-5a2f-490c-bf4f-a530df41c1ae)

## Security Settings
 <ul>
      <li>   
         <h6>User Access/Password Control</h6>
            <p>Passwords are encrypted and not exposed in any requests, and they are managed by a key  accessed by the Crypto class in a file created on the server. Ideally, the access should be through a key created within Azure or AWS, thereby enhancing security.
         </p>
      </li>
      <li>
         <h6>API Model-View Layer</h6>
            <p>The repository layer is independent, and its implementation is not visible because the model exposed in the endpoints is model-view objects that do not reveal the database implementation.
         </p>
      </li>
      <li>
         <h6>API Access</h6>
            <p>Only first access, such as user registration, user login, and password recovery, can be accessed without the need to log in with validated user and passwords. The rest of the API can only be accessed using an access token generated by the API. 
         </p>
      </li>
      <li>
         <h6>User Control</h6>
            <p>Only users with administrator profiles can list users and delete users. For the security of registered user information, all accesses to the user endpoint require JWT Bearer Authentication.
         </p>
      </li>                 
 </ul>


## Sonar Cloud

SonarCloud is a cloud-based static code analysis platform that helps development teams maintain code quality and identify issues early in the software development process. It offers automated code review, continuous inspection, and code analytics. SonarCloud scans your code for bugs, vulnerabilities, and code smells, providing actionable feedback to improve code quality and security. It is an essential tool for ensuring that your software projects are maintainable, reliable, and secure. via [Sonar Cloud](https://sonarcloud.io/).

This project Overview in Sonar Cloud can be access at [Overview Project in Sonar Cloud](https://sonarcloud.io/project/overview?id=alexfariakof_app-despesas-pessoais)) 
![sonarcloud](https://github.com/alexfariakof/despesas-backend-api-net-core/assets/42475620/fd4b2bc7-c254-438b-8194-a07ec62da86b)

