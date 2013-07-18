Feature: Manage account numbers
	In order to create direct debits
	As an administrative assistant
	I want to process account numbers

Scenario: When I provide a valid bank account it is stored and CCC and IBAN is created
	Given This bank account "1234", "5678", "06", "1234567890" 
	When I process them
	Then It is considered valid
	 And is stored
	 And The CCC "12345678061234567890" is created
	 And The spanish IBAN code "ES6812345678061234567890" is created
