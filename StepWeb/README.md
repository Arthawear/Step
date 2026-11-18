# Step Web

A browser-based version of the original Step activity timer. The original WPF/.NET project remains unchanged in the `Step` folder.

## Build the Windows desktop app

Install Node.js, then run `npm install` from the `StepWeb` folder. Use `npm start` to test the Electron desktop window. Run `npm run build:win` to create a Windows installer and a portable `.exe`; both are written to `StepWeb/release`.

The desktop app stores names and event timestamps in Electron's local app storage on that computer. It does not import the browser's existing local-storage data automatically; export that data as CSV in the browser first if you need to keep a copy.

## Run

Open `index.html` in a modern browser for a quick start. For local HTTP hosting, run `node StepWeb/serve.js` from the repository root and visit `http://127.0.0.1:8000`. The app has no package installation or build step; the small server uses Node.js built-ins only. Stop the server with Ctrl+C.

## Free web hosting

The repository includes a GitHub Actions workflow that publishes this folder to GitHub Pages whenever changes to `StepWeb` are pushed to the `dev` branch. Enable Pages for the repository with **Settings → Pages → Build and deployment → Source: GitHub Actions**. The first deployment then publishes the site at `https://arthawear.github.io/Step/`.

## Features

- 36 activity cards, initially named `Name`, like the desktop version.
- Rename activities; names and timestamp events are saved in this browser's local storage.
- Start and end actions record local timestamps and show which activities are currently active.
- Review an activity's event history.
- Export all events or one activity's events as CSV, which can be opened in Excel.
- Search activities and reset local data.

## Differences from the desktop app

Browsers cannot automate desktop Excel or write directly to `C:\Temp`. Instead, event data is stored in browser local storage and can be downloaded as CSV. Data is specific to the browser profile and device; use CSV exports as backups. The old **COMPRESS** action was tied to a hardcoded personal folder and is not included.
