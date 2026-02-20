# Chinese Auction API (WIP)
  Web API-based Chinese Auction management system using ASP.NET Core technology.
  
   🚧 This project is currently under active development. 
    Expect breaking changes and incomplete features

   This project is the backend server built with .NET. It communicates with a client frontend in the following link:
   [Frontend Repository](https://github.com/naama-git/ChineseAuctionClient)

  ## Project Structure
  ```
   ChineseAuctionAPI/
  ├── Controllers/       
  ├── Data/              # Database context (EF Core) and configuration
  ├── DTO/               
  ├── Interface/         
  ├── Logs/              
  ├── Mapping/           
  ├── Middlewares/      
  ├── Migrations/        
  ├── Models/            # Database entities and domain models
       ├── Exceptions 
       └── QueryParams    
  ├── Public/            
  ├── Repositories/      
  ├── Services/         
  ├── Validations/       
  ├── wwwroot/           
  ├── .gitignore         
  ├── appsettings.json     
  ├── ChineseAuctionAPI.http 
  ├── Program.cs         
  └── README.md
```
   
   ## Tech Stack

   - Framework: .NET 8 (ASP.NET Core)
   - Database: SQLserver 
   - ORM : Entity Framwork 8.0.x
   - Migrations : EF Core Migrations
   - Authentication: JWT Bearer Token
   - Logging : Serilog (File & Console sinks)
   - Mapping : AutoMapper
   - Filtering : AutoFilterer
   - Validation : FluentValidation
   - API Docs : Swagger UI

## Getting Started

  ### Prerequisites
  Before running this project, ensure you have the following installed:
  - NET 8 SDK
  - SQL Server
  - Visual Studio 2022 or VS Code
  - EF Core Tools: install via terminal
    ```
    $ dotnet tool install --global dotnet-ef
    ```
  
  ### Installation & Configuration
  - ```
    $ git clone https://github.com/naama-git/chinese-auction-api.git
    $ dotnet restore
    ```
  
  - Copy the contents of [appsettings.Example.json](https://github.com/naama-git/chinese-auction-api/blob/main/appsettings.Example.json) to a new file named appsettings.json and fill in the details.
  - run:
    ```
     $ dotnet ef database update
    ```
  - Run the Application:
    ```
    $ dotnet build
    ```

## Usage
  Once the application is running, you can access the Swagger UI at:
  https://localhost:7156/swagger
  


