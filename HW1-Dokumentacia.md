# LP-HW1 Dokumentácia
Zostavenie programu:
====================
## Úloha1 a Úloha2
Odovzdaná zložka obsahuje súbory  LPGenerator.cs a Makefile.
Pokiaľ sa budeme nachádzať v terminály v tejto zložke, resp. v zložke, ktorá obsahuje LPGenerator.cs
a Makefile. Tak stačí zadať do terminálu len
```sh
make
```
prípadne vykonať priamo to čo vykoná make, tj. zadať
```sh
csc LPGenerator.cs
```
To nám vygeneruje spustiteľný .exe súbor.

Používanie programu:
=================
## Úloha1 a Úloha2
Pre vygenerovanie LP pre daný vstup, napr. pre vstup1-002.tx,
stačí zadať nasledovné:
```sh
mono LPGenerator.cs first < vstup1-002.txt > vystup1-002.mod
```
Pri spúšťaní zadávame programu teda ešte jeden argument a to buď "first" alebo "second". Podľa toho či 
chceme aby sa na danom vstupe spustil generátor LP prvej alebo druhej úlohy. Pre riešenie LP programu teda už len stačí zadať vyrobený súbor glpsol-u.

Ako vyzerá vygenerovaný LP:
==========================
## Úloha 1
LP bude minimalizačný, kde budeme minimalizovať premennú `M`. Okrem tej budeme mať ešte pre každý vrchol `i` 
premennú `x[i]`.  Pre `M` vyždujeme nasledovné: `M >= -1`, a aby pre každý vrchol `i` platilo, že
`x[i] <= M`. Taktiež vyžadujeme, aby pre každú hranu `(u,v)` platilo to, že `x[u] + 1 <= x[v]`, čo mimo iného spolu s ostantnými podmienkami zaručuje to, že `x[i]` bude nezáporné celé číslo.
Z daných podmienok zrejme plynie, že ak topologické usporiadanie existuje, tak ho nájdeme a hodnoty priradené vrcholom budú "minimálne možné". Pretože dané topologické usporiadanie s "minimálnymi možnými" hodnotami je prípustné riešenie LP a zároveň i optimálne, pretože ak by nejaké "ohodnotenie" vrcholov malo menšiu hodnotu `M`, tak by nutne z podmienok pre `M`, `x[i]` a toho, že `x[i] + 1 <= x[j]` pre hranu `(i,j)`, plynulo, že buď sme neuvažovali optimálne riešenie alebo je nejaký taký vrchol `i`, ktorému sme priradili `x[i] < 0`. Kde obe možnosti vedú ku sporu.

## Úloha 2
LP bude mať pre každú jednu hranu `(u,v)` premennú v tvare `x[u,v]`, ktorá bude `0` alebo `1`, a hodnoty budú reprezentovať to či hranu v grafe nechávame, resp. ju odoberáme.
LP bude minimalizačný, kde budeme minimalizovať sumu v ktorej bude člen pre každú hranu `(u,v)` a ten bude vyzerať takto: `x[u,v]*c(u,v)`. Teda minimalizujeme cenu odobraných hrán, čo aj chceme. Následne máme pre každý trojcyklus,
resp. štvorcyklus podmienku na to, aby aspoň jedna hrana z neho bola odobraná. Teda napr. pre trojcyklus 
`(u,v),(v,r),(r,u)` máme podmienku `x[u,v] + x[v,r] + x[r,u] >= 1`. Kedže graf neobsahuje smyčky a ani cykli dĺžky dva, tak nový graf nebude obsahovať trojcyklus ani štvorcyklus práve vtedy, keď z každého trojcyklu, resp. štvorcyklu odoberieme aspoň jednu hranu. A presne to a len to hovovria podmienky v nami vygenerovanom LP. Teda riešenie LP korektne označí hrany, ktoré musíme odobrať pre odstránenie daných cyklov a vďaka minimalizačnej funkcii budú mať tieto hrany i minimálnu celkovú cenu.

Chovanie programu v hraničných prípadoch:
========================================
## Úloha 1
Ak graf nebude mať vrcholy, tak LP bude "úspešný" a hodnota účelovej funkcie bude `-1`. 

Inak ak má graf topologické usporiadanie, tak zrejme má i topologické usporiadanie s minimálnou maximálnou hodnotou vrchola, pretože počet všetkých možných priradení čísel od `0..i` pre `i` od `0..N-1`, kde `N` je počet vrcholov, je zrejme konečný počet. A zrejme keď si vrcholi topologicky usporiadame a priradíme im čísla od `0...N-1`, tak to bude nejaké usporiadanie. Teda z toho plynie, že ak má graf topologické usporiadnie, tak má i "minimálne" usporiadanie a toto bude prípustné riešenie a zároveň i optimálne, ak už bolo povedané v predchádzajúcej sekcii.

Ak graf nemá topologické usporiadanie, tak zrejme nemôže mať ani "minimálne" topologické usporiadanie. V tomto prípade nebude generovať daný kód LP pre glpsol žiadny vlastný výstup a to, že úloha nemá riešenie bude oznámené glpsol-om nasledujúcim reťazcom:
`PROBLEM HAS NO DUAL FEASIBLE SOLUTION`

## Úloha 2
Ak graf nebude mať vrcholy a teda ani hrany, tak LP bude "úspešný" a hodnota účelovej funkcie bude `0`.

V ostatných prípadoch bude mať úloha LP tiež riešenie. Pretože, počet hrán je konečný, teda počet podmnožín hrán je konečný. Teda počet možných riešení našej úlohy je konečný, resp. počet možných kombinácií priradenia `0` a `1` premenným odpovedajúcim hranám je konečný. A zrejme odobratie všetkých hrán je prípustné riešenie úlohy, resp. nastavenie všetkých premenných na `1` je prípustné riešenie. A teda keďže počet prípustných riešení je konečne veľa a nenulový počet, tak má úloha nutne vždy i optimálne riešenie. A keďže toto optimálne riešenie je optimálne a spĺňa všetky podmienky, tak bude i korektným riešením úlohy 2. 

Komentár k použitým knihovnám:
============================
Použil som v kóde 2 štandardné prídavné knihovny a to `System.Text` a `System.Collections.Generic`. Z prvej som využil jednu triedu, ktorá mi umožnila rýchlejšie zreťazovať reťazce. Z druhej som využil generické List-i ako implementáciu spojového zoznamu. 
