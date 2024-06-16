using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
using System.Security.Cryptography.X509Certificates;

namespace Catalogo
{
    public class centroArticulo
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();

            accesoDatos datos = new accesoDatos();


            try
            {
                datos.Consultar("select Codigo, Nombre, A.Descripcion, C.Descripcion as Categoria, M.Descripcion as Marca, A.IdMarca, A.IdCategoria, ImagenUrl, Precio, A.Id from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdCategoria = C.Id and A.IdMarca = M.Id");
                datos.Leer();

                while (datos.Lector.Read())
                {
                    Articulo auxiliar = new Articulo();
                    auxiliar.Id = (int)datos.Lector["Id"];
                    auxiliar.Codigo = (string)datos.Lector["Codigo"];
                    auxiliar.Nombre = (string)datos.Lector["Nombre"];
                    auxiliar.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull)) {
                        auxiliar.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    }

                    if (!(datos.Lector["Precio"] is DBNull))
                    {
                        auxiliar.Precio = (System.Decimal)datos.Lector["Precio"];
                    }
                    else {auxiliar.Precio = (decimal)0.00;}

                    auxiliar.Precio = Math.Round(auxiliar.Precio, 2);
                    auxiliar.Categoria = new Categoria();
                    auxiliar.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    auxiliar.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    auxiliar.Marca = new Marca();
                    auxiliar.Marca.Id = (int)datos.Lector["IdMarca"];
                    auxiliar.Marca.Descripcion = (string)datos.Lector["Marca"];

                    lista.Add(auxiliar);
                }

                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void agregar (Articulo nuevoArticulo)
        {
            accesoDatos agregar = new accesoDatos();
            try
            {
                agregar.Consultar("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio)values(@Codigo, @Nombre, @Descripcion, @idMarca, @idCategoria, @ImagenUrl, @Precio)");
                agregar.agregarParametro("Codigo", nuevoArticulo.Codigo);
                agregar.agregarParametro("Nombre", nuevoArticulo.Nombre);
                agregar.agregarParametro("Descripcion", nuevoArticulo.Descripcion);
                agregar.agregarParametro("idMarca", nuevoArticulo.Marca.Id);
                agregar.agregarParametro("idCategoria", nuevoArticulo.Categoria.Id);
                agregar.agregarParametro("ImagenUrl", nuevoArticulo.ImagenUrl);
                agregar.agregarParametro("Precio", nuevoArticulo.Precio);
                agregar.ejecutarNonQuery();
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
                agregar.cerrarConexion();
            }
        }

        public void modificar (Articulo modificar)
        {
            accesoDatos modificarArticulo = new accesoDatos();
            try
            {
                modificarArticulo.Consultar("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio Where Id = @Id");
                modificarArticulo.agregarParametro("@Codigo", modificar.Codigo);
                modificarArticulo.agregarParametro("@Nombre", modificar.Nombre);
                modificarArticulo.agregarParametro("@Descripcion", modificar.Descripcion);
                modificarArticulo.agregarParametro("@IdMarca", modificar.Marca.Id);
                modificarArticulo.agregarParametro("@IdCategoria", modificar.Categoria.Id);
                modificarArticulo.agregarParametro("@ImagenUrl", modificar.ImagenUrl);
                modificarArticulo.agregarParametro("@Precio", modificar.Precio);
                modificarArticulo.agregarParametro("@Id", modificar.Id);

                modificarArticulo.ejecutarNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                modificarArticulo.cerrarConexion();
            }
        }
        public void eliminar (int id)
        {
            try
            {
                accesoDatos eliminar = new accesoDatos();
                eliminar.Consultar("delete from ARTICULOS where Id = @Id");
                eliminar.agregarParametro("@Id", id);
                eliminar.ejecutarNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtrar)
        {
            List<Articulo> lista = new List<Articulo>();
            accesoDatos acceso = new accesoDatos();
            try
            {
                string consulta = "select Codigo, Nombre, A.Descripcion, C.Descripcion as Categoria, M.Descripcion as Marca, A.IdMarca, A.IdCategoria, ImagenUrl, Precio, A.Id from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdCategoria = C.Id and A.IdMarca = M.Id ";

                if(campo == "Precio")
                {
                    if (criterio == "Mayor a")
                    {
                        consulta += "and Precio > " + filtrar;
                    }else if (criterio == "Menor a")
                    {
                        consulta += "and Precio < " + filtrar;
                    }else
                    {
                        consulta += "and Precio = " + filtrar;
                    }

                }else if (campo == "Categoria")
                {
                    if (criterio == "Comienza con")
                    {
                        consulta += "and C.Descripcion like '" + filtrar + "%' ";
                    }
                    else if (criterio == "Termina con")
                    {
                        consulta += "and C.Descripcion like '%" + filtrar +"'";
                    }
                    else
                    {
                        consulta += "and C.Descripcion like '%" + filtrar + "%'";
                    }
                }
                else
                {
                    if (criterio == "Comienza con")
                    {
                        consulta += "and M.Descripcion like '" + filtrar + "%' ";
                    }
                    else if (criterio == "Termina con")
                    {
                        consulta += "and M.Descripcion like '%" + filtrar + "'";
                    }
                    else
                    {
                        consulta += "and M.Descripcion like '%" + filtrar + "%'";
                    }
                }
                acceso.Consultar(consulta);
                acceso.Leer();

                while (acceso.Lector.Read())
                {
                    Articulo auxiliar = new Articulo();
                    auxiliar.Id = (int)acceso.Lector["Id"];
                    auxiliar.Codigo = (string)acceso.Lector["Codigo"];
                    auxiliar.Nombre = (string)acceso.Lector["Nombre"];
                    auxiliar.Descripcion = (string)acceso.Lector["Descripcion"];

                    if (!(acceso.Lector["ImagenUrl"] is DBNull))
                    {
                        auxiliar.ImagenUrl = (string)acceso.Lector["ImagenUrl"];
                    }

                    if (!(acceso.Lector["Precio"] is DBNull))
                    {
                        auxiliar.Precio = (System.Decimal)acceso.Lector["Precio"];
                    }
                    else { auxiliar.Precio = (decimal)0.00; }

                    auxiliar.Precio = Math.Round(auxiliar.Precio, 2);
                    auxiliar.Categoria = new Categoria();
                    auxiliar.Categoria.Id = (int)acceso.Lector["IdCategoria"];
                    auxiliar.Categoria.Descripcion = (string)acceso.Lector["Categoria"];
                    auxiliar.Marca = new Marca();
                    auxiliar.Marca.Id = (int)acceso.Lector["IdMarca"];
                    auxiliar.Marca.Descripcion = (string)acceso.Lector["Marca"];

                    lista.Add(auxiliar);
                }
                    return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
