using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> listar()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select id, nombre, stock_actual, stock_minimo, porcentaje_ganancia, marca_id, categoria_id from productos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.stockactual = (int)datos.Lector["stock_actual"];
                    aux.stockminimo = (int)datos.Lector["stock_minimo"];
                    aux.ganancia = Convert.ToSingle(datos.Lector["porcentaje_ganancia"]);
                    aux.idmarca = (int)datos.Lector["marca_id"];
                    aux.idcategoria = (int)datos.Lector["categoria_id"];
                    lista.Add(aux);
                }

                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void agregar(string nombre, int stock, float ganancia, int idMarca, int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into productos (nombre, stock, ganancia, idmarca, idcategoria) values (@nombre, @stock, @ganancia, @idmarca, @idcategoria)");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@stock", stock);
                datos.setearParametro("@ganancia", ganancia);
                datos.setearParametro("@idmarca", idMarca);
                datos.setearParametro("@idcategoria", idCategoria);
                datos.ejecutarAccion();
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

        public void modificar(int id, string nombre, int stock, float ganancia, int idMarca, int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update productos set nombre = @nombre, stock = @stock, ganancia = @ganancia, idmarca = @idmarca, idcategoria = @idcategoria where id = @id");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@stock", stock);
                datos.setearParametro("@ganancia", ganancia);
                datos.setearParametro("@idmarca", idMarca);
                datos.setearParametro("@idcategoria", idCategoria);
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearParametro("@id", id);
                datos.setearConsulta("delete from productos where id = @id");
                datos.ejecutarAccion();
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
