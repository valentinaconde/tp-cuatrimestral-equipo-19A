using System;
using System.Collections.Generic;
using Dominio;

namespace Negocio
{
    public class CompraNegocio
    {
        public List<Compra> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Compra> lista = new List<Compra>();

            try
            {
                datos.setearConsulta("SELECT id, fecha, total, proveedor_id FROM compras");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Compra aux = new Compra
                    {
                        id = (int)datos.Lector["id"],
                        fecha = (DateTime)datos.Lector["fecha"],
                        total = (float)datos.Lector["total"],
                        proveedorID = (int)datos.Lector["proveedor_id"]
                    };

                    lista.Add(aux);
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

        public void agregar(DateTime fecha, float total, int proveedorID, List<DetalleCompra> detalles)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO compras (fecha, total, proveedor_id) VALUES (@Fecha, @Total, @ProveedorId); SELECT CAST(SCOPE_IDENTITY() AS int);");
                datos.setearParametro("@Fecha", fecha);
                datos.setearParametro("@Total", total);
                datos.setearParametro("@ProveedorId", proveedorID);

                  datos.abrirConexion();
                datos.Comando.Connection = datos.Conexion;
                int idCompra = Convert.ToInt32(datos.Comando.ExecuteScalar());
                datos.cerrarConexion();

                datos.setearConsulta("INSERT INTO detalle_compras (cantidad, precio_unitario, compra_id, producto_id) VALUES (@Cantidad, @PrecioUnitario, @CompraId, @ProductoId)");
                foreach (var detalle in detalles)
                {

                    datos.Comando.Parameters.Clear();
                    datos.setearParametro("@Cantidad", detalle.Cantidad);
                    datos.setearParametro("@PrecioUnitario", detalle.PrecioUnitario);
                    datos.setearParametro("@CompraId", idCompra);
                    datos.setearParametro("@ProductoId", detalle.ProductoId);

                    datos.ejecutarAccion();
                    
                }
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


        public void modificar(int id, DateTime fecha, float total, int proveedorID)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE compras SET fecha = @Fecha, total = @Total, proveedor_id = @ProveedorId WHERE id = @ID");
                datos.setearParametro("@ID", id);
                datos.setearParametro("@Fecha", fecha);
                datos.setearParametro("@Total", total);
                datos.setearParametro("@ProveedorId", proveedorID);

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
                datos.setearConsulta("DELETE FROM detalle_compras WHERE compra_id = @ID");
                datos.setearParametro("@ID", id);
                datos.ejecutarAccion();

                datos.setearConsulta("DELETE FROM compras WHERE id = @ID");
                datos.setearParametro("@ID", id);
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
