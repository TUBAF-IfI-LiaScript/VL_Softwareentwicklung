<!--

author:   Sebastian Zug & André Dietrich
email:    Sebastian.Zug@informatik.tu-freiberg.de & andre.dietrich@informatik.tu-freiberg.de
version:  0.0.1
language: de
narrator: Deutsch Female

import: https://raw.githubusercontent.com/liaScript/rextester_template/master/README.md
        https://raw.githubusercontent.com/liaTemplates/ExplainGit/master/README.md

-->

# Softwareentwicklung - 3 - Versionsverwaltung II

**TU Bergakademie Freiberg - Sommersemester 2020**

Link auf die aktuelle Vorlesung im Versionsmanagementsystem GitHub

[https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/03_VersionsverwaltungII.md](https://github.com/SebastianZug/CsharpCourse/blob/SoSe2020/03_VersionsverwaltungII.md)

Die interaktive Form ist unter diese Link zu finden ->
[LiaScript Vorlesung 03](https://liascript.github.io/course/?https://raw.githubusercontent.com/SebastianZug/CsharpCourse/SoSe2020/03_VersionsverwaltungII.md#1)

---------------------------------------------------------------------


## 7 Fragen in 7 Minuten


**1. Der Befehl `git pull` realisiert zwei Schritte:**

    [( )] `git branch` und `git merge`
    [(X)] `git fetch` und `git merge`
    [[?]] Der kombinierte Befehl bezieht sich auf die Interaktion mit dem Remote-Repository und dessen ggf. vor dem aktuellen lokalen Repository liegenden Versionen.
    ***********************************************************************

                                    {{1}}
    Mit `git pull` werden die aktuellen Änderungen des Branches, die auf dem Remote-Repository
    liegen abgerufen und in das lokale Repository mit `git merge` übernommen.

    ***********************************************************************

**2. Exklusives Bearbeiten (Sequentialisierung): Kreuze in der richtigen Reihenfolge an!**

    [[1.] [2.] [3.]]
    [( )  (X)  ( ) ]       Modify
    [(X)  ( )  ( ) ]       Lock
    [( )  ( )  (X) ]       Unlock
    ***********************************************************************

    Bei der Sequentialisierung handelt es sich um die pessimistische Versionsverwaltung, bei der einzelne Dateien vor einer Änderung durch den Benutzer gesperrt und nach Abschluss selbiger wieder freigegeben werden.

    ***********************************************************************


**3. Kollaboratives Arbeiten mit Mischen: Kreuze in der richtigen Reihenfolge an!**

    [[1.] [2.] [3.]]
    [(X)  ( )  ( ) ]       Copy
    [( )  ( )  (X) ]       Merge
    [( )  (X)  ( ) ]       Modify
    ***********************************************************************

    Hierbei handelt es sich um die optimistische Versionsverwaltung, bei der gleichzeitige Änderungen durch mehrere Benutzer an einer Datei möglich sind, da diese Änderungen anschließend automatisch oder manuell zusammengeführt werde (*Merge*).

    ***********************************************************************


**4. Vervollständige die Definition:**

    Ein Paar von Änderung aus Dokument 1 bzw. Dokument 2 gegenüber einem Ausgangsdokument kann unverträglich sein, wenn die Abbildung beider Änderungen in einem gemeinsamen Dokument nicht möglich ist. In diesem Fall spricht man von...

    [( )] einer Versionsdoppelung
    [( )] einem Repository-Fehler
    [(X)] einem Konflikt
    ***********************************************************************

    Im Falle eines Konflikts muss eine der betroffenen Änderungen weggelassen werden. Welche das sein soll kann anhand von *Interaktivem* oder *Nicht-interaktivem Mischen* entschieden werden.

    ***********************************************************************


**5. Wobei wird ein Entwickler unmittelbar in den Mischprozess eingebunden?**

    [(X)] Interaktives Mischen
    [( )] Nicht-interaktives Mischen
    [[?]] Was bedeutet das Wort *interaktiv*?
    [[?]] *Interaktion* = Wechselbeziehung zwischen Handlungspartnern
    ***********************************************************************

    Denkbare Entscheidungen für die Lösung eines Konflikts durch *Interaktives Mischen* wären die Übernahme einer der Änderungen, keiner Änderung oder die Übernahme einer modifizierten Änderung

    ***********************************************************************

**6. Kreuze die zugehörige Definition an!**

    [[2-Wege-Mischen] [3-Wege-Mischen]]
    [       ( )              (X)      ]       Mischen von Dokumenten gemeinsamen Ursprungs
    [       (X)              ( )      ]       Mischen unabhängiger Dokumente
    [[?]] Beim 2-Wege-Mischen ist die Erzeugung eines neuen Dokuments das Ziel, während beim 3-Wege-Mischen eine weiterentwickelte Version des ursprünglichen Dokuments entstehen soll. Übertrage dieses Wissen auf die Ausgangsdokumente um die richtigen Definitionen zuordnen zu können.
    ***********************************************************************

    Beim *2-Wege-Mischen* werden die gemeinsamen Komponenten und die individuellen Teilmengen voneinander unabhängiger Dokumente vereint.
    Beim *3-Wege-Mischen* werden (möglichst) alle Änderungen am Ursprungsdokument in eine weiterentwickelte Version des selbigen integriert.

    ***********************************************************************


**7. Welche Form der Versionsverwaltung findet noch heute in manchen Büroanwendungen (bspw. *Word*) Verwendung?**

    [( )] Verteilte Versionsverwaltung
    [( )] Zentrale Versionsverwaltung
    [(X)] Lokale Versionsverwaltung
    [[?]] Die genannten Anwendungen speichern die Versionen eines Dokuments in der Datei des Dokuments selbst - dabei handelt es sich um eine `?` Speicherung.




## Zustände einer Datei im git-Kontext

Sie kennen bereits die möglichen Zuständer einer Datei innerhalb der GitHub-Welt.

Wir unterscheiden zwischen `tracked` und `untracked` files, also Dateien, die vom Versionssystem berücksichtigt werden oder eben nicht. Nicht beobachtet können zum Beispiel Testdaten sein, die man nutzt, um den eigentlichen Algorithmus, der natürlich getrackt wird zu evaluieren. Wenn Dateien einmal mit `git add` erfasst wurden, gehören Sie zum Kreis der versionierten Dateien. Die Erfassung jeder Änderung kann nun mit `git add` in den Versionen Berücksichtigung finden.

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii

unbeobachtet                           "beobachtet"
=============  ========================================================
   nicht              nicht            modifiziert            staged
  erfasst          modifiziert
    |                   |       edit        |                   |
    |                   |- - - - - - - - - >|                   |
    |                   |                   |     git add       |
    |                   |                   |- - - - - - - - - >|
    |                   |     git add       |                   |
    |- - - - - - - - - - - - - - - - - - - - - - - - - - - - - >|
    |                   |                   |     git commit    |
    |                   |< - - - - - - - - - - - - - - - - - - -|
    |    remove         |                   |                   |
    |< - - - - - - - - -|                   |                   |              .
```

Die Editoren unterstützen den Nutzer bei der Arbeit, in dem Sie die eigentlichen Commandozeilentools rund um die Arbeitsfläche anordnen.

![ScreenShot](./img/03_VersionsverwaltungII/ArbeitmitGitImEditor.png)<!-- width="100%" -->

| Bereich | Bedeutung                                                                                                                                                                                                                                  |
| ------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| A       | Darstellung der Dateien im Ordner, wobei der Typ über die führenden Symbole und der Zustand über die Farbgebung hervorgehoben wird (grün untracked Files, orange getrackte Dateien mit Änderungen).                                        |
| B       | Hier erfolgt die Darstellung der `Staged Changes` also der registrierten Änderungen. Offenbar ist die Datei `02_Versionsverwaltung.md` verändert worden, ohne dass ein entsprechendes `git add 02_Versionsverwaltung.md` ausgeführt wurde. |
| C       | Die Übersicht der erfassten Änderngen mit einem `stage` Status übernimmt die aus der darüber geführten Liste, wenn der Befehlt ausgeführt wurde. Offenbar ist dies fr `.gitignore` der Fall.                                               |
| D       | Die Übertragung ins lokale Repository wird gerade vorbereitet. Die Commit-Nachricht ist bereits eingegeben. Der Button zeigt den zugehörigen Branch.                                                                                       |
|         | G                                                 An dieser Stelle wird ein kurzes Log der letzten Commits ausgeben. Dies ermöglich das effiziente Durchsuchen der nach bestimmten Veränderungen.                                                                                                                                                                                          |

Die identischen Informationen lassen sich auch auf der Kommandozeile einsehen:

```console
▶git status
On branch SoSe2020dev
Your branch is up to date with 'origin/SoSe2020dev'.

Changes to be committed:
  (use "git reset HEAD <file>..." to unstage)

        modified:   .gitignore

Changes not staged for commit:
  (use "git add/rm <file>..." to update what will be committed)
  (use "git checkout -- <file>..." to discard changes in working directory)


        modified:   02_Versionsverwaltung.md
        deleted:    03_ContinuousIntegration.md

Untracked files:
  (use "git add <file>..." to include in what will be committed)

        03_VersionsverwaltungII.md
        code/
        img/03_VersionsverwaltungII/
```

```console
▶git log
commit d7603554c958c478f1ec600bd3ccea437d91ae9a (HEAD -> SoSe2020dev, origin/SoSe2020dev)
Author: Sebastian Zug <Sebastian.Zug@informatik.tu-freiberg.de>
Date:   Thu Apr 9 13:16:38 2020 +0200

    First version of L3

commit 39fc168222f4c7a7d062adcaccff17fe34bccbe3
Author: Sebastian Zug <Sebastian.Zug@informatik.tu-freiberg.de>
Date:   Thu Apr 9 13:16:12 2020 +0200

    First version of L02

commit 350c127c7dfbc61a81edc8bd148f605ee681a07a

Author: Sebastian Zug <Sebastian.Zug@informatik.tu-freiberg.de>
Date:   Tue Apr 7 07:01:02 2020 +0200

    Final version of L1
....
```

Häufig bettet ein Projekt Dateien ein, die Git nicht automatisch hinzufügen oder schon gar nicht als „nicht versioniert“ anzeigen soll. Beispiele dafür sind automatisch generierte Dateien, wie Log-Dateien oder die Binaries, die von Ihrem Build-System erzeugt wurden. In solchen Fällen können Sie die Datei .gitignore erstellen, die eine Liste mit Vergleichsmustern enthält. Hier ist eine .gitignore Beispieldatei:

```console     gitignoreExample
# ignore all .a files
*.a

# but do track lib.a, even though you're ignoring .a files above
!lib.a

# only ignore the TODO file in the current directory, not subdir/TODO
/TODO

# ignore all files in any directory named build
build/

# ignore doc/notes.txt, but not doc/server/arch.txt
doc/*.txt

# ignore all .pdf files in the doc/ directory and any of its subdirectories
doc/**/*.pdf
```

Unter [gitIgnoreBeispiele](https://github.com/github/gitignore) gibt es eine ganze Sammlung von Konfigurationen für bestimmte Projekttypen.

## Interaktion mit dem Remote-Repository
<!--
@@ Hinweis für die Realisierung
    git commit -m "Test"
-->

Versuchen wir das ganze noch mal etwas plastischer nachzuvollziehen. Rechts oben sehen Sie unser Remote-Repository auf dem Server. Im mittleren Bereich den Status unseres lokalen Repositories.

``` text @ExplainGit.eval
create origin
```


## Arbeiten mit Branches

Die Organisation von Versionen in unterschiedlichen Branches ist ein zentrales
Element der Arbeit mit git. Branches sind Verzweigungen des Codes, die bestimmte
Entwicklungsziele kapseln.

Der größte Nachteil bei der Arbeit mit nur einem Branch liegt darin, dass bei einem
defekten Master(-Branch) die Arbeit sämtlicher Beteiligter unterbrochen wird. Branches
schaffen einen eignen (temporären) Raum für die Entwicklung neuer Features, ohne
die Stabilität des Gesamtsystems zu gefährden. Gleichzeitig haben die Entwickler den gesamten Verlauf eines Projekts in strukturierter Art zur Hand.

Wie sieht das zum Beispiel für unsere Kursmaterialien aus?

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii

        vSoSe2019                                                   vSoSe2020
Master   O-----------------------------------------  ....  ---------O
          \                                                        ^
           \               Offizielle Versionen                   /
SoSe2020    \              O-->O                 O          ---->O
             \            ^     \               /
              v          /       v             /
SoSe2020dev    O->O---->O---->O->O---->O-->O->O      ....
               Vorlesung      Vorlesung
               00             01
```

Ein Branch in Git ist einfach ein Zeiger auf einen Commit zeigt. Der zentrale Branch wird zumeist als `master` bezeichnet.

### Generieren und Navigation über Branches
<!--
@@ Hinweis für die Realisierung
    git branch feature
    git checkout feature
    git checkout 0e8bf9e
    git branch newFeature
-->

Wie navigieren wir nun konkret über den verschiedenen Entwicklungszweigen.

``` text @ExplainGit.eval
git commit -m V1
git commit -m V2
git commit -m V3
```


### Mergoperationen über Branches
<!--
@@ Hinweis für die Realisierung
    git checkout master
    git branch hotfix
    git checkout hotfix
    git commit -m "Solve bug"
    git checkout master
    git merge hotfix
    You have performed a fast-forward merge.
    git branch -d hotfix
    git checkout newFeature
    git commit -m FeatureV3
    git checkout master
    git merge newFeature
    Beim fast-forward merge gibt es keine nachfolgende Version. Der master wird
    einfach auf den anderen branch verschoben.
    Im zweiten Fall läuft ein 3 Wege Merge ab
-->
Nehmen wir folgendes Scenario an. Sie arbeiten an einem Issue, dafür haben Sie
einen separaten Branch (newFeature) erzeugt und haben bereits einige Commits
realisiert. Beim Kaffeetrinken denken Sie über den Code von letzter Woche nach und Ihnen fällt ein Bug ein, den Sie noch nicht behoben haben. Jetzt aber schnell!

Legen Sie dafür einen neuen Branch an, commiten Sie eine Version und mergen
Sie diese mit dem Master. Kehren Sie dann in den Feature-Branch zurück und
beenden Sie die Arbeit. Mergen Sie auch diesen Branch mit dem Master.
Worin unterscheiden sich beide Vorgänge?

``` text @ExplainGit.eval
git branch newFeature
git checkout newFeature
git commit -m FeatureV1
git commit -m FeatureV2
```

Mergen ist eine nicht-destruktive Operation. Die bestehenden Branches werden auf keine Weise verändert. Das Ganze "bläht" aber den Entwicklungsbaum auf.

### Rebase mit einem Branch
<!--
@@ Hinweis für die Realisierung
    git checkout master
    git rebase newFeature
    git branch -d newFeature
-->

Zum `merge` exisitert auch noch eine alternative Operation. Mit `rebase` werden die Änderungen eins branches in einem Patch zusammengefasst. Dieser wird dann auf head angewandt.

``` text @ExplainGit.eval
git branch newFeature
git checkout newFeature
git commit -m FeatureV1
git commit -m FeatureV2
git checkout master
git commit -m V1
```

## Typische Abläufe

**Ergänzen eines Commits um zusätzliche Dateien**

Ein typisches Problem ist, dass sie im Entwicklungseifer nicht alle zugehörigen Dateien dem letzten Commit zu geordnet haben. Sie können nun einen weiteren Commit "hinterherschicken", dieses vorgehen zerstört aber Ihren Fluß.

```console
▶git add newClass.cs
▶git commit -m "Adds a new fancy feature"
▶touch newClass_api_documentation.md
▶git add newClass_api_documentation.md
▶git commit --amend -no-edit
```

**Letzte(n) Commits rückgängig machen**

Sie haben einen Commit realisiert, der Ihnen im Nachhinein als unsinnig erscheint und möchten diesen zurücknehmen. Die Veränderungen sollten aber erhalten bleiben. Nach dem Rückgesetzten des Commits liegen diese wieder so im Stage-Bereich, wie davor.

```console
▶git reset --soft HEAD^
```

Mit `HEAD^` geht von HEAD einen Schritt zurück HEAD^^/HEAD~2 geht 2 Committs zurück, …)

```console
▶git reset --hard HEAD^
```

ACHTUNG: Damit sind alle Veränderungen auch im Staging-Bereich UND im Working Directory WEG!!!

``` text @ExplainGit.eval
git commit -m V1
git commit -m V2
git commit -m V3
```

ExplainGit kann den Vorgang des `reset --soft` aus nachvollziehbaren gründen nicht abbilden.

**Umgestalten der History**

Ein sehr mächtiges Werkzeug ist der interaktive Modus von `git rebase`. Damit kann die
Geschichte neugeschrieben werden, wie es die git Dokumentation beschreibt. Im Grund können Sie damit Ihre Versionsgeschichte "aufräumen". Einzelne Commits umbenennen, löschen oder fusionieren. Dafür besteht ein eigenes Interface, dass Sie mit dem folgenden Befehl aufrufen können:

```console
▶git rebase -i HEAD~5

pick d2a06e4 Update main.yml
pick 78839b0 Reconfigures checkout
pick f70cfc7 Replaces wildcard by specific filename
pick 05b76f3 New pandoc command line
pick c56a779 Corrects md filename

# Rebase a3b07d4..c56a779 onto a3b07d4 (5 commands)
#
# Commands:
# p, pick = use commit
# r, reword = use commit, but edit the commit message
# e, edit = use commit, but stop for amending
# s, squash = use commit, but meld into previous commit
# f, fixup = like "squash", but discard this commit's log message
# x, exec = run command (the rest of the line) using shell
# d, drop = remove commit
#
# These lines can be re-ordered; they are executed from top to bottom.
#
# If you remove a line here THAT COMMIT WILL BE LOST.
#
# However, if you remove everything, the rebase will be aborted.
#
# Note that empty commits are commented out
```

Als Anwendungsfall habe ich mir meine Aktivitäten im Kontext einiger Experimente
mit den GitHub Actions, die im nächsten Abschnitt kurz eingeführt werden, ausgesucht.
Schauen wir zunächst auf den ursprünglichen Stand. Alle Experimente drehten sich darum, eine Datei anzupassen und dann auf dem Server die Korrektheit zu testen.

```console
▶
c56a779 - Sebastian Zug, 7 hours ago : Corrects md filename
05b76f3 - Sebastian Zug, 7 hours ago : New pandoc command line
f70cfc7 - Sebastian Zug, 8 hours ago : Replaces wildcard by specific filename
78839b0 - Sebastian Zug, 21 hours ago : Reconfigures checkout
d2a06e4 - Sebastian Zug, 22 hours ago : Update main.yml
...
aa04051 - Sebastian Zug, 23 hours ago : Restart action activities
4b22d12 - Sebastian Zug, 23 hours ago : Deleting old state
64075cc - Sebastian Zug, 24 hours ago : Update main.yml
01f341b - Sebastian Zug, 24 hours ago : Missing links added
...
29c8e68 - Sebastian Zug, 11 days ago : Update README.md
```

Unser lokaler Branch liegt nach dem Löschen aber um einiges hinter dem auf GitHub entsprechend müssen wir mit `git push --force` das Überschreiben erzwingen.

**Pull requests und Reviews**

Natürlich wollen wir nicht, dass "jeder" Änderungen ungesehen in unseren Code einspeist. Entsprechend kapseln wir ja auch unseren Master-Branch. Den Zugriff regeln sowohl die Rechte der einzelnen Mitstreiter als auch die
Pull-Request und Review Konfiguration.

| Status des Beitragenden | Einreichen des Codes |
| ----------------------- | -------------------- |
| Collaborator            | Auschecken des aktuellen Repositories und Arbeit. Wenn die Arbeit in einem eigenen Branch erfolgt, wird diese mit einem Pull-Request angezeigt und gemerged.|
| non Collaborator        | Keine Möglichkeit seine Änderungen unmittelbar ins Repository zu schreiben. Der Entwickler erzeugtn eine eigene Remote Kopie (_Fork_) des Repositories und dortige Realsierung der Änderungen.  Danach werden diese als Pull-Request eingereicht.                  |

Wird ein Pull Request akzeptiert, so spricht man von einem _Merge_, wird er geschlossen, so spricht man von einem _Close_. Vor dem Merge sollte eine gründliche Prüfung sichergestellt werden, diese kann in Teilen automatisch erfolgen oder durch Reviews [Doku](https://github.com/features/code-review/)


## Ein Wort zur Zusammenarbeit

Bitte haben Sie immer den spezifischen Kontext Ihres Projektes vor Augen. Üblicherweise legt man am Anfang, bei einem "kleinen Hack" keinen Wert auf formelle Abläufe und Stukturen. Diese sind
aber in großen Projekten unablässig.

Ein neues Feature wird in einem Issue beschrieben, in einem eigenen Branch implementiert, mit Commits beschrieben, auf den master branch abgebildet und das Issue mit Referenz auf den commit geschlossen.

Entsprechend ist die Dokumentation in Form der Issues und Commit-Messages der zentrale Schlüssel für die Interaktion im Softwareentwicklerteam. Entsprechend hoch ist Ihre Bedeutung anzusetzen.

Stöbern Sie dafür mal durch anderen Projekte (zum Beispiel [GitHub Tensorflow](https://github.com/tensorflow/tensorflow)) und informieren Sie sich über deren Policies.

![TensorflowOnGithub](./img/03_VersionsverwaltungII/ScreenshotTensorflow.png)<!-- width="100%" -->

Folgende Regeln sollte man für die Beschreibung eines Commits berücksichtigen:

+ Trennen Sie den Betreff durch eine Leerzeile vom folgenden Text
+ Beschränkt Sie sich bei der Betreffzeile auf maximal 50 Zeichen
+ Beginnen Sie die Betreffzeile mit einem Großbuchstaben
+ Schreiben Sie die Betreffzeile im Imperativ
+ Brechen Sie den Text der Message 72 Zeichen um
+ Beschreiben Sie in der Commit-Nachricht das was und warum, aber nicht das wie.

Eine weiterführende Diskussion zum Thema bietet zum Beispiel die Webseite [TheServerSide](https://www.theserverside.com/video/Follow-these-git-commit-message-guidelines).

## Automatisierung der Arbeit

Hervorragend! Wir sind nun in der Lage die Entwicklung unseres Codes zu "verwalten". Allerdings sagt noch niemand, dass ein eingereichter Code auch lauffähig ist. Wie können wir aber möglichst schnell realisieren, dass etwas schief geht? Es wäre wünschenswert, dass wir unmittelbar mit den Aktivitäten unserer Entwickler entsprechende Tests durchführen und zum Beispiel deren Commits zurückweisen.

An dieser Stelle wollen wir zunächst die Möglichkeiten des _Continuous Integration_ aufzeigen, die differenzierte Diskussion einer Folge von Build und Test-Schritten folgt später. Wir werden diese Möglichkeit im Rahmen der Übungsaufgaben nutzen, um Ihre Lösungen zu testen.

GitHub stellt dafür die sogenannten _Actions_ zur Verfügung. Dies sind Verarbeitungsketten, die auf verschiedensten Architekturen, Betriebssystemen, Konfigurationen usw. laufen können. Damit haben wir die Möglichkeit einen Quellcode für verschiedene Plattformen zu bauen und zu testen oder eine Dokumentation zu erstellen.

Ein _Workflow_ wird durch vordefinierten _Trigger_ ausgelöst. Dies können das Anlegen einer Datei, ein Commit oder ein Pull Request sein. Danach wird das System konfiguriert und die Folge der Verarbeitungsschritte gestartet.

<!--
style="width: 100%; max-width: 560px; display: block; margin-left: auto; margin-right: auto;"
-->
```ascii
    +------+
    |      |\           +----> Check Syntax
  +-|      +-+          |
  | | File.a |          |               +---> Windows 10   --+
+-| +--------+  Commit  |               |                    |
| | File.b  |  ---------+----> Build ---+---> Ubuntu 18.04 --+
| +---------+           |               |                    |
| File.c   |            |               +---> MacOS ---------+
+----------+            |                                    |
                        +----> Test <------------------------+
                        |
                        +----> Generierung Dokumentation                     .
```

GitHub gliedert diese Punkte in _Workflows_ und _Jobs_ in einer hierachischen Struktur, die über `yaml` Files beschrieben werden. Eine kurze Einführung zur Syntax findet sich unter [Wikipedia](https://de.wikipedia.org/wiki/YAML).

```yaml   main.yaml
name: Hello World
on: [push]

jobs:
  build-and-run:
    name: Print Hello World
    runs-on: ubuntu-latest
    steps:
      - name: Checkout files (master branch)
        uses: actions/checkout@v2
      - name: Show all files
        run: pwd && whoami && ls -all
```

Spanndend wird die Sache nun dadurch, dass es eine breite Community rund um die Actions gibt. Diese stellen häufig benötigte _Steps_ bereits zur Verfügung, fertige Tools für das Bauen und Testen von .NET Code.

Die Dokumentation zu den GitHub-Actions findet sich unter [https://github.com/features/actions](https://github.com/features/actions). Ein umfangreicheres Beispiel finden Sie in unserem Projektordner im aktuellen Branch `SoSe2020`. Hier werden alle LiaScript-Dateien in ein pdf umgewandelt.

> **Merke** Workflow files müssen unter `.github\workflows\*.yml` abgelegt werden.

## Aufgaben

1. Recherchieren Sie die Methode des "Myers-diff-Algorithmus" https://blog.jcoglan.com/2017/02/12/the-myers-diff-algorithm-part-1/
2. Legen Sie sich ein lokales Repository mit git an und experimentieren Sie damit.
3. Richten Sie sich für Ihren GitHub-Account einen SSH basierten Zugriff ein (erspart einem das fortwährende Eingeben eines Passwortes).
3. Sie erhalten im Laufe der Woche Ihre erste Einladung für einen GitHub Classroom. Ausgehend davon werden Sie aufgefordert sich in Zweierteams zu organiseren und werden dann gemeinsam erste Gehversuche unter git zu unternehmen.
4. Möchten Sie die Action zu Generierung des Skripts verbessern? Ich habe dazu die wünschenswerten Features in einem Issue erfasst.
