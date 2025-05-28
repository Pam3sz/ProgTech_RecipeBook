1. Projekt célja
A projekt célja egy felhasználóbarát, ablakos (WPF alapú) receptkönyv alkalmazás létrehozása, amely lehetővé teszi:

Receptek hozzáadását, törlését, megtekintését

Receptek szűrését különböző szempontok szerint

Adatok tartós tárolását SQLite adatbázisban

Fontos események és kivételek loggolását

Fejlesztői szempontból strukturált, SOLID elvek mentén megvalósított, tesztelhető architektúra kialakítását

2. Főbb funkcionalitások
CRUD műveletek: receptek hozzáadása, törlése

Listanézet és részletes nézet

Keresés és szűrés (kulcsszó vagy hossz alapján)

Receptek címkézése (Decorator segítségével: pl. 🌶️ Spicy)

Loggolás fájlba (log.txt)

Adatbázis tárolás SQLite-tal

Unit tesztelés külön fájlban

3. Architektúra és felépítés
Technológia: C#, WPF (.NET 6), SQLite, NLog

Főbb osztályok:

MainWindow: UI vezérlés

Recipe: adatmodell

DatabaseHelper: SQLite CRUD logika

RecipeBuilder: builder pattern

ICommand, AddRecipeCommand, DeleteRecipeCommand: command pattern

IRecipeFilterStrategy, KeywordFilter, LengthBasedFilter: strategy pattern

IRecipeDecorator, SpicyRecipeDecorator, QuickRecipeDecorator: decorator pattern

Rétegek:
UI réteg: MainWindow.xaml

Logika réteg: parancsok, stratégiák, builder

Adatkezelés: DatabaseHelper + SQLite

Loggolás: NLog log.txt fájlba

4. Tervezési minták (Design Patterns)
Minta	                                        Használat	                                                Fájl(ok)
Strategy	                                    Különböző receptszűrési módok (kulcsszó, hossz)	          IRecipeFilterStrategy, KeywordFilter, LengthBasedFilter
Builder	                                      Recept objektumok fokozatos létrehozása	                  RecipeBuilder
Command	                                      Gombműveletek (Add/Delete) logikájának külön kezelése	    ICommand, AddRecipeCommand, DeleteRecipeCommand
Decorator	                                    Címkézett nevek: „🌶️ Spicy”, „⏱️ Quick”	                IRecipeDecorator, SpicyRecipeDecorator, QuickRecipeDecorator

Ezek jól elválasztják a felelősségeket, és bemutatják az OCP, SRP elveket is.

5. Tesztelés és loggolás
Loggolás: NLog segítségével a log.txt fájlba

Gombnyomások

Kivételkezelések

Szűrési műveletek

Unit tesztek (példa):

Recept hozzáadása érvényes és üres mezőkkel

Recept létrehozás Builder-rel

Kulcsszavas szűrés Strategy alapján

Kivétel kiváltása DatabaseHelper hibás hívás esetén
