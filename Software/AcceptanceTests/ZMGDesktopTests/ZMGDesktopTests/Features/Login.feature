Feature: Login
Radnik je dodan kao korisnik u bazi podataka.
Aplikacija je pokrenuta.

Scenario: Ispravno korisnicko ime i lozinka
    Given Radnik se nalazi na formi prijave
    When Radnik unosi podatke: Korisnicko ime="sarbutina20", Lozinka="12345"
        And Radnik klikne gumb Prijava
    Then Radnik je prebačen s forme za prijavu na glavni izbornik

Scenario Outline: Neispravno korisničko ime i/ili lozinka
  Given Korisnik se nalazi na stranici za prijavu
  When Korisnik unese "<korime>" u polje za korisničko ime
    And unese "<lozinka>" u polje za lozinku
    And klikne gumb "Prijava"
  Then prikazuje se poruka "Krivi podaci"

Examples:
    | korime | lozinka  |
    | sarbutina      | 12345    |
    | korisnik123    | password |
    |                |          |


