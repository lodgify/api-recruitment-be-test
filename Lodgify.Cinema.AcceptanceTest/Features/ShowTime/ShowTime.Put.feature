Feature: ShowTimePut

Testing ShowTime Put Endpoint

@ShowTimePostFeature
Scenario: Post New Show Times
	Given i have this show time to update
	| Id | Imdb_id | StardDate | EndDate  |
	|  1 | 1375645 | yesterday | tomorrow |
	When i send the put data for the api
	Then the data must be updated and will returned a success status code