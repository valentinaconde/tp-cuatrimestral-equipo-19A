using Dominio;
using System;
using System.Collections;
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
                datos.setearConsulta("select id, nombre, stock_actual, precio_unitario, porcentaje_ganancia, marca_id, categoria_id, activo from productos");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.stockactual = (int)datos.Lector["stock_actual"];
                    aux.precio_unitario = (int)datos.Lector["precio_unitario"];
                    aux.ganancia = Convert.ToSingle(datos.Lector["porcentaje_ganancia"]);
                    aux.idmarca = (int)datos.Lector["marca_id"];
                    aux.idcategoria = (int)datos.Lector["categoria_id"];
                    aux.activo = Convert.ToBoolean(datos.Lector["activo"]);
                    if (aux.activo == true) lista.Add(aux);
                }

                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void agregar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into productos (nombre, stock_actual, precio_unitario, porcentaje_ganancia, marca_id, categoria_id, activo) values (@nombre, @stock_actual, @precio_unitario, @porcentaje_ganancia, @marca_id, @categoria_id, @activo)");
                datos.setearParametro("@nombre", producto.nombre);
                datos.setearParametro("@stock_actual", producto.stockactual);
                datos.setearParametro("@precio_unitario", producto.precio_unitario);
                datos.setearParametro("@porcentaje_ganancia", producto.ganancia);
                datos.setearParametro("@marca_id", producto.idmarca);
                datos.setearParametro("@categoria_id", producto.idcategoria);
                datos.setearParametro("@activo", producto.activo);
                datos.ejecutarAccion();
                datos.Comando.Parameters.Clear();
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

        public void modificar(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update productos set nombre = @nombre, stock_actual = @stock_actual, precio_unitario = @precio_unitario, porcentaje_ganancia = @porcentaje_ganancia, marca_id = @marca_id, categoria_id = @categoria_id, activo = @activo where id = @id");
                datos.setearParametro("@nombre", producto.nombre);
                datos.setearParametro("@stock_actual", producto.stockactual);
                datos.setearParametro("@precio_unitario", producto.precio_unitario);
                datos.setearParametro("@porcentaje_ganancia", producto.ganancia);
                datos.setearParametro("@marca_id", producto.idmarca);
                datos.setearParametro("@categoria_id", producto.idcategoria);
                datos.setearParametro("@activo", producto.activo);
                datos.setearParametro("@id", producto.id);
                datos.ejecutarAccion();
                datos.Comando.Parameters.Clear();
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
                datos.setearConsulta("update productos set activo = 0 where id = @id");

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

        public Producto buscarProductoPorId(int id)
        {
            Producto producto = new Producto();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select * from productos where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    producto = new Producto();
                    producto.id = (int)datos.Lector["id"];
                    producto.nombre = datos.Lector["nombre"].ToString();
                    producto.stockactual = (int)datos.Lector["stock_actual"];
                    producto.precio_unitario = (int)datos.Lector["precio_unitario"];
                    producto.ganancia = Convert.ToSingle(datos.Lector["porcentaje_ganancia"]);
                    producto.idmarca = (int)datos.Lector["marca_id"];
                    producto.idcategoria = (int)datos.Lector["categoria_id"];
                    producto.activo = Convert.ToBoolean(datos.Lector["activo"]);
                }

                datos.cerrarConexion();
                return producto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Producto buscarProductoPorNombreYMarca(string nombre, int marcaId)
        {
            Producto producto = new Producto();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select * from productos where nombre = @nombre and marca_id = @marca_id");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@marca_id", marcaId);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    producto = new Producto();
                    producto.id = (int)datos.Lector["id"];
                    producto.nombre = datos.Lector["nombre"].ToString();
                    producto.stockactual = (int)datos.Lector["stock_actual"];
                    producto.precio_unitario = (int)datos.Lector["precio_unitario"];
                    producto.ganancia = Convert.ToSingle(datos.Lector["porcentaje_ganancia"]);
                    producto.idmarca = (int)datos.Lector["marca_id"];
                    producto.idcategoria = (int)datos.Lector["categoria_id"];
                    producto.activo = Convert.ToBoolean(datos.Lector["activo"]);
                }

                datos.cerrarConexion();
                return producto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void activarProducto(string nombre, int marcaId)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update productos set activo = 1 where nombre = @nombre and marca_id = @marca_id");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@marca_id", marcaId);
                datos.ejecutarAccion();
                datos.Comando.Parameters.Clear();
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
