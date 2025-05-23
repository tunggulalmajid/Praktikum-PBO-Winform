using Npgsql;
using Praktikum_PBO_Winform.Models;

namespace Praktikum_PBO_Winform
{
    public partial class FormLogin : Form
    {
        UserContext context = new UserContext();
        public FormLogin()
        {
            InitializeComponent();
            tbPassword.UseSystemPasswordChar = true;
        }

        private void BtnShowPassword_Click_1(object sender, EventArgs e)
        {
            bool isPasswordHidden = tbPassword.UseSystemPasswordChar;
            tbPassword.UseSystemPasswordChar = !isPasswordHidden;
            tbPassword.PasswordChar = isPasswordHidden ? '\0' : '●';

            Image icon = isPasswordHidden ? Properties.Resources.eye_off : Properties.Resources.eye_on;
            BtnShowPassword.BackgroundImage = icon;
            BtnShowPassword.BackgroundImageLayout = ImageLayout.Zoom;
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbUsername != null && tbPassword != null)
                {
                    string username = tbUsername.Text;
                    string password = tbPassword.Text;
                    bool valid = context.Validate(username, password, out int userId);
                    if (valid)
                    {
                        MessageBox.Show("Login berhasil. Klik OK untuk melanjutkan", "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FormProduk formProduk = new FormProduk(userId);
                        this.Hide();
                        formProduk.Show();
                    }
                    else
                    {
                        MessageBox.Show("Email atau password salah", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Username & Password harus diisi!", "Perhatian!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Terjadi kesalahan saat menghubungi database: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RegisterLink_LinkClicked(object sender, EventArgs e)
        {
            this.Hide();
            FormRegister formRegister = new FormRegister();
            formRegister.Show();
        }
    }
}
