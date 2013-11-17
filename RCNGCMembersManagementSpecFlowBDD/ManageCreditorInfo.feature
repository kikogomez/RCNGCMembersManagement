Feature: Manage Creditor Info
	In order to charge the bills to my club members
	As a Club Manager
	I want to manage all my info as a direct debit creditor

Background: 
	
	Given My creditor info is
	| NIF       | Name                              |
	| G35008770 | Real Club Náutico de Gran Canaria |

Scenario: Create a creditor agent
	Given I have a bank
	When I register the bank as a creditor agent
	Then The creditor agent is correctly created

Scenario: Register a direct debit initiation contract
	Given I have a creditor agent
	When I register a contract data
	Then The contract is correctly registered

Scenario: Register more than one direct debit initiation contract
	Given I have a direct debit initiation contract registered
	When I register a second contract data
	Then The contract is correctly registered

Scenario: I change the bank account for my contract
	Given I have a direct debit initiation contract
	When I change the creditor account
	Then The contract is correctly updated

Scenario: I change remove a direct debit initiation contract
	Given I have a direct debit initiation contract
	When I remove the contract
	Then The contract is correctly removed
