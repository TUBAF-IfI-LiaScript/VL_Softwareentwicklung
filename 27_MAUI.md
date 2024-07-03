<!--

author:   Galina Rudolf, Sebastian Zug, 
email:    sebastian.zug@informatik.tu-freiberg.de
version:  1.0.1
language: de
narrator: Deutsch Female
comment:  Grundlagen der Programmierung mit MAUI
tags:      
logo:     

import: https://github.com/liascript/CodeRunner

import: https://raw.githubusercontent.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/master/config.md

-->

[![LiaScript](https://raw.githubusercontent.com/LiaScript/LiaScript/master/badges/course.svg)](https://liascript.github.io/course/?https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/27_MAUI.md)

# Maui-GUI Programmierung

| Parameter                | Kursinformationen                                                                    |
| ------------------------ | ------------------------------------------------------------------------------------ |
| **Veranstaltung:**       | `Vorlesung Softwareentwicklung`                                                      |
| **Teil:**                | `27/27`                                                                              |
| **Semester**             | @config.semester                                                                     |
| **Hochschule:**          | @config.university                                                                   |
| **Inhalte:**             | @comment                                                                             |
| **Link auf den GitHub:** | https://github.com/TUBAF-IfI-LiaScript/VL_Softwareentwicklung/blob/master/27_MAUI.md |
| **Autoren**              | @author                                                                              |

![](https://media.giphy.com/media/26tn33aiTi1jkl6H6/source.gif)

---------------------------------------------------------------------


# Maui I

"MAUI" in .NET MAUI steht für Multi-platform App UI und hat keine direkte Verbindung zur hawaiianischen Insel Maui.

![](https://upload.wikimedia.org/wikipedia/commons/5/5c/Maui_Landsat_Photo.jpg "Maui Landsat satellite photo (false color). [https://commons.wikimedia.org/wiki/File:Maui_Landsat_Photo.jpg](https://commons.wikimedia.org/wiki/File:Maui_Landsat_Photo.jpg)")

## GUI

> GUI - eine Form von Benutzerschnittstelle eines Computers mit der Aufgabe Anwendungssoftware auf einem Rechner mittels grafischer Symbole, Steuerelemente oder auch Widgets genannt, bedienbar zu machen.

https://de.wikipedia.org/wiki/Grafische_Benutzeroberfl%C3%A4che



> Anekdote (erzält von ChatGPT): Steve Jobs besuchte das Xerox PARC (Palo Alto Research Center) in den späten 1970er Jahren und war beeindruckt von den Technologien, die er dort sah, insbesondere von der grafischen Benutzeroberfläche. Er erkannte sofort das Potenzial und implementierte ähnliche Konzepte in den Apple Lisa und später in den Macintosh. Jobs soll gesagt haben, dass der Besuch bei Xerox PARC einer der inspirierendsten Momente seiner Karriere war.

![Xerox Alto](https://upload.wikimedia.org/wikipedia/commons/thumb/8/8b/Xerox_Alto.jpg/220px-Xerox_Alto.jpg "Xerox Alto (1973")
![Apple Lisa](https://upload.wikimedia.org/wikipedia/commons/thumb/2/21/Apple_Lisa_%28Little_Apple_Museum%29_%288032162544%29.jpg/220px-Apple_Lisa_%28Little_Apple_Museum%29_%288032162544%29.jpg "Apple Lisa 1983")


### Warum grafisch?

> Zitat: "The screen is a window through which one sees a virtual world. The challenge is to make the world look real, act real, sound real, and feel real." – Ivan Sutherland

+ Benutzerfreundlich und intuitiv bedienbar, optimalere Lernkurve für weniger technisch versierte Benutzer 
+ Umfangreiche Möglichkeiten komplexe Daten zu visualisieren durch Diagramme, Tabellen und Grafiken 
+ Interaktivität: Feedback in Echtzeit
+ Minimierung von Eingabefehler
+ Ästhetik 

### Einstiegsbeispiel

```python
import tkinter as tk
from tkinter import messagebox

def say_hello():
    messagebox.showinfo("Status",
                        "Aus kleinem Anfang entspringen alle Dinge - Cicero")

window = tk.Tk()
window.title("Beispiel-GUI")
window.geometry('520x300')

hello_button = tk.Button(window, text="Ich bin keine einfache Anzeige ",
                        command=say_hello)
hello_button.pack(pady=20)

window.mainloop()
```

Was sind die Kernelemente einer GUI-Anwendung?

+ **Fenster**: Die Hauptoberfläche, die alle anderen Elemente enthält.
+ **Widgets**: Interaktive Elemente wie Schaltflächen, Textfelder, Listen, Menüs, usw.
+ **Layouts**: Die Anordnung der Widgets im Fenster.
+ **Ereignisse**: Aktionen, die durch Benutzerinteraktionen ausgelöst werden.
+ **Logik**: Die Programmlogik, die auf Benutzeraktionen reagiert.


## .NET MAUI - mehr als GUI

https://de.wikipedia.org/wiki/.NET_MAUI 

https://learn.microsoft.com/de-de/dotnet/maui/what-is-maui?view=net-maui-8.0

> .NET MAUI (Multi-Platform App UI) ist ein plattformübergreifendes Framework zum Erstellen nativer Mobil- und Desktop-Apps mit C# und XAML.
>
> --Handbuch von .NET MAUI 8 

![.NET MAUI](https://learn.microsoft.com/de-de/dotnet/maui/media/what-is-maui/maui-overview.png?view=net-maui-8.0 "Multiplattform-App Vision aus dem Handbuch von .NET MAUI 8 https://learn.microsoft.com/de-de/dotnet/maui/what-is-maui?view=net-maui-8.0")

NET MAUI vereint Android-, iOS-, macOS- und Windows-APIs in einer einzigen API, die eine Write-Once-Run-Anywhere-Entwicklung ermöglicht und gleichzeitig umfassenden Zugriff auf alle Aspekte der jeweiligen nativen Plattform bietet.

![.NET MAUI Architektur](https://learn.microsoft.com/de-de/dotnet/maui/media/what-is-maui/architecture-diagram.png?view=net-maui-8.0 "Architektur von MAUI https://learn.microsoft.com/de-de/dotnet/maui/what-is-maui?view=net-maui-8.0")

In einer .NET MAUI-Anwendung schreiben Sie Code, der hauptsächlich mit der .NET MAUI-API (1) interagiert. Die .NET MAUI nutzt dann direkt die APIs der nativen Plattform (3). Darüber hinaus kann der Anwendungscode bei Bedarf direkt auf die APIs der Plattform zugreifen (2).

.NET Multi-Platform App UI (.NET MAUI)-Apps können für die folgenden Plattformen geschrieben werden:

+ Android 5.0 (API 21) oder höher ist erforderlich.
+ iOS 11 oder höher ist erforderlich.
+ macOS 10.15 oder höher mit Mac Catalyst.
+ Windows 11 und Windows 10, Version 1809 oder höher, mit Windows UI Library (WinUI) 3.


**MAUI- Geschichte**:

+ Ursprung: Xamarin.Forms wurde als eine plattformübergreifende UI-Toolkit für .NET-Entwickler eingeführt, um UI-Code zu teilen und native Benutzeroberflächen für iOS, Android und UWP (Universal Windows Platform) zu erstellen, trotz Popularität gab es Einschränkungen hinsichtlich der Wartbarkeit, Erweiterbarkeit und der Unterstützung neuerer Plattformen wie macOS und Linux.
+ Mai 2020: Microsoft kündigte .NET MAUI erstmals auf der Build-Konferenz. Die Idee war, eine weiterentwickelte Version von Xamarin.Forms zu schaffen, die eine einzige Codebasis bietet, um Anwendungen für iOS, Android, Windows und macOS zu entwickeln.
+ 2022: Veröffentlichung als als Teil von .NET 6
+ 2023 - aktuelle Version .NET 8

![](https://learn.microsoft.com/de-de/previous-versions/xamarin/get-started/what-is-xamarin-forms-images/xamarin-forms-architecture.png "Architektur von Xamarin.Forms https://learn.microsoft.com/de-de/previous-versions/xamarin/get-started/what-is-xamarin-forms-images/xamarin-forms-architecture.png")


### Minibeispiel (ohne XAML, nur C#)

> Der Code des Beispiels ist im Projektordner unter `/code/27_MAUI/MauiMinimal` zu finden. Versuchen Sie die Struktur nachzuvollziehen und die dabei verwendeten Merkmale der Sprache C# zu identifizieren.

```csharp 
using Microsoft.Maui;
using Microsoft.Maui.Controls;

public class App : Application
{
    public App()
    {
        //Hauptseite der App als C#-Code
        MainPage = new ContentPage 
        {
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        Text = "Welcome to our new lecture on .NET MAUI!"
                    }
                }
            }
        };
    }
}

public static class MauiProgram
{
    //gibt Instanz der MauiApp zurück
    public static MauiApp CreateMauiApp()
    {
        //erstellt Builder-Objekt
        var builder = MauiApp.CreateBuilder();
        //konfiguriert Builder-Objekt: App als Haupteinstiegspunkt zu verwenden
        builder .UseMauiApp<App>();
        return builder.Build();
    }
}
```

![Resulting output](./img/27_Maui/MinimalBeispiel.png "Ausgabe des Minimalbeispiels bei der Anwendung auf Android Traget")

### Wichtigste Klassen und ihr Verhältnis zueinander

Um die Benutzeroberfläche zu erstellen werden in einer Maui-Anwendung die Klassen aus C# und XAML kombiniert. Dabei wird der strukturelle Aufbau und das Design der Benutzeroberfläche in XAML definiert, während die Logik und Funktionalität der Anwendung in C# implementiert wird. Beide Teile arbeiten zusammen, um eine vollständige Anwendung zu erstellen. Es existieren für jede Klasse zwei Dateien, .xaml- Datei und .xaml.cs (C#-Code-Behind)-Datei, die üblicherweise zur Laufzeit (als CIL-Code) zusammengeführt werden. 

https://learn.microsoft.com/de-de/dotnet/maui/xaml/fundamentals/get-started?view=net-maui-8.0

Achtung: Partielle Klassen!

**Application**:
    Die Application-Klasse repräsentiert die gesamte Anwendung. Sie dient als Einstiegspunkt und zentraler Verwalter der App.

Hauptfunktionen:

+ Initialisiert die App und ihre Ressourcen.
+ Verwaltet des Lebenszyklus der App (z.B. Starten, Beenden, Anhalten, Fortsetzen).
+ Definiert globale Ressourcen und Styles, die in der gesamten App verwendet werden können.
+ Gestaltet die Hauptseite der Anwendung, die beim Start angezeigt wird.

Methoden:

+ für die Behandlung von Lebenszyklusereignissen,
+ zum Erstellen neuer Windows für die Anwendung. Die .NET MAUI-Anwendung hat standardmäßig ein einziges Fenster, aber Sie können zusätzliche Fenster erstellen und starten.

```csharp App.xaml.cs
public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = new AppShell();
	}
}
```

```xml App.xaml
<?xml version = "1.0" encoding = "UTF-8" ?>
<Application xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:MAUIErstesBeispiel"
             x:Class="MAUIErstesBeispiel.App">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="Resources/Styles/Colors.xaml" />
                <ResourceDictionary Source="Resources/Styles/Styles.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

**Shell**:
Die Shell-Klasse beschreibt visuelle Hierarchie der App, ist ein Container für die gesamte App-Navigation. Sie bietet eine einheitliche Möglichkeit, die Navigationsstruktur und das Erscheinungsbild der App zu definieren.

Hauptfunktionen:

+ Verwaltung von Routen und Navigation innerhalb der App.
+ Unterstützung von Flyout-Menüs, Tab-Leisten und Seitenhierarchien.
+ Ermöglicht die Konfiguration von URI-basierten Navigationen und Deep Linking.

```csharp AppShell.xaml.cs
public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}
}
```

```xml AppShell.xaml
<?xml version="1.0" encoding="UTF-8" ?>
<Shell
    x:Class="MAUIErstesBeispiel.AppShell"
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:local="clr-namespace:MAUIErstesBeispiel"
    Shell.FlyoutBehavior="Disabled"
    Title="MAUIErstesBeispiel">

    <ShellContent
        Title="Home"
        ContentTemplate="{DataTemplate local:MainPage}"
        Route="MainPage" />

</Shell>
```

**ContentPage**:
Die ContentPage-Klasse ist die grundlegende Seite für die Anzeige von Inhalten in der App. Jede Seite stellt eine einzelne Ansicht dar, die vom Benutzer angezeigt wird.

Hauptfunktionen:

+ Enthält Views und Layouts, die auf der Seite angezeigt werden.
+ Ermöglicht das Festlegen und Verwalten des Seiteninhalts, einschließlich Header, Footer und Hauptinhalt.
+ Unterstützt Lebenszyklusereignisse wie Appearing und Disappearing für die Verwaltung der Seitendarstellung.

```csharp MainPage.xaml.cs
public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			if (count > 10)
				CounterBtn.Text = $"Clicked enough";
			else
				CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}
```

```xml MainPage.xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MAUIErstesBeispiel.MainPage">
    <ScrollView ...>
        <VerticalStackLayout>
            <Image ... />
            <Label ... />
            <Label ... />
            <Button ... />
        </VerticalStackLayout>
    </ScrollView>
</ContentPage>
```

Wenn eine .NET MAUI - Klasse erstellt wird, wird eine LoadFromXaml-Methode indirekt aufgerufen. Dies geschieht, weil die Code-Behind-Datei für eine XAML-Klasse die Methode InitializeComponent von ihrem Konstruktor aus aufruft:

```csharp
private void InitializeComponent() //generiert für MainPage
{
    global::Microsoft.Maui.Controls.Xaml.Extensions.LoadFromXaml(this, typeof(MainPage));
    //...
}
```
Fazit: Mit .NET MAUI kann die Benutzeroberfläche einer App auch dynamisch nur mithilfe von C#-Code erstellt werden. Effizienter ist jedoch die Benutzeroberfläche statisch zur Kompilierzeit mit XAML (Extensible Application Mark-up Language) zu definieren. 
XAML ermöglicht es, den Benutzeroberflächencode von dem Verhaltenscode zu trennen, damit beide sich einfacher verwalten lassen.

### Kompilierungsprozess

+ NET MAUI XAML wird mit dem XAML-Compiler (XAMLC) direkt in Intermediate Language (IL) kompiliert (XAML-Kompilierung). Dieser IL-Code enthält alle notwendigen Instruktionen zur Erstellung der UI-Elemente und deren Eigenschaften.
+ Der IL-Code aus den XAML-Dateien wird mit dem IL-Code aus den Code-Behind-Dateien (.xaml.cs) und dem restlichen C#-Code der Anwendung zusammengeführt.
+ Der gesamte IL-Code wird dann zu einer ausführbaren Datei für die Zielplattform zusammengeführt.

XAML-Kompilierung kann deaktiviert werden, was jedoch nicht empfohlen wird, da XAML dann zur Laufzeit analysiert und interpretiert wird, wodurch die App-Leistung reduziert wird.

https://learn.microsoft.com/de-de/dotnet/maui/xaml/xamlc?view=net-maui-8.0


## Trennung View Modell

### Model View Controller

> Model View Controller (MVC, Modell-Ansicht-Steuerung) ist ein Entwurfsmuster zur Unterteilung einer Software in die drei Komponenten Datenmodell (model), Ansicht (view) und Programmsteuerung (controller) ... Ziel des Musters ist ein flexibler Programmentwurf, der eine spätere Änderung oder Erweiterung erleichtert und eine Wiederverwendbarkeit der einzelnen Komponenten ermöglicht - Wikipedia

Im den Fällen, wenn das dasselbe Modell mit einem anderen Framework oder für ein anderes Betriebssystem visualisiert werden soll, müssen nur Controller und View neu implementiert werden.

+ **Model** enthält Daten
+ **View**  ist für die Darstellung der Daten des Modells und die Realisierung der Benutzerinteraktionen zuständig
+ **Contoller** verwaltet die Ansicht und das Modell, im Allgemeinen wertet er die Benutzerinteraktionen von View und passt das Modell an (oder umgekehrt).

![MVC](https://upload.wikimedia.org/wikipedia/commons/thumb/b/b5/ModelViewControllerDiagram2.svg/220px-ModelViewControllerDiagram2.svg.png)

### Model-View-ViewModel

.NET MAUI verwendet in der Regel das "Model-View-ViewModel" (MVVM)-Entwurfsmuster, eine Variante des MVC, das speziell für die Entwicklung von Benutzeroberflächen vorgesehen ist. MVBM sieht vor eine Trennung zwischen Darstellung und Logik der Benutzerschnittstelle (UI) und des Software-Backends.

https://de.wikipedia.org/wiki/Model_View_ViewModel

**Komponenten**:

**Model**: Die Daten- und Geschäftslogikschicht. 

**View**: Die Darstellungsschicht ist für das Layout und die Anzeige der Daten verantwortlich. In .NET MAUI wird die View häufig durch XAML-Dateien (eXtensible Application Markup Language) beschrieben.

**ViewModel**: Die Vermittlungsschicht zwischen Model und View enthält die Bindungslogik und verbindet die Daten im Model mit der Darstellung in der View. Das ViewModel übernimmt auch die Benutzerinteraktionen und stellt diese Daten für die View bereit.

![MVVM](https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/MVVMPattern.png/440px-MVVMPattern.png)

## XAML

https://learn.microsoft.com/de-de/dotnet/maui/xaml/fundamentals/get-started?view=net-maui-8.0

### Übersicht

https://learn.microsoft.com/de-de/dotnet/maui/xaml/?view=net-maui-8.0

Die eXtensible Application Markup Language (XAML) ist eine XML-basierte Sprache, die statt Instanziieren und Initialisieren von Objekten im Programmcode diese in statischen Hierarchien speziell für die Benutzeroberflächen organisiert.

XAML eignet sich auch gut für die Verwendung mit dem Model-View-ViewModel (MVVM)-Muster für Definition der Ansicht. 

In einer XAML-Datei können komplete Benutzeroberflächen mit allen .NET MAUI-Views, Layouts und Pages sowie benutzerdefinierten Klassen definieren. 

XAML hat gegenüber gleichwertigem Code einige Vorteile:

+ XAML ist häufig prägnant und lesbarer als gleichwertiger Code.
+ Die Verhältnisse von übergeordneten (untergeordneten) Elementen sind klar erkennbar.

Nachteile bzw. Einschränkungen:

+ XAML darf keinen Code enthalten, z.B. keine Kontroll-Strukturen und in der Regel keine Klassen instanziiren, die keinen parameterlosen Konstruktor definieren, und Methoden aufrufen. 
+ Alle Ereignishandler müssen in drr Regel in einer Codedatei definiert werden.

### Syntax

Wichtigste Syntaxelemente:

+ **Elemente** repräsentieren UI-Komponenten wie Pages, Layouts, Buttons, Labels usw.
+ **Attribute** definieren Eigenschaften dieser Elemente (FontSize, TextColor, ...) und sind oft elementspezifisch. Alle Elemente können jedoch das Attribut `x:Name` verwenden, um das Element zu identifizieren und darauf im Code-behind zuzugreifen.

```xml
<ContentPage>
    <!-- UI-Inhalt hier -->
</ContentPage>
```

```xml
<Label x:Name="LabelBesch" Text="Eine Beschriftung" FontSize="20" TextColor="Black" />
```

+ **Ereignisse** verbinden die UI mit der Logik im Code-behind.

```xml
<Button Text="Click Me" Clicked="OnButtonClicked" />
```

+ **xmlns**: deklariert XML-Namespaces, um XAML-Elemente zu identifizieren.
+ **x:Class**: stellt die Verbindung zur entsprechenden Klasse in der Code-behind-Datei her (Namespace.Class)

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="Anwendung1.MainPage">
```


### Plattformunterschiede

.NET MAUI-Apps können die Darstellung der Benutzeroberfläche auf Plattformbasis mithilfe der Klassen `OnPlatform` und `On` anpassen.

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="...">
    <ContentPage.Padding>
        <OnPlatform x:TypeArguments="Thickness" Default="20">
            <On Platform="iOS, Android" Value="10,20,20,10" />
        </OnPlatform>
    </ContentPage.Padding>
    ...
</ContentPage>
```

## Koordinatensystem und Einheiten

+ Der Ursprungspunkt (0,0) befindet sich in der oberen linken Ecke des Containers (z.B. des Bildschirms, eines Fensters oder eines Panels).
+ Die x-Achse verläuft horizontal von links nach rechts. Die y-Achse verläuft vertikal von oben nach unten.
+ Um Elemente präzise zu positionieren, müssen deren x- und y-Koordinaten innerhalb des Containers festgelegt werden (absolute Positionierung). Relative Positionierung bezieht sich auf andere Elemente oder den Container.
+ Die Breite und Höhe von UI-Elementen können ebenfalls mit festen Größen oder relativ zur Größe des Containers oder anderer Elemente definiert werden werden.
+ Um die Konsistenz der Benutzeroberfläche auf verschiedenen Geräten mit unterschiedlichen Pixeldichten sicherzustellen verwendet MAUI standardmäßig *geräteunabhängige Einheiten*, die an die Pixeldichte des Bildschirms angepasst werden (Grid, StackLayout, FlexLayout, FontSize, Padding, Margin).

```xml
<Label Text="Hello, World!"
       FontSize="16" /> <!-- FontSize in device-independent units (like dp) -->
```

+ Für die pixelgenaue Positionierung können in Ausnahmefällen jedoch absolute Einheiten verwendet werden, z.B. in spezifischen Layouts wie AbsoluteLayout.
  
```csharp
// Absolute layout in code behind (px example)
var absoluteLayout = new AbsoluteLayout();
AbsoluteLayout.SetLayoutBounds(label, new Rectangle(0, 0, 200, 50)); // Position and size in pixels
```

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/layouts/absolutelayout?view=net-maui-8.0

Beispiel für besonderes Design mit absoluten Angaben:

```
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="AbsoluteLayoutDemos.Views.XAML.StylishHeaderDemoPage"
             Title="Stylish header demo">
    <AbsoluteLayout Margin="20">
        <BoxView Color="Silver"
                 AbsoluteLayout.LayoutBounds="0, 10, 200, 5" />
        <BoxView Color="Silver"
                 AbsoluteLayout.LayoutBounds="0, 20, 200, 5" />
        <BoxView Color="Silver"
                 AbsoluteLayout.LayoutBounds="10, 0, 5, 65" />
        <BoxView Color="Silver"
                 AbsoluteLayout.LayoutBounds="20, 0, 5, 65" />
        <Label Text="Stylish Header"
               FontSize="24"
               AbsoluteLayout.LayoutBounds="30, 25" />
    </AbsoluteLayout>
</ContentPage>
```

Die Werte "0, 10, 200, 5" geben die Position (x=0, y=10) und die Größe (width=200, height=5) des BoxView-Elements in Pixeln an.

Relative Angaben für Position (Größe): 

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/align-position?view=net-maui-8.0

+ Die Ausrichtung einer View, relativ zu ihrem übergeordneten Element, kann gesteuert werden, indem die Eigenschaft HorizontalOptions oder VerticalOptions der View auf eines der öffentlichen Felder aus der LayoutOptions-Struktur festgelegt wird. Die öffentlichen Felder sind Start, Center, End und Fill.

```xml
<StackLayout>
  ...
  <Label Text="Start" BackgroundColor="Gray" HorizontalOptions="Start" />
  <Label Text="Center" BackgroundColor="Gray" HorizontalOptions="Center" />
  <Label Text="End" BackgroundColor="Gray" HorizontalOptions="End" />
  <Label Text="Fill" BackgroundColor="Gray" HorizontalOptions="Fill" />
</StackLayout>
```

+ Durch die Einstellung von HorizontalOptions und VerticalOptions auf LayoutOptions.Fill ("Fill") kann erreicht werden, dass ein Element übereinstimmend mit dem übergeordneten Element dargestellt wird.

```xml
<StackLayout>
    <BoxView Color="Silver" 
        HorizontalOptions="Fill" 
        VerticalOptions="Fill" />
</StackLayout>
```

## Steuerelemente

https://learn.microsoft.com/de-de/dotnet/communitytoolkit/maui/views/

Die Benutzeroberfläche einer .NET Multi-Platform (.NET MAUI)-App besteht aus Objekten, die den nativen Steuerelementen jeder Zielplattform zugeordnet sind.

Die Hauptsteuerelementgruppen, die zum Erstellen der Benutzeroberfläche einer .NET MAUI-App verwendet werden, sind Seiten, Layouts und Ansichten. Eine .NET MAUI-Seite nimmt im Allgemeinen den gesamten Bildschirm oder das gesamte Fenster ein. Die Seite enthält normalerweise ein Layout, das Ansichten und möglicherweise andere Layouts enthält. Seiten, Layouts und Ansichten leiten sich von der `VisualElement`-Klasse ab. Diese Klasse stellt eine Vielzahl von Eigenschaften, Methoden und Ereignissen bereit, die in abgeleiteten Klassen nützlich sind.

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/?view=net-maui-8.0

### Pages

![ContentPage](https://learn.microsoft.com/de-de/dotnet/maui/user-interface/pages/media/contentpage/pages.png?view=net-maui-8.0)

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/?view=net-maui-8.0

**ContentPage:** zeigt eine einzelne Ansicht an und ist der am häufigsten verwendete Seitentyp. 

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/pages/contentpage?view=net-maui-8.0

**FlyoutPage:**	ist eine Seite, die zwei verwandte Seiten mit Informationen verwaltet – eine Flyout-Seite, die Elemente darstellt, und eine Detailseite, die Informationen zu Elementen auf der Flyout-Seite darstellt. 

**NavigationPage:** stellt eine hierarchische Navigation bereit, bei welcher Sie wie gewünscht in der Vorwärts- und in der Rückwärtsrichtung durch Seiten navigieren können. 

**TabbedPage:** besteht aus einer Reihe von Seiten, die von Registerkarten über den oberen oder unteren Seitenrand navigierbar sind, wobei jede Registerkarte den Seiteninhalt lädt. 

Seitenwechsel:

```csharp
button.Clicked += async (sender, args) =>
{
    await Navigation.PushAsync(new HelloXamlPage());
};
```

![Pages](https://www.plantuml.com/plantuml/png/RP3F2i8m3CRlUOgzmDv0nb3qvf_3y9vkOLJIJjPK4NnugM5gYvUn_BvVFaBR83XBT0neWipOIzKpXb0TpOLwAUdQ-W4Dq_zqo-J82-HMLTVFPkbJHLdPTi2R7TESIKlfsOQ8d4NPWZfqroYpuOGZAKy6TEIkZ4B3bwS_s4nUoCkRAivCsF5ZYb8iwH5Y9ry4-YRxo6BJl4gV1dJEpGwD-Jtcuq1BVeYENm00)

### Layouts

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/layouts/?view=net-maui-8.0

Layoutklassen ermöglichen das Anordnen und Gruppieren von UI-Steuerelementen auf verschedene Art. 

![Layouts](https://learn.microsoft.com/de-de/dotnet/maui/user-interface/layouts/media/layouts.png?view=net-maui-8.0)

+ StackLayout - organisiert Elemente in einem eindimensionalen Stapel, entweder horizontal oder vertikal (auch über Orientation-Eigenschaft)
+ HorizontalStackLayout
+ VerticalStackLayout
+ Grid
+ FlexLayout: kann Elemente sowohl horizontal als auch vertikal in einem flexiblen Raster anordnen (FlexDirection: Row, Column, RowReverse,ColumnReverse). Es bricht die Zeile oder Spalte um, wenn zu viele Elemente vorhanden sind, um in eine einzelne Zeile oder Spalte zu passen.
+ AbsoluteLayout : legt Position und Größe von untergeordneten Elementen mit expliziten Werten fest. Die Position wird durch die obere linke Ecke des untergeordneten Elements relativ zur oberen linken Ecke des AbsoluteLayout in geräteunabhängigen Einheiten angegeben.

<StackLayout Margin="20,20,20,20" Spacing="10"">
    <Button Text="auf die Plätze" />
    <Button Text="fertig" />
    <Button Text="los" />
</StackLayout>

```csharp
using Microsoft.Maui.Controls;

namespace MauiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            var grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(2, GridUnitType.Star) },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                }
            };

            var label1 = new Label
            {
                Text = "Oben links",
                BackgroundColor = Colors.LightBlue
            };

            var label2 = new Label
            {
                Text = "Oben rechts",
                BackgroundColor = Colors.LightCoral
            };

            var label3 = new Label
            {
                Text = "Unten links",
                BackgroundColor = Colors.LightGreen
            };

            var label4 = new Label
            {
                Text = "Unten rechts",
                BackgroundColor = Colors.LightGoldenrodYellow
            };


			grid.Children.Add(label1);
            Grid.SetRow(label1, 0);
            Grid.SetColumn(label1, 0);

            grid.Children.Add(label2);
            Grid.SetRow(label2, 0);
            Grid.SetColumn(label2, 1);

            grid.Children.Add(label3);
            Grid.SetRow(label3, 1);
            Grid.SetColumn(label3, 0);

            grid.Children.Add(label4);
            Grid.SetRow(label4, 1);
            Grid.SetColumn(label4, 1);

            Content = grid;
        }
    }
}
```

Um gewünschte Layout zu erstellen ist es unter Umständen notwendig die Layouts zu verschachteln.

![Layouts](https://www.plantuml.com/plantuml/png/SoWkIImgAStDuN9CAixFAIr9zKcip2yjKL3GrQsnKu1mkDnoKYkmYRiNPQQWYWIN92PdEoKMfw88At1vv1TbLkNdbIJcW5MW60RNmuLFBYorg2Gp6Rz-XzIy5A1V0000)

### Views (Controls, Widgets) 
https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/?view=net-maui-8.0

.NET MAUI-Ansichten sind die UI-Objekte wie Labels, Schaltflächen und Schieberegler, die häufig als Steuerelemente oder Widgets in anderen Umgebungen bezeichnet werden.

Eine Auswahl der Views:

**Label**: einfache Textausgabe

**Button**: Schalter

**Entry**: Texteingabe

**Editor**: ermöglicht die Eingabe und Bearbeitung mehrerer Textzeilen

**CheckBox**: Ankreuzfeld

![CheckBox](https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/media/checkbox/checkbox-databinding.png?view=net-maui-8.0)

RadioButton : Auswahlschalter einer RadioGroup

![RadioButton](https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/media/radiobutton/radiobuttons-default.png?view=net-maui-8.0)

**Bild** 

Die .NET Multi-Platform App UI (.NET MAUI) Image zeigt ein Bild an, das aus einer lokalen Datei, einem URI oder einem Datenstrom geladen werden kann. Die standardmäßigen Plattformbildformate werden unterstützt, einschließlich animierter GIFs, und lokale SVG-Dateien (Scalable Vector Graphics) werden ebenfalls unterstützt. 

Eigenschaften: 

+ Aspect, vom Typ Aspect, definiert den Skalierungsmodus des Bilds.
+ Source, vom Typ ImageSource, gibt die Quelle des Bilds an: FromFile, FromUri, FromStream.


```xml
<Image Source="dotnet_bot.png" />
<Image Source="https://aka.ms/campus.jpg" />
```

```csharp
Image image1 = new Image
{
    Source = ImageSource.FromFile("dotnet_bot.png")
};
// Image imag1e = new Image { Source = "dotnet_bot.png" };

