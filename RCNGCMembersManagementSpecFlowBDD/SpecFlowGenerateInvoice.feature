Feature: SpecFlowGenerateInvoice
	In order to bill the club members
	As a administrtative assistant
	I want to generate invoices

@mytag
Scenario: Generate an invoice for a simple service
	Given I have a Club Member
	And the member use a club service
	When I bill a this service
	Then An invoice is created for the cost of the service
