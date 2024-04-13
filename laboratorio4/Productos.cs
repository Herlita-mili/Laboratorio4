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
    public partial class Productos : Form
    {
        
        public Productos()
        {
            InitializeComponent();
        }

        private void FiltrarProductos(string filtro)
        {
            DataView dv = ((DataTable)dataGridView1.DataSource).DefaultView;
            StringBuilder filterExpression = new StringBuilder();
            filterExpression.Append("(");
            filterExpression.Append("NombreProducto LIKE '%" + filtro + "%'");
            filterExpression.Append(")");
            dv.RowFilter = filterExpression.ToString();

        }

        private void MostrarTodosLosProductos()
        {
            DataView dv = ((DataTable)dataGridView1.DataSource).DefaultView;
            dv.RowFilter = string.Empty;
        }

        

        void cargaProductos()
        {
            try 
            {
                using(SqlConnection cd=new SqlConnection(Properties.Settings.Default.cn))

                {
                    cd.Open();
                    SqlDataAdapter da = new SqlDataAdapter("select * from PRODUCTO",cd);
                    DataSet ds = new DataSet();
                    ds.Tables.Add();
                    da.Fill(ds, "productos");
                    dataGridView1.DataSource = ds.Tables["productos"];
                }
                

            } catch(Exception ex)

            { 
                MessageBox.Show(ex.Message);
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            cargaProductos();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            string filtro = txtProducto.Text;

            FiltrarProductos(filtro);
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            MostrarTodosLosProductos();
        }
    }
}
