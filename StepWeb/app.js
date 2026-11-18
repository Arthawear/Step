'use strict';

const STORAGE_KEY = 'step-web-v1';
const ACTIVITY_COUNT = 36;
const grid = document.getElementById('activity-grid');
const searchInput = document.getElementById('search-input');
const editDialog = document.getElementById('edit-dialog');
const historyDialog = document.getElementById('history-dialog');
const editForm = document.getElementById('edit-form');
const nameInput = document.getElementById('activity-name');
const historyTitle = document.getElementById('history-title');
const historyContent = document.getElementById('history-content');
const toast = document.getElementById('toast');
let activities = loadActivities();
let editingId = null;
let historyId = null;
let toastTimer;

function defaultActivities() {
  return Array.from({ length: ACTIVITY_COUNT }, (_, index) => ({
    id: index + 1,
    name: 'Name',
    events: []
  }));
}

function loadActivities() {
  try {
    const saved = JSON.parse(localStorage.getItem(STORAGE_KEY));
    if (!Array.isArray(saved) || saved.length !== ACTIVITY_COUNT) return defaultActivities();
    return defaultActivities().map((fallback, index) => {
      const item = saved[index];
      if (!item || !Array.isArray(item.events)) return fallback;
      return {
        id: fallback.id,
        name: typeof item.name === 'string' && item.name.trim() ? item.name : 'Name',
        events: item.events.filter(event => event && ['start', 'end'].includes(event.type) && typeof event.at === 'string')
      };
    });
  } catch {
    return defaultActivities();
  }
}

function saveActivities() {
  localStorage.setItem(STORAGE_KEY, JSON.stringify(activities));
  render();
}

function isRunning(activity) {
  return activity.events.at(-1)?.type === 'start';
}

function formatTime(value) {
  return new Intl.DateTimeFormat(undefined, { hour: 'numeric', minute: '2-digit' }).format(new Date(value));
}

function formatDateTime(value) {
  return new Intl.DateTimeFormat(undefined, {
    month: 'short', day: 'numeric', year: 'numeric', hour: 'numeric', minute: '2-digit'
  }).format(new Date(value));
}

function render() {
  const query = searchInput.value.trim().toLocaleLowerCase();
  const visible = activities.filter(activity => activity.name.toLocaleLowerCase().includes(query));
  grid.replaceChildren(...visible.map(createActivityCard));
  document.getElementById('empty-message').hidden = visible.length !== 0;
  document.getElementById('active-count').textContent = activities.filter(isRunning).length;

  const today = new Date().toDateString();
  const startsToday = activities.reduce((sum, activity) => sum + activity.events.filter(event =>
    event.type === 'start' && new Date(event.at).toDateString() === today
  ).length, 0);
  document.getElementById('session-count').textContent = startsToday;
}

function createActivityCard(activity) {
  const running = isRunning(activity);
  const card = document.createElement('article');
  card.className = `activity-card${running ? ' is-running' : ''}`;
  card.dataset.id = activity.id;

  const top = document.createElement('div');
  top.className = 'activity-top';
  const name = document.createElement('h3');
  name.className = 'activity-name';
  name.title = activity.name;
  name.textContent = activity.name;
  const rename = document.createElement('button');
  rename.className = 'icon-button';
  rename.type = 'button';
  rename.dataset.action = 'rename';
  rename.setAttribute('aria-label', `Rename ${activity.name}`);
  rename.textContent = '✎';
  top.append(name, rename);

  const status = document.createElement('div');
  status.className = 'activity-status';
  const dot = document.createElement('span');
  dot.className = 'status-dot';
  const statusText = document.createElement('span');
  statusText.textContent = running ? 'In progress' : 'Ready to start';
  status.append(dot, statusText);

  const lastTime = document.createElement('div');
  lastTime.className = 'last-time';
  const lastEvent = activity.events.at(-1);
  lastTime.textContent = lastEvent ? `${lastEvent.type === 'start' ? 'Started' : 'Ended'} ${formatTime(lastEvent.at)}` : 'No activity yet';

  const actions = document.createElement('div');
  actions.className = 'card-actions';
  const start = createAction('start-action', '▶', 'Start', 'start', running);
  const end = createAction('end-action', '■', 'End', 'end', !running);
  actions.append(start, end);

  const history = document.createElement('button');
  history.type = 'button';
  history.className = 'view-log';
  history.dataset.action = 'history';
  history.textContent = `View log · ${activity.events.length} event${activity.events.length === 1 ? '' : 's'}`;

  card.append(top, status, lastTime, actions, history);
  return card;
}

function createAction(className, icon, label, action, disabled) {
  const button = document.createElement('button');
  button.type = 'button';
  button.className = `card-action ${className}`;
  button.dataset.action = action;
  button.disabled = disabled;
  const iconElement = document.createElement('span');
  iconElement.setAttribute('aria-hidden', 'true');
  iconElement.textContent = icon;
  button.append(iconElement, document.createTextNode(label));
  return button;
}

