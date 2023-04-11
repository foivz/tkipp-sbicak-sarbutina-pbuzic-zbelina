Feature: Skeniranje QR koda
Korisnik je prijavljen

Scenario: QR skener prepoznaje kameru na uređaju i vidljiv je prikaz kamere na ekranu
    Given Korisnik je na formi za upravljanje katalogom usluga i materijala
    When Korisnik klikne na gumb "Zaprimi materijal"
    And Korisnik odabire kameru "Integrated camera" na padajućem izborniku
    And Korisnik klikne gumb "Kreni skeniranje"
    Then Prikaz kamere je vidljiv na ekranu

Scenario: Skeniranje ispravnog QR koda
    Given Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
    When Korisnik klikne na gumb "Zaprimi materijal"
    And Korisnik odabire svoju kameru za skeniranje QR koda
    And Korisnik klikne gumb Kreni skeniranje
    Then Prikaz kamere je vidljiv na ekranu
    When Korisnik približi QR kod svojoj kameri
    Then Skener prepoznaje QR kod
    And Korisniku se daje mogućnost određivanja količine materijala

Scenario: Skeniranje neispravnog QR koda
    Given Korisnik se nalazi na formi za upravljanje katalogom usluga i materijala
    When Korisnik klikne na gumb "Zaprimi materijal"
    And Korisnik odabire svoju kameru za skeniranje QR koda
    And Korisnik klikne gumb Kreni skeniranje
    Then Prikaz kamere je vidljiv na ekranu
    When Korisnik približi QR kod svojoj kameri
    Then Skener prepoznaje QR kod
    And Prikazuje se poruka da je neispravan QR kod