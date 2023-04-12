Feature: Generiranje i spremanje dokumenta primka

Scenario: Uspješno ažuriranje količine materijala bez spremanja dokumenta primke
    Given Korisnik se nalazi na formi glavnog izbornika
    When Korisnik klikne na gumb 'Stanje skladišta'
    Then Korisnik se nalazi na formi za upravljanje katalogom
    When Korisnik klikne na gumb "Zaprimi materijal"
    Then Korisnik se nalazi na formi za zaprimanje materijala
    And Korisnik simulira uspješno skeniranje QR koda materijala tako što klikne na gumb za isprobavanje uspješnog skeniranja
    When Korisnik upiše "20" u numerički izbornik
    And Korisnik klikne Ažuriraj količinu
    And Korisniku se prikaže forma generirane primke
    And Korisnik klikne na gumb Zatvori
    Then Količina skeniranog materijala je promjenjena u sustavu

 Scenario: Prikaz ispravnih podataka zaprimljenog materijala na formi generirane primke
 Given Korisnik se nalazi na formi glavnog izbornika
  When Korisnik klikne na gumb 'Stanje skladišta'
  Then Korisnik se nalazi na formi za upravljanje katalogom
  When Korisnik klikne na gumb "Zaprimi materijal"
  Then Korisnik se nalazi na formi za zaprimanje materijala
  And Korisnik simulira uspješno skeniranje QR koda materijala tako što klikne na gumb za isprobavanje uspješnog skeniranja
  When Korisnik upiše "20" u numerički izbornik
  And Korisnik klikne Ažuriraj količinu
  And Korisniku se prikaže forma generirane primke
  Then Korisnik provjeri podudaranje podataka tako da je Količina: "20", a Naziv: Aluminij

Scenario: Spremanje dokumenta primke na sustavu računala i provjera ispravnosti podataka
Given Korisnik se nalazi na formi glavnog izbornika
  When Korisnik klikne na gumb 'Stanje skladišta'
  Then Korisnik se nalazi na formi za upravljanje katalogom
  When Korisnik klikne na gumb "Zaprimi materijal"
  Then Korisnik se nalazi na formi za zaprimanje materijala
  And Korisnik simulira uspješno skeniranje QR koda materijala tako što klikne na gumb za isprobavanje uspješnog skeniranja
  When Korisnik upiše "35" u numerički izbornik
  And Korisnik klikne Ažuriraj količinu
  And Korisniku se prikaže forma generirane primke
  And korisnik klikne na gumb Pohrani datoteku lokalno
  And Korisnik odabere mjesto spremanja datoteke na Desktop i nazove datoteku "primka"
  And Korisnik locira dokument na Desktopu i otvori ga
  Then Prikazuje se poruka da je dokument uspješno spremljen lokalno s ispravnim podacima

