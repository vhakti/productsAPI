# productsAPI
requirements and dependencies:
1. Net 8
2. AutoFac 8
3. Entity Framework core 8
4. SqlLite
   

# Database Set up in windows:
Assumptions: the database ProductsCatalog.sb will be created in %AppData%/Local
1. By using  package manager console, create the database with SQlLite with the following command: update-database -context MigrationContext

2. If the database was createed go to run basic unit tests in the project:PersistenceTests.

#About the project
This API was developed under Net 8 and was designed with DDD in mind. You will find also an autofac Module with the persistence library which is useful to 
work with modules and get the benefits of selfcontained components that can be plugged and unplugged on needs.

Domain layer is based on Model library. Which contains Entities and Interfaces.

The Repository pattern is applied and is dependant on the DataBase context provided by the MS EF.
In order to perform some performance telemetry, a middleware was added to measure request time which then is logged in txt files. you will find the log file in the root path 
of the project with the name:metrics[yyymmdd].log.  Serilog packaged is used for text logging purposes.





