Feature: Brisanje klijenata koji nema račun, radni nalog ili robu

Radnik je prijavljen u sustav
Te može brisati klijenta koji nema nijedan račun, radni nalog ili robu vezan za sebe


Scenario: Uspješno brisanje klijenta
	Given Korisnik je na glavnom izborniku
	When Korisnik klikne gumb klijenti
	Then Korisniku se prikazuje popis svih klijenata
	And Korisnik odabere klijenta "Casa Grad"
	And Korisnik klikne na gumb Izbrisi klijenta
	And Klijent je uspješno obrisan

Scenario: Brisanje klijenta koji ima račun ili radni nalog ili robu
	Given Korisnik je na glavnom izborniku
	When Korisnik klikne gumb klijenti
	Then Korisniku se prikazuje popis svih klijenata
	And Korisnik klikne na gumb Izbrisi klijenta
	And Prikazuje se poruka: "Zabranjeno brisanje klijenta. Klijent ima radne naloge, račune i robu"

