using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FormLogin {
    public partial class Form2 : Form {

        Form1 form1;
        MySqlDataAdapter da;
        MySqlCommand cmd;
        MySqlDataReader rd;
        RadioButton radioButtonTemp;
        Object a;

        public Form2(Form1 form1) {
            InitializeComponent();
            this.form1 = form1;
            radioButtonTemp = new RadioButton();
        }

        private void Form2_Load(object sender, EventArgs e) {
            tampilGrid();
            isiCombo();
            isiCombo2();
        }

        private void clean() {
            textBoxKode.Clear();
            textBoxHarga.Clear();
            textBoxQty.Clear();
            textBoxSearch.Clear();
        }

        private void tampilGrid() {
            da = new MySqlDataAdapter("select * from pulsa", form1.myCon);
            //ds = new DataSet();
            //ds.Clear();
            //da.Fill(ds, "pulsa");
            //dataGridView1.DataSource = ds.Tables;
            DataTable t = new DataTable();
            da.Fill(t);
            dataGridView1.DataSource = t;
        }

        private void isiCombo() {
            form1.myCon.Close();
            form1.myCon.Open();
            listBox1.Items.Clear();
            cmd = new MySqlCommand("select * from pulsa", form1.myCon);
            rd = cmd.ExecuteReader();
            while (rd.Read()) {
                listBox1.Items.Add(rd.GetString(1));
            }
            form1.myCon.Close();
        }

        private void insert_Click(object sender, EventArgs e) {
            form1.myCon.Open();
            if (textBoxKode.Text != "" && textBoxHarga.Text != "" && textBoxQty.Text != "") {
                try {
                    cmd = new MySqlCommand("select * from pulsa where kode = '" + textBoxKode.Text + "'", form1.myCon);
                    rd = cmd.ExecuteReader();
                    rd.Read();
                    if (rd.HasRows) {
                        MessageBox.Show("Data sudah ada", "Insert Gagal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clean();
                    } else {
                        rd.Close();
                        cmd = new MySqlCommand("insert into pulsa values(@id, @kode, @harga, @qty)", form1.myCon);
                        cmd.Parameters.AddWithValue("@id", "");
                        cmd.Parameters.AddWithValue("@kode", textBoxKode.Text);
                        cmd.Parameters.AddWithValue("@harga", textBoxHarga.Text);
                        cmd.Parameters.AddWithValue("@qty", textBoxQty.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Data berhasil ditambahkan", "Insert Berhasil", MessageBoxButtons.OK, MessageBoxIcon.None);
                        clean();
                        tampilGrid();
                        isiCombo();
                        isiCombo2();
                    }
                    form1.myCon.Close();
                } catch (Exception) {
                    throw;
                }
            } else {
                MessageBox.Show("Semua field harus diisi", "Insert Gagal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                form1.myCon.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            rd.Close();
            form1.myCon.Open();
            cmd = new MySqlCommand("select * from pulsa where kode = '" + listBox1.Text + "'", form1.myCon);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows) {
                textBoxKode.Text = rd.GetString(1);
                textBoxHarga.Text = rd.GetString(2);
                textBoxQty.Text = rd.GetString(3);
            }
            form1.myCon.Close();
        }

        private void button6_Click(object sender, EventArgs e) {
            form1.myCon.Open();
            if (textBoxKode.Text != "" && textBoxHarga.Text != "" && textBoxQty.Text != "") {
                rd.Close();
                cmd = new MySqlCommand("update pulsa set harga = '" + textBoxHarga.Text + "', qty = '" + textBoxQty.Text + "' where kode = '" + textBoxKode.Text + "'", form1.myCon);
                MessageBox.Show("Data berhasil diupdate", "Update Berhasil", MessageBoxButtons.OK, MessageBoxIcon.None);
                cmd.ExecuteNonQuery();
                clean();
                tampilGrid();
            } else {
                MessageBox.Show("Semua field harus diisi", "Update Gagal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxKode.Focus();
            }
            form1.myCon.Close();
        }

        private void button8_Click(object sender, EventArgs e) {
            form1.myCon.Open();
            if (textBoxKode.Text == "") {
                MessageBox.Show("Tidak ada data yang dihapus", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else {
                if (MessageBox.Show("Yakin menghapus produk ini?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                    rd.Close();
                    cmd = new MySqlCommand("delete from pulsa where kode = '" + textBoxKode.Text + "'", form1.myCon);
                    cmd.ExecuteNonQuery();
                    clean();
                    isiCombo();
                    isiCombo2();
                    rd.Close();
                    tampilGrid();
                }
            }
            form1.myCon.Close();
        }

        private void textBoxSearch_KeyPress(object sender, KeyPressEventArgs e) {
            form1.myCon.Open();
            if (e.KeyChar == (char) 13) {
                rd.Close();
                cmd = new MySqlCommand("select * from pulsa where kode like '%" + textBoxSearch.Text + "%'", form1.myCon);
                rd = cmd.ExecuteReader();
                rd.Read();
                if (rd.HasRows) {
                    rd.Close();
                    da = new MySqlDataAdapter("select * from pulsa where kode like '%" + textBoxSearch.Text + "%'", form1.myCon);
                    DataTable t = new DataTable();
                    da.Fill(t);
                    dataGridView1.DataSource = t;
                } else {
                    MessageBox.Show("Produk tidak ditemukan", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rd.Close();
                    tampilGrid();
                }
            }
            form1.myCon.Close();
        }

        private void button7_Click(object sender, EventArgs e) {
            tabControl1.SelectedTab = tabPage1;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        //tabPage2

        private void isiCombo2() {
            form1.myCon.Close();
            form1.myCon.Open();
            comboBoxKode.Items.Clear();
            cmd = new MySqlCommand("select * from pulsa", form1.myCon);
            rd = cmd.ExecuteReader();
            while (rd.Read()) {
                comboBoxKode.Items.Add(rd.GetString(1) + new String(' ', 3));
            }
            form1.myCon.Close();
        }

        private void tampilGrid2() {
            form1.myCon.Open();
            da = new MySqlDataAdapter("select * from pulsa", form1.myCon);
            DataTable t = new DataTable();
            da.Fill(t);
            dataGridView1.DataSource = t;
            form1.myCon.Close();
        }

        private void comboBoxKode_SelectedIndexChanged(object sender, EventArgs e) {
            form1.myCon.Open();
            rd.Close();
            cmd = new MySqlCommand("select * from pulsa where kode = '" + comboBoxKode.Text + "'", form1.myCon);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows) {
                tbHargaSatuan.Text = rd.GetString(2);
            }
            form1.myCon.Close();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
            comboBoxQty.Enabled = false;
            comboBoxQty.Text = "";
            double number = Convert.ToDouble(tbHargaSatuan.Text.ToString()) * 1;
            tbHargaTotal.Text = Convert.ToString(number);
            a = "1";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) {
            comboBoxQty.Enabled = true;
            comboBoxQty.Items.Clear();
            comboBoxQty.Items.Add("2 pcs");
            comboBoxQty.Items.Add("3 pcs");
            comboBoxQty.Items.Add("4 pcs");
            comboBoxQty.Items.Add("5 pcs");
            comboBoxQty.Items.Add("6 pcs");
            a = "Lebih dari 1";
        }

        int x;
        private void comboBoxQty_SelectedIndexChanged(object sender, EventArgs e) {
            form1.myCon.Open();
            double number;
            switch (comboBoxQty.Text) {
                case "2 pcs":
                    number = Convert.ToDouble(tbHargaSatuan.Text.ToString()) * 2;
                    tbHargaTotal.Text = number.ToString();
                    x = 2;
                    break;
                case "3 pcs":
                    number = Convert.ToDouble(tbHargaSatuan.Text.ToString()) * 3;
                    tbHargaTotal.Text = number.ToString();
                    x = 3;
                    break;
                case "4 pcs":
                    number = Convert.ToDouble(tbHargaSatuan.Text.ToString()) * 4;
                    tbHargaTotal.Text = number.ToString();
                    x = 4;
                    break;
                case "5 pcs":
                    number = Convert.ToDouble(tbHargaSatuan.Text.ToString()) * 5;
                    tbHargaTotal.Text = number.ToString();
                    x = 5;
                    break;
                case "6 pcs":
                    number = Convert.ToDouble(tbHargaSatuan.Text.ToString()) * 6;
                    tbHargaTotal.Text = number.ToString();
                    x = 6;
                    break;
                default:
                    break;
            }
            form1.myCon.Close();
        }

        ErrorProvider errorProvider = new ErrorProvider();
        private void button1_Click(object sender, EventArgs e) {
            if (tbNama.Text == "" || tbAlamat.Text == "" || tbTelepon.Text == "" || radioButtonTemp.Text == "") {
                errorProvider.SetError(tbNama, "Tidak boleh kosong");
                errorProvider.SetError(tbAlamat, "Tidak boleh kosong");
                errorProvider.SetError(tbTelepon, "Tidak boleh kosong");
                errorProvider.SetError(radioButtonTemp, "Tidak boleh kosong");
            } else {
                errorProvider.SetError(tbNama, "");
                errorProvider.SetError(tbAlamat, "");
                errorProvider.SetError(tbTelepon, "");
            }
            if (tbNama.Text == "" || tbAlamat.Text == "" || tbTelepon.Text == "" || radioButtonTemp.Text == "") {
                MessageBox.Show("Field tidak boleh kosong", "Isi data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbNama.Text = "";
                tbAlamat.Text = "";
                tbTelepon.Text = "";
            } else {
                listBox2.Items.Clear();
                listBox2.Items.Add("Nama\t\t\t: " + tbNama.Text);
                listBox2.Items.Add("Jenis Kelamin\t\t: " + radioButtonTemp.Text);
                listBox2.Items.Add("Alamat\t\t\t: " + tbAlamat.Text);
                listBox2.Items.Add("Telepon\t\t\t: " + tbTelepon.Text);
                listBox2.Items.Add("Kode Produk\t\t: " + comboBoxKode.Text);
                listBox2.Items.Add("Qty\t\t\t: " + comboBoxQty.Text);
                listBox2.Items.Add("Harga Total\t\t: " + tbHargaTotal.Text);
                for (int i = 1; i <= x; i++) {
                    listBox2.Items.Add("VOUCHER " + i + "\t\t: " + RandomString(12));
                }
                MessageBox.Show("Nama: " + tbNama.Text + "\nJenis Kelamin: " + radioButtonTemp.Text + "\nALamat: " + tbAlamat.Text + "\nTelepon: " + tbTelepon.Text + "\nKode: " + comboBoxKode.Text + "\nQty: " + comboBoxQty.Text + "\nHarga Total: " + tbHargaTotal.Text, "Detail", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            RadioButton rb = (RadioButton) sender;
            radioButtonTemp = rb;
        }

        private void button3_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e) {
            tbNama.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            tbAlamat.Text = "";
            tbTelepon.Text = "";
            comboBoxKode.Text = "";
            comboBoxQty.Text = "";
            tbHargaSatuan.Text = "";
            tbHargaTotal.Text = "";
            listBox1.Items.Clear();
        }

        private static Random random = new Random();
        public static string RandomString(int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }
    }
}
