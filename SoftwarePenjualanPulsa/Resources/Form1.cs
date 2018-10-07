using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace FormLogin {
    public partial class Form1 : Form {

        BackgroundWorker bgw = new BackgroundWorker();

        MySqlDataAdapter adapter;
        DataTable table = new DataTable();
        public MySqlConnection myCon;

        Form2 form2;
        Form3 form3;

        public Form1() {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(100, Color.White);
            textBoxPassword.UseSystemPasswordChar = true;
            form2 = new Form2();
            form3 = new Form3(this);
            string conStr = "datasource=localhost; port=3306; username=root; password=pemweb; database=penjualan_pulsa; SslMode=none";
            myCon = new MySqlConnection(conStr);
            myCon.Open();
        }

        private void checkShowPass_CheckedChanged(object sender, EventArgs e) {
            if (checkShowPass.Checked) {
                textBoxPassword.UseSystemPasswordChar = false;
            } else {
                textBoxPassword.UseSystemPasswordChar = true;
            }
        }

        private void linkLabelRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            form3.Show();
        }

        private void button1_Click(object sender, EventArgs e) {
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
            bgw.WorkerReportsProgress = true;
            if (!bgw.IsBusy)
                bgw.RunWorkerAsync();
        }

        private delegate void updateProgressDelegate();

        private void updateProgressBar() {
            progressBar1.Visible = true;
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e) {
            this.Invoke(new updateProgressDelegate(updateProgressBar));
            int total = 15;
            for (int i = 0; i <= total; i++) {
                Thread.Sleep(100);
                int percents = (i * 100) / total;
                bgw.ReportProgress(percents, i);
            }
        }

        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            progressBar1.Value = e.ProgressPercentage;
        }

        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            progressBar1.Visible = false;
            adapter = new MySqlDataAdapter("SELECT username, password FROM users WHERE username = '" + textBoxUsername.Text + "' AND password = '" + textBoxPassword.Text + "'", myCon);
            adapter.Fill(table);
            if (table.Rows.Count <= 0) {
                if (MessageBox.Show("Username/Password yang Anda masukkan salah!", "Login Gagal", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry) {
                    textBoxUsername.Clear();
                    textBoxPassword.Clear();
                }
            } else {
                form2.Show();
            }
            table.Clear();
            myCon.Close();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e) {
            progressBar1.Visible = false;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 1;
        }
    }
}
