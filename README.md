# Kafka-Domain-Service
POC for .NET Core domain service using Kafka as a message broker

### To develop for this locally:
- Install the latest version of .NET 2.2 from http://dot.net
- You can now compile everything: Navigate to the root directory and type `dotnet build`
- (That's really it...)

#### To run this locally there are dependencies on:
- Kafka
- Zookeeper
- PostgreSQL

_I have created a docker-compose.yml in the root directory which will set up and run all of those dependencies for you.  In the same docker-compose there is also a pgadmin image which will give you SQL management tools locally over port 80 to administer PostgreSQL._

#### Run all of your data migrations:
- simply run `./update-databases` in the root directory
- (Postgres needs to be running either in a container or installed locally for the migrations to be applied.  You will know if it doesn't work)

#### _Each project with a `.Service` suffix is an API project for a particuar domain and each project with a `.Worker` suffix is a command and event consumer application._ 
- _Each one of these Services or Workers can be started by navigating to their corresponding directory and typing `dotnet run`.  (Workers need ro have this command followed by a single argument that tells that app what environment they are runinng as.  For example: Locally we use: `dotnet run Development`_

### Running this on other environments
...instructions coming soon
