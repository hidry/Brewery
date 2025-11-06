# GitHub Actions & Pages Setup

Dieses Repository enthält zwei GitHub Actions Workflows für die Angular-Anwendung:

## 📋 Workflows

### 1. CI Workflow (`ci.yml`)
**Zweck:** Automatische Build- und Test-Prüfung bei Pull Requests

**Trigger:**
- Pull Requests auf `main`, `master`, `dev-service`
- Pushes auf `main`, `master`, `dev-service`

**Was wird gemacht:**
- ✅ Code auschecken
- ✅ Node.js 20.x installieren
- ✅ Dependencies installieren
- ✅ Linting durchführen
- ✅ Production Build erstellen
- ✅ Tests ausführen (Headless Chrome)
- ✅ Build-Artefakte hochladen

### 2. GitHub Pages Deployment (`deploy-pages.yml`)
**Zweck:** Automatisches Deployment auf GitHub Pages

**Trigger:**
- Push auf `main` oder `master` Branch
- Manueller Trigger über GitHub UI

**Was wird gemacht:**
- ✅ Production Build mit GitHub Pages Base-Href
- ✅ Automatisches Deployment auf GitHub Pages
- ✅ URL: `https://<username>.github.io/Brewery/`

## 🚀 GitHub Pages Aktivierung

Um GitHub Pages für dieses Repository zu aktivieren:

1. **Gehe zu Repository Settings:**
   - Öffne dein Repository auf GitHub
   - Klicke auf "Settings"

2. **Pages konfigurieren:**
   - Navigiere zu "Pages" im linken Menü
   - Unter "Build and deployment"
   - Source: Wähle **"GitHub Actions"**

3. **Workflow ausführen:**
   - Push auf `main` oder `master` Branch
   - Oder manuell: Actions → Deploy to GitHub Pages → Run workflow

4. **App aufrufen:**
   - Nach erfolgreichem Deployment unter:
   - `https://<dein-username>.github.io/Brewery/`

## 🔧 Lokale Entwicklung

```bash
cd WebApp
npm install --legacy-peer-deps
npm start
```

Die App läuft dann auf `http://localhost:4200/`

## 📝 Hinweise

- **Legacy Peer Deps:** Die App verwendet `--legacy-peer-deps` aufgrund von ag-grid Abhängigkeiten
- **Base Href:** Für GitHub Pages wird automatisch `/Brewery/` als Base-Href gesetzt
- **Node Version:** Die Workflows verwenden Node.js 20.x
- **Browser Tests:** Tests laufen im headless Chrome-Modus

## ⚙️ Workflow-Status

Die Workflow-Status-Badges können zur README hinzugefügt werden:

```markdown
![CI Status](https://github.com/<username>/Brewery/workflows/CI%20-%20Build%20and%20Test/badge.svg)
![Deploy Status](https://github.com/<username>/Brewery/workflows/Deploy%20to%20GitHub%20Pages/badge.svg)
```

## 🛠️ Anpassungen

Falls ein anderer Base-Path benötigt wird, ändere in `deploy-pages.yml`:

```yaml
run: npm run build -- --configuration production --base-href=/DEIN-PATH/
```
