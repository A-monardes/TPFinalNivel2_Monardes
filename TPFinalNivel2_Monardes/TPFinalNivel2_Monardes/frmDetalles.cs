using Catalogo;
using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPFinalNivel2_Monardes
{
    public partial class frmDetalles : Form
    {
        private Articulo detalles;
        public frmDetalles(Articulo detalles)
        {
            InitializeComponent();
            this.detalles = detalles;
        }
        private void btnDetalleCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDetalles_Load(object sender, EventArgs e)
        {
            lblCodDetalle.Text = detalles.Codigo;
            lblDesDetalle.Text = detalles.Descripcion;
            lblNombreDetalle.Text = detalles.Nombre;
            lblPrecioDetalle.Text = detalles.Precio.ToString();
            lblCateDetalle.Text = detalles.Categoria.ToString();
            lblMarcaDetalle.Text = detalles.Marca.ToString();
            try
            {
            pboxDetalle.Load(detalles.ImagenUrl);
            }
            catch (Exception)
            {
                pboxDetalle.Load("https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM=");
            }
        }
    }
}
