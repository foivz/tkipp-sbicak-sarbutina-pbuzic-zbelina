Feature: Pregledavanje_racuna

Kao radnik,
zelim pregledati izdane racune,
kako bih imao uvid u poslovanje


Background:
	Given Korisnik se nalazi na formi za pregledavanje racuna
	Then Korisnik klikne na gumb Ocisti

Scenario: Postoje racuni u bazi podataka
Then Korisniku se prikazuju svi racuni iz baze podataka

Scenario: Pregledavanje svih racuna odabranog klijenta
When Korisnik klikne na gumb Pretrazivanje
Then Korisniku se prikazuju svi racuni klijenta

Scenario: Pregledavanje svih racuna odabranog klijenta uzlazno
When Korisnik klikne na radiogumb Uzlazno
Then Korisnik klikne na gumb Pretrazivanje
And Korisniku se prikazuju svi racuni klijenta uzlazno prema identifikatorima

Scenario: Pregledavanje svih racuna odabranog klijenta silazno
When Korisnik klikne na radiogumb Silazno
Then Korisnik klikne na gumb Pretrazivanje
And Korisniku se prikazuju svi racuni klijenta silazno prema identifikatorima

Scenario: Pregledavanje svih racuna odabranog klijenta uzlazno prema ukupnoj cijeni
When Korisnik klikne na radiogumb Uzlazno
Then Korisnik klikne na radiogumb Ukupni iznos racuna
Then Korisnik klikne na gumb Pretrazivanje
And Korisniku se prikazuju svi racuni klijenta uzlazno prema ukupnoj cijeni

Scenario: Pregledavanje svih racuna odabranog klijenta silazno prema ukupnoj cijeni
When Korisnik klikne na radiogumb Silazno
Then Korisnik klikne na radiogumb Ukupni iznos racuna
Then Korisnik klikne na gumb Pretrazivanje
And Korisniku se prikazuju svi racuni klijenta silazno prema ukupnoj cijeni

Scenario: Brisanje kriterija pretrazivanja
When Korisnik klikne na gumb Pretrazivanje
Then Korisnik klikne na gumb Ocisti
Then Korisniku se prikazuju svi racuni iz baze podataka



