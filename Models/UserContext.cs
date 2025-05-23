using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Praktikum_PBO_Winform.Models
{
    internal class UserContext
    {
        private readonly string connStr;
        public UserContext()
        {
            connStr = "Host=localhost;Username=postgres;Password=Gunungsari;Database=CRUDPR";
        }
        public bool Validate(string username, string password, out int userId)
        {
            userId = 0;

            string query = "SELECT user_id, username FROM users WHERE username = @username AND password = @password";
            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userId = reader.GetInt32(0);
                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Terjadi kesalahan saat memvalidasi login: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return false;
        }
    }
}
