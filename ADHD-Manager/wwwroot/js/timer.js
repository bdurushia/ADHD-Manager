let timerInterval;
let remainingSeconds;
let timerPaused = false;
let isRunning = false;

// function to switch to short break
function setShortBreak() {
    $.get('/PomodoroTimer/SetShortBreak')
        .done(function (response) {
            remainingSeconds = response;
            updateTimerDisplay();
        }).fail(function (err) {
            console.error('Error setting short break timer: ', err)
        });
}
// function to switch to long break
function setLongBreak() {
    $.get('/PomodoroTimer/SetLongBreak')
        .done(function (response) {
            remainingSeconds = response;
            updateTimerDisplay();
        }).fail(function (err) {
            console.error('Error setting long break timer: ', err)
        });
}
// function to switch to pmodoro focus
function setPomodoroTime() {
    $.get('/PomodoroTimer/SetPomodoroTime')
        .done(function (response) {
            remainingSeconds = response;
            updateTimerDisplay();
        }).fail(function (err) {
            console.error('Error setting pomodoro timer: ', err)
        });
}

function updateTimerDisplay() {
    let minutes = Math.floor(remainingSeconds / 60);
    let seconds = remainingSeconds % 60;

    let timeDisplay = document.getElementById("timerDisplay");
    timeDisplay.innerHTML = minutes.toString().padStart(2, '0') + ':' + seconds.toString().padStart(2, '0');
}

function startTimer() {
    if (!isRunning) {
        $.get('/PomodoroTimer/Start')
            .done(function (response) {
                if (!timerPaused) {
                    remainingSeconds = response;
                }
                updateTimerDisplay();
                timerInterval = setInterval(updateTimer, 1000);
            }).fail(function (err) {
                console.error('Error starting timer: ', err)
            });
        isRunning = true;
    }
}

function stopTimer() {
    $.get('/PomodoroTimer/Stop')
        .done(function (response) {
            isRunning = false;
            timerPaused = true;
            clearInterval(timerInterval);
        }).fail(function (err) {
            console.error('Error stopping timer: ', err);
        });
}

function resetTimer() {
    $.get('/PomodoroTimer/Reset')
        .done(function (response) {
            remainingSeconds = response;
            isRunning = false;
            timerPaused = false;
            clearInterval(timerInterval);
            updateTimerDisplay();
        }).fail(function (err) {
            console.error('Error resetting the timer: ', err);
        });
}

function updateTimer() {
    remainingSeconds--;
    updateTimerDisplay();
    if (remainingSeconds <= 0) {
        clearInterval(timerInterval);
    }
}