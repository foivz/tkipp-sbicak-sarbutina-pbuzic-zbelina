Feature: Generiranje i spremanje dokumenta primka

Scenario: Uspješno ažuriranje količine materijala bez spremanja dokumenta primke
  Given Korisnik se nalazi na formi za zaprimanje materijala
  And Korisnik je uspješno skenirao QR kod materijala
  When Korisnik upiše "20" u numerički izbornik
  And Korisnik klikne gumb Ažuriraj količinu
  And Korisniku se prikaže forma generirane primke
  And Korisnik klikne na gumb Zatvori
  Then Količina skeniranog materijala je promjenjena u sustavu

 Scenario: Prikaz ispravnih podataka zaprimljenog materijala na formi generirane primke
  Given Korisnik se nalazi na formi za zaprimanje materijala
  When Korisnik upiše "20" u numerički izbornik
    And Korisnik klikne Ažuriraj količinu
    And Korisniku se prikaže generirana primka
  Then Korisnik provjeri podudaranje podataka tako da je Količina: "20", a Datum: današnji datum
  And Generirani dokument primka sadrži ispravne podatke.

Scenario: Spremanje dokumenta primke na sustavu računala i provjera ispravnosti podataka
  Given Korisnik je na formi za zaprimanje materijala
  When Korisnik upiše "35" u numerički izbornik i klikne Ažuriraj količinu
  And Korisniku se prikaže generirana primka
  And korisnik klikne na gumb Pohrani datoteku lokalno
  And Korisnik odabere mjesto spremanja datoteke na Desktop i nazove datoteku "primka"
  And Korisnik locira dokument na Desktopu i otvori ga
  Then Prikazuje se poruka da je dokument uspješno spremljen lokalno s ispravnim podacima

