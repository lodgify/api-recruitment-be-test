## Lodgify Movie Api Coding Test - By Diogo Nunes

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
  - To run local tests go to Visual Studio 2022 > Test > Test Explores - Build the project and wait the Test Explorer show the tests > Click in the play button
  - If the Acceptance Test dont run, please, go to Extensions > Manage Extensions > Search and install specflow, after this restart the visual studio
  - 9 Specflow + Gherkin Acceptance Test were written
  - 31 unit tests were written
  - 

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

# Notes

 - The Overengineering in architecture was purposeful, I wanted to show knowledge about some design patterns

---

# External libraries

 - Specflow (use cucumber in dotnet)
 - MOQ (using moqs)
 - FluentAsserstions (test assertions)
 - NewtonSoft.Json (json manager)
 - Xunit (unit tests)

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

#talk about

Polly, Docker, Tests, Worker, Auth, Optimizatoion, Patterns, Log, CQRS, Cache,

All controllers hinerited from BaseController has ErrorHandling and MetricsLog (MetricsFilterAttribute)
I could do it using filters or middlewares

ILodgifyLogService
 - Worker calls
 - Metrics

 LaunchSetting EnvinronmentVariables

 Basic HealthCheck in /healthz

 # Thank you so much

 - \o/