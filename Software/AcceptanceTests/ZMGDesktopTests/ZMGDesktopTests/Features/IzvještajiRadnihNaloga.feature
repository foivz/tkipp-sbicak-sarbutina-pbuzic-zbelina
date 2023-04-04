Feature: IzvještajiRadnihNaloga

Kao korisnik
trebao bi moći kreirati izvještaje radnih naloga

Scenario: Ispravno kreiran izvještaj radnih naloga
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb izvještaji
	Then Korisniku se otvara forma za izvještaje
	And Korisnik klikne na gumb za Izrada izvještaja radnih naloga
	And Korisniku se otvara forma u kojoj se nalazi izvještaj o svim radnim nalozima po statusima na stupčastom i grafu pita
