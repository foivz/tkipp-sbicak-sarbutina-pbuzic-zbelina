Feature: Login
Radnik je dodan kao korisnik u bazi podataka.
Aplikacija je pokrenuta.

Scenario: Ispravno korisnicko ime i lozinka
    Given Radnik se nalazi na formi prijave
    When Radnik unosi podatke: Korisnicko ime="abc", Lozinka="12345"
        And Radnik klikne gumb Prijava
    Then Radnik je prebačen s forme za prijavu na glavni izbornik

Scenario Outline: Neispravno korisničko ime i/ili lozinka
    Given Radnik se nalazi na formi prijave
    When Radnik unese "<korime>" u polje za korisničko ime i unese "<lozinka>" u polje za lozinku
    And Radnik klikne gumb Prijava
    Then Radniku se prikazuje poruka "Krivi podaci"

Examples:
    | korime | lozinka  |
    | sarbutina      | 12345    |
    | korisnik123    | password |
    |                |          |


