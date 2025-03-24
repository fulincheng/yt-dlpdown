using System;
using System.Windows.Forms;

namespace yt_dlp_GUI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // 确保这里的Form1与你的窗体类名一致
        }
    }
}