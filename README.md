# LoggerEngine

The LoggerEngine app is a C#-WPF that emulates the assembly loader using .Net framework Reflection. To accomplish that, this application performs a Log in different locations:

* File
* Console
* Database

The application is able to load/unload specific assemblies regarding the location defined. For that, it is able to load/unload the Assembly provided in execution time.

The below are the current Assemblies allowed:

* **FileLogger**: Log to disk file (**FileLogger.dll**).
* **ConsoleLogger**: Log to Console (**ConsoleLogger.dll**).
* **DatabaseLooger**: Log to Database (**DatabaseLogger.dll**).

The above .dll files were already generated from other class libraries. For testing purposes, all of them are available in the current solution. If you want to see the engine of those loggers, please go to my other repositories:

* https://github.com/MarcoPalomino/FileLogger
* https://github.com/MarcoPalomino/ConsoleLogger
* https://github.com/MarcoPalomino/DatabaseLogger

On the other hand, in order to add assemblies to the application, all external logger engines should be added to the below path:

* `[SolutionPath]\LoggerEngine\LoggerEngine\bin\Debug` -- (I)

You could build the class libraries from my other repositories mentioned before, but in the below path you could get the logger assemblies already created:

* `[SolutionPath]\LoggerEngine\LoggerEngine.Util\ExternalReferences` -- (II)

Just copy the assemblies from (II) to (I) for using those libraries in the application. **On the other hand, copy those assemblies in the Test project for running the unit tests.**

## Main Screen

The image below is the Main Screen of the Application

https://github.com/MarcoPalomino/LoggerEngine/blob/master/LoggerEngine.Util/Image/LoggerEngineMainScreen.png

The WPF application shows a single screen with the below functionality:

### Header Section

The header section has three input controls. These fields allow the system to load/unload the specific Assembly provided.

* **Logger Engine**: This field asks for the specific Logger Engine to load. It could be (File, Database or Console)
* **Assembly Name**: This field asks for the **exact** Assembly Name. Just for testing purposes, it could be (FileLogger, ConsoleLogger or DatabaseLogger). **_Only the Assemblies previouslly added to the path in (II) would be visible to the application._**
* **Domain Name**: This field asks for the new AppDomain that should be created in order to load the Assembly. With a new AppDomain, the app will be able to Unload the assemblies.

### Test Section

* **Message**: It is the message that will be logged.
* **Type**: It is the message type. It could be (Warning, Informative or Error)
* **Logger Engine**: It is the logger engine. (Console, File or Database)

The "Log Message" button will allow the user to **LOG** the message entered with the below format similarly in all loggers:

* Creation Date / Type/ Message 

At the end, if the log is processed succesfully, the system should display an error message. In background the @Log entity used in the application will have a new status that indicates the Log was saved without issues.

Finally, the system should validate that the selected Assembly was previouslly loaded; otherwise, it should display an error message.

## Pluggable Engine

Currently, the system assumes three Logger Engines, but if you want to prepare and user your own FileLogger or ConsoleLogger, you just have to **_replace the FileLogger.dll or ConsoleLogger.dll_** in:

* `[SolutionPath]\LoggerEngine\LoggerEngine\bin\Debug` -- (I)

## Final Considerations

- [x] Update App.config with your current settings. For instance: "ConnectionString"
- [x] Run the SQL scripts provided in the solution. It creates the DBLogger table and the Stored procedure that inserts the log.
- [x] Copy the assemblies mentioned from (II) to (I)
- [x] Load the assemblies before using the Log engine in the Application.
- [x] The "Display Assemblies" in the application shows the current loaded assemblies.
- [x] Make sure to have enough privileges for creating a File in the path specified. By default, the paht defined is D:\Log

As I mentioned before, it is just an experiment for loading/unloading assemblies in execution time. Any doubt or improvement, is well received. 
Please contact me.   :+1:
