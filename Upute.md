U našem projektu koristili smo 3 pomoćne klase, GuiDriver, GuiDriverv2 te GuiDriverAppOpen. 
**GuiDriver** klasa koristi se kada se pokreće aplikacija od glavnog izbornika, GuiDriverAppOpen
omogućava da se testira od trenutnog prozora, a klasa GuiDriverv2 korištena je samo za testiranje prijave korisnika u sustav radi toga da ne bi svaki put prilikom testiranja
aplikacija bila pokrenuta od logina, nego od glavnog izbornika. <br> <br>
Što se tiče testova, **AddClientsFeature, BrisanjeKlijenataKojiNemaRacunRadniNalogIliRobu, ClientDetailsFeature, ClientReportFeature, ImportClientsFromXMLFileFeature**
potrebno je pokrenuti test jedan po jedan jer nisu povezani, odnosno ako se samo klikne na AddClientsFeature te se pokrenu svi testovi neće biti ispravno zbog toga jer će
se pojavljivati različiti pop up prozori s greškama. <br> <br>
Unutar **AddClientsFeature** potrebno je test **UspješnoDodavanjeKlijenta** provesti zadnji jer ako će se provesti prije onda će uvijek bacati grešku da postoji klijent s ovim imenom umjesto drugih potrebnih grešaka. <br> <br>
Nakon provedenog testa potrebno je ručno zatvoriti aplikaciju jer nismo uspjeli prebaciti driver s **driver.switchTo().window()** na određenu formu jer je uvijek bacalo grešku  
da ne postoji ta forma, a korištenjem **First()** i **Last()** uvijek je **windowhandles()** sadržavao jednu formu umjesto više njih. <br> <br>
Kod uvoza klijenta iz XML datoteke potrebno je  **prvo** unijeti klijente iz XML datoteke **ručno**, odnosno Vi provodite taj test prvo kako bi se prilikom otvaranja prozora kod odabira datoteke otvorio ponovno taj prozor. Nakon što ste to napravili, obrišite unesene klijente (**Hudić, Hadar, BAMT, Boxmark**) pa tek
onda provedite taj test jer inače neće moći pronaći potrebnu datoteku jer će otvorit nasumičan prozor za odabir datoteke, a ako se ne obrišu klijente koje ste vi ručno unijeli prije provođenja testa onda će baciti grešku da klijenti već postoje.

Prije provođenja testova potrebno je postaviti **putanje** pomoćne klase:
U **GuiDriver** i **GuiDriverAppOpen** stavite putanju do **ZMG/ZMGDesktop.exe** <br>
U **GuiDriverv2** stavite putanju do **ZMGv2/ZMGDesktop.exe** <br>
