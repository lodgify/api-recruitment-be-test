Feature: ImdbStatus.HealtCheck

Test healtcheck in ImdsStatus, using Lodigify endpoint

@ImdbStatus.HealtCheck
Scenario: Imdb Status check using Lodgify Movies Api
	When i call the HealthCheck Api
	Then i get the health about imdb api
