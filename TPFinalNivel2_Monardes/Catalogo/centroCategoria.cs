using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace Catalogo
{
    public class centroCategoria
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            accesoDatos datos = new accesoDatos();
            try
            {
                datos.Consultar("Select Id, Descripcion from CATEGORIAS");
                datos.Leer();
                while (datos.Lector.Read())
                {
                    Categoria auxCategoria = new Categoria();
                    auxCategoria.Id = (int)datos.Lector["Id"];
                    auxCategoria.Descripcion = (string)datos.Lector["Descripcion"];

                lista.Add(auxCategoria);
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
