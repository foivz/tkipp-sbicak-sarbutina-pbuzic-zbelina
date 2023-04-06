Feature: AžuriranjeRanogNaloga_InformiranjeKlijenata

Kao korisnik
Trebao bi moći ažurirati podatke radnog naloga
te bi klijent trebao biti obaviješten o promjeni
statusa njihovog radnog naloga.

Background: 
	Given Korisnik se nalazi na početnom ekranu
	When Korisnik klikne na gumb Radni nalozi
	Then Korisniku se otvara forma za prikaz svih radnih naloga

Scenario: Promijenjeni svi podaci radnog naloga koji se mogu promijeniti
	And Korisnik odabire radni nalog s bilo kojim statusom
	And Korisnik klikne na gumb Detalji
	And Korisnik klikne na gumb Izmijeni
	And Korisnik unosi u polje Količina = "50", polje Opis = "Čeličenje šipki e2 i e4.", status ostaje isti te dodaje čelik u tablicu materijala
	And Korisnik klikne na gumb Spremi
	And Podaci radnog naloga su izmijenjeni i spremljeni

Scenario: Promijenjeni svi podaci osim polja količine koje je ostalo prazno
	And Korisnik odabire radni nalog s bilo kojim statusom
	And Korisnik klikne na gumb Detalji
	And Korisnik klikne na gumb Izmijeni
	And Korisnik ostavlja polje Količina prazno pa unosi Opis = "Čeličenje šipki e2 i e4."
	And Korisnik klikne na gumb Spremi
	And Prikaz poruke "Morate upisati količinu i status"

Scenario: Nema promjene statusa radnog naloga
	And Korisnik odabire radni nalog s bilo kojim statusom
	And Korisnik klikne na gumb Detalji
	And Korisnik klikne na gumb Izmijeni
	And Korisnik ne mijenja status radnog naloga
	And Korisnik klikne na gumb Spremi
	And Podaci radnog naloga su izmijenjeni i spremljeni te Email poruka o promjeni nije poslana

Scenario: Promjena statusa radnog naloga iz "Napravljen" u status "U obradi"
	And Korisnik odabire radni nalog s popisa s sa statusom Napravljen
	And Korisnik klikne na gumb Detalji
	And Korisnik klikne na gumb Izmijeni
	And Korisnik mijenja status radnog naloga iz padajuće liste iz Napravljen u U obradi
	And Korisnik klikne na gumb Spremi
	And Korisnik dobiva E-mail poruku o promjeni statusa radnog naloga

Scenario: Promjena statusa radnog naloga iz "U obradi" u status "Dovršen"
	And Korisnik odabire radni nalog s popisa s sa statusom U obradi
	And Korisnik klikne na gumb Detalji
	And Korisnik klikne na gumb Izmijeni
	And Korisnik mijenja status radnog naloga iz padajuće liste iz U obradi u Dovršen
	And Korisnik klikne na gumb Spremi
	And Korisnik dobiva E-mail poruku o promjeni statusa radnog naloga
