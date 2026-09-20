using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using NHotkey;
using NHotkey.WindowsForms;

namespace AutoLockcreen
{

    public partial class frmMain : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool LockWorkStation();

        private int remainingSeconds = 0;
        private const string RunRegistryKey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private const string AppRegistryName = "AutoLockcreen"; // Đặt tên định danh cho app trong Registry

        private const string HOTKEY_NAME = "GlobalCtrlAltR";

        public frmMain()
        {
            InitializeComponent();
            btnCancel.Enabled = false;

            this.Resize += Form1_Resize;
            this.notifyIcon.MouseDoubleClick += notifyIcon_MouseDoubleClick;
            this.FormClosing += frmMain_FormClosing;
            chkStartup.Checked = IsRunOnStartupEnabled();

            // Gán sự kiện khi người dùng click CheckBox
            chkStartup.CheckedChanged += chkStartup_CheckedChanged;
            

            chkAutoRun.Checked = Properties.Settings.Default.AutoStartTimer;
            if (Properties.Settings.Default.DefaultMinutes > 0)
            {
                numMinutes.Value = Properties.Settings.Default.DefaultMinutes;
            }

            // Gán sự kiện lưu cấu hình khi người dùng thay đổi
            chkAutoRun.CheckedChanged += chkAutoRun_CheckedChanged;
            numMinutes.ValueChanged += numMinutes_ValueChanged;

            // Tạo menu chuột phải cho icon dưới khay hệ thống
            SetupTrayContextMenu();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //LockWorkStation();
            //btnStart_Click(null, null);

            try
            {
                // Đăng ký trực tiếp bằng thư viện NHotkey
                HotkeyManager.Current.AddOrReplace("MyHotkey", Keys.Control | Keys.Alt | Keys.R, OnHotkeyPressed);
            }
            catch (HotkeyAlreadyRegisteredException)
            {
                MessageBox.Show("Hotkey đã bị ứng dụng khác chiếm giữ!");
            }

            if (chkAutoRun.Checked)
            {
                // Gọi sự kiện bấm nút Start
                btnStart.PerformClick();
            }

        }

        private void OnHotkeyPressed(object sender, HotkeyEventArgs e)
        {
            //MessageBox.Show("Thành công! NHotkey đã bắt được Ctrl + Alt + R.");
            //btnCancel.PerformClick();
            //btnStart.PerformClick();
            StopTimer();
            StartTimer();
            e.Handled = true; // Đánh dấu đã xử lý xong
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            remainingSeconds--;
            UpdateLabel();

            if (remainingSeconds <= 0)
            {
                StopTimer();
                StartTimer();
                //btnStart.PerformClick();
                LockWorkStation();
            }
        }

        private void UpdateLabel()
        {
            TimeSpan time = TimeSpan.FromSeconds(remainingSeconds);
            lblRemaining.Text = $"Còn lại: {time.Minutes:D2}:{time.Seconds:D2}";
            notifyIcon.Text = $"Còn lại: {time.Minutes:D2}:{time.Seconds:D2}";
        }

        private void StopTimer()
        {
            timer.Stop();
            btnStart.Enabled = true;
            numMinutes.Enabled = true;
            btnCancel.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            StopTimer();
            lblRemaining.Text = "Đã hủy hẹn giờ.";
        }

        private void StartTimer() {
            remainingSeconds = (int)numMinutes.Value * 60;

            if (remainingSeconds <= 0)
            {
                MessageBox.Show("Vui lòng nhập số phút lớn hơn 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnStart.Enabled = false;
            numMinutes.Enabled = false;
            btnCancel.Enabled = true;

            UpdateLabel();
            timer.Start();
            MinimizeToTray();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartTimer();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();                  // Ẩn cửa sổ Form
                this.ShowInTaskbar = false;   // Xóa icon khỏi thanh Taskbar
                notifyIcon.Visible = true;   // Hiện icon ở góc phải (System Tray)

                // (Tùy chọn) Hiện thông báo bong bóng nhỏ
                notifyIcon.ShowBalloonTip(1500, "Thông báo", "Ứng dụng đang chạy ngầm.", ToolTipIcon.Info);
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RestoreForm();
        }

        private void SetupTrayContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();

            ToolStripMenuItem itemShow = new ToolStripMenuItem("Mở ứng dụng");
            itemShow.Click += (s, ev) => RestoreForm();

            ToolStripMenuItem itemExit = new ToolStripMenuItem("Thoát hoàn toàn");
            itemExit.Click += (s, ev) =>
            {
                notifyIcon.Visible = false; // Xóa icon trước khi tắt
                Application.Exit();
            };

            contextMenu.Items.Add(itemShow);
            contextMenu.Items.Add(new ToolStripSeparator());
            contextMenu.Items.Add(itemExit);

            notifyIcon.ContextMenuStrip = contextMenu;
        }

        // Hàm khôi phục lại Form về trạng thái bình thường
        private void RestoreForm()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;       // Hiện lại trên Taskbar
            notifyIcon.Visible = false;      // Ẩn icon khay hệ thống
            this.BringToFront();
        }


        #region Xử lý Khởi động cùng Windows (Registry)

        // Sự kiện khi người dùng bật/tắt checkbox
        private void chkStartup_CheckedChanged(object sender, EventArgs e)
        {
            SetRunOnStartup(chkStartup.Checked);
        }

        // Kiểm tra xem ứng dụng đã được đăng ký khởi động chưa
        private bool IsRunOnStartupEnabled()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunRegistryKey, false))
                {
                    if (key == null) return false;
                    object value = key.GetValue(AppRegistryName);
                    return value != null;
                }
            }
            catch
            {
                return false;
            }
        }

        // Thêm hoặc xóa ứng dụng khỏi Registry
        private void SetRunOnStartup(bool enable)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RunRegistryKey, true))
                {
                    if (key == null) return;

                    if (enable)
                    {
                        // Lấy đường dẫn file .exe của ứng dụng hiện tại và bọc trong dấu ngoặc kép
                        string exePath = Application.ExecutablePath;
                        key.SetValue(AppRegistryName, $"\"{exePath}\"");
                    }
                    else
                    {
                        // Xóa khỏi Registry nếu bỏ chọn
                        key.DeleteValue(AppRegistryName, false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cấu hình khởi động cùng Windows: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void chkAutoRun_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoStartTimer = chkAutoRun.Checked;
            Properties.Settings.Default.Save();
        }

        // Lưu lại số phút khi người dùng thay đổi
        private void numMinutes_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DefaultMinutes = numMinutes.Value;
            Properties.Settings.Default.Save();
        }

        private void MinimizeToTray()
        {
            this.Hide();
            this.ShowInTaskbar = false;
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(1000, "Khóa màn hình", "Ứng dụng đã tự động thu nhỏ xuống khay hệ thống.", ToolTipIcon.Info);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //UnregisterHotKey(this.Handle, HOTKEY_ID);
        }
    }

}
