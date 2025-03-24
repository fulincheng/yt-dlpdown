using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yt_dlp_GUI
{
    public partial class Form1 : Form
    {
        private string downloadFolderPath = string.Empty;
        private bool outputExpanded = true;
        private const int ExpandedHeight = 280;
        private const int CollapsedHeight = 0;
        private const int FormExpandedHeight = 581;
        private const int FormCollapsedHeight = 180;

        public Form1()
        {
            InitializeComponent();
            txtUrl.KeyDown += TxtUrl_KeyDown;
            btnSelectFolder.Click += BtnSelectFolder_Click;
            btnDownload.Click += BtnDownload_Click;
            btnToggleOutput.Click += BtnToggleOutput_Click;

            // 初始状态
            txtOutput.Height = ExpandedHeight;
            this.Height = FormExpandedHeight;
        }

        private async void BtnToggleOutput_Click(object sender, EventArgs e)
        {
            btnToggleOutput.Enabled = false;
            await ToggleOutputWithAnimation();
            btnToggleOutput.Enabled = true;
        }

        private async Task ToggleOutputWithAnimation()
        {
            int targetOutputHeight = outputExpanded ? CollapsedHeight : ExpandedHeight;
            int targetFormHeight = outputExpanded ? FormCollapsedHeight : FormExpandedHeight;
            int step = (targetOutputHeight - txtOutput.Height) / 10;

            while (outputExpanded ? txtOutput.Height > targetOutputHeight
                                : txtOutput.Height < targetOutputHeight)
            {
                txtOutput.Height += step;
                this.Height += step;
                await Task.Delay(15);

                // 确保不会超出目标值
                if ((step > 0 && txtOutput.Height > targetOutputHeight) ||
                    (step < 0 && txtOutput.Height < targetOutputHeight))
                {
                    txtOutput.Height = targetOutputHeight;
                    this.Height = targetFormHeight;
                }
            }

            outputExpanded = !outputExpanded;
            btnToggleOutput.Text = outputExpanded ? "隐藏输出 ▼" : "显示输出 ▲";
        }

        private void TxtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int lineCount = txtUrl.GetLineFromCharIndex(txtUrl.TextLength) + 1;
                int newHeight = Math.Min(Math.Max(20, 15 * lineCount), 100);
                int heightDelta = newHeight - txtUrl.Height;

                txtUrl.Height = newHeight;
                MoveControlsDown(heightDelta);
                this.Height += heightDelta;
            }
        }

        private void MoveControlsDown(int delta)
        {
            Control[] controlsToMove = {
                btnSelectFolder,
                lblSelectedFolder,
                btnDownload,
                btnToggleOutput,
                progressBar,
                lblProgressPercentage,
                lblDownloadSpeed,
                lblDownloadTitle,
                txtOutput
            };

            foreach (var control in controlsToMove)
            {
                control.Top += delta;
            }
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "选择下载目录";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    downloadFolderPath = folderDialog.SelectedPath;
                    lblSelectedFolder.Text = $"文件夹：{downloadFolderPath}";
                }
            }
        }

        private async void BtnDownload_Click(object sender, EventArgs e)
        {
            string[] urls = txtUrl.Lines;
            if (urls.Length == 0 || string.IsNullOrWhiteSpace(urls[0]))
            {
                MessageBox.Show("请输入至少一个有效URL", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(downloadFolderPath))
            {
                MessageBox.Show("请先选择下载文件夹", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            btnDownload.Enabled = false;
            txtOutput.Clear();

            try
            {
                foreach (string url in urls)
                {
                    if (string.IsNullOrWhiteSpace(url))
                        continue;

                    await RunYtDlpAsync(url, downloadFolderPath);
                }
            }
            catch (Exception ex)
            {
                AppendOutput($"错误：{ex.Message}");
            }
            finally
            {
                btnDownload.Enabled = true;
                UpdateProgressPercentage("下载完成");
                UpdateDownloadSpeed("");
            }
        }

        private async Task RunYtDlpAsync(string url, string outputFolder)
        {
            string ytDlpPath = @".\yt-dlp.exe";
            string arguments = $"--progress --download-archive videos.txt " +
                              $"--merge-output-format mp4 -o \"{Path.Combine(outputFolder, "%(title)s.%(ext)s")}\" " +
                              $"--socket-timeout 5 --downloader aria2c --concurrent-fragments 16 {url}";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ytDlpPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                process.OutputDataReceived += (s, ev) => ParseOutput(ev.Data);
                process.ErrorDataReceived += (s, ev) => ParseOutput(ev.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                await Task.Run(() => process.WaitForExit());
            }
        }

        private void ParseOutput(string output)
        {
            if (string.IsNullOrEmpty(output))
                return;

            AppendOutput(output);

            if (output.StartsWith("[download]"))
            {
                var progressMatch = Regex.Match(output, @"(\d+\.\d+)%");
                if (progressMatch.Success)
                {
                    double progress = double.Parse(progressMatch.Groups[1].Value);
                    UpdateProgressBar(progress);
                    UpdateProgressPercentage($"{progress:F1}%");
                }

                var speedMatch = Regex.Match(output, @"(\d+\.\d+)([KM]iB/s)");
                if (speedMatch.Success)
                {
                    double speed = double.Parse(speedMatch.Groups[1].Value);
                    string unit = speedMatch.Groups[2].Value;

                    if (unit == "KiB/s") speed /= 1024;
                    UpdateDownloadSpeed($"{speed:F2}MB/s");
                }

                var titleMatch = Regex.Match(output, @"Destination: (.+\.mp4)");
                if (titleMatch.Success)
                {
                    string title = Path.GetFileNameWithoutExtension(titleMatch.Groups[1].Value);
                    UpdateDownloadTitle($"正在下载：{title}");
                }
            }
        }

        private void UpdateProgressBar(double progress)
        {
            if (progressBar.InvokeRequired)
                progressBar.Invoke(new Action<double>(UpdateProgressBar), progress);
            else
                progressBar.Value = (int)progress;
        }

        private void UpdateDownloadTitle(string title)
        {
            if (lblDownloadTitle.InvokeRequired)
                lblDownloadTitle.Invoke(new Action<string>(UpdateDownloadTitle), title);
            else
                lblDownloadTitle.Text = title;
        }

        private void UpdateProgressPercentage(string text)
        {
            if (lblProgressPercentage.InvokeRequired)
                lblProgressPercentage.Invoke(new Action<string>(UpdateProgressPercentage), text);
            else
                lblProgressPercentage.Text = text;
        }

        private void UpdateDownloadSpeed(string speed)
        {
            if (lblDownloadSpeed.InvokeRequired)
                lblDownloadSpeed.Invoke(new Action<string>(UpdateDownloadSpeed), speed);
            else
                lblDownloadSpeed.Text = speed;
        }

        private void AppendOutput(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (txtOutput.InvokeRequired)
                    txtOutput.Invoke(new Action<string>(AppendOutput), text);
                else
                    txtOutput.AppendText(text + Environment.NewLine);
            }
        }

        // 快捷键支持 Ctrl+O 切换输出框
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.O))
            {
                BtnToggleOutput_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}