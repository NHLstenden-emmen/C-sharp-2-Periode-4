# Startdocument for C# Periode 4 Year 2

-   Startdocument of **Daan Daling**. Studentnumber **4815580**.
-   Startdocument of **Kevin Smulders**. Studentnumber **4806131**.
-   Startdocument of **Roan Meijer**. Studentnumber **4872584**.
-   Startdocument of **Simchaja Schonewille**. Studentnumber **4733312**.

### Problem Discription

Voor het project moet een game worden gemaakt. Voor deze game is gekozen voor een top-down racer game. Deze game is 2d
en hierin moet een speler zo snel mogelijk een map voltooien. De besturing zal worden uitgevoerd met de W A S D toetsen,
ook hebben de spelers acceleratie en deceleratie zodat de spelers niet gelijk van 0 naar 100 kunnen gaan of 100 naar 0.
In dit spel moet een AI doormiddel van machine learning leren om een rondje in het spel af te leggen. Hierin moet de AI
met behulp van verschillende generaties zichzelf verbeteren om een zo snel mogelijke map tijd af te leggen. Ook moet het
hierna voor een speler mogelijk zijn om een map tegen de AI af te leggen.

-   Maak een top down racer game
-   Maak de controls voor de racer game
-   Maak het visuele aspect van de game
-   Pas toe dat de spelers kunnen accelereren deceleren
-   Pas machine learning toe met de gebruik van een neural network
-   Geef de input van de AI weer
-   Geef de score van een map weer
-   Geef een timer weer
-   Maak het mogelijk om met meerdere instanties van de AI te trainen
-   Pas incentive learning toe (checkpoints, reward systeem etc.)
-   Maak het mogelijk tegen de AI te spelen

### Input & Output

in de volgende sectie wordt de input en output van de applicatie beschreven

#### Input

| Case         | Type | Conditions |
| ------------ | ---- | ---------- |
| KeyDown("w") | Key  | in game    |
| KeyDown("a") | Key  | in game    |
| KeyDown("s") | Key  | in game    |
| KeyDown("d") | Key  | in game    |

#### Output

| Case                    | Type           |
| ----------------------- | -------------- |
| actions preformed by AI | keys           |
| scores                  | integer        |
| best preforming AI      | neural network |

#### Remarks

-   VOORBEELD Input wordt gevalideerd
-   VOORBEELD alleen de main bevat XXXX
-   VOORBEELD wordt getest

## Class Diagram

![UML Diagram]()

## Testplan

De testcases die worden gebruikt om de applicatie te testen

### Testcases voor de schermen

Dit is het testplan voor de verschilende schermen die in beeld komen.

#### Testcase #1, Bij het opstarten van de aplicatie zie je een startmenu

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcase #2, Bij het winnen van een ronde zie je een scherm met de score van de ronde

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcase #3, Bij het verliezen van een ronde zie je een scherm met de score van de ronde

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcase #4, Een menu voor de aanpassing van de AI

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcase #5, Een menu voor het opstarten van een nieuwe ronde

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

### Testplan Game

Dit is het testplan om de game te testen

#### Testcases #1, Auto raakt muur en gaat dood

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #2, Auto raakt checkpoint

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #3, Score gaat omhoog

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #4, Score gaat omlaag

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #5, Auto kan worden bewogen door de speler

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #6, De auto accelereert en de snelheid gaat omhoog

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #7, De auto kan vertragen als de speler het gaspedaal los laat

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #8, De speler kan de score zien

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #9, De speler kan de tijd zien

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #10, De speler kan een leader board zien met de tijden/punten van de spelers

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

### Testplan Ai

Dit is het testplan om de AI te testen

#### Testcases #1, De Ai kan de auto bewegen

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #2, De versies met de hoogste score volgens de reward system worden gekozen voor een nieuwe generatie

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #3, Het is mogelijk om meerdere generaties te gelijk te trainen

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #4, De reward system word getriggerd als de auto een checkpoint raakt

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #5, De auto gaat dood als die een muur raakt en de reward system word getriggerd

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #6, Er kan een nieuwe ronde worden gestart als alle autos dus generaties dood zijn

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #7, Aan het einde van een generatie word er een nieuwe generatie gemaakt en een neiuwe ronde gestart

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #8, Er word een nieuwe ronde gestart na een beapalde tijd

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #9, de generaties/autos kunnen door elkaar heen rijden en houden geen rekening met elkaar

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |

#### Testcases #10, Er kan een nieuwe ronden worden gestart als alle auto's op het einde van de ronde zijn

| Step | Input | Action | Expected output |
| ---- | ----- | ------ | --------------- |
|      |       |        |                 |
