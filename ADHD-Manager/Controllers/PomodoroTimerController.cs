using ADHD_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Timers;

namespace ADHD_Manager.Controllers
{
    public class PomodoroTimerController : Controller
    {
        private PomodoroTimer _timer;
        private System.Timers.Timer _countDownTimer;
        private int _remainingSeconds;
        private int _pauseTime;

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

            // Start Timer logic here
            // Set the duration to 25 minutes
            if (_remainingSeconds <= 0)
            {
                _remainingSeconds = 25 * 60;
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
            _timer.StartTime = 25 * 60;
            _timer.IsRunning = false;

            // Reset Timer logic here

            return Ok(_timer.StartTime);
        }
    }
}
