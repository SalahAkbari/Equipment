# Equipment
This project is about Online construction equipment rental with an Angular (v5.2.0) frontend and ASP.NET Core 2.2 WebApi.

# Development Environment

    Visual Studio 2017 (MSSQLLocalDB)
    Node 8.9.4 & NPM 5.6.0
    .NET Core 2.2 sdk
    Angular CLI -> npm install -g @angular/cli https://github.com/angular/angular-cli
    
# Setup
To build and run the project:

    Create the database with Update-Database with Package Manager Console in the EquipmentRental project.
    Run the project with EquipmentRental project as the StartUp project.
    
Please also note, that you can also use SwaggerUI instead of Angular to see the services separately and test them by uncommenting
UseSwaggerUI block in the Configure method in the Startup class like this:

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Equipment Rental API V1");
        c.RoutePrefix = string.Empty;
    });
    
# Extra
I have used and configured the .NET Core Identity membership as well and have stored some fake users to db with DbSeeder, and I'm
ready to implement the jwt-based authentication in the project just in one or two hours for future use cases.


