Feature: Dodavanje materijala
Korisnik je prijavljen

Scenario: Uneseni ispravni podaci.
	Given Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
	When Korisnik klikne na gumb 'Dodaj materijal'
	And otvara se forma za dodavanje novog materijala
	And Radnik unosi "Guma" u polje za naziv
	And unosi "2" u polje za količinu
	And odabire "kg" u padajućem izborniku za mjernu jedinicu
	And unosi "15" u polje za cijenu po jedinici
	And unosi "mehanički čvrst, žilav i izvanredno elastičan polimerni materijal dobiven vulkanizacijom kaučuka" u polje za opis
	And označava "false" u polju za opasnost po život
	And klikne gumb "Dodaj"
	Then Materijal "Guma" se prikazuje na popisu materijala

Scenario: Dodavanje materijala s postojećim nazivom
	Given Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
	And postoji materijal "Celik" u katalogu
	When Korisnik klikne na gumb 'Dodaj materijal'
	And otvara se forma za dodavanje novog materijala
	And Korisnik unosi "Celik" u polje za naziv
	And klikne gumb "Dodaj"
	Then Prikazuje se poruka "Materijal već postoji"

Scenario: Neispunjeno polje naziva materijala
	Given Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala 
	When Korisnik klikne na gumb 'Dodaj materijal'
    And otvara se forma za dodavanje novog materijala
    And klikne gumb "Dodaj"
	Then Prikazuje se poruka "Potrebno je ispuniti sva polja"

Scenario: Neispravan naziv materijala
  Given Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
  When Korisnik klikne na gumb 'Dodaj materijal'
  And Korisnik unosi "Pijesak123" u polje za naziv
  And Korisnik klikne na gumb Dodaj
  Then Prikazuje se poruka "Naziv može sadržavati samo slova"


Scenario: Ručna promjena naziva mjerne jedinice u padajućem izborniku
  Given Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
  When Korisnik klikne na gumb 'Dodaj materijal'
  And Otvara se forma za dodavanje novog materijala
  And Korisnik unosi "Pijesak123" u polje za naziv
  And Korisnik unosi "Rucna promjena" u polje za mjernu jedinicu
  And Korisnik klikne na gumb Dodaj
  Then Prikazuje se poruka da treba ispuniti sva polja

Scenario: Naziv materijala podržava sva slova u hrvatskoj abecedi
  Given da je korisnik na formi za upravljanje katalogom usluga i materijala
  When korisnik klikne na gumb 'Dodaj materijal'
  And Otvara se forma za dodavanje novog materijala
  And Korisnik unosi naziv 'Đćčšž'
  And Korisnik klikne na gumb 'Dodaj'
  Then Korisnik bi trebao vidjeti novi materijal s nazivom 'Đćčšž' na popisu svih materijala






