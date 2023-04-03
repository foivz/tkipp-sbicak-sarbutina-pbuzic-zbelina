Feature: ClientDetails

Radnik je prijavljen u sustav
Te može vidjeti pojedinosti o klijentu


Scenario: Ispravan prikaz radnih naloga i računa za pojedinog klijenta
	Given Korisnik se nalazi u glavnom izborniku
	When Korisnik odabere gumb klijenti
	Then Otvara se forma za prikaz svih klijenata
	And Korisnik selektira klijenta "SmartShop"
	And Korisnik klikne na gumb Detalji klijenta
	And Korisniku se otvara forma na kojoj se prikazuju računi i radni nalozi za klijenta "SmartShop"