Image image2 = new Image();
image2.Source = new UriImageSource
{
    Uri = new Uri("https://tu-freiberg.de/themes/custom/tubaf_barrio/logo.svg"),
    CacheValidity = new TimeSpan(10,0,0,0)
};

Image image3 = new Image
{
    Source = ImageSource.FromStream(() => stream)
};
```

Web-View

```xml
<WebView HeightRequest="800" WidthRequest="400" Source="https://tu-freiberg.de" />
```

```csharp
WebView webView = new WebView
{
    Source = "https://tu-freiberg.de"
};

//...

webView.Reload();
```


https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/label?view=net-maui-8.0

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/button?view=net-maui-8.0

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/radiobutton?view=net-maui-8.0

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/checkbox?view=net-maui-8.0

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/entry?view=net-maui-8.0

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/editor?view=net-maui-8.0

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/image?view=net-maui-8.0

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/controls/webview?view=net-maui-8.0&pivots=devices-android


![Views](https://www.plantuml.com/plantuml/png/ROv1IiOm48NtEKKlq2j8gNGXY8A8hYV9eD59KfA9jU3XfOxbJyA_w_lUrvUPCaFYuac1pfp0y608SXuUzGSQxkdiWWLDUBrT23q6LgqrZEFFpRadYh1-xG6-tDkH6-aSVN6v03LLyuCuk4dLs6ekp36S3MxgzXtDvno_AtGNriB7LyMMm6cNBFKVj8vZwkazQ2I1_tcFXIlmZ7ubOBINDyHHJs1Th4ziLFMAkZjhmc2Bfry0)

Nicht behandelt weitere GUI-Elemente:
Graphiken, Windows, Dialoge, Toolbars, Menüs

### Handler

https://learn.microsoft.com/de-de/dotnet/maui/user-interface/handlers/?view=net-maui-8.0

.NET Multi-platform App UI (.NET MAUI) bietet eine Sammlung plattformübergreifender Schnittstellendarstellungen, die die Steuerelemente abstrahiert. Plattformübergreifende Steuerelemente, die diese Schnittstellen implementieren, werden als virtuelle Ansichten bezeichnet. Handler ordnen diese virtuellen Ansichten Steuerelementen auf jeder Plattform zu, die als systemeigene Ansichten bezeichnet werden. Handler sind auch für die Instanziierung der zugrunde liegenden nativen Ansicht und das Zuordnen der plattformübergreifenden Steuerelement-API zur nativen Ansichts-API verantwortlich. Beispielsweise ordnet ein Handler unter iOS ein .NET MAUI Button einem iOS UIButtonzu. Unter Android ist die Button Zuordnung zu einem AppCompatButton: 

![ButtonHandler](https://learn.microsoft.com/de-de/dotnet/maui/user-interface/handlers/media/overview/button-handler.png?view=net-maui-8.0)

Auf .NET MAUI-Handler wird über ihre steuerelementspezifische Schnittstelle zugegriffen, z. B. IButton für einen Button.

## Kompletes Beispiel mit verschieden Views

Chaos on the desk:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="Chaos.MainPage">

   <ScrollView>
        <StackLayout Padding="10">            
            <!-- Zu erledigen -->
            <Label Text="Zu erledigen" FontAttributes="Bold" FontSize="Large" />
            <HorizontalStackLayout>
                <CheckBox IsChecked="False" VerticalOptions="Center"/>
                <Label Text="Organisieren die Aufgaben" VerticalOptions="Center"/>
            </HorizontalStackLayout>
            <HorizontalStackLayout>
                <CheckBox IsChecked="False" VerticalOptions="Center"/> 
                <Label Text="Halten den Arbeitsplatz sauber" VerticalOptions="Center"/>
            </HorizontalStackLayout>
            <BoxView HeightRequest="10" /> <!-- Spacer -->
            <!-- Kluge Sprüche -->
            <Label Text="Computer werden kleiner und kleiner, bald verschwinden sie völlig - Ephraim Kishon" 
                   FontAttributes="Bold" FontSize="Small" />
            <BoxView HeightRequest="10" /> <!-- Spacer -->
            <!-- Mäusebilder aus Ressorces/Images-->
            <Label Text="Mäusebilder" FontAttributes="Bold" FontSize="Large" />
            <HorizontalStackLayout>
                <Image Source="maus1.png" Aspect="AspectFit" HeightRequest="150" />
                <Image Source="maus2.jpg" Aspect="AspectFit" HeightRequest="150" />
            </HorizontalStackLayout>
            <BoxView HeightRequest="10" /> <!-- Spacer -->
            <!-- Tagebuch -->
            <Label Text="Tagebuch" FontAttributes="Bold" FontSize="Large" />
            <Editor Placeholder="Tagebucheinträge..." HeightRequest="100" />
            <!-- Kalenderansicht -->
            <Label Text="Kalender" FontAttributes="Bold" FontSize="Large" />
            <DatePicker FontSize="Large" Date="07/01/2024" />
            <BoxView HeightRequest="10" /> <!-- Spacer -->
        </StackLayout>
    </ScrollView>

</ContentPage>
```

