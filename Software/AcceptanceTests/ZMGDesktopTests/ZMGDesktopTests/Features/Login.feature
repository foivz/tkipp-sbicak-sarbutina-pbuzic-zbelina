Feature: Login
Radnik je dodan kao korisnik u bazi podataka.
Aplikacija je pokrenuta.

Scenario: Ispravno korisnicko ime i lozinka
Given Radnik se nalazi na formi prijave
When Radnik unosi podatke: Korisnicko ime="sarbutina20", Lozinka="12345"
And Radnik klikne gumb Prijava
Then Radnik je prebačen s forme za prijavu na glavni izbornik


