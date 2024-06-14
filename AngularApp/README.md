# Frontend Angular Despesas Pessoais 

## Cloud Application in Production 

  > This project can be access at [Despesas Pessoais In Production](http://alexfariakof.com).

## Cloud Application in Development 

  > This project can be access at [Despesas Pessoais In Development](http://alexfariakof.com:4200).

![image](https://github.com/alexfariakof/despesas-frontend-angular/assets/42475620/9f614cb1-434f-4d26-a795-ae4ada0c571a)


## Run Local Development Server
  > This server points the endpoint to the local application API.

Run `npm start` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Run Development Server
  > This server points the endpoint to the development application API.
> 
Run `npm run start:dev` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Run Production Server
  > This server points the endpoint to the production application API.

Run `npm run start:prod` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Run Local Development Server In Docker
  > This build may take a little while, be patient.

  > This server points the endpoint to the local application API.

  > Make sure have instaled Docker Engine instaled, if not go to [Install Docker Engine](https://docs.docker.com/engine/install/).

Run `docker-compose up --build` for a dev server in docker. Navigate to `http://localhost:4200/`. The application will not automatically reload if you change any of the source files.

## Run Development Server In Docker
  > This build may take a little while, be patient.

  > This server points the endpoint to the development application API.

  > Make sure have instaled Docker Engine instaled, if not go to [Install Docker Engine](https://docs.docker.com/engine/install/).

Run `docker-compose -f .\docker-compose.dev.yml  up --build` for a dev server. Navigate to `http://localhost:4200/`. The application will not automatically reload if you change any of the source files.

## Run Production Server In Docker
  > This build may take a little while, be patient.

  > This server points the endpoint to the production application API.

  > Make sure have instaled Docker Engine instaled, if not go to [Install Docker Engine](https://docs.docker.com/engine/install/).

Run `docker-compose -f .\docker-compose.prod.yml  up --build` for a dev server. Navigate to `http://localhost:3000/`. The application will not automatically reload if you change any of the source files.

## Code Scaffolding New Components

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Builds 

* Development
  
  Run `npm run build:dev` to build the project with development configurations. The build artifacts will be stored in the `dist/` directory.

* Production
  
  Run `ng build` or `npm run build:prod` to build the project with production configurations. The build artifacts will be stored in the `dist/` directory.

## Run Unit Tests

Run `npm run test` or  `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Run Tests With Report Coverage

Run `npm run test:coverage ` or `ng test --code-coverage --no-watch` to execute the tests and generate coverage report. The Report will not be open automatically.

Run  `./generate_coverage_report.ps1` if using windows, or Run `./generate_coverage_report.sh` if using linux. The Report will open in default browser automatically.

![image](https://github.com/alexfariakof/despesas-frontend-angular/assets/42475620/4c113589-e49a-40eb-af94-673da9a43c27)

## Tests End-to-End With Python/Playwright
> This project can be access at [Tests End-to-End With Python/Playwright](https://github.com/alexfariakof/despesas-frontend-angular-tests-e2e).

The end-to-end tests have been implemented in a separate project using Python with Playwright and are integrated into the project's workflow. These tests are executed at two key points: when a push is made to the pre-release branch or any branch with the naming convention "release/*," and during pull requests to the pre-release branch. This setup ensures that the test executions are always aligned with the latest development environment, enhancing the quality and reliability of our end-to-end tests. Playwright, as an open-source framework for testing web applications, provides powerful automation capabilities for web interactions and supports various browsers. It is recognized for its flexibility, concise syntax, and speed. We have chosen to leverage Playwright to achieve these testing objectives, further strengthening the robustness of our application.

![report_playwright](https://github.com/alexfariakof/despesas-frontend-angular/assets/42475620/e99a0471-ce74-42b4-8fb9-2e03f6679f87)

## Sonar Cloud

SonarCloud is a cloud-based static code analysis platform that helps development teams maintain code quality and identify issues early in the software development process. It offers automated code review, continuous inspection, and code analytics. SonarCloud scans your code for bugs, vulnerabilities, and code smells, providing actionable feedback to improve code quality and security. It is an essential tool for ensuring that your software projects are maintainable, reliable, and secure. via [Sonar Cloud](https://sonarcloud.io/).

[Overview Project in Sonar Cloud](https://sonarcloud.io/project/overview?id=alexfariakof_despesas-frontend-angular) 
![sonar_cloud](https://github.com/alexfariakof/despesas-frontend-angular/assets/42475620/6001490b-74f5-4c28-a4d9-c16bd60f333b)

[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular) [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular) [![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular) [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular) [![Coverage](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=coverage)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular) [![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular) [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular) [![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular) [![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular) [![Bugs](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-frontend-angular&metric=bugs)](https://sonarcloud.io/summary/new_code?id=alexfariakof_despesas-frontend-angular)

