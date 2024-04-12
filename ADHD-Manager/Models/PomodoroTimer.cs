using System.Threading.Tasks;
using System.Timers;

namespace ADHD_Manager.Models
{
    public class PomodoroTimer
    {
        public int StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public bool IsRunning { get; set; }

        //public static System.Timers.Timer dateTimer;
        //public static void SetTimer()
        //{
        //    dateTimer = new System.Timers.Timer(1000);

        //    dateTimer.Elapsed += OnTimedEvent;
        //    dateTimer.AutoReset = true;
        //    dateTimer.Enabled = true;
        //}

        //private static void OnTimedEvent(object source, ElapsedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
