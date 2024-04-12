let timerInterval;
let remainingSeconds;
let timerPaused = false;
let isRunning = false;

function updateTimerDisplay() {
    let minutes = Math.floor(remainingSeconds / 60);
    let seconds = remainingSeconds % 60;

    let timeDisplay = document.getElementById("timerDisplay");
    timeDisplay.innerHTML = minutes.toString().padStart(2, '0') + ':' + seconds.toString().padStart(2, '0');
}


// clicking timer start button multiple times messes with the timer speed and it needs fixing.
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