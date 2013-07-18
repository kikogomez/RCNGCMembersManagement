Feature: Manage account numbers
	In order to create direct debits
	As an administrative assistant
	I want to process account numbers

Scenario: When I provide a valid bank account it is stored and CCC and IBAN is created
	Given This bank account "1234", "5678", "06", "1234567890" 
	When I process the bank account
	Then It is considered "valid"
	 And the bank account is "stored"
	 And The CCC "12345678061234567890" is created
	 And The spanish IBAN code "ES6812345678061234567890" is created

Scenario: When I provide an invalid bank account it is stored but no CCC nor IBAN are created
	Given This bank account "1234", "5678", "05", "1234567890" 
	When I process the bank account
	Then It is considered "invalid"
	 But the bank account is "stored"
	 And No CCC is created
	 And No spanish IBAN is created

Scenario Outline: Theese are the results when processing theese bank accounts
	Given This bank account <Bank>, <Office>, <ControlDigit>, <AccountNumber> 
	When I process the bank account
	Then It is considered <valid>
	 And the bank account is <stored>
	 And The CCC <CCC> is created
	 And The spanish IBAN code <IBAN> is created

Scenarios:
| Bank   | Office | ControlDigit | AccountNumber | valid     | stored   | CCC                    | IBAN                       |
| "1234" | "5678" | "06"         | "1234567890"  | "valid"   | "stored" | "12345678061234567890" | "ES6812345678061234567890" |
| "1234" | "5678" | "05"         | "1234567890"  | "invalid" | "stored" | "null"                 | "null"                     |
