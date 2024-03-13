using Microsoft.VisualBasic.Logging;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace zayavki
{
    public class DB
    {
        NpgsqlConnection conn = new NpgsqlConnection("Server=localhost;Port=5432;Database=zayavki_db;User Id=postgres;Password=123");
        public NpgsqlConnection Get_Conn()
        {
            return conn;
        }
        public void Open_conn()
        { conn.Open(); }

        public void Request_create(int cl_id, string equip, string malf_type, string malf_desc)
        {
            using (conn)
            {
                conn.Open();
                using(NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO request (client_id,status_id,master_id,add_date,equipment,malf_type,malf_description) VALUES (@client_id,@status_id,@master_id,@add_date,@equipment,@malf_type,@malf_description)", conn))
                {
                    cmd.Parameters.Add("@client_id", NpgsqlDbType.Integer).Value = cl_id;
                    cmd.Parameters.Add("@status_id", NpgsqlDbType.Integer).Value = 1;
                    cmd.Parameters.Add("@master_id", NpgsqlDbType.Integer).Value = 1;
                    cmd.Parameters.Add("@add_date", NpgsqlDbType.Timestamp).Value = DateTime.Now ;
                    cmd.Parameters.Add("@equipment", NpgsqlDbType.Text).Value = equip;
                    cmd.Parameters.Add("@malf_type", NpgsqlDbType.Text).Value = malf_type;
                    cmd.Parameters.Add("@malf_description", NpgsqlDbType.Text).Value = malf_desc;

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Заявка успешно создана!");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }



                }
            }
        }
        public int Get_user_id(string login)
        {
            using (conn)
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT id FROM users WHERE login = @login", conn))
                {
                    cmd.Parameters.Add("@login", NpgsqlDbType.Varchar).Value = login;
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int user_id = reader.GetInt32(reader.GetOrdinal("id"));
                            return user_id;
                        }
                        return 0;
                    }

                }
            }
        }
        public int Get_master_id(int user_id)
        {
            using (conn)
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT id FROM master WHERE user_id = @user_id", conn))
                {
                    cmd.Parameters.Add("@user_id", NpgsqlDbType.Integer).Value = user_id;
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            return id;
                        }
                        return 0;
                    }

                }
            }
        }
        public int Get_client_id(int user_id)
        {
            using (conn)
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT id FROM client WHERE user_id = @user_id", conn))
                {
                    cmd.Parameters.Add("@user_id", NpgsqlDbType.Integer).Value = user_id;
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("id"));
                            return id;
                        }
                        return 0;
                    }

                }
            }
        }
        public void Request_update(int id, int master_id, string equip, string malf_type, string malf_desc)
        {
            using (conn)
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand($"UPDATE request SET status_id = @status_id, master_id = @master_id, equipment = @equipment, malf_type = @malf_type, malf_description = @malf_description WHERE id = @id", conn))
                {
                    cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
                    cmd.Parameters.Add("@status_id", NpgsqlDbType.Integer).Value = 2;
                    cmd.Parameters.Add("@master_id", NpgsqlDbType.Integer).Value = master_id;
                    cmd.Parameters.Add("@equipment", NpgsqlDbType.Varchar).Value = equip;
                    cmd.Parameters.Add("@malf_type", NpgsqlDbType.Varchar).Value = malf_type;
                    cmd.Parameters.Add("@malf_description", NpgsqlDbType.Varchar).Value = malf_desc;





                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Заявка успешно обновлёна!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        public void Request_finsh_status_upd(int id)
        {
            using (conn)
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand($"UPDATE request SET status_id = @status_id, finish_date = @finish_date WHERE id = @id", conn))
                {
                    cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
                    cmd.Parameters.Add("@status_id", NpgsqlDbType.Integer).Value = 3;
                    cmd.Parameters.Add("@finish_date", NpgsqlDbType.Timestamp).Value = DateTime.Now;






                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }

}
