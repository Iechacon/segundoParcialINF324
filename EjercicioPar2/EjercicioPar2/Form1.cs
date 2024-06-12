using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace EjercicioPar2
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        MySqlConnection connection = new MySqlConnection("Server=localhost;Database=ej2;Uid=root;Pwd=;");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.jpg|*.png|*.gif|";
            openFileDialog1.ShowDialog();
            bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Color c = new Color();
            c = bmp.GetPixel(15, 15);
            textBox1.Text = c.R.ToString();
            textBox2.Text = c.G.ToString();
            textBox3.Text = c.B.ToString();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            string connectionString = "Server=localhost;Database=ej2;Uid=root;Pwd=;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                Color c = bmp.GetPixel(e.X, e.Y);
                int r = c.R;
                int g = c.G;
                int b = c.B;

                string selectQuery = "SELECT descripcion, R, G, B FROM PixelData WHERE R = @R AND G = @G AND B = @B";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@R", r);
                    command.Parameters.AddWithValue("@G", g);
                    command.Parameters.AddWithValue("@B", b);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = c.R.ToString();
                            textBox2.Text = c.G.ToString();
                            textBox3.Text = c.B.ToString();
                            textBox8.Text = reader["descripcion"].ToString(); 
                        }
                        else{
                            textBox1.Text = c.R.ToString();
                            textBox2.Text = c.G.ToString();
                            textBox3.Text = c.B.ToString();
                            textBox8.Clear();
                        }
                    }
                }

                connection.Close();
            }
        }

