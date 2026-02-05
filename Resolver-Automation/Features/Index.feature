Feature: Index

  # Test 1
  Scenario: Verify login form fields accept credentials
    Given the user is on the home page
    Then the login form shows the email and password inputs and the sign-in button
    When the user enters the email "user@example.com"
    And the user enters the password "Password123!"
    Then the login form retains the entered email and password

  # Test 2
  Scenario: Verify list items and badge values
    Given the user is on the home page
    When the user reviews the list group
    Then the list group contains 3 items
    And the second list item text equals "List Item 2"
    And the second list item badge equals "6"

  # Test 3
  Scenario: Verify dropdown defaults and selection change
    Given the user is on the home page
    Then the dropdown defaults to "Option 1"
    When the user selects "Option 3" from the dropdown
    Then the dropdown displays "Option 3"

  # Test 4
  Scenario: Verify button enablement states
    Given the user is on the home page
    Then the first button is enabled
    And the second button is disabled

  # Test 5
  Scenario: Wait for dynamic button, click, and confirm success
    Given the user is on the home page
    When the dynamic button appears
    And the user clicks the dynamic button
    Then a success alert is shown
    And the dynamic button becomes disabled

  # Test 6
  Scenario: Validate table cell value at coordinates 2, 2
    Given the user is on the home page
    When the user reads the table cell at row 2 column 2
    Then the table cell value equals "Ventosanzap"
