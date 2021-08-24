using System.Diagnostics;

namespace Code.DebugScripts
{
    public class NotAScript
    {
        private void VoidFunction()
        {
            // Start
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // End
            sw.Stop();
            UnityEngine.Debug.Log(sw.ElapsedTicks);
        }
    }
}
