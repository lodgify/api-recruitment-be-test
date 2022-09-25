Following parts accomplished:

[Validation]:
- Input validation done with ValidateModelActionFilter. There is no need to check if ModelSatte.IsValid. ValidateModel automatically checks it and returns BacRequest (HttpCode = 400) if validation fails.
- Validation rules are defined in Dto classes by using DataAnnotations attributes. All Dto classes are derived from IValidatableObject interface to implement more complex validationa among class properties. For example raise a vlidation error if StartDate > EndDate!

[Authorization]:
- Authorization is done with AuthorizedTokenAttribute. Enumeration defined for Read and Write privilages to be passed to AuthoziedToken action filter.

[GlobalExceptionHandling]:
- Exception handling done by defining a ExceptionFilterAttribute and adding it to ther filters. If catched exception is known exception (throwed by new CustomeException) it will be catched and will generate friendly response message with error key and description and InternalServerError status (HttpCode = 500). 

[ExecutionTracking]:
- Done by defining a middleware and adding it to middlewares. ExecutionTimeMiddleware starts on invoking every http request and stops watching when resonse is ready. It loggs the information to the console for every action in the controllers.

[ImdbStatusBackgroundService]:
- A background service defined and added to hosted services. It periodically calls an api from imdb an updates singleton object. To view the latest status of imdb, ImdbStatusController defined. 

[SnakeCase]:
- Unfortunately SnakeCase json naming policy is not supprted by System.Text.Json. Newtonsoft supports snake_case naming policy by System.Text.Json is much faster than Newtonsof. So I created a JsonSnakeCaseNamingPolicy derived from JsonNamingPolicy and implemented the Snake Case for using.

/*********************************************************************************************************************************************/
Other feedbaks:

- It is better to move the Movie class to a independent class. In the current implementation, Movie is child of Showtime (has foreign key). It cause duplicate and redundancy in the database.

- I changed the ShowtimeRepository methods to async. By defining methods async, we can use await to release the thread until reply comes. It has good performance benefits.

- I changed Func<IQueryable<MovieEntity>, bool> to Expression<Func<MovieEntity, bool>> in ShowtimeRepository. I think passing an expression is more easier to be understood.

- I didn't include Service layer and directly injected the repositories into the controller considering controller as service layer. In more complex projects its better to define a Service layer between repository and controller to prepare Dtos and do some business rules.

- I didn't add unit tests but it's better to have unit test project in the solution.

- I used Include function to fill nested properties like Movie in Showtime. This is not recommended. I prefer to add another light weight function without populating nested objects.

- Added regular expression to validate input format for Schedule time.

- Added validation for StarDate and EndDates in Showtime for checking that always StartDate should be less that or equal to EndDate
