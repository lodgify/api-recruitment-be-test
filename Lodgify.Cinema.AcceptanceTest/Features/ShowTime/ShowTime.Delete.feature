Feature: ShowTimeDelete

Testing ShowTime Delete Endpoint

@ShowTimeDeleteFeature
Scenario: Delete a Show Time
	Given i have a show time id with id number 2
	When i call the api to delete the record
	Then the result shold be a success status for delete
	And i need to call a get route to confirm the deletion of this record