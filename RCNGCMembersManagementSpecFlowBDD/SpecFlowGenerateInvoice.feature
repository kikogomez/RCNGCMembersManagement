Feature: Generating Invoices
	In order to bill the club members
	As a administrtative assistant
	I want to generate invoices

Background: 
	Given Last generated InvoiceID is "MMM2013000023"
	
	Given A Club Member
	| MemberID | Name      | FirstSurname  | SecondSurname |
	| 00001    | Francisco | Gomez-Caldito | Viseas        |

	Given This set of taxes
	| Tax Type            | Tax Value |
	| No IGIC             | 0         |
	| IGIC Reducido 1     | 2.75      |
	| IGIC Reducido 2     | 3.00      |
	| IGIC General        | 7.00      |
	| IGIC Incrementado 1 | 9.50      |
	| IGIC Incrementado 2 | 13.50     |
	| IGIC Especial       | 20.00     |

	Given This services
	| Service Name                | Default Cost | Default Tax  |
	| Rent a kajak                | 50.00        | IGIC General |
	| Rent a mouring              | 150.00       | IGIC General |
	| Full Membership Monthly Fee | 79.00        | No IGIC      |

Scenario: Generate an invoice for a simple service
	Given The member uses the club service "Rent a kajak"
	When I generate an invoice for the service
	Then An invoice is created for the cost of the service: 53.50
	And A single bill is generated for the total amount of the invoice: 53.50

Scenario: Two consecutive invoices generated the same year have consecutive Invoice ID
	Given The member uses the club service "Rent a kajak"
	When I generate an invoice for the service
	And I generate a new invoice on the same year
	Then the new invoice has a consecutive invoice ID

Scenario: Up to 999999 invoices in a year
	Given Last generated InvoiceID is "MMM2013999999"
	Given The member use a club service
	| Description  | Default Cost per Hour | Default Tax |
	| Rent a kajak | 50.00                 | No IGIC     |
	When I generate an invoice for the service
	Then The application doesn't accept more than 999999 invoices in the year

Scenario: Generate an invoice for multiple transactions with one tax type
	Given This set of transactions
	| Units | Service Name                | Description      | Unit Cost | Tax | Discount |
	| 1     | Full Membership Monthly Fee | Monthly Fee June | 79.00     | 0   | 0        |
	| 2     | Rent a mouring              | Mouring May-June | 150.00    | 7   | 10       |

	When I generate an invoice for this/these transaction/s
	Then An invoice is created for the cost of the service: 53.50
	And A single bill is generated for the total amount of the invoice: 53.50
