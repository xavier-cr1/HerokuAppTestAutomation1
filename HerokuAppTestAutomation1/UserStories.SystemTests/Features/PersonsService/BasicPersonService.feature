Feature: BasicPersonService

@Type:API
Scenario: Create and get a person from express server
	Given The user posts a new person with the following properties
	    | Name      | Age                                       |
        | Jerome	| 25										|
	Then The rest response should be '200'
	Given The user requests a list of people from express server
	Then The rest basic people response should be '200'
	And The people in the list should not be empty