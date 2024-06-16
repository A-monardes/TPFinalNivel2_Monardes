using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using Catalogo;

namespace TPFinalNivel2_Monardes
{
    public partial class frmAgregarArticulo : Form
    {
        private Articulo articulo = null;
        public frmAgregarArticulo()
        {
            InitializeComponent();
            btnAceptar.Enabled = false;
        }
        public frmAgregarArticulo(Articulo articuloModificar)
        {
            InitializeComponent();
            articulo = articuloModificar;
            Text = "Modificar articulo";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            centroArticulo centro = new centroArticulo();
            try
            {
                if(articulo == null)
                {
                    articulo = new Articulo();
                }
                
                articulo.Codigo = txtbCodigo.Text;
                articulo.Nombre = txtbNombre.Text;
                articulo.Descripcion = txtbDescripcion.Text;
                articulo.Categoria = (Categoria)cboxCategoria.SelectedItem;
                articulo.Marca = (Marca)cboxMarca.SelectedItem;
                articulo.ImagenUrl = txtbImagen.Text;
                articulo.Precio = decimal.Parse(txtbPrecio.Text);
                
                if(articulo.ImagenUrl == null)
                {
                    articulo.ImagenUrl = " ";
                }

                if(articulo.Id != 0)
                {
                    centro.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente.");
                }else{
                    centro.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente.");
                }

                Close();
            }
            catch (Exception)
            {

                MessageBox.Show("Por favor, llena todos los campos.");
            }
        }            
        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {
            if(articulo == null)
            {
                txtbCodigo.ForeColor = Color.Gray;
                txtbNombre.ForeColor = Color.Gray;
                txtbDescripcion.ForeColor = Color.Gray;
                txtbPrecio.ForeColor = Color.Gray;
            }

            centroCategoria desplegableCate = new centroCategoria();
            centroMarca desplegableMarca = new centroMarca();
            try
            {
                cboxCategoria.DataSource = desplegableCate.listar();
                cboxCategoria.ValueMember = "Id";
                cboxCategoria.DisplayMember = "Descripcion";
                cboxMarca.DataSource = desplegableMarca.listar();
                cboxMarca.ValueMember = "Id";
                cboxMarca.DisplayMember = "Descripcion";       

                if (articulo != null)
                {
                    txtbCodigo.Text = articulo.Codigo;
                    txtbNombre.Text = articulo.Nombre;
                    txtbDescripcion.Text = articulo.Descripcion;
                    txtbImagen.Text = articulo.ImagenUrl;
                    txtbPrecio.Text = articulo.Precio.ToString();
                    cargarImagen(txtbImagen.Text);
                    cboxCategoria.SelectedValue = articulo.Categoria.Id;
                    cboxMarca.SelectedValue = articulo.Marca.Id;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pboxImagen.Load(imagen);
            }
            catch (Exception)
            {
                pboxImagen.Load("https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM=");
            }
        }
        private void txtbImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtbImagen.Text);
        }
        private void txtbPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
          if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
             e.Handled = true;
            }   

          if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
             e.Handled = true;
            }
        }
        private void validarAceptar()
        {
            if (txtbCodigo.Text.Length > 0 && txtbNombre.Text.Length > 0 && txtbDescripcion.Text.Length > 0 && txtbPrecio.Text.Length > 0 && txtbCodigo.ForeColor != Color.Gray && txtbNombre.ForeColor != Color.Gray && txtbDescripcion.ForeColor != Color.Gray && txtbPrecio.ForeColor != Color.Gray)
            {
                btnAceptar.Enabled = true;
            }
            else
            {
                btnAceptar.Enabled = false;
            }
        }
        private void txtbCodigo_TextChanged(object sender, EventArgs e)
        {
            validarAceptar();
        }
        private void txtbNombre_TextChanged(object sender, EventArgs e)
        {
            validarAceptar();
        }
        private void txtbDescripcion_TextChanged(object sender, EventArgs e)
        {
            validarAceptar();
        }
        private void txtbPrecio_TextChanged(object sender, EventArgs e)
        {
            validarAceptar();
        }
        private void borrarTexto(TextBox text)
        {        
                if(text.ForeColor == Color.Gray)
                {
                    text.Text = "";
                    text.ForeColor = Color.Black;
                }        
        }
        private void txtbCodigo_Enter(object sender, EventArgs e)
        {
            borrarTexto(sender as TextBox);
        }
        private void txtbNombre_Enter(object sender, EventArgs e)
        {
            borrarTexto(sender as TextBox);
        }
        private void txtbDescripcion_Enter(object sender, EventArgs e)
        {
            borrarTexto(sender as TextBox);
        }
        private void txtbPrecio_Enter(object sender, EventArgs e)
        {
            borrarTexto(sender as TextBox);
        }
        private void frmAgregarArticulo_Enter(object sender, EventArgs e)
        {
            borrarTexto(sender as TextBox);
        }
    }
}
