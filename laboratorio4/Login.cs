using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laboratorio4
{
    public partial class Login : Form
    {
        private bool isChecked = false;
        public Login()
        {
            InitializeComponent();

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ctAuteticacion_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = ctAuteticacion.Checked;
            txtUsuario.Enabled = !isChecked;
            txtContraseña.Enabled = !isChecked;
        }

        private void conecta(string connectionString)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    MessageBox.Show("Conexión exitosa");
                    Productos form1 = new Productos();
                    form1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
            }
        }

        private bool validarCampos()
        {
            string servidor = txtServidor.Text.Trim();
            string baseDatos = txtBaseDatos.Text.Trim();

            if (servidor != "." && servidor.ToLower() != "(local)" && !servidor.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Por favor, ingrese un nombre válido para el servidor .");
                return false;
            }
            if (baseDatos.ToLower() != "negocios")
            {
                MessageBox.Show("Por favor, ingrese el nombre correcto de la base de datos.");
                return false;
            }

            return true;
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            if (isChecked || (!isChecked && !string.IsNullOrEmpty(txtUsuario.Text) && !string.IsNullOrEmpty(txtContraseña.Text)))
            {
                if (validarCampos())
                {
                    string connectionString;
                    if (ctAuteticacion.Checked)
                    {
                        // Autenticación de Windows
                        connectionString = Properties.Settings.Default.cc;
                    }
                    else
                    {
                        // Autenticación de SQL Server
                        if (string.IsNullOrEmpty(txtUsuario.Text) || string.IsNullOrEmpty(txtContraseña.Text))
                        {
                            MessageBox.Show("Por favor, complete los campos de usuario y contraseña.");
                            return;
                        }

                        connectionString = Properties.Settings.Default.cn; // Utiliza la cadena de conexión 'cs'
                    }

                    conecta(connectionString);
                }
            }
            else
            {
                MessageBox.Show("Por favor, marque la casilla 'Conectar' o complete los campos de usuario y contraseña antes de intentar establecer la conexión.");
            }

        }
        
    }
}
