<!--
**(ERASMUS students please see the english version (README_ENG.md) of this document)**


 Inicijalne upute za prijavu 1. projekta iz kolegija Testiranje i kvaliteta programskih proizvoda

Poštovane kolegice i kolege, 

čestitamo vam jer ste uspješno prijavili svoj projektni tim na kolegiju Testiranje i kvaliteta programskih proizvoda, te je za vas automatski kreiran repozitorij koji ćete koristiti za verzioniranje vašega koda, testova, ali i za pisanje dokumentacije.

Ovaj dokument (README.md) predstavlja **osobnu iskaznicu vašeg projekta**. Vaš prvi zadatak je **prijaviti vlastiti projektni prijedlog** na način da ćete prijavu vašeg projekta, sukladno uputama danim u ovom tekstu, napisati upravo u ovaj dokument, umjesto ovoga teksta.

Za upute o sintaksi koju možete koristiti u ovom dokumentu i kod pisanje vaše projektne dokumentacije pogledajte [ovaj link](https://guides.github.com/features/mastering-markdown/).
Sav programski kod i testove je potrebno verzionirati u glavnoj **master** grani i **obvezno** smjestiti u mapu Software. Sve artefakte (npr. slike) koje ćete koristiti u vašoj dokumentaciju obvezno verzionirati u posebnoj grani koja je već kreirana i koja se naziva **master-docs** i smjestiti u mapu Documentation.

Povratnu informaciju na samu prijavu tima i projekta, kao i na završnu predaju ćete od nastavnika dobiti kroz sekciju Discussions (također dostupnu na GitHubu vašeg projekta). A sada, vrijeme je da prijavite vaš projekt. Za prijavu vašeg projektnog prijedloga molimo vas koristite **predložak** koji je naveden u nastavku, a započnite tako da kliknete na *olovku* u desnom gornjem kutu ovoga dokumenta :) 
-->

# Naziv projekta
ZMG Desktop

## Projektni tim
Ime i prezime | E-mail adresa (FOI) | JMBAG | Github korisničko ime
------------  | ------------------- | ----- | ---------------------
Sebastijan Bičak | sbicak20@student.foi.hr | 0016150730 | sbicak20
Sebastian Arbutina | sarbutina20@student.foi.hr | 0016147707 | sarbutina20
Zvonimir Belina | zbelina20@student.foi.hr | 0016149673 | zbelina20
Patrik Bužić | pbuzic20@student.foi.hr | 0016146757 | puzic20

## Model rada na projektu
(1) Nastavak rada na projektu iz kolegija "Razvoj programskih proizvoda (RPP)"
<!-- (Ovdje navedite model rada na projektu, pri čemu su dostupne opcije: , (2) Rad na projektu u suradnji s nastavnicima, (3) Rad na projektu u suradnji s industrijom. -->

## Opis projekta
ZMG je privatni obrt koji se bavi zaštitom metalne galanterije, od kud i naziv obrta.
ZMG radi tako da klijent donese zahrđali metal koji se zatim, važe. Temeljem kilaže robe izračunava se ukupna cijena. Metal se obrađuje cinčanjem, a cinčanje je nanošenje sloja cinka na površinu metala zbog zaštite od korozije.

Problem s kojim se poslodavac suočava je ručno izvođenje izdavanja računa pomoću Microsoft Excela, velika papirologija za praćenje transakcija. Ujedno je i otežano pregledavanje klijenata.

ZMG Desktop će biti buduće rješenje za navedene probleme privatnog obrta. Aplikacija će omogućiti lakši način rada, automatizirati će slanje poruke klijentima da je roba koja se obrađuje gotova, printanje i izadavanje računa preko aplikacije. ZMG Desktop će imati bazu podataka u kojoj su spremljeni računi i klijenti za koje privatni obrt radi. Obrađivanje podataka će se vršiti pomoće te baze podataka. Poslodavac će imati mogućnost pregledavanja obračuna kroz cijeli tjedan, nekoliko mjeseci ili godinu dana.


## Specifikacija projekta

Oznaka | Naziv | Kratki opis | Odgovorni član tima
------ | ----- | ----------- | -------------------
F01 | Login | Korisnik će se morati prijaviti kako bi pristupio aplikaciji. | Sebastian Arbutina
F02 | Izdavanje računa | Aplikacija će omogućiti poslodavcu izdavanje računa. Izdani račun će u stavkama računa sadržavati potrošeni materijal i usluge koje su provedene. Skeniranjem QR koda s radnog naloga, stavke će se automatski popuniti u računu i korisnik će imati mogućnost ručnog unosa stavki. | Sebastijan Bičak
F03 | Pregled računa | Aplikacija će omogućiti pregled svih računa i pregled detalja pojedinog računa. U pregledu će se moći pretraživati i sortirati račune. | Sebastijan Bičak
F04 | Pregled klijenata | Aplikacija će omogućiti prikaz svih klijenata npr. ime i prezime klijenta, adresa, broj telefona itd. Prilikom pregledavanja detalja o pojedinom klijentu prikazivat će se popis radnih naloga i izdanih računa za klijenta | Patrik Bužić
F05 | Izrada radnog naloga | Aplikacija će omogućiti izradu radnog naloga za odrađivanje usluga zaštite metalne galanterije, na kojem se može evidentirati i potrošnja materijala pomoću skeniranja barkoda ili QR koda. | Zvonimir Belina
F06 | Unos klijenta u bazu podataka| Aplikacija će omogućiti ručni unos klijenta u bazu i uvoz klijenta iz XML datoteke.  | Patrik Bužić
F07 | Upravljanje katalogom usluga i materijala  | Aplikacija će omogućiti prikaz, dodavanje i brisanje te će za svaku uslugu i materijal generirati QR kod  koji će se moći skenirati kasnije za identificiranje usluge i materijala. | Sebastian Arbutina
F08 | Promjena statusa i informiranje klijenata | Aplikacija će omogućiti informiranje klijenata preko e-maila o promjeni statusa radnog naloga (kreiran, u izradi, završeno, ...) te samu promjenu i evidenciju statusa radnog naloga. | Zvonimir Belina
F09 | Zaprimanje materijala | Aplikacija će omogućiti skeniranje QR koda za zaprimanje materijala i generirat će dokument primka | Sebastian Arbutina
F10 | Izrada izvještaja | Aplikacija će omogućiti izradu izvještaja, koji će po statusima prikazivati radne naloge preko tablica i grafova. | Zvonimir Belina
F11 | Izračun isplativosti | Aplikacija će imati mogućnost da kreira izvještaj koji će prikazati popis 10 najvećih klijenata. (Tablični prikaz i graf pita)  | Patrik Bužić
F12 | Stvaranje PDF dokumentacije | Aplikacija će poslodavcu omogućiti automatsko generiranje računa u PDF-u koji će se poslati klijentu na određeni email. | Sebastijan Bičak

## Tehnologije i oprema
U našem projektu koristit ćemo .NET Framework. .NET Framework služi za izradu različitih aplikacija kao što su web stranice, desktop aplikacije i dr. Razvojno okruženje koje ćemo koristiti je Visual Studio 2022 od tvrtke Microsoft. Koristiti ćemo i MySQL bazu podataka.
