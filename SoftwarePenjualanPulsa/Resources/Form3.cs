using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace FormLogin {
    public partial class Form3 : Form {

        Form1 form1;

        public Form3(Form1 form1) {
            InitializeComponent();
            this.form1 = form1;
        }

        private void buttonRegister_Click(object sender, EventArgs e) {
            if (textBoxName.Text != "" && textBoxPassword.Text != "" && textBoxUsername.Text != "" && textBoxPhone.Text != "") {
                try {
                    MySqlCommand cmd = new MySqlCommand("insert into users values(@id, @username, @password, @name, @phone_number)", form1.myCon);
                    form1.myCon.Open();
                    cmd.Parameters.AddWithValue("@id", "");
                    cmd.Parameters.AddWithValue("@username", textBoxUsername.Text);
                    cmd.Parameters.AddWithValue("@password", textBoxPassword.Text);
                    cmd.Parameters.AddWithValue("@name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@phone_number", textBoxPhone.Text);
                    cmd.ExecuteNonQuery();
                    form1.myCon.Close();
                    MessageBox.Show("Data berhasil ditambahkan");
                    this.Close();
                } catch (Exception) {

                    throw;
                }
            } else {
                MessageBox.Show("Semua field harus diisi", "Register Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
