Feature: Generating Direct Debit Remmitances
	In order to charge the bills to my club members
	As an administrative assistant
	I want to generate direct debit remitances to bank

Scenario: Create a new direct debit remmitance
	Given I have a I have a direct debit initiation contract
	When I generate a new direct debit remmitance
	Then An empty direct debit remmitance is created

Scenario: Add a bill to a direct debbit remmitance
	Given I have direct debit remmitance
	And I have a bill with a direct debit mandate asssociated
	When I assign the bill to de direct debit remmitance
	Then The bill is marked as "Prepared for remmitance"
	And The bill is added to the direct debit remmitance 

