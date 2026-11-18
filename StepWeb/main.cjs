'use strict';

const { app, BrowserWindow } = require('electron');
const path = require('node:path');

function createWindow() {
  const iconPath = app.isPackaged
    ? path.join(process.resourcesPath, 'StepIcon.ico')
    : path.join(__dirname, '..', 'Step', 'Images', 'StepIcon.ico');

  const window = new BrowserWindow({
    width: 1440,
    height: 960,
    minWidth: 360,
    minHeight: 540,
    title: 'Step — Activity tracker',
    icon: iconPath,
    backgroundColor: '#f6f7fb',
    webPreferences: {
      nodeIntegration: false,
      contextIsolation: true,
      sandbox: true
    }
  });

  window.loadFile(path.join(__dirname, 'index.html'));
}

app.whenReady().then(() => {
  createWindow();
  app.on('activate', () => {
    if (BrowserWindow.getAllWindows().length === 0) createWindow();
  });
});

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') app.quit();
});
