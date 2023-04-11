Feature: AžuriranjeRanogNaloga_InformiranjeKlijenata

Kao korisnik
Trebao bi moći ažurirati podatke radnog naloga
te bi klijent trebao biti obaviješten o promjeni
statusa njihovog radnog naloga.

Scenario: Promijenjeni svi podaci radnog naloga koji se mogu promijeniti
	Given Korisnik se nalazi na početnom ekranu
	When Korisnik klikne na gumb Radni nalozi
	Then Korisniku se otvara forma za prikaz svih radnih naloga
	And Korisnik klikne na gumb Detalji
	And Korisnik klikne na gumb Izmijeni
	And Korisnik unosi u polje Količina = "50", polje Opis = "Čeličenje šipki e2 i e4.", status ostaje isti te dodaje čelik u tablicu materijala
	And Korisnik klikće na gumb Spremi
	And Podaci radnog naloga su izmijenjeni i spremljeni

Scenario: Promijenjeni svi podaci osim polja količine koje je ostalo prazno
	Given Korisnik se nalazi na početnom ekranu
	When Korisnik klikne na gumb Radni nalozi
	Then Korisniku se otvara forma za prikaz svih radnih naloga
	And Korisnik klikne na gumb Detalji
	And Korisnik klikne na gumb Izmijeni
	And Korisnik ostavlja polje Količina prazno pa unosi Opis = "Čeličenje šipki e2 i e4."
	And Korisnik klikće na gumb Spremi
	And Prikaz poruke "Morate upisati količinu i status!"

Scenario: Nema promjene statusa radnog naloga
	Given Korisnik se nalazi na početnom ekranu
	When Korisnik klikne na gumb Radni nalozi
	Then Korisniku se otvara forma za prikaz svih radnih naloga
	And Korisnik klikne na gumb Detalji
	And Korisnik klikne na gumb Izmijeni
	And Korisnik klikće na gumb Spremi
	And Podaci radnog naloga su izmijenjeni i spremljeni te Email poruka o promjeni nije poslana

Scenario: Promjena statusa radnog naloga iz "Napravljen" u status "U obradi"
	Given Korisnik se nalazi na početnom ekranu
	When Korisnik klikne na gumb Radni nalozi
	Then Korisniku se otvara forma za prikaz svih radnih naloga
	And Korisnik odabire radni nalog s popisa sa statusom Napravljen
	And Korisnik klikne na gumb Detalji
	And Korisnik klikne na gumb Izmijeni
	And Korisnik mijenja status radnog naloga iz padajuće liste iz Napravljen u U obradi
	And Korisnik klikće na gumb Spremi
	And Korisnik dobiva E-mail poruku o promjeni statusa radnog naloga

Scenario: Promjena statusa radnog naloga iz "U obradi" u status "Dovršen"
	Given Korisnik se nalazi na početnom ekranu
	When Korisnik klikne na gumb Radni nalozi
	Then Korisniku se otvara forma za prikaz svih radnih naloga
	And Korisnik odabire radni nalog s popisa sa statusom U obradi
	And Korisnik klikne na gumb Detalji
	And Korisnik klikne na gumb Izmijeni
	And Korisnik mijenja status radnog naloga iz padajuće liste iz U obradi u Dovršen
	And Korisnik klikće na gumb Spremi
	And Korisnik dobiva E-mail poruku o promjeni statusa radnog naloga
