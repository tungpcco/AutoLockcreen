namespace AutoLockcreen
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            groupBox1 = new GroupBox();
            chkAutoRun = new CheckBox();
            chkStartup = new CheckBox();
            numMinutes = new NumericUpDown();
            btnCancel = new Button();
            btnStart = new Button();
            label1 = new Label();
            timer = new System.Windows.Forms.Timer(components);
            statusStrip1 = new StatusStrip();
            lblRemaining = new ToolStripStatusLabel();
            notifyIcon = new NotifyIcon(components);
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMinutes).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(chkAutoRun);
            groupBox1.Controls.Add(chkStartup);
            groupBox1.Controls.Add(numMinutes);
            groupBox1.Controls.Add(btnCancel);
            groupBox1.Controls.Add(btnStart);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 13);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(266, 180);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Tự động khóa";
            // 
            // chkAutoRun
            // 
            chkAutoRun.AutoSize = true;
            chkAutoRun.Location = new Point(45, 106);
            chkAutoRun.Name = "chkAutoRun";
            chkAutoRun.Size = new Size(151, 24);
            chkAutoRun.TabIndex = 6;
            chkAutoRun.Text = "Tự động chạy app.";
            chkAutoRun.UseVisualStyleBackColor = true;
            chkAutoRun.CheckedChanged += chkAutoRun_CheckedChanged;
            // 
            // chkStartup
            // 
            chkStartup.AutoSize = true;
            chkStartup.Location = new Point(45, 136);
            chkStartup.Name = "chkStartup";
            chkStartup.Size = new Size(161, 24);
            chkStartup.TabIndex = 5;
            chkStartup.Text = "Start cùng windows!";
            chkStartup.UseVisualStyleBackColor = true;
            chkStartup.CheckedChanged += chkStartup_CheckedChanged;
            // 
            // numMinutes
            // 
            numMinutes.Location = new Point(92, 27);
            numMinutes.Name = "numMinutes";
            numMinutes.Size = new Size(109, 27);
            numMinutes.TabIndex = 4;
            numMinutes.Value = new decimal(new int[] { 45, 0, 0, 0 });
            numMinutes.ValueChanged += numMinutes_ValueChanged;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(126, 59);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 33);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Dừng lại";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(45, 59);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(75, 33);
            btnStart.TabIndex = 2;
            btnStart.Text = "Bắt đầu";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 29);
            label1.Name = "label1";
            label1.Size = new Size(41, 20);
            label1.TabIndex = 0;
            label1.Text = "Phút:";
            // 
            // timer
            // 
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblRemaining });
            statusStrip1.Location = new Point(0, 201);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(288, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblRemaining
            // 
            lblRemaining.Name = "lblRemaining";
            lblRemaining.Size = new Size(57, 17);
            lblRemaining.Text = "Sẵn sàng!";
            // 
            // notifyIcon
            // 
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Text = "Hẹn giờ khóa màn hình";
            notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(288, 223);
            Controls.Add(statusStrip1);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Auto Lock Screen";
            FormClosing += frmMain_FormClosing;
            Load += frmMain_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMinutes).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Label label1;
        private Button btnStart;
        private System.Windows.Forms.Timer timer;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblRemaining;
        private Button btnCancel;
        private NumericUpDown numMinutes;
        private NotifyIcon notifyIcon;
        private CheckBox chkStartup;
        private CheckBox chkAutoRun;
    }
}
