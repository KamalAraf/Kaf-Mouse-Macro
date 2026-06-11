# Kaf Mouse Macro

Applicazione Windows per l'automazione di sequenze di clic del mouse. Consente di registrare una serie di posizioni sullo schermo e riprodurle in ciclo continuo con un ritardo configurabile, il tutto controllabile tramite hotkey globali.

## Funzionalita

- **Registrazione punti**: premi `Z` per catturare la posizione corrente del mouse e aggiungerla alla lista.
- **Esecuzione ciclo**: premi `X` per avviare la macro. Il mouse si spostera su ogni punto registrato, eseguira un clic sinistro, e attendera il delay impostato prima di passare al successivo. Al termine della lista, il ciclo ricomincia dal primo punto.
- **Arresto**: premi `Q` per fermare immediatamente la macro.
- **Delay configurabile**: imposta il ritardo (in millisecondi) tra un clic e il successivo tramite interfaccia.
- **Persistenza**: salva e carica le liste di posizioni su file JSON tramite finestra di dialogo.
- **Hotkey globali**: i tasti di controllo funzionano anche quando la finestra non e in primo piano.

## Requisiti di sistema

- Windows 10 o Windows 11 (x64)
- [.NET 10 Runtime](https://dotnet.microsoft.com/download/dotnet) (se non si utilizza la build standalone)
- Oppure nessuna dipendenza esterna se si utilizza la build self-contained

## Utilizzo

1. Avviare l'applicazione.
2. Aggiungere posizioni:
   - Portare il mouse sul punto desiderato.
   - Premere `Z` (finche la macro non e in esecuzione). La posizione apparira nella lista.
   - Oppure cliccare il pulsante "Aggiungi posizione" nell'interfaccia.
3. Impostare il delay in millisecondi campo "Delay (ms)".
4. Per avviare il ciclo, premere `X` o cliccare "Avvia macro". La macro scorrera ininterrottamente tutte le posizioni.
5. Per fermare la macro, premere `Q` o cliccare "Ferma macro".
6. Rimuovere punti selezionati con "Rimuovi selezionato" o pulire la lista con "Pulisci tutto".
7. Usare "Salva" e "Carica" per gestire file JSON di posizioni.

## Hotkey di controllo

| Tasto | Azione                     | Condizione        |
|-------|----------------------------|-------------------|
| `Z`   | Aggiunge posizione corrente| Macro non attiva  |
| `X`   | Avvia la macro             | Macro non attiva  |
| `Q`   | Ferma la macro             | Macro attiva      |

## Build dal codice sorgente

### Prerequisiti

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Compilazione

```powershell
# Build standard
dotnet build -c Release

# Build self-contained (singolo .exe senza dipendenze runtime)
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# Build self-contained con compressione
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true
```

Il binario si trovera in `bin\Release\net10.0-windows\win-x64\publish\`.

## Licenza

Distribuito sotto licenza MIT. Vedi il file `LICENSE` per maggiori informazioni.
