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
using TPFinalNivel2_Monardes;

namespace winform
{
    public partial class FormArticulos : Form
    {
        private List<Articulo> listaArticulos;
        public FormArticulos()
        {
            InitializeComponent();
            txtbFiltrar.Enabled = false;
            btnBuscar.Enabled = false;
        }

        private void ocultar()
        {
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
        }

        private void cargarDgv()
        {
            centroArticulo centro = new centroArticulo();
            try
            {
                listaArticulos = centro.listar();
                dgvArticulos.DataSource = listaArticulos;

                ocultar();

                cargarImagen(listaArticulos[0].ImagenUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarColumnas()
        {
            cboxBusqueda.Items.Add("en Todos");
            cboxBusqueda.Items.Add("en Nombre");
            cboxBusqueda.Items.Add("en Código");
            cboxBusqueda.Items.Add("en Categoria");
            cboxBusqueda.Items.Add("en Marca");
            cboxBusqueda.SelectedIndex = 0;
            cboxCampo.Items.Add("Precio");
            cboxCampo.Items.Add("Categoria");
            cboxCampo.Items.Add("Marca");
        }

        private void FormArticulos_Load(object sender, EventArgs e)
        {
            cargarDgv();
            cargarColumnas();
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvArticulos.CurrentRow != null && dgvArticulos.Rows.Count > 0)
            {
                Articulo artSeleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(artSeleccionado.ImagenUrl);
            }
        }
        private void cargarImagen(string imagen)
        {
            try
            {
            pboxArticulos.Load(imagen);
            }
            catch (Exception)
            {
                pboxArticulos.Load("https://media.istockphoto.com/id/1147544807/vector/thumbnail-image-vector-graphic.jpg?s=612x612&w=0&k=20&c=rnCKVbdxqkjlcs3xH87-9gocETqpspHFXu5dIGB4wuM=");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo frmAgregar = new frmAgregarArticulo();
            frmAgregar.ShowDialog();
            cargarDgv();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAgregarArticulo frmModificar = new frmAgregarArticulo(seleccionado);
            frmModificar.ShowDialog();
            cargarDgv();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            centroArticulo articulo = new centroArticulo();
            Articulo seleccionado;
            try
            {
                DialogResult confirmacion = MessageBox.Show("El articulo se eliminara permanentemente. Está seguro?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmacion == DialogResult.Yes)
                {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                articulo.eliminar(seleccionado.Id);
                cargarDgv();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            txtbBuscar.Text = string.Empty;
            centroArticulo articulos = new centroArticulo();
            try
            {
            string campo = cboxCampo.SelectedItem.ToString();
            string criterio = cboxCriterio.SelectedItem.ToString();
            string filtrar = txtbFiltrar.Text;

                dgvArticulos.DataSource = articulos.filtrar(campo, criterio, filtrar);
            }
            catch (Exception)
            {
                MessageBox.Show("Por favor, complete todos los campos.");
            }
        }

        private void txtbBuscar_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> listaBusqueda;
            string busqueda = txtbBuscar.Text;

            listaBusqueda = listaArticulos;

            string opcion = cboxBusqueda.SelectedItem.ToString();

            if (busqueda != "")
            {
                switch (opcion)
                {
                case "en Todos":
                    listaBusqueda = listaArticulos.FindAll(x => x.Nombre.ToLower().Contains(busqueda.ToLower()) || x.Codigo.ToLower().Contains(busqueda.ToLower()) || x.Marca.Descripcion.ToLower().Contains(busqueda.ToLower()) || x.Categoria.Descripcion.ToLower().Contains(busqueda.ToLower()));
                    break;
                case "en Nombre":
                    listaBusqueda = listaArticulos.FindAll(x => x.Nombre.ToLower().Contains(busqueda.ToLower()));
                    break;
                case "en Código":
                    listaBusqueda = listaArticulos.FindAll(x => x.Codigo.ToLower().Contains(busqueda.ToLower()));
                    break;
                case "en Categoria":
                    listaBusqueda = listaArticulos.FindAll(x => x.Categoria.Descripcion.ToLower().Contains(busqueda.ToLower()));
                    break;
                case "en Marca":
                    listaBusqueda = listaArticulos.FindAll(x => x.Marca.Descripcion.ToLower().Contains(busqueda.ToLower()));
                    break;
                }
            }
            else
            {
              listaBusqueda = listaArticulos;
            }

            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaBusqueda;
            ocultar();
        }

        private void cboxCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboxCriterio.SelectedIndex = -1;
            txtbFiltrar.Enabled = false;
            btnBuscar.Enabled = false;

            string campo = cboxCampo.SelectedItem.ToString();
            if (campo == "Precio")
            {
                cboxCriterio.Items.Clear();
                cboxCriterio.Items.Add("Mayor a");
                cboxCriterio.Items.Add("Menor a");
                cboxCriterio.Items.Add("Igual a");
            }
            else
            {
                cboxCriterio.Items.Clear();
                cboxCriterio.Items.Add("Comienza con");
                cboxCriterio.Items.Add("Termina con");
                cboxCriterio.Items.Add("Contiene");
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmDetalles frmDetalles = new frmDetalles(seleccionado);
            frmDetalles.ShowDialog();
        }


        private void cboxCriterio_TextChanged(object sender, EventArgs e)
        {
            if (cboxCampo.SelectedIndex > -1 && cboxCriterio.SelectedIndex > -1)
            {
                txtbFiltrar.Enabled = true;
                btnBuscar.Enabled = true;
            }
            if (cboxCampo.SelectedItem.ToString() == "Precio")
            {
                txtbFiltrar.Text = "Solo números";
                txtbFiltrar.ForeColor = Color.Gray;
            }
            else
            {
                txtbFiltrar.Text = "";
                txtbFiltrar.ForeColor = Color.Black;
            }
        }

        private void txtbFiltrar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cboxCampo.SelectedItem.ToString() == "Precio")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void deshabilitarBotones()
        {
            if (dgvArticulos.Rows.Count > 0)
            {
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void dgvArticulos_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            deshabilitarBotones();
        }

        private void dgvArticulos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            deshabilitarBotones();
        }

        private void txtbFiltrar_Enter(object sender, EventArgs e)
        {
            if(cboxCampo.SelectedItem.ToString() == "Precio")
            {
            txtbFiltrar.Text = "";
            txtbFiltrar.ForeColor = Color.Black;
            }
        }
    }
}
