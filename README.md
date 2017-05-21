# LoggerEngine

The LoggerEngine app is a C#-WPF that emulates the assembly loader using .Net framework Reflection. To accomplish that, this application performs a Log in different locations:

* File
* Console
* Database

The application is able to load/unload specific assemblies regarding the location defined. For that, it is able to load/unload the Assembly provided in execution time.
The current Assemblies that are allowed in the application are:

* **FileLogger**: Log to disk file (**FileLogger.dll**).
* **ConsoleLogger**: Log to Console (**ConsoleLogger.dll**).
* **DatabaseLooger**: Log to Database (**DatabaseLogger.dll**).

The above .dll files were already generated from other class libraries. For testing purposes, all of them are available in the current solution. If you want to see the engine of those loggers, please go to my other repositories.

Those .dll files are available in the below path within the solution:

* `[SolutionPath]\LoggerEngine\LoggerEngine\bin\Debug` -- (I)

## Main Screen

The image below is the Main Screen of the Application

https://github.com/MarcoPalomino/LoggerEngine/blob/master/LoggerEngine.Util/Image/LoggerEngineMainScreen.png

The WPF application shows a single screen with the below functionality:

### Header Section

The header section has three input controls. These fields allow the system to load/unload the specific Assembly provided.

* **Logger Engine**: This field asks for the specific Logger Engine to load. It could be (File, Database or Console)
* **Assembly Name**: This field asks for the **exact** Assembly Name. Just for testing purposes, it could be (FileLogger, ConsoleLogger or DatabaseLogger). Only the Assemblies previouslly added to the path in (I) would be visible to the application.
* **Domain Name**: This field asks for the new AppDomain that should be created in order to load the Assembly. With a new AppDomain, the app will be able to Unload the assemblies.

### Test Section

* **Message**: It is the message the will be logged.
* **Type**: It is the message type. It could be (Warning, Informative or Error)
* **Logger Engine**: It is the logger engine. (Console, File or Database)

The "Log Message" button will allow the user to **LOG** the message entered with the below format similarly in all loggers:

* Creation Date / Type/ Message 

The system should validate that the selected Assembly was previouslly loaded; otherwise, it should display an error message.

## Pluggable Engine

Currently, the system assumes three Logger Engines, but if you want to prepare and user your own FileLogger or ConsoleLogger, you just have to replace the FileLogger.dll or ConsoleLogger.dll in:

* `[SolutionPath]\LoggerEngine\LoggerEngine\bin\Debug` -- (I)

As I mentioned before, it is just an experiment for loading/unloading assemblies in execution time. Any doubt or improvement, is well received. 
Please contact me.   :+1:
