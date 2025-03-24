namespace yt_dlp_GUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtUrl;
        private Button btnSelectFolder;
        private Label lblSelectedFolder;
        private Button btnDownload;
        private ProgressBar progressBar;
        private Label lblProgressPercentage;
        private Label lblDownloadSpeed;
        private Label lblDownloadTitle;
        private TextBox txtOutput;
        private Button btnToggleOutput;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txtUrl = new TextBox();
            btnSelectFolder = new Button();
            lblSelectedFolder = new Label();
            btnDownload = new Button();
            progressBar = new ProgressBar();
            lblProgressPercentage = new Label();
            lblDownloadSpeed = new Label();
            lblDownloadTitle = new Label();
            txtOutput = new TextBox();
            btnToggleOutput = new Button();
            SuspendLayout();
            // 
            // txtUrl
            // 
            txtUrl.Location = new Point(14, 17);
            txtUrl.Margin = new Padding(4);
            txtUrl.Multiline = true;
            txtUrl.Name = "txtUrl";
            txtUrl.ScrollBars = ScrollBars.Vertical;
            txtUrl.Size = new Size(536, 27);
            txtUrl.TabIndex = 0;
            txtUrl.Text = "单独或批量或列表，在此处输入URL（每行一个）";
            // 
            // btnSelectFolder
            // 
            btnSelectFolder.Location = new Point(14, 64);
            btnSelectFolder.Margin = new Padding(4);
            btnSelectFolder.Name = "btnSelectFolder";
            btnSelectFolder.Size = new Size(140, 33);
            btnSelectFolder.TabIndex = 1;
            btnSelectFolder.Text = "下载目录";
            btnSelectFolder.UseVisualStyleBackColor = true;
            // 
            // lblSelectedFolder
            // 
            lblSelectedFolder.AutoSize = true;
            lblSelectedFolder.Location = new Point(161, 71);
            lblSelectedFolder.Margin = new Padding(4, 0, 4, 0);
            lblSelectedFolder.Name = "lblSelectedFolder";
            lblSelectedFolder.Size = new Size(0, 17);
            lblSelectedFolder.TabIndex = 2;
            // 
            // btnDownload
            // 
            btnDownload.Location = new Point(14, 113);
            btnDownload.Margin = new Padding(4);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(140, 33);
            btnDownload.TabIndex = 3;
            btnDownload.Text = "开始下载";
            btnDownload.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(14, 163);
            progressBar.Margin = new Padding(4);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(408, 33);
            progressBar.TabIndex = 4;
            // 
            // lblProgressPercentage
            // 
            lblProgressPercentage.AutoSize = true;
            lblProgressPercentage.Location = new Point(429, 170);
            lblProgressPercentage.Margin = new Padding(4, 0, 4, 0);
            lblProgressPercentage.Name = "lblProgressPercentage";
            lblProgressPercentage.Size = new Size(26, 17);
            lblProgressPercentage.TabIndex = 5;
            lblProgressPercentage.Text = "0%";
            // 
            // lblDownloadSpeed
            // 
            lblDownloadSpeed.AutoSize = true;
            lblDownloadSpeed.Location = new Point(490, 170);
            lblDownloadSpeed.Margin = new Padding(4, 0, 4, 0);
            lblDownloadSpeed.Name = "lblDownloadSpeed";
            lblDownloadSpeed.Size = new Size(63, 17);
            lblDownloadSpeed.TabIndex = 6;
            lblDownloadSpeed.Text = "0.00MB/s";
            // 
            // lblDownloadTitle
            // 
            lblDownloadTitle.AutoSize = true;
            lblDownloadTitle.Location = new Point(14, 205);
            lblDownloadTitle.Margin = new Padding(4, 0, 4, 0);
            lblDownloadTitle.Name = "lblDownloadTitle";
            lblDownloadTitle.Size = new Size(0, 17);
            lblDownloadTitle.TabIndex = 7;
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(14, 234);
            txtOutput.Margin = new Padding(4);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ReadOnly = true;
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(536, 282);
            txtOutput.TabIndex = 8;
            txtOutput.Text = resources.GetString("txtOutput.Text");
            // 
            // btnToggleOutput
            // 
            btnToggleOutput.Location = new Point(408, 113);
            btnToggleOutput.Margin = new Padding(4);
            btnToggleOutput.Name = "btnToggleOutput";
            btnToggleOutput.Size = new Size(140, 33);
            btnToggleOutput.TabIndex = 9;
            btnToggleOutput.Text = "隐藏输出 ▼";
            btnToggleOutput.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(565, 540);
            Controls.Add(btnToggleOutput);
            Controls.Add(txtOutput);
            Controls.Add(lblDownloadTitle);
            Controls.Add(lblDownloadSpeed);
            Controls.Add(lblProgressPercentage);
            Controls.Add(progressBar);
            Controls.Add(btnDownload);
            Controls.Add(lblSelectedFolder);
            Controls.Add(btnSelectFolder);
            Controls.Add(txtUrl);
            Margin = new Padding(4);
            Name = "Form1";
            Text = "yt-dlp下载器";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}