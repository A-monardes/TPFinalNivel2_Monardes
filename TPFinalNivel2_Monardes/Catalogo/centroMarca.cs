using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace Catalogo
{
    public class centroMarca
    {
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            accesoDatos datos = new accesoDatos();
            try
            {
                datos.Consultar("Select Id, Descripcion from MARCAS");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    Marca auxMarca = new Marca();
                    auxMarca.Id = (int)datos.Lector["Id"];
                    auxMarca.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(auxMarca);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
