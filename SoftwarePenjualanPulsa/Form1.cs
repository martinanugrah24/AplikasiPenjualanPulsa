using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormLogin {
    public partial class Form1 : Form {
        Form2 form2;
        public Form1() {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(100, Color.White);
            form2 = new Form2();
        }

        private void button1_Click(object sender, EventArgs e) {
            if (textBoxUsername.Text.Equals("martinanugrah24") && textBoxPassword.Text.Equals("12345678")) {
                form2.Show();
            } else {
                if (MessageBox.Show("Username/Password yang Anda masukkan salah!", "Login Gagal", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning) == DialogResult.Retry) {
                    textBoxUsername.Clear();
                    textBoxPassword.Clear();
                }
            }
        }
    }
}
