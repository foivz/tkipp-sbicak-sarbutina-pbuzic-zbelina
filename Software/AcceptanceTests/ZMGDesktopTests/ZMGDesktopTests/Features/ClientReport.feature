Feature: ClientReport

Radnik je prijavljen u sustav
Radnik može izraditi izvještaj za 10 najvećih klijenata


Scenario: Uspješno kreiran izvještaj za 10 najvećih klijenata
	Given Korisnik je na glavnom izborniku.
	When Korisnik klikne na gumb Izvjestaji
	Then Korisniku se otvara forma za izvjestaje
	And Korisnik klikne na gumb Izrada klijentskih izvještaja
	And Korisniku se otvara forma na kojoj je izvještaj o 10 najvećih klijenata (stupčasti graf i graf pita)

Scenario: Ne može se kreirati izvještaj jer nema dovoljno podataka
	Given Korisnik je na glavnom izborniku.
	When Korisnik klikne na gumb Izvjestaji
	Then Korisniku se ispise poruka "Nema dovoljno podataka za prijaz deset najvećih klijenata"
	And Korisniku se otvara forma za izvjestaje
	And Korisnik klikne na gumb Izrada klijentskih izvještaja
	And Korisniku se ispise poruka "Nema dovoljno podataka za kreiranje izvještaja o deset najvećih klijenata"
