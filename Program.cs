using System;
using System.Windows.Forms;

namespace yt_dlp_GUI
{
    static class Program
    {
        /// <summary>
        /// Ӧ�ó��������ڵ㡣
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1()); // ȷ�������Form1����Ĵ�������һ��
        }
    }
}