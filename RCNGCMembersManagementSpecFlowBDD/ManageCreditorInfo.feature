﻿Feature: Manage Creditor Info
	In order to charge the bills to my club members
	As a Club Manager
	I want to manage all my info as a direct debit creditor

Background: 
	
	Given My creditor info is
	| NIF       | Name                              |
	| G35008770 | Real Club Náutico de Gran Canaria |

Scenario: Register a creditor agent
	Given I have a bank
	When I register the bank as a creditor agent
	Then The creditor agent is correctly registered

Scenario: Register a creditor agent direct debit initiation contract
	Given I have a creditor agent
	When I register a contract data
	Then The contract is correctly registered

Scenario: Register more than one creditor agent direct debit initiation contract
	Given I have a direct debit initiation contract registered
	When I register a contract data
	Then The contract is correctly registered

Scenario: I change the bank account for my contract
	Given I have a direct debit initiation contract
	When I change the creditor account
	Then The contract is correctly updated

Scenario: I change the creditor bussines code for my contract
	Given I have a direct debit initiation contract
	When I change the creditor bussines code
	Then The contract is correctly updated