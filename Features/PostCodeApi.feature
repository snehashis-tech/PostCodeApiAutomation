Feature: Postcode API validation

Scenario: Validates post code validation
Given i have postcode "EC1A1BB"
When i call the postcode API
Then the response code should be 200
Then the postcode should be "EC1A1BB"
Then the country should be "England"