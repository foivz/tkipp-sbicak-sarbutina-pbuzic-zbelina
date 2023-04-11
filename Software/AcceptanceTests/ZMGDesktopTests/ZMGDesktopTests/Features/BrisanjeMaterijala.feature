Feature: Brisanje materijala
Korisnik je prijavljen

Scenario: Brisanje materijala koji se ne nalazi u nijednom radnom nalogu ili primci
  Given korisnik je na formi za upravljanje katalogom usluga i materijala
  And postoji materijal "Drvo" u popisu svih materijala
  When korisnik klikne na red ili čeliju unutar reda materijala "Drvo"
  And korisnik klikne na gumb "Obriši materijal"
  Then materijal "Drvo" se ne prikazuje više u popisu svih materijala

  Scenario: Brisanje materijala koji se nalazi u jednom ili više radnih naloga i/ili primci
  Given korisnik je na formi za upravljanje katalogom usluga i materijala
    And postoji materijal "Celik" u popisu svih materijala
    When korisnik klikne na red ili čeliju unutar reda materijala "Celik"
    And korisnik klikne na gumb "Obriši materijal"
    Then prikazuje se poruka da nije moguće obrisati materijal koji se nalazi u nekom radnom nalogu ili primci
