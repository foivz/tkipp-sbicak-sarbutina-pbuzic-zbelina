Feature: AddClients

Radnik je prijavljen u sustav
Radnik može dodavati nove klijente


Scenario: Klijent uspješno dodan
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis", OIB = "27155265125", Adresa = "Zlatar 20", IBAN = "HR2812572642712582172", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Klijent uspješno dodan u bazu podataka

Scenario: Dodavanje klijenta s postojećim nazivom
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Impuls", OIB = "27155265125", Adresa = "Zlatar 20", IBAN = "HR2812572642712582172", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Postoji već klijent s ovim nazivom: Impuls"

Scenario: Dodavanje klijenta s postojećim OIB-om
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis", OIB = "21852758212", Adresa = "Zlatar 20", IBAN = "HR2812572642712582172", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Postoji već klijent s ovim OIB-om: "

Scenario: Dodavanje klijenta s postojećim IBAN-om
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis", OIB = "27155265125", Adresa = "Zlatar 20", IBAN = "HR2817528125291258271", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Ovaj IBAN već postoji: "

Scenario: Dodavanje klijenta s postojećim Emailom
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis", OIB = "27155265125", Adresa = "Zlatar 20", IBAN = "HR2812572642712582172", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "bauhaus@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Ovaj email je već u upotrebi: "

Scenario: Dodavanje klijenta s postojećim brojem telefona
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis", OIB = "27155265125", Adresa = "Zlatar 20", IBAN = "HR2812572642712582172", Mjesto ="Zlatar", Broj telefona = "0913955196", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Ovaj broj telefona se već koristi: "

Scenario: Nisu ispunjeni svi podaci za klijenta
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: OIB = "27155265125", Adresa = "Zlatar 20", IBAN = "HR2812572642712582172", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Potrebno je ispuniti sva polja"

Scenario: Neispravan naziv klijenta
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis123", OIB = "27155265125", Adresa = "Zlatar 20", IBAN = "HR2812572642712582172", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Naziv može sadržavati samo slova"

Scenario: Neispravan OIB klijenta
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis", OIB = "2715526512h", Adresa = "Zlatar 20", IBAN = "HR2812572642712582172", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Krivo unesen OIB"

Scenario: Neispravna adresa klijenta
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis", OIB = "27155265125", Adresa = "Zla..12", IBAN = "HR2812572642712582172", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Krivo unesena adresa"

Scenario: Neispravan IBAN klijenta
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis", OIB = "27155265125", Adresa = "Zlatar 20", IBAN = "KFR2817528125291258273", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Krivo uneesn IBAN račun"

Scenario: Neispravan mjesto klijenta
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis", OIB = "27155265125", Adresa = "Zlatar 20", IBAN = "HR2812572642712582172", Mjesto ="Zlat--ar", Broj telefona = "0972152712", Email = "preis@gmail.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Krivo uneseno mjesto"

Scenario: Neispravan email klijenta
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb klijenti
	Then Korisniku se otvara forma za prikaz svih klijenata
	And Korisnik klikne na gumb Dodaj klijenta
	And Korisniku se otvara forma za dodavanje klijenta
	And Korisnik unosi podatke za klijenta: Naziv = "Preis", OIB = "27155265125", Adresa = "Zlatar 20", IBAN = "HR2812572642712582172", Mjesto ="Zlatar", Broj telefona = "0972152712", Email = "example.com"
	And Korisnik klikne na gumb Spremi
	And Prikazuje se poruka "Krivi email"
