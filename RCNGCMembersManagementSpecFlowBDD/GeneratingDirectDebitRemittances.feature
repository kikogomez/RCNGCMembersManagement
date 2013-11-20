Feature: Generating Direct Debit Remmitances
	In order to charge the bills to my club members
	As an administrative assistant
	I want to generate direct debit remitances to bank

Background: 
	
	Given My Direct Debit Initiation Contract is
	| NIF       | Name                              | BIC         | CreditorAgentName | LocalBankCode | CreditorBussinesCode | CreditorAccount          |
	| G35008770 | Real Club Náutico de Gran Canaria | CAIXESBBXXX | CAIXABANK         | 2100          | 777                  | ES5621001111301111111111 |

	Given These Club Members
	| MemberID | Name      | FirstSurname  | SecondSurname | Reference | Account              | BIC         |
	| 00001    | Francisco | Gomez-Caldito | Viseas        | 1234      | 01821111601111111111 | BBVAESMMXXX |
	| 00002    | Pedro     | Perez         | Gomez         | 1235      | 21001111301111111111 | CAIXESBBXXX |

	Given These bills
	| MemberID | TransactionConcept           | Amount |
	| 00001    | Cuota Mensual Octubre 2013   | 79     |
	| 00002    | Cuota Mensual Octubre 2013   | 79     |
	| 00002    | Cuota Mensual Noviembre 2013 | 79     |

Scenario: Create a new direct debit remmitance
	Given I have a I have a direct debit initiation contract
	When I generate a new direct debit remmitance
	Then An empty direct debit remmitance is created
