# GitHub Actions und CI/CD Setup

Dieses Dokument beschreibt die GitHub Actions Workflows für den Brewery Server und wie du automatisches Deployment einrichten kannst.

## Übersicht der Workflows

### 1. Server CI - Build and Test (`server-ci.yml`)
**Wann läuft es:** Bei jedem Pull Request oder Push zu `main`, `master`, `dev-service`

**Was macht es:**
- Checkt den Code aus
- Installiert .NET 8
- Restored Dependencies
- Baut das Projekt
- Führt Tests aus (falls vorhanden)
- Erstellt ein Publish-Artefakt
- Lädt das Artefakt hoch (7 Tage verfügbar)

**Status:** ✅ Automatisch aktiv

### 2. Server Docker - Build and Push (`server-docker.yml`)
**Wann läuft es:**
- Bei Push zu `main`/`master`
- Bei Git Tags (z.B. `v1.0.0`)
- Manuell über "Run workflow"

**Was macht es:**
- Baut Docker Images für:
  - `linux/amd64` (normale PCs)
  - `linux/arm64` (Raspberry Pi 4/5, 64-bit)
  - `linux/arm/v7` (Raspberry Pi 3, 32-bit)
- Pusht Images zur GitHub Container Registry
- Tagged Images automatisch

**Image Location:** `ghcr.io/hidry/brewery/brewery-server:latest`

**Status:** ✅ Automatisch aktiv (benötigt Package-Permissions)

### 3. Server Deploy - Raspberry Pi (`server-deploy.yml`)
**Wann läuft es:**
- Manuell über "Run workflow"
- Bei Push mit `[deploy]` im Commit-Message

**Deployment-Methoden:**

#### Option A: Docker Deployment (Standard)
Deployed via SSH zu deinem Raspberry Pi

#### Option B: Systemd Deployment
Benötigt self-hosted GitHub Runner

**Status:** ⚠️ Benötigt Konfiguration (siehe unten)

## Setup-Anleitung

### Schritt 1: GitHub Container Registry aktivieren

1. Gehe zu deinem Repository → **Settings** → **Actions** → **General**
2. Unter "Workflow permissions":
   - ✅ Read and write permissions
   - ✅ Allow GitHub Actions to create and approve pull requests
3. Speichern

### Schritt 2: Docker Images nutzen

Nach dem ersten Push wird automatisch ein Docker Image gebaut.

**Image herunterladen:**
```bash
# Einmalig: Login (falls Repository privat)
echo $GITHUB_TOKEN | docker login ghcr.io -u USERNAME --password-stdin

# Image pullen
docker pull ghcr.io/hidry/brewery/brewery-server:latest
```

**Auf Raspberry Pi starten:**
```bash
cd /opt/brewery
docker-compose up -d
```

### Schritt 3: Automatisches Deployment einrichten

#### Option A: SSH Deployment (empfohlen für Anfänger)

1. **SSH-Schlüssel generieren** (auf deinem PC):
```bash
ssh-keygen -t ed25519 -f ~/.ssh/brewery_deploy -N ""
```

2. **Public Key auf Raspberry Pi kopieren**:
```bash
ssh-copy-id -i ~/.ssh/brewery_deploy.pub pi@raspberrypi.local
```

3. **GitHub Secrets einrichten**:
   - Gehe zu: Repository → **Settings** → **Secrets and variables** → **Actions**
   - Klicke **New repository secret** und füge hinzu:
     - `RPI_HOST`: `raspberrypi.local` (oder IP-Adresse)
     - `RPI_USERNAME`: `pi`
     - `RPI_SSH_KEY`: Inhalt von `~/.ssh/brewery_deploy` (Private Key!)
     - `RPI_SSH_PORT`: `22` (optional, nur wenn geändert)

4. **Docker Compose auf Raspberry Pi vorbereiten**:
```bash
ssh pi@raspberrypi.local
cd /opt/brewery
cp docker-compose.yml /opt/brewery/
cp .env.example /opt/brewery/.env
# Bearbeite .env mit deinen Einstellungen
nano .env
```

5. **Deployment testen**:
   - Gehe zu: **Actions** → **Server Deploy - Raspberry Pi** → **Run workflow**

#### Option B: Self-Hosted Runner (für fortgeschrittene Nutzer)

**Vorteile:**
- Schnellere Deployments
- Kein SSH nötig
- Direkter Zugriff auf Hardware

**Setup:**

1. **Auf Raspberry Pi**:
```bash
# Runner herunterladen
mkdir ~/actions-runner && cd ~/actions-runner
curl -o actions-runner-linux-arm64-2.311.0.tar.gz -L \
  https://github.com/actions/runner/releases/download/v2.311.0/actions-runner-linux-arm64-2.311.0.tar.gz
tar xzf ./actions-runner-linux-arm64-2.311.0.tar.gz

# Runner konfigurieren
./config.sh --url https://github.com/hidry/Brewery --token YOUR_TOKEN

# Als Service installieren
sudo ./svc.sh install
sudo ./svc.sh start
```

2. **Token holen**:
   - Gehe zu: Repository → **Settings** → **Actions** → **Runners** → **New self-hosted runner**
   - Kopiere das Token

3. **Workflow aktivieren**:
Bearbeite `.github/workflows/server-deploy.yml`:
```yaml
if: false  # ← Ändere zu: if: true
```

## Verwendung

### Automatisches Deployment

