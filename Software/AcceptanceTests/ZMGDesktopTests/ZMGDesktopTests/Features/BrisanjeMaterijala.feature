Feature: Brisanje materijala
Korisnik je prijavljen

Scenario: Brisanje materijala koji se ne nalazi u nijednom radnom nalogu ili primci
  Given korisnik je na formi za upravljanje katalogom usluga i materijala
  And postoji materijal "Drvo" u popisu svih materijala
  When korisnik klikne na red ili čeliju unutar reda materijala "Drvo"
  And korisnik klikne na gumb Obriši materijal
  Then materijal "Drvo" se ne prikazuje više u popisu svih materijala

  Scenario: Brisanje materijala koji se nalazi u jednom ili više radnih naloga i/ili primci
  Given korisnik je na formi za upravljanje katalogom usluga i materijala
    And materijal "Celik" se nalazi u nekom radnom nalogu ili primci
    When korisnik klikne na red ili čeliju unutar reda materijala "Celik"
    And korisnik klikne na gumb Obriši materijal
    Then prikazuje se poruka da nije moguće obrisati materijal koji se nalazi u nekom radnom nalogu ili primci

Scenario: QR skener prepoznaje kameru na uređaju i vidljiv je prikaz kamere na ekranu
    Given Korisnik je na formi za upravljanje katalogom usluga i materijala
    When Korisnik klikne na gumb "Zaprimi materijal"
    And Korisnik odabire kameru "Integrated camera" na padajućem izborniku
    And Korisnik klikne gumb "Kreni skeniranje"
    Then Prikaz kamere je vidljiv na ekranu

Scenario: Skeniranje ispravnog QR koda
    Given Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
    When Korisnik klikne na gumb Zaprimi materijal
    And Korisnik odabire svoju kameru za skeniranje QR koda
    And Korisnik klikne gumb Kreni skeniranje
    Then Prikaz kamere je vidljiv na ekranu
    When Korisnik približi QR kod svojoj kameri
    Then Skener prepoznaje QR kod
    And Korisniku se daje mogućnost određivanja količine materijala

Scenario: Skeniranje neispravnog QR koda
    Given Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
    When Korisnik klikne na gumb Zaprimi materijal
    And Korisnik odabire svoju kameru za skeniranje QR koda
    And Korisnik klikne gumb Kreni skeniranje
    Then Prikaz kamere je vidljiv na ekranu
    When Korisnik približi QR kod svojoj kameri
    Then Skener prepoznaje QR kod
    And Prikazuje se poruka da je neispravan QR kod
