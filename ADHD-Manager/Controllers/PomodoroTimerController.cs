using ADHD_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Timers;

namespace ADHD_Manager.Controllers
{
    public class PomodoroTimerController : Controller
    {
        private PomodoroTimer _timer;
        private System.Timers.Timer _countDownTimer;
        private int _remainingSeconds;
        private bool _shortBreak { get; set; }
        private bool _longBreak { get; set; }

        public PomodoroTimerController()
        {
            _timer = new PomodoroTimer();
        }

        public IActionResult Index()
        {
            return View(_timer);
        }

        public IActionResult Start()
        {
            _timer.IsRunning = true;

            if (_remainingSeconds <= 0)
            {
                SetTime(_shortBreak, _longBreak);
            }

            // Create and start the countdown timer
            _countDownTimer = new System.Timers.Timer(1000);
            _countDownTimer.Elapsed += CountDownTimerCallBack;
            _countDownTimer.AutoReset = true;
            _countDownTimer.Enabled = true;

            return Ok(_remainingSeconds);
        }

        private async void CountDownTimerCallBack(object sender, ElapsedEventArgs e)
        {
            _remainingSeconds--;

            if (_remainingSeconds <= 0)
            {
                // Stop timer if the countdown reaches zero
                _countDownTimer.Dispose();
                _timer.IsRunning = false;
                _countDownTimer.Enabled = false;

                // Create something else to trigger here to prompt user to take a short break
            }
        }

        public IActionResult Stop(System.Timers.Timer _countDownTimer)
        {
            _countDownTimer.Enabled = false;
            _countDownTimer.Dispose();
            _timer.IsRunning = false;

            // Stop Timer logic here

            return Ok();
        }

        public IActionResult Reset()
        {
            // _timer.StartTime = 25 * 60;
            _timer.StartTime = SetTime(_shortBreak, _longBreak);
            _timer.IsRunning = false;

            return Ok(_timer.StartTime);
        }

        public IActionResult SetPomodoroTime()
        {
            _shortBreak = false;
            _longBreak = false;
            SetTime(_shortBreak, _longBreak);

            return Ok(_remainingSeconds);
        }

        public IActionResult SetShortBreak()
        {
            _shortBreak = true;
            _longBreak = false;
            SetTime(_shortBreak, _longBreak);

            return Ok(_remainingSeconds);
        }
        public IActionResult SetLongBreak()
        {
            _shortBreak = false;
            _longBreak = true;
            SetTime(_shortBreak, _longBreak);

            return Ok(_remainingSeconds);
        }

        private int SetTime(bool _shortBreak, bool _longBreak)
        {
            if (_shortBreak == false && _longBreak == true)
            {
                _remainingSeconds = 15 * 60;
            }
            else if (_shortBreak == true && _longBreak == false)
            {
                _remainingSeconds = 5 * 60;
            }
            else
            {
                _remainingSeconds = 25 * 60;
            }

            return _remainingSeconds;
        }
    }
}