**Nach jedem Push zu main:**
```bash
git commit -m "Update server [deploy]"
git push
```

Das `[deploy]` Keyword triggert automatisches Deployment.

### Manuelles Deployment

1. Gehe zu: **Actions**
2. Wähle: **Server Deploy - Raspberry Pi**
3. Klicke: **Run workflow**
4. Wähle Branch und Environment
5. Klicke: **Run workflow**

### Docker Image Updates

Docker Images werden automatisch gebaut bei:
- Push zu `main`/`master`
- Git Tags: `git tag v1.0.0 && git push --tags`

**Auf Raspberry Pi updaten:**
```bash
cd /opt/brewery
docker-compose pull
docker-compose up -d --force-recreate
```

**Mit Watchtower (automatisch):**
```bash
# docker-compose.yml anpassen:
# Entferne "profiles: - auto-update" bei watchtower
docker-compose up -d
```

## Monitoring und Logs

### Workflow-Logs ansehen
1. Gehe zu: **Actions**
2. Klicke auf einen Workflow-Run
3. Klicke auf einen Job (z.B. "build-and-test")

### Server-Logs auf Raspberry Pi

**Docker:**
```bash
docker logs -f brewery-server
```

**Systemd:**
```bash
sudo journalctl -u brewery.service -f
```

## Troubleshooting

### Problem: Docker Build schlägt fehl

**Lösung:**
- Prüfe, ob .NET 8 SDK installiert ist
- Schaue in die Workflow-Logs
- Stelle sicher, dass alle Projekte erfolgreich builden

### Problem: Docker Push schlägt fehl

**Fehler:** `denied: permission_denied`

**Lösung:**
1. Settings → Actions → General
2. "Read and write permissions" aktivieren

### Problem: SSH Deployment funktioniert nicht

**Lösung:**
```bash
# Teste SSH-Verbindung manuell
ssh -i ~/.ssh/brewery_deploy pi@raspberrypi.local

# Prüfe, ob docker-compose installiert ist
docker-compose --version
```

### Problem: Container startet nicht auf Raspberry Pi

**Lösung:**
```bash
# Prüfe Container-Logs
docker logs brewery-server

# Prüfe GPIO-Zugriff
ls -la /dev/gpiochip*

# Starte mit debug
docker run -it --rm --privileged \
  ghcr.io/hidry/brewery/brewery-server:latest /bin/bash
```

### Problem: Self-hosted Runner offline

**Lösung:**
```bash
# Status prüfen
sudo ./svc.sh status

# Neu starten
sudo ./svc.sh stop
sudo ./svc.sh start

# Logs ansehen
journalctl -u actions.runner.* -f
```

## Best Practices

### 1. Versionen taggen
```bash
git tag -a v1.0.0 -m "Release 1.0.0"
git push origin v1.0.0
```

### 2. Branch-Strategie
- `main`: Produktions-Code (auto-deploy)
- `dev-service`: Development (nur build/test)
- Feature-Branches: Pull Requests → `dev-service`

### 3. Secrets rotieren
Ändere SSH-Keys regelmäßig:
```bash
ssh-keygen -t ed25519 -f ~/.ssh/brewery_deploy_new -N ""
# Aktualisiere GitHub Secret
# Teste Deployment
# Lösche alten Key
```

### 4. Backups vor Deployment
Automatisch im Workflow enthalten:
```bash
/opt/brewery.backup.20241106_150000
```

### 5. Health Checks überwachen
```bash
# Regelmäßig Status prüfen
curl http://raspberrypi.local:8800/api/status/serverStatus
```

## Erweiterte Konfiguration

### Multi-Environment Setup

Erstelle verschiedene Environments in GitHub:
1. Settings → Environments → New environment
2. Erstelle: `production`, `staging`
3. Füge Environment-spezifische Secrets hinzu

### Slack/Discord Notifications

Füge am Ende jedes Workflows hinzu:
```yaml
- name: Notify on success
  if: success()
  uses: slackapi/slack-github-action@v1
  with:
    webhook-url: ${{ secrets.SLACK_WEBHOOK }}
    payload: |
      {
        "text": "✅ Deployment successful!"
      }
```

### Rollback-Strategie

Bei Problemen nach Deployment:
```bash
# Docker: Zurück zu vorherigem Image
docker-compose down
docker pull ghcr.io/hidry/brewery/brewery-server:sha-abc123
docker-compose up -d

# Systemd: Backup wiederherstellen
sudo systemctl stop brewery.service
sudo rm -rf /opt/brewery
sudo mv /opt/brewery.backup.20241106_150000 /opt/brewery
sudo systemctl start brewery.service
```

## Kosten

Alle verwendeten GitHub Actions Features sind **kostenlos** für:
- ✅ Public Repositories
- ✅ Private Repositories (2000 Minuten/Monat)
- ✅ GitHub Container Registry (500 MB kostenlos)

Self-hosted Runner haben **keine** Minutenlimits.

## Hilfreiche Links

- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [Self-hosted Runners](https://docs.github.com/en/actions/hosting-your-own-runners)
- [Container Registry](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-container-registry)
- [Docker Compose Documentation](https://docs.docker.com/compose/)

## Support

Bei Fragen oder Problemen:
1. Prüfe die Workflow-Logs in GitHub Actions
2. Schaue in die Docker/systemd Logs
3. Erstelle ein GitHub Issue mit:
   - Fehlermeldung
   - Workflow-Log (falls relevant)
   - Server-Log (falls relevant)
