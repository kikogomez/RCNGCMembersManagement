Feature: Manage bills
	In order charge my invoices
	As an administrative assistant
	I want reate and manage bills for my invoices

Background: 
	Given Last generated InvoiceID is "MMM2013000023"
	
	Given A Club Member with a default Payment method
	| MemberID | Name      | FirstSurname  | SecondSurname | Default Payment method | Spanish IBAN Bank Account          | Direct Debit Reference Number |
	| 00001    | Francisco | Gomez-Caldito | Viseas        | Direct Debit           | IBAN ES68 1234 5678 0612 3456 7890 | 12345                         |

	Given This set of taxes
	| Tax Type            | Tax Value |
	| No IGIC             | 0         |
	| IGIC Reducido 1     | 2.75      |
	| IGIC Reducido 2     | 3.00      |
	| IGIC General        | 7.00      |
	| IGIC Incrementado 1 | 9.50      |
	| IGIC Incrementado 2 | 13.50     |
	| IGIC Especial       | 20.00     |

	Given These services
	| Service Name                | Default Cost | Default Tax  |
	| Rent a kajak                | 50.00        | IGIC General |
	| Rent a katamaran            | 100.55       | IGIC General |
	| Rent a mouring              | 150.00       | IGIC General |
	| Full Membership Monthly Fee | 79.00        | No IGIC      |

	Given These products
	| Product Name   | Default Cost | Default Tax  |
	| Pennant        | 10.00        | IGIC General |
	| Cup            | 15.00        | IGIC General |
	| Member ID Card | 1.50         | No IGIC      |

Scenario: A single bill automatically created for a new invoice with the member's default payment method associated
	Given The member uses the club service "Rent a kajak"
	When I generate an invoice for the service
	Then An invoice is created for the cost of the service: 53.50
	And A single bill To Collect is generated for the total amount of the invoice: 53.50
	And The bill payment method is the default one associated to the member
