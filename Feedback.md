## Lodgify Movie Api Coding Test - By Diogo Nunes

---

## About

- This project was made from scratch
- I used Visual Community Studio 2022 With Dot Net Core 3.1
- Classlibraries are using Dot Net Standard 2.1

---

# Architectural Patterns

  - MVC
  - Onion
  - CQRS

---

# Design Patterns

  - IOC
  - DI
  - Singleton
  - Circuit Braker (with Polly)
  - Some Test Patterns
  - Some Design Patterns

---

# Best Practices

  - Clean Code Practices
  - Clean Arch Practices
  - SOLID
  - Unit tests
  - Acceptance tests

---

# Tests

  - XUnit for Unit Tests
  - Specflow for Acceptance Tests
  - FluentAsertions for assertions
  - MOQ for Mocks
  - To run local tests go to Visual Studio 2022 > Test > Test Explorer - Build the project and wait the Test Explorer show the tests > Click in the play button
  - If the Acceptance Test dont run, please, go to Extensions > Manage Extensions > Search and install specflow, after this restart the visual studio
  - 9 Specflow + Gherkin Acceptance Test were written
  - 31 unit tests were written

---

# Tunning

  - Response Caching are Enabled for 30 SECONDS in ShowTime Get Route (IMPORTANT)
  - Response Compression with Brotli Optimization are Enabled
  - The business class is injected by the usage scoped lifetime, it is better than the transient lifetime because 
    the container does not need to create and destroy objects with each dependency injection, alleviating the 
    garbage collector and helping with memory management (transient lifecycle are not wrong, but, you need choose the right moment for the use)


---

# Security

  - For test reasons Cors policy are totally disabled
---

# Documentation

  - Swagger

---

#Logs

- The entire system log is being managed by the LodgifyLogService class, if you want to change it, just change the behavior of the Log method of this class, the default is the console log

---

# Response Metrics

- I put the metrics in a BaseController, all controllers are inheriting from it, therefore, they are all monitored, the monitoring filter uses the log class to write the return to the console and also with each request I return a Lodgify-Response-Time
in response headers

---

#Imdb Api status

- I didn't find a healtcheck method in the imdb api, so I developed it from scratch

---

#Environment

- All the created environment variables are in launchSettings.json, for the creation of the TestServer with the specflow I declare the variables in Lodgify.Cinema.AcceptanceTest.MoviesApiTestServer.SetupEnvironment()
- Using environment variables already prepares the application to use good practices for storing variables in containerized environments

---

#Authorization

- I implemented the requested forms of authentication and added the parameter with a default value in the swagger, however, I know that I should have created a policy for each type of authorization and declared it explicitly in the controllers, unfortunately I didn't have time to do that, but , the way I did it solved

# HealthCheck

- I put a basic healthchek in /healthz

---

#HttpClient

 - HttpClient for accessing external services built on the microsoft standard to avoid socket exhaustion
 - I put Circuit Breaker in a Base class that all repositories that call external APIs using the HTTP protocol must inherit, with this all calls already go with CircuitBraker activated, I know that it would also be possible to put it in the Dependency Injector

---

#Filters

 - I used filters instead of middlewares because they are simpler, but I understand the usefulness and difference of both

 ---

#StringErrorMessages 

- BusinessMessages are managed by a Resource - BusinessMessage.resx (can be internacionalized)

 ---

#ImMemoryDataBase 

- Added a command to clear the database every time the system loads
- Be careful when running the tests or executing the curls, as the same base is changed
- I put a lock on this class, I left a comment there, I know that for concurrency we should use the ConcurrentClass, it was just to solve a bug in the acceptance tests with specflow

 ---

#Async

- I used asynchronous methods where possible to optimize the use of threads/trheadpool

---

#Startup 

- I organized the entire Startup class regarding dependency injection settings and environment variables using the IOC layer and extension methods

---

# Notes

- The Overengineering in architecture was purposeful, I wanted to show knowledge about some design patterns
- I didn't write more tests because of time, I started doing TDD but the project got quite big, as it's only for testing I tested only the basics with unit testing and all endpoints with integration tests, in a real project scenario I would do the code coverage around 80% and would write more complete and cohesive tests
- Didn't handle movie scheduling conflict and some rules for null fields due to time
- The idea of ​​this project was to show my general knowledge about several disciplines, sorry for the size of the project =D

---

# Thank you so much

 - \o/