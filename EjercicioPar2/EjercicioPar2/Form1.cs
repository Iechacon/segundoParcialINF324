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

                // Obtener el color del píxel clickeado
                Color c = bmp.GetPixel(e.X, e.Y);
                int r = c.R;
                int g = c.G;
                int b = c.B;

                // Crear la consulta para verificar si el color existe en la base de datos
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
                            // El color existe en la base de datos
                            textBox1.Text = c.R.ToString();
                            textBox2.Text = c.G.ToString();
                            textBox3.Text = c.B.ToString();
                            textBox8.Text = reader["descripcion"].ToString(); // Asumimos que descripcion es una descripción textual
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

        // Crear una nueva imagen con el color modificado
        Bitmap bmpR = new Bitmap(bmp.Width, bmp.Height);

        // Limpiar el RichTextBox antes de agregar nuevos cambios
        richTextBox1.Clear();

        // Diccionario para almacenar los cambios únicos
        Dictionary<string, string> cambiosUnicos = new Dictionary<string, string>();

        // Recorrer la imagen y cambiar los píxeles según la base de datos
        for (int i = 0; i < bmp.Width; i++)
        {
            for (int j = 0; j < bmp.Height; j++)
            {
                Color c = bmp.GetPixel(i, j);
                int r = c.R;
                int g = c.G;
                int b = c.B;
                // Crear la consulta para verificar si el color existe en la base de datos
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
                            // El color existe en la base de datos, obtener los valores de cambio
                            int rCambio = Convert.ToInt32(reader["R_cambio"]);
                            int gCambio = Convert.ToInt32(reader["G_cambio"]);
                            int bCambio = Convert.ToInt32(reader["B_cambio"]);
                            string dec = Convert.ToString(reader["descripcion_cambio"]);
                            Color newColor = Color.FromArgb(rCambio, gCambio, bCambio);
                            bmpR.SetPixel(i, j, newColor);

                            // Agregar el cambio al diccionario si es único
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
                            // El color no existe en la base de datos, mantener el color original
                            bmpR.SetPixel(i, j, c);
                        }
                    }
                }
            }
        }

        // Asignar la imagen modificada al PictureBox
        pictureBox2.Image = bmpR;

        // Mostrar los cambios únicos en el RichTextBox
        foreach (var cambio in cambiosUnicos)
        {
            richTextBox1.AppendText(cambio.Key + "\n");
        }

        // Cerrar la conexión
        connection.Close();
    }
}





        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Conectar a la base de datos MySQL
            string connectionString = "Server=localhost;Database=ej2;Uid=root;Pwd=;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Obtener valores de los TextBox
                int r = int.Parse(textBox1.Text);
                int g = int.Parse(textBox2.Text);
                int b = int.Parse(textBox3.Text);
                int rc = int.Parse(textBox4.Text);
                int gc = int.Parse(textBox5.Text);
                int bc = int.Parse(textBox6.Text);
                string cc = textBox7.Text;
                string cx = textBox8.Text;

                // Crear la consulta de inserción
                string insertQuery = "INSERT INTO PixelData (descripcion, R, G, B, descripcion_cambio, R_cambio, G_cambio, B_cambio) " +
                                     "VALUES (@Descripcion, @R, @G, @B, @DescripcionCambio, @RCambio, @GCambio, @BCambio)";

                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    // Asignar valores a los parámetros de la consulta
                    command.Parameters.AddWithValue("@Descripcion", cx);
                    command.Parameters.AddWithValue("@R", r);
                    command.Parameters.AddWithValue("@G", g);
                    command.Parameters.AddWithValue("@B", b);
                    command.Parameters.AddWithValue("@DescripcionCambio", cc);
                    command.Parameters.AddWithValue("@RCambio", rc);
                    command.Parameters.AddWithValue("@GCambio", gc);
                    command.Parameters.AddWithValue("@BCambio", bc);

                    // Ejecutar la consulta de inserción
                    command.ExecuteNonQuery();
                }

                // Cerrar la conexión
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
            // Aquí puedes agregar código si deseas realizar alguna acción cuando se hace clic en una celda del DataGridView
        }


    }
}
