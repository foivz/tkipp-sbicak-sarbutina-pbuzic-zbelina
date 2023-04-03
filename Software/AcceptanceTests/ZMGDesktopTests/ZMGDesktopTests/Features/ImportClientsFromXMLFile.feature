Feature: ImportClientsFromXMLFile

Radnik je prijavljen u sustav
Te može uvesti klijente iz XML datoteke

Background: 
	Given Korisniku se otvara glavni izbornik

Scenario: Ispravan uvoz klijenta iz XML datoteke
	
	When Korisnik izabere gumb klijenti
	Then Korisniku se otvara forma za popis svih klijenata
	And Korisnik klikne na gumb Uvezi klijenta (XML)
	And Korisniku se otvara forma za odabir datoteke za uvoz klijenta
	And Korisnik klikne na gumb Odaberi
	And Korisniku se otvara forma gdje odabire datoteku pod nazivom "klijenti.xml"
	And Korisniku se otvara prozor koji ispisuje putanju do datoteke gdje se klikne na gumb OK
	And Korisnik klikne na gumb Unesi
	And Prikazuje se poruka "Uspješno učitani korisnici" te su klijenti vidljivi na popisu svih klijenata

Scenario: Neispravan naziv oznake u XML datoteci
	
	When Korisnik izabere gumb klijenti
	Then Korisniku se otvara forma za popis svih klijenata
	And Korisnik klikne na gumb Uvezi klijenta (XML)
	And Korisniku se otvara forma za odabir datoteke za uvoz klijenta
	And Korisnik klikne na gumb Odaberi
	And Korisniku se otvara forma gdje odabire datoteku pod nazivom "klijenti2.xml"
	And Korisniku se otvara prozor koji ispisuje putanju do datoteke gdje se klikne na gumb OK
	And Korisnik klikne na gumb Unesi
	And Korisniku se prikazuje poruka "Neispravni format datoteke"

Scenario: Neispravna vrijednost za bilo koje od svojstva klijenta
	
	When Korisnik izabere gumb klijenti
	Then Korisniku se otvara forma za popis svih klijenata
	And Korisnik klikne na gumb Uvezi klijenta (XML)
	And Korisniku se otvara forma za odabir datoteke za uvoz klijenta
	And Korisnik klikne na gumb Odaberi
	And Korisniku se otvara forma gdje odabire datoteku pod nazivom "klijenti3.xml"
	And Korisniku se otvara prozor koji ispisuje putanju do datoteke gdje se klikne na gumb OK
	And Korisnik klikne na gumb Unesi
	And Korisniku se prikazuje greška
	And Korisniku se prikazuje poruka "Neuspješno ubacivanje korisnika"