private void button3_Click(object sender, EventArgs e)
{
    string connectionString = "Server=localhost;Database=ej2;Uid=root;Pwd=;";
    using (MySqlConnection connection = new MySqlConnection(connectionString))
    {
        connection.Open();

        Bitmap bmpR = new Bitmap(bmp.Width, bmp.Height);

        richTextBox1.Clear();

        Dictionary<string, string> cambiosUnicos = new Dictionary<string, string>();

        for (int i = 0; i < bmp.Width; i++)
        {
            for (int j = 0; j < bmp.Height; j++)
            {
                Color c = bmp.GetPixel(i, j);
                int r = c.R;
                int g = c.G;
                int b = c.B;
                string selectQuery = "SELECT descripcion, R_cambio, G_cambio, B_cambio, descripcion_cambio FROM PixelData WHERE R = @R AND G = @G AND B = @B";
                using (MySqlCommand command = new MySqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@R", r);
                    command.Parameters.AddWithValue("@G", g);
                    command.Parameters.AddWithValue("@B", b);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int rCambio = Convert.ToInt32(reader["R_cambio"]);
                            int gCambio = Convert.ToInt32(reader["G_cambio"]);
                            int bCambio = Convert.ToInt32(reader["B_cambio"]);
                            string dec = Convert.ToString(reader["descripcion_cambio"]);
                            Color newColor = Color.FromArgb(rCambio, gCambio, bCambio);
                            bmpR.SetPixel(i, j, newColor);

                            string descripcion = reader["descripcion"].ToString();
                            string descripcionCambio = dec;
                            string key = "Original: " + descripcion + " (R: " + r + ", G: " + g + ", B: " + b + ") -> Cambio: " + descripcionCambio + " (R: " + rCambio + ", G: " + gCambio + ", B: " + bCambio + ")";
                            if (!cambiosUnicos.ContainsKey(key))
                            {
                                cambiosUnicos.Add(key, descripcionCambio);
                            }
                        }
                        else
                        {
                            bmpR.SetPixel(i, j, c);
                        }
                    }
                }
            }
        }
        pictureBox2.Image = bmpR;

        foreach (var cambio in cambiosUnicos)
        {
            richTextBox1.AppendText(cambio.Key + "\n");
        }

        connection.Close();
    }
}





        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Database=ej2;Uid=root;Pwd=;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                int r = int.Parse(textBox1.Text);
                int g = int.Parse(textBox2.Text);
                int b = int.Parse(textBox3.Text);
                int rc = int.Parse(textBox4.Text);
                int gc = int.Parse(textBox5.Text);
                int bc = int.Parse(textBox6.Text);
                string cc = textBox7.Text;
                string cx = textBox8.Text;

                string insertQuery = "INSERT INTO PixelData (2descripcion, R, G, B, descripcion_cambio, R_cambio, G_cambio, B_cambio) " +
                                     "VALUES (@Descripcion, @R, @G, @B, @DescripcionCambio, @RCambio, @GCambio, @BCambio)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", cx);
                    command.Parameters.AddWithValue("@R", r);
                    command.Parameters.AddWithValue("@G", g);
                    command.Parameters.AddWithValue("@B", b);
                    command.Parameters.AddWithValue("@DescripcionCambio", cc);
                    command.Parameters.AddWithValue("@RCambio", rc);
                    command.Parameters.AddWithValue("@GCambio", gc);
                    command.Parameters.AddWithValue("@BCambio", bc);

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadData()
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM PixelData", connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Bitmap bmpB = new Bitmap(pictureBox1.Image);
            string connectionString = "Server=localhost;Database=ej2;Uid=root;Pwd=;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    string query = @"SELECT R, G, B, R_cambio, G_cambio, B_cambio, descripcion, descripcion_cambio FROM PixelData";

                    MySqlCommand comando = new MySqlCommand(query, connection);

                    using (MySqlDataReader reader = comando.ExecuteReader())
                    {
                        // Limpiar el RichTextBox antes de agregar nuevos cambios
                        richTextBox1.Clear();

                        // Diccionario para almacenar los cambios únicos
                        Dictionary<string, string> cambiosUnicos = new Dictionary<string, string>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int cR_origen = reader.GetInt32("R");
                                int cG_origen = reader.GetInt32("G");
                                int cB_origen = reader.GetInt32("B");
                                int cR_destino = reader.GetInt32("R_cambio");
                                int cG_destino = reader.GetInt32("G_cambio");
                                int cB_destino = reader.GetInt32("B_cambio");
                                string dec = Convert.ToString(reader["descripcion_cambio"]);
                                string des = Convert.ToString(reader["descripcion"]);

                                for (int i = 0; i < bmpB.Width; i += 10)
                                {
                                    for (int j = 0; j < bmpB.Height; j += 10)
                                    {
                                        int sR = 0, sG = 0, sB = 0;
                                        int pixelCount = 0;

                                        for (int ip = i; ip < i + 10 && ip < bmpB.Width; ip++)
                                        {
                                            for (int jp = j; jp < j + 10 && jp < bmpB.Height; jp++)
                                            {
                                                Color pixelColor = bmpB.GetPixel(ip, jp);
                                                sR += pixelColor.R;
                                                sG += pixelColor.G;
                                                sB += pixelColor.B;
                                                pixelCount++;
                                            }
                                        }

                                        if (pixelCount > 0)
                                        {
                                            sR /= pixelCount;
                                            sG /= pixelCount;
                                            sB /= pixelCount;
                                        }

                                        if (((cR_origen - 20 <= sR) && (sR <= cR_origen + 20)) &&
                                            ((cG_origen - 20 <= sG) && (sG <= cG_origen + 20)) &&
                                            ((cB_origen - 82 <= sB) && (sB <= cB_origen + 20)))
                                        {
                                            for (int ip = i; ip < i + 10 && ip < bmpB.Width; ip++)
                                            {
                                                for (int jp = j; jp < j + 10 && jp < bmpB.Height; jp++)
                                                {
                                                    bmpB.SetPixel(ip, jp, Color.FromArgb(cR_destino, cG_destino, cB_destino));
                                                }
                                            }

                                            // Agregar el cambio al diccionario si es único
                                            string key = "Original:"+des+" RGB(" + cR_origen + "," + cG_origen + "," + cB_origen +
                                                         ") -> Cambio:"+dec+" RGB(" + cR_destino + "," + cG_destino + "," + cB_destino + ")";
                                            if (!cambiosUnicos.ContainsKey(key))
                                            {
                                                cambiosUnicos.Add(key, key);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron filas en la tabla.");
                        }

                        // Mostrar los cambios únicos en el RichTextBox
                        foreach (var cambio in cambiosUnicos)
                        {
                            richTextBox1.AppendText(cambio.Key + "\n");
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error al ejecutar la consulta: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            // Actualizar la imagen del PictureBox
            pictureBox1.Image = bmpB;
        }
    }
}
