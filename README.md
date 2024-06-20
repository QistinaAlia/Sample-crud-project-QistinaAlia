# Setting Up and Connecting ASP.NET Core Project (Simple CRUD) with SQL Server in Visual Studio

This guide will walk you through setting up and connecting an ASP.NET Core project that uses SQL Server for the database. Follow these steps to get started:

## Prerequisites

1. **Visual Studio 2022**: Make sure you have Visual Studio installed (Preferably Visual Studios 2022). You can download it from [Visual Studio Downloads](https://visualstudio.microsoft.com/downloads/). (!! Make sure to include 'ASP.NET and web development workload')

2. **SQL Server**: Install SQL Server or use an existing instance. If you don't have SQL Server installed, you can download SQL Server Express from [Microsoft SQL Server Downloads](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).

3. **Clone the Repository**: Clone the repository containing the ASP.NET Core project to your local machine.

## Cloning the Repository

1. Open Visual Studio.
2. Go to `File -> Clone Repository`.
3. Enter the URL (https://github.com/QistinaAlia/Sample-crud-project-QistinaAlia) and choose a location on your local machine to clone the repository.
4. Click `Clone`.

## Setting Up the Database

### Creating the Database and Table Using Visual Studio

1. **Open Server Explorer**:
   - In Visual Studio, go to `View -> Server Explorer`.

2. **Connect to Database**:
   - Right-click on "Data Connections" and choose "Add Connection".
   - Enter your SQL Server instance name (How to find instance name: [https://www.youtube.com/watch?v=qFNZNFw_Wf8&t=0s](https://www.youtube.com/watch?v=qFNZNFw_Wf8&t=0s)) and authentication details (Windows or SQL Server authentication).
   - In the "Connect to Server" dialog, select or enter the name of the database to connect to, or choose "New..." to create a new database.
   - If creating a new database, name it `ToDoActivities` and click "OK".

3. **Create the Table and Insert Sample Data**:
   - Once connected, expand the new `ToDoActivities` database under "Data Connections".
   - Right-click on the "Tables" folder and choose "New Query".
   - Paste the following SQL script to create the `ToDoList` table and insert sample data:

      ```sql
     CREATE TABLE ToDoList (
         id INT NOT NULL PRIMARY KEY IDENTITY,
         activity VARCHAR(100) NOT NULL,
         describe VARCHAR(300) NULL,
         created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
     );

     INSERT INTO ToDoList (activity, describe)
     VALUES
     ('Cook', 'Make Nasi Lemak'),
     ('Wash Clothes', 'Separate whites from colored'),
     ('Do Work', 'Continue Development of app');
     ```

   - Execute the query by clicking the "Execute" button.

## Configuring the Connection String

1. **Open `appsettings.json`**:
   - In Visual Studio, open the `appsettings.json` file located in the root directory of your project.

2. **Modify the Connection String**:
   - Update the `DefaultConnection` string under `ConnectionStrings` to match your SQL Server instance and database configuration. Replace `[ServerName]` with your actual server name or instance name. The `[DatabaseName]` should remain `ToDoActivities`:

     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=[ServerName];Database=ToDoActivities;Integrated Security=True;"
       },
       "Logging": {
         "LogLevel": {
           "Default": "Information",
           "Microsoft": "Warning",
           "Microsoft.Hosting.Lifetime": "Information"
         }
       },
       "AllowedHosts": "*"
     }
     ```

## Running the Application

1. **Build and Run the Application**:
   - In Visual Studio, press `Ctrl+F5` to build and run the application.
   - The application should now connect to your SQL Server database and be ready to use.

2. **Verify Functionality**:
   - Navigate to pages that interact with the database (e.g., creating new tasks).
   - Ensure that operations such as adding, updating, and deleting tasks function correctly.

## Troubleshooting

- **Connection Issues**: If you encounter connection issues, double-check the connection string in `appsettings.json`. Ensure that your SQL Server instance is running and accessible from your development environment.
- **Permissions**: Ensure that your SQL Server instance allows integrated security or that you have appropriate credentials if using SQL Server authentication.
