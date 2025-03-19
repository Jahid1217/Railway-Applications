using System;
using System.Windows.Forms;

public class IdleTimeChecker
{
    private const int IDLE_THRESHOLD_SECONDS = 10;
    private Timer idleTimer;

    public IdleTimeChecker()
    {
        idleTimer = new Timer();
        idleTimer.Interval = 30000; // Check every second
        idleTimer.Tick += IdleTimer_Tick;
        ResetIdleTimer();
    }

    private void IdleTimer_Tick(object sender, EventArgs e)
    {
        int idleTime = (int)NativeMethods.GetIdleTime();
        // Check if idle time exceeds the threshold
        if (idleTime > IDLE_THRESHOLD_SECONDS * 1000)
        {
            DialogResult result = MessageBox.Show("Mouse hasn't moved for 30 seconds. Exiting application..", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (result == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }
    }

    public void StartMonitoring()
    {
        idleTimer.Start();
    }

    public void StopMonitoring()
    {
        idleTimer.Stop();
    }

    public void ResetIdleTimer()
    {
        NativeMethods.ResetIdleTime();
    }
}

public static class NativeMethods
{
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    private struct LASTINPUTINFO
    {
        public uint cbSize;
        public int dwTime;
    }

    public static int GetIdleTime()
    {
        LASTINPUTINFO lii = new LASTINPUTINFO();
        lii.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(LASTINPUTINFO));
        GetLastInputInfo(ref lii);
        return Environment.TickCount - lii.dwTime;
    }

    public static void ResetIdleTime()
    {
        LASTINPUTINFO lii = new LASTINPUTINFO();
        lii.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(typeof(LASTINPUTINFO));
        GetLastInputInfo(ref lii);
    }
}
