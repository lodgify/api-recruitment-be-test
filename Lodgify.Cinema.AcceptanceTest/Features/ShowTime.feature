Feature: ShowTime

@ShowTimeFeature
Scenario: Get All Show Times
	Given i will not send filters
	When i call the api with the readonly token
	Then the result must be contains a list of show times