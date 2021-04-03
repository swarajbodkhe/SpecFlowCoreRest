Feature: TrelloRest
	Simple calculator for adding two numbers

@mytag
Scenario: Send Get Trello Boards Request
	Given I create a new Rest Client
	When I create a new GET Request to "1/boards/60671136d50fb537f7d4ffd8?"
	And I send the rest request
	Then I validate the response code to be "OK"
	And I validate the response details for boards to be as following:
	| Key  | Value                    | Type   |
	| name | TempBoard                | string |
	| id   | 60671136d50fb537f7d4ffd8 | string |
	|closed|  false				      | bool   |
	And I store the Board Id


Scenario: Send Put Boards Request
Given I create a new Rest Client
	When I create a new PUT Request to "1/boards/60671136d50fb537f7d4ffd8?" with body details from "PutRequestForBoard_1.json"
	And I send the rest request
	Then I validate the response code to be "OK"
	And I validate the response details for boards to be as following:
	| Key  | Value                    | Type   |
	| name | TempBoard                | string |
	| id   | !Board.ID				  | string |
	|closed|  false				      | bool   |
	And I store the Board Id
