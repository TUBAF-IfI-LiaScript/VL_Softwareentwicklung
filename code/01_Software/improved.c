#include <stdio.h>
#include <stdlib.h>

/**
 * @brief BubbleSort-Algorithmus als eigene Funktion (Prozedur) -> Wartbarkeit
 * Ausnutzen von Seiteneffekten (Zeigeruebergabe -> Call by Reference) zum Sortieren des originalen Arrays,
 * kein Anfertigen einer Array-Kopie notwendig -> Effizienz
 * @param arr zu sortierendes Integer-Array
 * @param n   Laenge des Arrays (Anz. Elemente im Array)
 */
void bubbleSort(int arr[], int n) {
    int i, j, temp;
    
    // auessere Schleife -> bestimmt, wie oft das Array durchlaufen wird
    for (i = 0; i < n - 1; i++) {
        // innere Schleife zum Vergleich benachbarter Elemente (j und j + 1)
        /* 'n - i - 1' sorgt dafuer, dass bereits sortierte Elemente 
            am Ende des Arrays nicht erneut geprueft werden. -> Effizienz */
        for (j = 0; j < n - i - 1; j++) {
            
            if (arr[j] > arr[j + 1]) {
                // Tauschvorgang mit temp-Variable
                temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
        }
    }
}

int main() {
    char pfad[256];
    int *daten = NULL;
    int kapazitaet = 10;
    int anzahl = 0;
    int wert;
    FILE *datei = NULL;

    // flexibler Pfad statt hart gecodeter Standardpfad zur Standard-Datei
    // Fehlerbehandlung bei falscher Eingabe
    while (datei == NULL) {
        printf("Bitte den Pfad zur Datei eingeben (ohne Leerzeichen): ");
        if (scanf("%255s", pfad) != 1) return 1;

        // Oeffnen der Datei inkl. Fehlerbehandlung
        datei = fopen(pfad, "r");
        if (datei == NULL) {
            printf("Datei nicht gefunden! Bitte erneut versuchen.\n");
        }
    }

    // Allokieren dynamischen Speichers, damit Arraygroesse erst zur Laufzeit festgelegt wird 
    daten = malloc(kapazitaet * sizeof(int));
    if (daten == NULL) {
        fclose(datei);
        return 1;
    }

    // Einlesen der Daten
    while (fscanf(datei, "%d", &wert) == 1) {
        if (anzahl >= kapazitaet) {
            kapazitaet *= 2;
            // realloc fuer dynamische Speicherallokierung zur Laufzeit, ebenso Behandlung 
            // von Platzmangel an zusammenhaengendem Speicherbereich auf dem Heap
            int *optimiert = realloc(daten, kapazitaet * sizeof(int));
            
            if (optimiert == NULL) {
                free(daten);
                fclose(datei);
                return 1;
            }
            daten = optimiert;
        }
        daten[anzahl++] = wert;
    }

    // Datei am Ende schliessen
    fclose(datei);

    // Aufruf der Bubblesort-Funktion
    if (anzahl > 1) {
        bubbleSort(daten, anzahl);
    }

    // Ausgabe der sortierten Liste
    printf("\nSortierte Ergebnisse:\n");
    for (int i = 0; i < anzahl; i++) {
        printf("%d ", daten[i]);
    }
    printf("\nFertig! (%d Eintraege verarbeitet)\n", anzahl);

    // Freigeben des vorher genutzten Heapspeichers
    free(daten);
    // Programm erfolgreich beendet
    return 0;
}