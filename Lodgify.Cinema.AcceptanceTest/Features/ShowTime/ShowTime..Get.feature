﻿Feature: ShowTimeGet

Testing ShowTime Get Endpoint

@ShowTimeGetFeature
Scenario: Get All Show Times
	Given i will not send filters
	When i call the api with the readonly token
	Then the result must be contains a list of show times

Scenario: Get All Show Times without Readonly Token
	Given i will not send filters
	When i call the api without the readonly token
	Then the result must be contains a 401 error

Scenario: Get a movie uisng tha title filter
	Given i have the film Inception
	When i call the api with the readonly token
	Then the result will be returned with the movie title Inception