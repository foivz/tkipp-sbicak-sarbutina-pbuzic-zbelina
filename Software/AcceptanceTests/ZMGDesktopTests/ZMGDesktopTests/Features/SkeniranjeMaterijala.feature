Feature: Skeniranje QR koda
Korisnik je prijavljen

Scenario: QR skener prepoznaje kameru na uređaju i vidljiv je prikaz kamere na ekranu
    Given Korisnik se nalazi na formi glavnog izbornika
    When Korisnik klikne na gumb 'Stanje skladišta'
    Then Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
    When Korisnik klikne na gumb "Zaprimi materijal"
    And Korisnik odabire kameru "Integrated camera" na padajućem izborniku
    And Korisnik klikne gumb za početak skeniranja
    Then Prikaz kamere je vidljiv na ekranu

Scenario: Skeniranje ispravnog QR koda
    Given Korisnik se nalazi na formi glavnog izbornika
    When Korisnik klikne na gumb 'Stanje skladišta'
    Then Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
    When Korisnik klikne na gumb "Zaprimi materijal"
    And Korisnik odabire kameru "Integrated camera" na padajućem izborniku
    And Korisnik klikne gumb za početak skeniranja
    Then Prikaz kamere je vidljiv na ekranu
    Then Skener prepoznaje QR kod i korisniku se daje mogućnost određivanja količine materijala

Scenario: Skeniranje neispravnog QR koda
    Given Korisnik se nalazi na formi glavnog izbornika
    When Korisnik klikne na gumb 'Stanje skladišta'
    Then Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
    When Korisnik klikne na gumb "Zaprimi materijal"
    And Korisnik odabire kameru "Integrated camera" na padajućem izborniku
    And Korisnik klikne gumb za početak skeniranja
    Then Prikaz kamere je vidljiv na ekranu
    And Skener prepoznaje neispravan QR kod i prikazuje se poruka da nije ispravan QR kod