### Feedback

I have splitted the code into 3 different layers (Domain, Infraestructure and ApiApplication). Each one in a separated project.


Repository pattern is done using Expresions and Specification pattern.
* I have done some recursivity for concatenating multiple Expressions in AndSpecification class. Perhaps it is not necessary such complexity, but I just 
	wanted to try this way (adding an undefined number of Expressions and using them all as a whole).

AutoMapper library is used to easily map data between DTOs and Entities.


Auth policies added through Read/Write roles. One of these two Request Headers, depending on each case, must be added to the call in order to be processed by the application.

Exception handler for the requests is done using class ExceptionMiddlewareExtensionscs.

Logging request time is done through class RequestLoggerMiddleware.

Background task to check IMDB Api status is implemented in class IMDBStatusBackgroundTask.


Some configuration params have been added to the appSettings.json file so that they can be modified by the user without having to recompile the application.

Options pattern is used in the application to inject the mentioned Configuration information.


Unit Tests have been added for Service class. I don't have added for other classes because I think that for a tecnical test, this class already contains many methods
	and scenarios to use for expressing skills on Unit Test.

cUrl.txt contains a sequence of calls to interact with the Api.


I have done just one commit to the branch with all the changes, although in a real scenario I would have been doing multiple commits with smaller changes.