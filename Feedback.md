### Feedback

*Please add below any feedback you want to send to the team*

#Response Metrics
All response time is loggin in console the response time and write in Http response header the follower header Lodgify-Response-Time
The logic is in MetricsFilterAttribute

Api return tjust release year and not full date to movie ReleaseDate

#Imdb status
I not found a healtcheck method in api, to, i developed from scratch


#talk about

Polly, Docker, Tests, Worker, Auth, Optimizatoion, Patterns, Log, CQRS, Cache,

All controllers hinerited from BaseController has ErrorHandling and MetricsLog (MetricsFilterAttribute)
I could do it using filters or middlewares

ILodgifyLogService
 - Worker calls
 - Metrics