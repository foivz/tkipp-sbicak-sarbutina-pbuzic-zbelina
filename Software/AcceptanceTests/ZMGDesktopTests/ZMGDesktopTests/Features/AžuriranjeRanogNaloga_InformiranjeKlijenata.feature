Feature: AžuriranjeRanogNaloga_InformiranjeKlijenata

Kao korisnik
Trebao bi moći ažurirati podatke radnog naloga
te bi klijent trebao biti obaviješten o promjeni
statusa njihovog radnog naloga.

Background: 
	Given Korisnik odabire radni nalog s popisa s bilo kojim statusom
	And Korisnik klikne na gumb "Izmijeni" kako bi mogao mijenjati informacije radnog naloga

Scenario: Promijenjeni svi podaci radnog naloga koji se mogu promijeniti
	When Korisnik unosi u polje Količina = 50, polje Opis = "Cinčanje šipki e2 i e4", status ostaje isti te dodaje čelik u tablicu materijala
	And Korisnik klikne na gumb spremi
	Then Podaci radnog naloga su izmijenjeni i spremljeni

Scenario: Promijenjeni svi podaci osim polja količine koje je ostalo prazno
	When Korisnik ostavlja polje "Količina" prazno pa unosi Opis = "Cinčanje šipki e2 i e4"
	And Korisnik klikne na gumb spremi
	Then Prikazuje se poruka "Morate upisati količinu i status"

Scenario: Nema promjene statusa radnog naloga
	When Korisnik ne mijenja status radnog naloga
	And Korisnik klikne na gumb spremi
	Then Podaci radnog naloga su izmijenjeni i spremljeni te Email poruka o promjeni nije poslana

Scenario: Promjena statusa radnog naloga iz "Napravljen" u status "U obradi"
	When Korisnik mijenja status radnog naloga iz padajuće liste iz "Napravljen" u "U obradi"
	And Korisnik klikne na gumb spremi
	Then Korisnik dobiva E-mail poruku o promjeni statusa radnog naloga

Scenario: Promjena statusa radnog naloga iz "U obradi" u status "Dovršen"
	When Korisnik mijenja status radnog naloga iz padajuće liste iz "U obradi" u "Dovršen"
	And Korisnik klikne na gumb spremi
	Then Korisnik dobiva E-mail poruku o promjeni statusa radnog naloga
