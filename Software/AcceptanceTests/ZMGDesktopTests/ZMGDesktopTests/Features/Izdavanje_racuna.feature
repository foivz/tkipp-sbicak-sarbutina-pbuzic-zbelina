Feature: Izdavanje_racuna

Kao radnik,
zelim izdati racune,
zato sto je to obveza u poslovanju

Background: 
	Given Korisnik se nalazi na formi za izdavanje racuna

Scenario: Izdati racun bez jedne stavke i opisa
When Korisnik klikne na gumb Izdaj racun
Then Korisniku se izbacuje poruka sustava za nemogucnost izdavanja racuna
And Korisnik klikne na gumb Ok1

Scenario: Izdati racun s dvije stavke i bez opisa
When Korisnik klikne na gumb Upravljanje stavkama
Then Korisniku se otvara forma za upravljanje stavkama
Then Korisnik unese potrebne podatke
And Korisnik klikne na gumb Dodaj1
Then Korisnik promijeni robu
And Korisnik klikne na gumb Dodaj1
And Korisnik klikne na gumb Natrag
When Korisnik klikne na gumb Izdaj racun
Then Korisniku se izbacuje poruka sustava za nemogucnost izdavanja racuna
And Korisnik klikne na gumb Ok1

Scenario: Provjeriti ukupnu cijenu iznosa racuna
Then Nakon unosa dvije stavke provjeriti iznos racuna

