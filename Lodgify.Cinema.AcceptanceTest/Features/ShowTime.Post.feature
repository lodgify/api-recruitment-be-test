Feature: ShowTimePost

Testing ShowTime Post Endpoint

@ShowTimePostFeature
Scenario: Post New Show Times
	Given i have this show time
	| Imdb_id | StardDate | EndDate  |
	| 1375660 | yesterday | tomorrow |
	When i send the post data for the api
	Then the data will be saved and returned a success status code

	Scenario: Post New Show Times without token
	Given i have this show time
	| Imdb_id | StardDate | EndDate  |
	| 1375655 | yesterday | tomorrow |
	When i send the post data for the api without token
	Then the post result must be contains a 401 error

		Scenario: Post New Show Times with wrong token
	Given i have this show time
	| Imdb_id | StardDate | EndDate  |
	| 1375650 | yesterday | tomorrow |
	When i send the post data for the api with wrong token
	Then the post result must be contains a 401 error



