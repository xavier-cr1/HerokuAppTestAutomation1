Feature: BasicPersonService

@Type:API
Scenario: Get people from express server
	Given The user requests a list of people from express server
	Then The response should be '200'
	And The people in the list should not be empty