function showToast(message) {
  toast.textContent = message;
  toast.classList.add('visible');
  clearTimeout(toastTimer);
  toastTimer = setTimeout(() => toast.classList.remove('visible'), 2400);
}

function downloadCsv(rows, filename) {
  const csv = rows.map(row => row.map(value => `"${String(value).replaceAll('"', '""')}"`).join(',')).join('\r\n');
  const blob = new Blob(['\ufeff', csv], { type: 'text/csv;charset=utf-8' });
  const url = URL.createObjectURL(blob);
  const link = document.createElement('a');
  link.href = url;
  link.download = filename;
  document.body.append(link);
  link.click();
  link.remove();
  URL.revokeObjectURL(url);
}

function exportEvents(list, filename) {
  const rows = [['Activity', 'Event', 'Timestamp']];
  list.forEach(activity => activity.events.forEach(event => rows.push([
    activity.name,
    event.type === 'start' ? 'Start' : 'End',
    new Date(event.at).toLocaleString()
  ])));
  if (rows.length === 1) {
    showToast('There are no recorded events to export yet.');
    return;
  }
  downloadCsv(rows, filename);
  showToast('CSV export downloaded.');
}

grid.addEventListener('click', event => {
  const button = event.target.closest('button[data-action]');
  if (!button || button.disabled) return;
  const card = button.closest('.activity-card');
  const activity = activities.find(item => item.id === Number(card.dataset.id));
  if (!activity) return;

  switch (button.dataset.action) {
    case 'start':
    case 'end': {
      const type = button.dataset.action;
      activity.events.push({ type, at: new Date().toISOString() });
      saveActivities();
      showToast(type === 'start' ? `${activity.name} started.` : `${activity.name} ended.`);
      break;
    }
    case 'rename':
      editingId = activity.id;
      nameInput.value = activity.name;
      editDialog.showModal();
      nameInput.focus();
      nameInput.select();
      break;
    case 'history':
      openHistory(activity);
      break;
  }
});

function openHistory(activity) {
  historyId = activity.id;
  historyTitle.textContent = `${activity.name} history`;
  historyContent.replaceChildren();
  if (activity.events.length === 0) {
    const empty = document.createElement('p');
    empty.className = 'history-empty';
    empty.textContent = 'No events recorded for this activity yet.';
    historyContent.append(empty);
  } else {
    const table = document.createElement('table');
    table.className = 'history-table';
    const thead = document.createElement('thead');
    const heading = document.createElement('tr');
    ['EVENT', 'DATE & TIME'].forEach(label => {
      const cell = document.createElement('th');
      cell.textContent = label;
      heading.append(cell);
    });
    thead.append(heading);
    const tbody = document.createElement('tbody');
    [...activity.events].reverse().forEach(item => {
      const row = document.createElement('tr');
      const eventCell = document.createElement('td');
      const pill = document.createElement('span');
      pill.className = `event-pill${item.type === 'end' ? ' end' : ''}`;
      pill.textContent = item.type === 'start' ? 'Start' : 'End';
      eventCell.append(pill);
      const dateCell = document.createElement('td');
      dateCell.textContent = formatDateTime(item.at);
      row.append(eventCell, dateCell);
      tbody.append(row);
    });
    table.append(thead, tbody);
    historyContent.append(table);
  }
  historyDialog.showModal();
}

editForm.addEventListener('submit', event => {
  event.preventDefault();
  const activity = activities.find(item => item.id === editingId);
  const name = nameInput.value.trim();
  if (!activity || !name) return;
  activity.name = name;
  saveActivities();
  editDialog.close();
  showToast('Activity name saved.');
});

document.querySelectorAll('[data-close]').forEach(button => {
  button.addEventListener('click', () => document.getElementById(button.dataset.close).close());
});

document.getElementById('export-button').addEventListener('click', () => exportEvents(activities, 'step-activity-log.csv'));
document.getElementById('export-activity-button').addEventListener('click', () => {
  const activity = activities.find(item => item.id === historyId);
  if (activity) exportEvents([activity], `${safeFilename(activity.name)}-log.csv`);
});

document.getElementById('reset-button').addEventListener('click', () => {
  if (!window.confirm('Reset all activity names and recorded events? This cannot be undone.')) return;
  activities = defaultActivities();
  saveActivities();
  showToast('All activity data has been reset.');
});

searchInput.addEventListener('input', render);

document.getElementById('today-label').textContent = new Intl.DateTimeFormat(undefined, {
  weekday: 'long', month: 'short', day: 'numeric'
}).format(new Date());

function safeFilename(value) {
  return value.trim().replace(/[^a-z0-9-_]+/gi, '-').replace(/^-|-$/g, '').toLowerCase() || 'activity';
}

render();
