Feature: DodavanjeRadnogNaloga

Kao korisnik
Želim biti u mogućnosti 
dodavati nove radne naloga

Background: 
	Given Korisnik se nalazi na glavnom izborniku
	When Korisnik klikne na gumb radni nalozi
	Then Korisniku se otvara forma za prikaz svih radnih naloga
	And Korisnik klikne na gumb novi radni nalog
	And Korisniku se otvara forma za dodavanje novog radnog naloga

Scenario: Svi podaci radnog naloga uspješno uneseni
	And Korisnik redom unosi podatke za radni nalog: Količina = "15", Opis = "Cinčanje robe.", odabire Status = "Napravljen", odabire Datum stvaranja = "31.04.2023", odabire Klijent = "Aggreko", odabire Materijal = "Cink" i dodaje ga u tablicu, odabire robu "Šipka e2, Šipka e4" iz tablice klijentove robe te ju dodaje u tablicu robe radnog naloga, klikće na gumb generiraj QR kode
	And Korisnik klikne na gumb Podnesi
	And Radni nalog uspješno dodan u bazu podataka

Scenario: Uneseni svi podaci radnog naloga osim količine korištenih materijala
	And Korisnik redom unosi podatke za radni nalog: Opis = "Cinčanje robe.", odabire Status = "Napravljen", odabire Datum stvaranja = "31.04.2023.", odabire Klijent = "Aggreko", odabire Materijal = "Cink" i dodaje ga u tablicu, odabire robu "Šipka e2, Šipka e4" iz tablice klijentove robe te ju dodaje u tablicu robe radnog naloga, klikće na gumb generiraj QR kode
	And Korisnik klikne na gumb Podnesi
	And Prikazuje se poruka "Morate upisati količinu i status"

Scenario: Uneseni svi podaci radnog naloga osim statusa radnog naloga
	And Korisnik redom unosi podatke za radni nalog: Količina = "15", Opis = "Cinčanje robe.", odabire Datum stvaranja = "31.04.2023.", odabire Klijent = "Aggreko", odabire Materijal = "Cink" i dodaje ga u tablicu, odabire robu "Šipka e2, Šipka e4" iz tablice klijentove robe te ju dodaje u tablicu robe radnog naloga, klikće na gumb generiraj QR kode
	And Korisnik klikne na gumb Podnesi
	And Prikazuje se poruka "Morate upisati količinu i status"

Scenario: Uneseni svi podaci radnog naloga osim materijala
	And Korisnik redom unosi podatke za radni nalog: Količina = "15", Opis = "Cinčanje robe.", odabire Status = "Napravljen", odabire Datum stvaranja = "31.04.2023", odabire Klijent = "Aggreko", odabire robu "Šipka e2, Šipka e4" iz tablice klijentove robe te ju dodaje u tablicu robe radnog naloga, klikće na gumb generiraj QR kode
	And Korisnik klikne na gumb Podnesi
	And Prikazuje se poruka "Morate staviti materijal i robu u radni nalog"

Scenario: Uneseni svi podaci radnog naloga osim robe
	And Korisnik redom unosi podatke za radni nalog: Količina = "15", Opis = "Cinčanje robe.", odabire Status = "Napravljen", odabire Datum stvaranja = "31.04.2023", odabire Klijent = "Aggreko", odabire Materijal = "Cink" i dodaje ga u tablicu, klikće na gumb generiraj QR kode
	And Korisnik klikne na gumb Podnesi
	And Prikazuje se poruka "Morate staviti materijal i robu u radni nalog"

Scenario: Dodavanje nove robe klijenta bez unošenja podataka robe 
	And Korisnik klikne na gumb dodaj novu robu bez upisivanja informacija o robi
	And Radni nalog uspješno dodan u bazu podataka
	And Prikazuje se poruka "Morate upisati naziv robe i količinu robe koju želite unijeti"