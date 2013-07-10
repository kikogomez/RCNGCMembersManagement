Feature: SpecFlowGenerateInvoice
	In order to bill the club members
	As a administrtative assistant
	I want to generate invoices

@mytag
Scenario: Generate an invoice for a simple service
	Given I have a Club Member
	And the member use a club service
	When I bill this service
	Then An invoice is created for the cost of the service
	And A single bill is generated for the total amount of the invoice

Scenario: Two consecutive invoices generated the same year have consecutive Invoice ID
	Given I generate an invoice
	When I generate a new invoice on the same year
	Then the new invoice has a consecutive invoice ID

