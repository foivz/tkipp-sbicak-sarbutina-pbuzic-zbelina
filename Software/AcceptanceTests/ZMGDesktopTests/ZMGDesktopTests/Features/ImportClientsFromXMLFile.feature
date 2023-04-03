Feature: ImportClientsFromXMLFile

Radnik je prijavljen u sustav
Te može uvesti klijente iz XML datoteke

Scenario: Ispravan uvoz klijenta iz XML datoteke
	Given Korisnik se nalazi na formi za popis svih klijenata
	When Korisnik klikne na gumb Uvezi klijenta (XML)
	Then Korisniku se otvara forma za odabir datoteke za uvoz klijenta
	And Korisnik klikne na gumb Odaberi
	And Korisniku se otvara forma gdje odabire datoteku pod nazivom klijenti
	And Korisniku se ispisuje putanja do datoteke
	And Korisnik klikne na gumb Unesi
	And Prikazuje se poruka "Uspješno učitani korisnici" te su klijenti vidljivi na popisu svih klijenata

Scenario: Neispravan naziv oznake u XML datoteci
	Given Korisnik se nalazi na formi za popis svih klijenata
	When Korisnik klikne na gumb Uvezi klijenta (XML)
	Then Korisniku se otvara forma za odabir datoteke za uvoz klijenta
	And Korisnik klikne na gumb Odaberi
	And Korisniku se otvara forma gdje odabire datoteku pod nazivom klijenti2
	And Korisniku se ispisuje putanja do datoteke
	And Korisnik klikne na gumb Unesi
	And Prikazuje se poruka "Neispravni format datoteke"

Scenario: Neispravna vrijednost za bilo koje od svojstva klijenta
	Given Korisnik se nalazi na formi za popis svih klijenata
	When Korisnik klikne na gumb Uvezi klijenta (XML)
	Then Korisniku se otvara forma za odabir datoteke za uvoz klijenta
	And Korisnik klikne na gumb Odaberi
	And Korisniku se otvara forma gdje odabire datoteku pod nazivom klijenti3
	And Korisniku se ispisuje putanja do datoteke
	And Korisnik klikne na gumb Unesi
	And Prikazuje se poruka "Krivo unesen OIB"
	And Prikazuje se poruka "Neuspješno ubacivanje korisnika"