# Maui II

## App-Lebenszyklus
https://learn.microsoft.com/de-de/dotnet/maui/fundamentals/app-lifecycle?view=net-maui-8.0


NET MAUI löst plattformübergreifende Lebenszyklusereignisse für die Klasse Window aus:

![Lebenszyklus](https://learn.microsoft.com/de-de/dotnet/maui/fundamentals/media/app-lifecycle/app-lifecycle.png?view=net-maui-8.0)

+ Wenn zum Beispiel eine App zum ersten Mal installiert oder ein Gerät gestartet wird, kann die App als **Not Running** betrachtet werden. 
+ Wenn die App gestartet wird, werden die Ereignisse `Created` und `Activated` ausgelöst und die App wechselt zu "**Running**". 
+ Wenn ein anderes App-Fenster den Fokus erhält, wird das `Deactivated`-Ereignis ausgelöst und die App wechselt zu **Deactivated**. 
+ Wechselt der Benutzer zu einer anderen App oder kehrt zum Home-Bildschirm des Geräts zurück, sodass das App-Fenster nicht mehr sichtbar ist, werden die Ereignisse `Deactivated` und `Stopped` ausgelöst und die App wird gestoppt (**Stopped**). 
+ Wenn der Benutzer zur App zurückkehrt, wird das `Resumed`-Ereignis ausgelöst, und die App weckselt zum Zustand **Running**. 
+ Wennn die App von einem Benutzer beendet wird während sie ausgeführt wird, wird die App erst inaktiv (**Deactived**),  dann gestoppt (**Stopped**) und schlißlich wird das Ereignis `Destroying` ausgelöst und die App beendet. Ebenso kann das Gerät die App beenden, falls sie aufgrund von Ressourcenbeschränkungen bereits gestoppt ist.

### App-Lebenszyklus-Ereignisse

+ Plattformübergreifende Lebenszyklusereignisse sind verschiedenen Plattformereignissen zugeordnet

| Event | Android | Windows |  |
| :---- |  :---- | :---- |:------------------------------------------------ |
| **Created** | OnCreate | Created | das systemeigene Fenster wird erstellt, ist aber möglicherweise noch nicht sichtbar | 	
| **Activated** | OnResume | Activated (CodeActivated und PointerActivated) | das fokussierte Fenster ist oder wird sichtbar | 	
| **Deactivated**| OnPause | Activated (Deactivated) | das Fenster ist nicht mehr das fokussiert, aber möglicherweise weiterhin sichtbar | 	
| **Stopped** | OnStop | VisibilityChanged | das Fenster ist nicht mehr sichtbar | 	
| **Resumed** | OnRestart | Resumed |die App wurde gestoppt und dann wieder fortgesetzt |	
| **Destroying** | OnDestroy | Closed |das systemeigene Fenster wird zerstört| 	

###  App-Lebenszyklus-Methoden

Zusätzlich zu diesen Ereignissen verfügt die Klasse Window über die folgenden überschreibbaren Lebenszyklusmethoden:
    OnCreated,
    OnActivated, 
    OnDeactivated, 
    OnStopped, 
    OnResumed, 
    OnDestroying, 
    OnBackgrounding (wird aufgerufen, wenn das Backgrounding-Ereignis ausgelöst wird).

```csharp
using System.Diagnostics;
namespace MyMauiApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            Window window = base.CreateWindow(activationState);
            window.Created += (s, e) =>
            {
                // Custom code:
                Debug.WriteLine("Window created at: " + DateTime.Now);           //Logging
                if (!Connectivity.NetworkAccess.HasFlag(NetworkAccess.Internet)) //Connectivity
                {
                    MainPage.DisplayAlert("No Internet", "Please check your internet connection.", "OK");
                }
            };

            return window;
        }
    }
}
```

oder 

```csharp
namespace MyMauiApp
{
    public class MyWindow : Window
    {
        public MyWindow() : base()
        {
        }

        public MyWindow(Page page) : base(page)
        {
        }

        protected override void OnCreated()
        {
            // Custom code
        }
    }
}
```

### Page-Lebenszyklus-Ereignisse (Methoden)

Der Lebenszyklus einer Page umfasst die Konstruktion der Seite, das Erscheinen (Appearing), Interaktionen und Updates während der Anzeige, das Verschwinden (Disappearing) und schließlich die Destruktion. 

+ **Appearing**: wird ausgelöst, wenn eine Seite angezeigt wird
+ **Disappearing**: wird ausgelöst, wenn eine Seite ausgeblendet wird.

```csharp
protected override void OnAppearing()
{
    base.OnAppearing();
    // Aktion
}
```

Das Überschreiben von entsprechenden Methoden ermöglicht die spezifische Logik zu verschiedenen Zeitpunkten im Lebenszyklus einer Seite zu implementieren.

## Ereignisbehandlung

Die Eventbehandlung in .NET MAUI umfasst die Reaktion auf verschiedene Ereignisse, die sowohl durch Benutzeraktionen wie Klicks auf Buttons bzw. Texteingaben in Editoren als auch durch Systemereignisse, wie das Laden von Seiten, Netzwerkänderungen, Timerabläufe oder Hardwareereignisse (z.B. das Drehen des Geräts), ausgelöst werden können.

Ereignisse können direkt in XAML oder im Code abonniert werden. 

### Benutzeraktion

Benutzeraktionen umfassen alle Interaktionen (mit Mous, Taste, ..., Touch, Swipe, ... ), die der Benutzer mit der Benutzeroberfläche ausführt, wie z.B. Klicken, Tippen, Streichen, usw. Ein Button-Steuerelement kann beispielsweise auf die Ereignisse Clicked, Pressed und Released reagieren, während ein Entry-Steuerelement über Ereignisse wie TextChanged verfügt. Hier eine Auswahl:

+ Button- und Mauseingaben:

| Event | wird ausgelöst |
| :---- | :------------------------------------------------ |
| **Clicked** | wenn ein Button oder ein anderes klickbares Element angeklickt wird | 
| **Pressed** | wenn ein Element gedrückt wird | 
| **Released** | wenn ein gedrücktes Element losgelassen wird | 

```csharp
Button button = new Button { Text = "Click Me" };
button.Clicked += (sender, args) => { /* Aktion */ };
button.Pressed += (sender, args) => { /* Aktion */ };
button.Released += (sender, args) => { /* Aktion */ };
```

+ Button, Label, Image (und einige andere) unterstützen Tap-Gesten; Image, CollectionView (und andere) die Swipe-Gesten. Diese Controls können durch Gestenerkenner (TapGestureRecognizer, SwipeGestureRecognizer, etc.) erweitert werden.

| Event | wird ausgelöst |
| :---- | :------------------------------------------------ |
| **Tapped** | wenn auf ein Element getippt (doppelt getippt) wird |
| **Swiped** | wenn eine Wischgeste erkannt wird |

```csharp
var tapGestureRecognizer = new TapGestureRecognizer();
//var doubleTapGestureRecognizer = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
tapGestureRecognizer.Tapped += (sender, args) => { /* Aktion */ };
image.GestureRecognizers.Add(tapGestureRecognizer);
```

```csharp
var swipeGestureRecognizer = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
swipeGestureRecognizer.Swiped += (sender, args) => { /* Aktion */ };
image.GestureRecognizers.Add(swipeGestureRecognizer);
```

+ Eingabefelder (Entry)

| Event | wird ausgelöst |
| :---- | :------------------------------------------------ |
| **TextChanged** | wenn der Text in einem Eingabefeld geändert wird |
| **Completed** | wenn die Eingabe in einem Eingabefeld abgeschlossen wird (z.B. durch Drücken der Enter-Taste) |

```csharp
Entry entry = new Entry { Placeholder = "Enter text" };
entry.TextChanged += (sender, args) => { /* Aktion */ };
entry.Completed += (sender, args) => { /* Aktion */ };
```
+ Listen

| Event | wird ausgelöst |
| :---- | :------------------------------------------------ |
| **ItemSelected** | wenn ein Element in einer Liste ausgewählt wird | 
| **ItemTapped** | wenn auf ein Element in einer Liste getippt wird | 


```csharp
ListView listView = new ListView();
listView.ItemSelected += (sender, args) => { /* Aktion */ };
```

+ RadioButton

| Event | wird ausgelöst |
| :---- | :------------------------------------------------ |
| **CheckedChanged** | | 

```xml
<RadioButton Content="Red"
             GroupName="colors"
             CheckedChanged="OnColorsRadioButtonCheckedChanged" />
```

```csharp
void OnColorsRadioButtonCheckedChanged(object sender, CheckedChangedEventArgs e)
{
    // Perform required operation
}
```

### Ereignis registrieren im Code

s. auch Events

**Vorteil**: Events können zur Laufzeit abonniert und abbestellt werden, was eine flexible Reaktion auf dynamische UI-Zustände ermöglicht.

**Ereignismethoden** müssen die folgenden Signaturanforderungen erfüllen:

+ Sie können keinen Wert zurückgeben (void).
+ Sie müssen zwei Parameter übernehmen: ein object-Verweis, der das Objekt angibt, das das Ereignis ausgelöst hat (als Sender bezeichnet), und einen EventArgs-Parameter, der alle Argumente enthält, die vom Sender an den Ereignishandler übergeben werden.
+ Der Ereignishandler sollte private sein. Dies ist zwar nicht zwingend erforderlich, aber wenn Sie einen Ereignishandler als öffentlich definieren, kann von außen auf ihn zugegriffen werden, und er könnte von einer anderen Aktion als dem erwarteten ausgelösten Ereignis aufgerufen werden.
+ Der Ereignishandler kann `async` sein, wenn asynchrone Vorgänge ausgeführt werden müssen.

```csharp
public partial class MainPage : ContentPage, IPage
{
    public MainPage()
    {
        InitializeComponent();
        Counter.Clicked += OnCounterClicked;
    }

    ...

    private void OnCounterClicked(object sender, EventArgs e)
    {
        ...
    }
}
Counter.Clicked -= OnCounterClicked;
```

Neben benannten Methoden können auch anonyme Methoden oder Lambda-Ausdrücke zur Eventbehandlung verwendet werden, was besonders nützlich für kurze und einmalige Event-Handler ist.

**Argumente der Eventhandler für Benutzeraktionen**

- Touch- und Mauseingaben: EventArgs, TappedEventArgs, SwipedEventArgs
- Eingabefelder und Formulare: TextChangedEventArgs, CompletedEventArgs
- Liste und Sammlung: SelectedItemChangedEventArgs, ItemTappedEventArgs

| Klasse | enthaltene Informationen |
| :---- | :------------------------------------------------ |
| **EventArgs** | Allgemeine Basisklasse für Ereignisdaten, enthält keine spezifischen Informationen | 
| **TappedEventArgs** | über das Tippen, wie die Anzahl der Taps und die Position | 
| **SwipedEventArgs** | über eine Wischgeste, wie die Richtung und die Geschwindigkeit der Geste | 
| **TextChangedEventArgs** | Änderungen im Text, einschließlich des neuen und alten Textwertes | 
| **CompletedEventArgs** | Wird verwendet, wenn eine Eingabe in einem Eingabefeld abgeschlossen wird, enthält keine zusätzlichen Informationen | 
| **SelectedItemChangedEventArgs** | über das ausgewählte Element in einer Liste, einschließlich des neuen ausgewählten Elements | 
| **ItemTappedEventArgs** | über ein angetipptes Element in einer Liste, wie das angetippte Element und dessen Position | 


### Ereignis registrieren im XAML-Markup

Eine Ereigniseigenschaft (z.B. Clicked bei Button) kann im XAML-Markup einer Seite mit dem Namen der Methode, die bei Auslösung des Ereignisses ausgeführt werden soll, initialisiert werden. 

```xml
<Button Text="Click Me" Clicked="OnButtonClicked" />
```

Die Event-Handler-Methoden werden im Code-behind definiert. 

```csharp
private void OnButtonClicked(object sender, EventArgs e)
{
    // Reaktion auf Button-Click
}
```

`InitializeComponent`-Methode, die im Konstruktor der Code-behind-Klasse aufgerufen wird, kümmert sich u.a. um die Verknüpfung von Event-Handler mit dem Event.

### Systemereignisse

Systemereignisse werden von der Plattform oder dem System ausgelöst (nicht durch Benutzeraktionen). Hier eine Auswahl:

+ `SizeChanged`, `OrientationChanged` werden ausgelöst, wenn sich die Größe eines Elements bzw. die Ausrichtung des Gerätes ändert.

```csharp
ContentPage page = new ContentPage();
page.SizeChanged += (sender, args) => { /* Aktion */ };
```

```csharp
var currentOrientation = DeviceDisplay.MainDisplayInfo.Orientation;
DeviceDisplay.MainDisplayInfoChanged += (sender, args) =>
{
    var orientation = args.DisplayInfo.Orientation;
    // Aktion
};
```
+ Veränderung des Netzwerkstatus (`ConnectivityChanged`) 

```csharp
Connectivity.ConnectivityChanged += (sender, args) =>
{
    var access = args.NetworkAccess;
    var profiles = args.ConnectionProfiles;
    // Aktion
};
```

**Argumente der Eventhandler für Systemereignisse**

- Gerätezustand und Orientierung: SizeChangedEventArgs, DisplayInfoChangedEventArgs
- Netzwerkstatus: ConnectivityChangedEventArgs
  
| Klasse | Informationen |
| :---- | :------------------------------------------------ |
| **SizeChangedEventArgs** | Größenänderung eines Elements, einschließlich der neuen und alten Größe | 
| **DisplayInfoChangedEventArgs** | Änderungen in der Anzeige, wie neue Anzeigeeigenschaften (z.B. Auflösung, Orientierung) | 
| **ConnectivityChangedEventArgs** | Änderungen im Netzwerkstatus, wie die aktuelle Netzwerkanbindung und verfügbare Verbindungstypen | 

## Multi-Threading

Notwendigket der Thread-Verwendung s. Vorlesung ...

https://learn.microsoft.com/de-de/dotnet/maui/platform-integration/appmodel/main-thread?view=net-maui-8.0

Problem: Die meisten Betriebssysteme verwenden ein für den Code, der die Benutzeroberfläche umfasst ein Thread. Dieser Thread wird als Hauptthread, Benutzeroberflächenthread oder UI-Thread bezeichnet. Die Verwendung dieses Modells ist notwendig, um Ereignisse der Benutzeroberfläche ordnungsgemäß zu serialisieren.  Der Nachteil dieses Modells ist, dass der Code, der auf Elemente der Benutzeroberfläche zugreift, ebenfalls im Hauptthread der Anwendung ausgeführt werden muss.

Ausführen von Code im UI-Thread aus einem sekundären Thread:

```csharp
MainThread.BeginInvokeOnMainThread(() =>
{
    // Code to run on the main thread
});
```

oder mit Abfrage: 

```csharp
void MyMainThreadCode() // method of main thread
{
    // Code to run on the main thread
}

if (MainThread.IsMainThread)
    MyMainThreadCode();

else
    MainThread.BeginInvokeOnMainThread(MyMainThreadCode);
```

Weitere Methoden:  

| Methode 	| Argumente 	| Rückgabe 	| Zweck | 
| :---- | :---- | :---- | :------------------------------------------------ |
| InvokeOnMainThreadAsync<T> 	| Func<T> 	| Task<T> 	| Ruft Func<T> auf dem Hauptthread auf und wartet auf den Abschluss.| 
| InvokeOnMainThreadAsync 	| Action 	| Task 	| Ruft Action auf dem Hauptthread auf und wartet auf den Abschluss.| 
| InvokeOnMainThreadAsync<T> 	| Func<Task<T>> 	| Task<T> 	| Ruft Func<Task<T>> auf dem Hauptthread auf und wartet auf den Abschluss.| 
| InvokeOnMainThreadAsync 	| Func<Task> 	| Task 	| Ruft Func<Task> auf dem Hauptthread auf und wartet auf den Abschluss.| 
| GetMainThreadSynchronizationContextAsync 		| | Task<SynchronizationContext> 	| Gibt SynchronizationContext für den Hauptthread zurück| 

Beispiel:

```csharp
private void OnClicked(object sender, EventArgs e)
{
    Task.Run(async () => 
    {
        Thread.Sleep(2000);
        string ergebnis="404";
        MainThread.BeginInvokeOnMainThread(() =>
        {
            EvaluateLabel.Text=antwort;
        });					
    });
}
```

```xml
<Label
    x:Name="EvaluateLabel"
    Text=" " />
<Button
    x:Name="Btn"
    Text="Click me" 
    Clicked="OnClicked"
    HorizontalOptions="Fill" />
```

## Datenbindung

Eine .NET Multiplattform App UI (.NET MAUI) App besteht aus einer oder mehreren Pages, von denen jede typischerweise mehrere Benutzerschnittstellen-Objekte enthält. Eine der Hauptaufgaben der App besteht darin, diese Views zu synchronisieren. Häufig stehen die Views für Werte einer zugrunde liegenden Datenquelle, und die Benutzer verändern die Views, um die Daten zu verändern. Wenn die View verändert wird, müssen alle zugrunde liegenden Daten und andere View diese Änderung nachvollziehen. Das kann über Eventhandler realisiert werden. 

Die Datenbindung automatisiert diesen Prozess und macht Ereignishandler überflüssig. Datenbindungen können sowohl in XAML als auch in Code implementiert werden, sind aber in XAML weitaus häufiger, da sie dazu beitragen, die Größe der Code-Behind-Datei zu reduzieren. Bindungsaktualisierungen werden von .NET MAUI an den UI-Thread automatisch übermittelt. 

Datenbindung ist also die Verknüpfung von Eigenschaften zweier Objekte. Eines der beiden Objekte ist immer ein von View abgeleitetes Element. Das andere Objekt ist

+ ein weiteres View-Derivat, meist auf der gleichen Seite
+ oder ein Objekt in einer Codedatei.

```csharp
public partial class BasicCodeBindingPage : ContentPage
{
    public BasicCodeBindingPage()
    {
        InitializeComponent();

        label.BindingContext = slider;
        label.SetBinding(Label.RotationProperty, "Value");
    }
}
```

Beispiel (https://learn.microsoft.com/de-de/dotnet/maui/fundamentals/data-binding/basic-bindings?view=net-maui-8.0): 

```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="DataBindingDemos.BasicXamlBindingPage"
             Title="Basic XAML Binding">
    <StackLayout Padding="10, 0">
        <Label Text="TEXT"
               FontSize="80"
               HorizontalOptions="Center"
               VerticalOptions="Center"
               BindingContext="{x:Reference Name=slider}"
               Rotation="{Binding Path=Value}" />

        <Slider x:Name="slider"
                Maximum="360"
                VerticalOptions="Center" />
    </StackLayout>
</ContentPage>
```

In diesem Beispiel ist das Label das Bindungsziel, und der Slider ist die Bindungsquelle. Änderungen der Slider-Quelle wirken sich auf die Drehung des Label-Ziels aus. 

oder 

```csharp
public partial class BasicCodeBindingPage : ContentPage
{
    public BasicCodeBindingPage()
    {
        InitializeComponent();

        label.BindingContext = slider;
        label.SetBinding(Label.RotationProperty, "Value");
    }
}
```
## Beispiel mit Eventhandling

```xml MainPage
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MitInteraktionen.MainPage">

    <ScrollView>
        <VerticalStackLayout
            Padding="30,0"
            Spacing="5">
        <Button Text="Zu erledigen" Clicked="OnZuErledigenClicked" />
        <Button Text="Notiz hinzufügen" Clicked="OnNotizHinzufuegenClicked" />
        <Button Text="Termin eintragen" Clicked="OnTerminEintragenClicked" />
        </VerticalStackLayout>
    </ScrollView>
</ContentPage>
```

```csharp MainPage
public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    private async void OnZuErledigenClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ZuErledigenPage());
    }

    private async void OnNotizHinzufuegenClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NotizHinzufuegenPage());
    }

    private async void OnTerminEintragenClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TerminEintragenPage());
    }
}
```

```xml ZuErledigenPage
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MitInteraktionen.ZuErledigenPage">

    <StackLayout Padding="10">
        <StackLayout Orientation="Horizontal">
            <CheckBox IsChecked="False" VerticalOptions="Center" />
            <Label Text="Zimmer aufräumen" VerticalOptions="Center" />
        </StackLayout>
        <StackLayout Orientation="Horizontal">
            <CheckBox IsChecked="False" VerticalOptions="Center" />
            <Label Text="Kuchen backen" VerticalOptions="Center" />
        </StackLayout>
    </StackLayout>
</ContentPage>
```

```csharp ZuErledigenPage
public partial class ZuErledigenPage : ContentPage
{
	public ZuErledigenPage()
	{
		InitializeComponent();
	}
}
```

```xml NotizHinzufuegenPage
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MitInteraktionen.NotizHinzufuegenPage">

    <StackLayout Padding="10">
        <Editor x:Name="NotizEditor" Placeholder="Schreibe deine Notiz hier..." HeightRequest="200" />
        <Button Text="Speichern" Clicked="OnSpeichernClicked" />
    </StackLayout>
</ContentPage>
```

```xml NotizHinzufuegenPage
public partial class NotizHinzufuegenPage : ContentPage
{
    public NotizHinzufuegenPage()
    {
        InitializeComponent();
    }

    private async void OnSpeichernClicked(object sender, EventArgs e)
    {
        DateTime thisDay = DateTime.Today;
        string not=thisDay.ToString() +"\n"+ NotizEditor.Text;
        using (StreamWriter outputFile = new StreamWriter("Notizen.txt"))
        {
            await outputFile.WriteAsync(not);
        }
        await DisplayAlert("Gespeichert", "Deine Notiz wurde gespeichert.", "OK");
    }
}
```

```xml TerminEintragenPage
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MitInteraktionen.TerminEintragenPage">

    <StackLayout Padding="10">
        <Label Text="Kalender" FontAttributes="Bold" FontSize="Large" />
        <DatePicker x:Name="SelectDate" FontSize="Large"/>
        <Button Text="Speichern" Clicked="OnSpeichernClicked" />
    </StackLayout>
</ContentPage>
```

```xml TerminEintragenPage
public partial class TerminEintragenPage : ContentPage
{
    public TerminEintragenPage()
    {
        InitializeComponent();
    }

    private void OnSpeichernClicked(object sender, EventArgs e)
    {
        string dat=SelectDate.Date.ToString();
        DisplayAlert("Gespeichert", $"Dein Termin {dat} wurde gespeichert.", "OK");
    }
}
```


## Platformspezifische Verzeichnisse und Dateien

### Android-Manifest

+ Platforms\Android\AndroidManifest.xml 
+ wird als Teil des .NET MAUI-Build-Prozesses auf Android generiert 
+ beinhaltet Eigenschaften der gesamten App, Berechtigungen, Komponenten (wie Activity)
  
```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android">
	<application android:allowBackup="true" android:icon="@mipmap/appicon" android:roundIcon="@mipmap/appicon_round" android:supportsRtl="true"></application>
	<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
	<uses-permission android:name="android.permission.INTERNET" />
    <activity android:label="My Maui App"
          android:name="crc64bdb9c38958c20c7c.MainActivity">
        <intent-filter>
                <action android:name="android.intent.action.MAIN" />
                <category android:name="android.intent.category.LAUNCHER" />
        </intent-filter>
</activity>
</manifest>
```

### Platformspezifische Einstellungen im  Quellcode

```csharp
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif
public partial class App : Application
{
    const int WindowWidth = 600;
    const int WindowHeight = 800;
	public App()
	{
		InitializeComponent();
        Microsoft.Maui.Handlers.WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            #if WINDOWS
            var mauiWindow = handler.VirtualView;
            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();
            IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
            WindowId windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(windowHandle);
            AppWindow appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new SizeInt32(WindowWidth, WindowHeight));
            #endif
        });

		MainPage = new AppShell();
	}
}
//using Microsoft.Extensions.Logging;
```

# Praktische Hinweise

+ Installation: Maui-Paket, Extensions, Android

https://learn.microsoft.com/de-de/dotnet/maui/get-started/installation?view=net-maui-8.0&tabs=visual-studio-code

+ Erstellen und Ausführen von Projekt

https://learn.microsoft.com/de-de/dotnet/maui/get-started/first-app?pivots=devices-android&view=net-maui-8.0&tabs=visual-studio-code

https://learn.microsoft.com/de-de/dotnet/maui/get-started/first-app?pivots=devices-windows&view=net-maui-8.0&tabs=visual-studio-code