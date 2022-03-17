![Image](https://financesonline.com/uploads/2020/06/Lodgify-logo1.png)
# Lodgify API Team Backend Test
 --- 

 Welcome to the test oriented to API for Lodgify candidates.

 Please follow the instructions, clone (or donwload) the repository into your Computer.  
 When you finish zip it and send it to us by email.

 Thank you and good luck! 

---

## Context

We want an C# .net core Web Application API project that meet our requisites, we provide you the solution skeleton and a few features implemented to save time.

The API must follow the RESTless architecture and the resources must be formatted in JSON with SnakeCase.

You will find the Data layer is implemented and is instructed to be a In-Memory Database. Please do not modify the Data layer Entities.

The application represents a Cinema. We want to manage the showtimes of the cinema, feeding some data from the [IMDB API](https://imdb-api.com/API).

An Authentication Scheme is already implemented that work with a very simple token. You will need to use the following values in the request headers depend the case, more explained in detail further:

**Read-only Token** Header Name: `ApiKey` | Header Value : `MTIzNHxSZWFk` 

**Write Token** Header Name: `ApiKey` | Header Value : `Nzg5NHxXcml0ZQ==` 

 To use the IMDB API you will need to create an account to get an Api Key to use with their system.

 ---

 ## Implementation instructions

 ### API Endpoints

 Create the classes for the following **Resources**

 **Showtime**
 ```
id : int
start_date : DateTime
end_date : DateTime,
schedule : string
movie : Movie
auditorium_id : int
 ```

 **Movie**
 ```
 title : string
 imdb_id : string
 starts: string
 release_date : DateTime
 ```

 **IMDB Status**
 ```
up : boolean
last_call : DateTime
 ```

 We want an API controller named `ShowtimeController` with the following methods:
  - `GET` : Get All Showtimes, 
    - optionally it can be filtered by a date (QueryString) that should match the current showtimes in projection.
    - optionally it can be queried by movie title (QueryString).
    
  - `POST` : Creates a showtime using a `Showtime` resource in the payload. The `movie` property must come only with the `imdb_id` and the rest of the values will be filled calling the IMDB API. For the `auditorium_id` use one of the already created `Auditorium`s, Ids: 1, 2 or 3. 
  
  - `PUT` : Updates the `Showtime` resource, if the `movie` property comes `null` is ignored. if not the data is updated using the `imdb_id` and calling the IMDB API.
  
  - `DELETE` : Deletes a `Showtime` by Id.
  
  - `PATCH` : Throws an Error 500, for testing the error handling. Read `Error Handling` after.

We also want antoher controller named `StatusController` with the follwing method:
- `GET` : Get the current `IMDB Status` (read the next line). 
We want a  **backgorund** task that calls the IMDB API, you can use any method you prefer or the main webpage, and update the Status into a Singleton object `IMDB Status` every 60 seconds.

### Data Layer Repository

To complete the API Controller implementation you will need to complete the implementation of `ShowtimesRepository`.

### Authorization

As we explained before we added the `Authentication`, so you don't have to implement.
But we want you to implement the `Authorization` policies. 

We ask you to create 2 `Authorization` policies.
  - `Read` : It allows to access if you use the **Read-only Token**.
    - Enforce the policy on the `GET` method of `ShowtimeController`.
  - `Write` : It allows to access if you use the **Write Token**.
    - Enforce the policy on the **non** `GET` methods of `ShowtimeController`.

### Execution Tracking

We want to track the execution time of call in `ShowtimeController` and log in the Console.
Be default we set the loggers to log in the Console, so you only need to worry to get the Logger in the code.
Track the execution time without adding code inside the `ShowtimeController`, find a way.

### Error Handling

We want to handle the errors in the Controller `ShowtimeController`. 
Find a way to do it without adding code inside the `ShowtimeController`.

## Add the Request to cUrls file

We added a file next to this readme named `cUrls.txt`. 
Please add the cURLs of the requests you will do in the file.

---

You can add any feedback you want to send us in the file `Feedback.md` located next to this file.
