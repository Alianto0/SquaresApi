# Homework Squares Api solution

#Startup
1. Clone the repo and open `Squares.sln`
1. Install Docker
1. Install SQL Server Express 
1. Setup Static port on your SQL server instance 49172 or to your desired one and update Connection string in appsettings.json
1. Enable SQL Server Authentication in server settings
1. Publish Squares.DB project onto your SQL Server
1. Run `Squares.Api` in Docker
1. Test with Swagger or [Insomnia](docs/Insomnia_collection.json)

## Implementation considerations

* No Authentication for any endpoint. No requirement for that. Anyone who can access API can call it and modify any of the data.
* Methods to calculate squares retrieved from http://www.csharphelper.com/howtos/howto_square_puzzle_solution.html not tested corectness of the algorithm.
* SQL Server connection string is stored in the source control for local development only, for production use a user should be set up with either integrated security or login created in a secure way.


