# Projeto Backend API Restful Despesas Pessoais 

## Introduction

In summary, this project involves an update of the Restful API application created in the current final project, upgrading from .NET Core 3.1 to .NET Core 7.0. Test-Driven Development (TDD) is implemented using XUnit, generating test coverage reports locally with ReportGenerator. CI/CD is implemented with GitActions/Workflows, which perform "Build, Unit Testing, and Static Code Analysis in the cloud using Sonar Cloud." The Restful API documentation is created using Swagger, and the application is dockerized. Document and image storage is done on an Amazon S3 bucket file server, with integration with either MySql Server or Sql Server through the Entity Framework using Migrations to control versioning of entities or tables created or updated during the project's evolution. The application is currently in production on an AWS EC2 server, ensuring high availability.

## Application in Production 

This project can be access at [API Restful Despesas Pessoais](http://alexfariakof.com:42535/swagger).
![backend](https://github.com/alexfariakof/despesas-backend-api-net-core/assets/42475620/d715b8d3-e275-4998-beb7-cb8c9690513f)

## Build

Run `dotnet build -restore ` to build the project. The build artifacts will be stored in the `bin/` directory. 

## Development server without automatically reload

* First way
     > In windows at root project path "CMD or Powershell" Run `dotnet run --project ./despesas-backend-api-net-core`. Navigate to `http://localhost:42535/swagger` or `https://localhost/swagger`.
     
     > In Linux bash at root project path Run `dotnet run --project ./despesas-backend-api-net-core`. Navigate to `http://localhost:42535/swagger` or `https://localhost/swagger`.
  
* Second way
     > In windows at root path "CMD or Powershell" Run `./run.ps1 `. Navigate to `http://localhost:42535/swagger` or `https://localhost/swagger`.
     
     > In Linux bash at root path Run `./run.sh`. Navigate to `http://localhost:42535/swagger` or `https://localhost/swagger`.

## Development server with automatically reload

Make sure have instaled tool watch, if not Run `dotnet tool install --global dotnet-watch`

* First way
     > In windows at root project path CMD or Powershell Run `dotnet watch run --project ./despesas-backend-api-net-core`.  Navigate to `http://localhost:42535/swagger` or `https://localhost/swagger`. The application automatically restart it when changes are detected.
     
     > In Linux bash at root project path Run `dotnet watch run --project ./despesas-backend-api-net-core`. Navigate to `http://localhost:42535/swagger` or `https://localhost/swagger`. The application  automatically restart it when changes are detected.
  
* Second way
     > In windows at root project path CMD or Powershell Run `./run.ps1 -w`. The application will open in default browser and automatically restart it when changes are detected.
     
     > In Linux bash at root project path Run `./run.sh -w`. Navigate to `http://localhost:42535/swagger` or `https://localhost/swagger`.

## Running Unit Tests

Run `dotnet test` to execute the unit tests.

## Running Unit Tests and Generate Report Test Coverage

Make sure have instaled tool ReportGenerator, if not Run `dotnet tool install --global dotnet-reportgenerator-globaltool`

* Report Generator
     > In windows at root project path CMD or Powershell Run `./generate_coverage_report.ps1`. The Report will open in default browser automatically.      
     
     > In Linux bash at root project path Run `./generate_coverage_report.sh`. The Report will open in default browser automatically.

![reportTestCoverage](https://github.com/alexfariakof/despesas-backend-api-net-core/assets/42475620/afd1b5e4-5a2f-490c-bf4f-a530df41c1ae)





<h2 align="center"> 
   Backend API Restful DESPESAS PESSOAIS 
</h2>

<h2 align="left"> 
Acesso documentação da API no Swagger <a target="_blank" href="http://alexfariakof.com:42535/swagger/index.html" target="_parent" >backend do projeto</a>
      <h6 align="justify">
        Obs.: Não é possivél executar através do swagger a maioria dos end-points pois todos estam com autenticação via Bearer Token. Utilizar Workspace Postaman é uma opção depois de realizar cadastro, fazer o singIn que retorna o token para ser utilizado nos headers dos end points.
    </h6>
</h2>

<h2 align="left"> 
Acesso a aplicação atualizada em produção  <a href="http://alexfariakof.com" target="_parent" >Despesas Pessoais</a> 
    <h6 align="justify">
    Obs.: Existe um usuário teste já criado para acessar aplicação "login=teste@teste.com/senha=teste"
    </h6>        
</h2>

<h2 align="left"> 
## Sonar Cloud
SonarCloud is a cloud-based static code analysis platform that helps development teams maintain code quality and identify issues early in the software development process. It offers automated code review, continuous inspection, and code analytics. SonarCloud scans your code for bugs, vulnerabilities, and code smells, providing actionable feedback to improve code quality and security. It is an essential tool for ensuring that your software projects are maintainable, reliable, and secure. via [Sonar Cloud](https://sonarcloud.io/).
[Overview Project in Sonar Cloud](https://sonarcloud.io/project/overview?id=alexfariakof_despesas-frontend-angular)    
</h2>

# Descrição 
<h5> 
   <p>API Rest HATEOAS CI/CD Dockerizada usando .Net core 7.0 com banco de dados mysql server,  Entity Framework e Migrations. Novas funcinalidades como TTD e CI/CD estam em processo de   implantação juntamento com verficação Coverage através do Cloud Sonar.
   </p>
</h5>

# Configurações de Segurança
<h5> 
   <ul>
      <li>   
         <h6>Controle de Acesso/Senha de Usuários</h6>
            <p>As Senhas estão cripitografadas e não são expostas em nenhuma requisição e por uma chave que não esta exposta no repositorio acessada pelo classe Crypto em um arquivo criado dentro do servidor. O ideal seria a acessar uma chave crida dentro do própio Azure ou AWS criando assim mais segurança.
         </p>
      </li>
      <li>
         <h6>Camada Model View da API</h6>
            <p>A Camada repositorio está idependente e não pode ser vista sua implementação pois o modelo   exposto nos endpoint são Objetos modelView que não espõem a implementação do banco.
         </p>
      </li>
      <li>
         <h6>Acesso a API</h6>
            <p> Apenas Primeiro Acesso no caso cadastro de um novo usuário, Login do usuário e recuperação de senhas podem ser acessados sem a necessidade de realizar login com usuário e senhas validadas. O restante da API só pode ser acessada através de um accessToken gerado pela API. 
         </p>
      </li>
      <li>
         <h6>Arquivos de Configuração </h6>
            <p> Arquivos contendo chaves de configuração com chave de cripitografia,  acesso ao banco de dados e amazon S3 estão protegidos pois não se encontram expostos no repositório GitHub . 
         </p>
      </li>
      <li>
         <h6>Controle de Usuários </h6>
            <p> Apenas usuários com perfil de administrador podem listar os usuários e efetuar a deleção de usuários. Por questões de segurança das informações dos usuários cadastrados todos os acessos ao Endpoint usuários estão com Auntenticação JWT Bearer Ativadas.
         </p>
      </li>                 
 </ul>
</h5>

![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=vulnerabilities) ![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=bugs) ![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=security_rating) ![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=sqale_rating) ![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=code_smells) ![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=ncloc) ![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=coverage) ![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=sqale_index) ![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=alert_status) ![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=reliability_rating) ![alt text](https://sonarcloud.io/api/project_badges/measure?project=alexfariakof_despesas-backend-api-net-core&metric=duplicated_lines_density)
