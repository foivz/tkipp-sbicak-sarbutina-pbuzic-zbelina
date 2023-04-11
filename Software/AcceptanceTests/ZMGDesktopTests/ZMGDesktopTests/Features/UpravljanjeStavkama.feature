Feature: UpravljanjeStavkama
Feature se odnosi na upravljanje stavkama, gdje radnik moze dodavati, pregledati i obrisati stavke.

Kao radnik,
kod izdavanja racuna,
zelim dodati stavke na racun.

Background:
	Given Korisniku se otvara forma za upravljanje stavkama

Scenario: Dodati stavku na racun bez potrebnog unosa
When Korisnik klikne na gumb Dodaj
Then Korisniku se izbacuje poruka sustava za unos stavke
And Korisnik klikne na gumb Ok


Scenario: Dodati stavku na racun s krivim unosom
When Korisnik unese krive podatke
Then Korisnik klikne na gumb Dodaj
And Korisniku se izbacuje poruka sustava za unos stavke
And Korisnik klikne na gumb Ok


Scenario: Dodati stavku s tocnim odredenim unosom
When Korisnik unese tocne podatke
Then Korisnik klikne na gumb Dodaj


Scenario: Dodati stavku s tocnim odredenim unosom ponovno
When Korinsik ponovno unese tocne podatke
Then Korisnik klikne na gumb Dodaj
And Korisniku se izbacuje poruka sustava za unos stavke
And Korisnik klikne na gumb Ok


Scenario: Obrisati odredenu stavku iz polja
When Korisnik oznaci stavku
Then Korisnik klikne na gumb Obrisi


	