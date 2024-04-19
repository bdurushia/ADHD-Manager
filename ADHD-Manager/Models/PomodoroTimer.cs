using System.Threading.Tasks;
using System.Timers;

namespace ADHD_Manager.Models
{
    public class PomodoroTimer
    {
        public int StartTime { get; set; }
        public bool IsRunning { get; set; }
    }
}
