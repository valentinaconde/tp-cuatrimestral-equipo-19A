using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class VentaNegocio
    {
        public List<Venta> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Venta> lista = new List<Venta>();


            try
            {
                datos.setearConsulta("SELECT id, fecha,total, numero_factura, cliente_id, usuario_id from VENTAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Venta aux = new Venta();

                    aux.id = (int)datos.Lector["id"];
                    aux.fecha = (DateTime)datos.Lector["fecha"];
                    aux.total = Convert.ToSingle(datos.Lector["total"]);
                    aux.numero_factura = datos.Lector["numero_factura"].ToString();
                    aux.cliente_id = (int)datos.Lector["cliente_id"];
                    aux.usuario_id = (int)datos.Lector["usuario_id"];

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
        public string numeroFacturaAleatorio()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT TOP 1 numero_factura FROM ventas ORDER BY id DESC");
                datos.ejecutarLectura();

                string ultimoNumeroFactura = "00000";
                if (datos.Lector.Read())
                {
                    ultimoNumeroFactura = datos.Lector["numero_factura"].ToString();
                }

                if (!int.TryParse(ultimoNumeroFactura, out int numero))
                {
                    throw new FormatException("El formato del número de factura es incorrecto.");
                }

                numero += 1;

                string nuevoNumeroFactura = numero.ToString("D5");

                return nuevoNumeroFactura;
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
        public void agregar(DateTime fecha, float total, int clienteID, int usuarioID, List<DetalleVenta> detalles)
        {
            string nAleatorio = numeroFacturaAleatorio();

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ventas (fecha, total, cliente_id, usuario_id, numero_factura) VALUES (@Fecha, @Total, @ClienteId, @UsuarioId, @NumeroFactura); SELECT SCOPE_IDENTITY();");
                datos.setearParametro("@Fecha", fecha);
                datos.setearParametro("@Total", total);
                datos.setearParametro("@ClienteId", clienteID);
                datos.setearParametro("@UsuarioId", usuarioID);
                datos.setearParametro("@NumeroFactura", nAleatorio);

                datos.abrirConexion();
                datos.Comando.Connection = datos.Conexion;
                int idVenta = Convert.ToInt32(datos.Comando.ExecuteScalar());
                datos.cerrarConexion();

                datos.setearConsulta("INSERT INTO detalle_ventas (venta_id, producto_id, cantidad, precio_unitario) VALUES (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario)");

                foreach (var detalle in detalles)
                {
                    datos.Comando.Parameters.Clear();
                    datos.setearParametro("@VentaId", idVenta);
                    datos.setearParametro("@ProductoId", detalle.Producto.id);
                    datos.setearParametro("@Cantidad", detalle.Cantidad);
                    datos.setearParametro("@PrecioUnitario", detalle.PrecioUnitario);
                    datos.ejecutarAccion();
                }


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


        public void modificar(DateTime fecha, float total, string nroFactura, int clienteID, int usuarioID)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ventas SET fecha = @Fecha, total = @Total, numero_factura = @NumeroFactura, cliente_id = @ClienteId, usuario_id = @UsuarioId WHERE id = @ID");
      
                datos.setearParametro("@Fecha", fecha);
                datos.setearParametro("@Total", total);
                datos.setearParametro("@NumeroFactura", nroFactura);
                datos.setearParametro("@ClienteId", clienteID);
                datos.setearParametro("@UsuarioId", usuarioID);
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

        
        public Venta BuscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Venta aux = null;
            try
            {
                datos.setearConsulta("SELECT * FROM ventas WHERE id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();


                if (datos.Lector.Read())
                {
                    aux = new Venta();

                    aux.id = (int)datos.Lector["id"];
                    aux.fecha = (DateTime)datos.Lector["fecha"];
                    aux.total = Convert.ToSingle(datos.Lector["total"]);
                    aux.numero_factura = datos.Lector["numero_factura"].ToString();
                    aux.cliente_id = (int)datos.Lector["cliente_id"];
                    aux.usuario_id = (int)datos.Lector["usuario_id"];

                }

                return aux;
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
                datos.setearConsulta("delete from ventas where id = @id");
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

            public List<DetalleVenta> listarDetallesFactura(string numeroFactura)
            {
                AccesoDatos datos = new AccesoDatos();
                List<DetalleVenta> detalles = new List<DetalleVenta>();

                try
                {
                    //datos.setearConsulta("SELECT id FROM ventas WHERE numero_factura = @numeroFactura");
                    //datos.setearParametro("@numeroFactura", numeroFactura);
                    //datos.ejecutarLectura();

                    //int ventaId = 0;
                    //if (datos.Lector.Read())
                    //{
                    //    ventaId = (int)datos.Lector["id"];
                    //}
                    //else
                    //{
                    //    throw new Exception("No se encontró una venta con el número de factura proporcionado.");
                    //}

                    datos.setearConsulta("SELECT dv.producto_id, dv.cantidad, dv.precio_unitario, p.nombre FROM detalle_ventas dv INNER JOIN productos p ON dv.producto_id = p.id WHERE dv.venta_id = @ventaId");
                    datos.setearParametro("@ventaId", numeroFactura);
                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {
                        DetalleVenta detalle = new DetalleVenta();
                        detalle.Producto = new Producto
                        {
                            id = (int)datos.Lector["producto_id"],
                            nombre = datos.Lector["nombre"].ToString()
                        };
                        detalle.Cantidad = (int)datos.Lector["cantidad"];
                        detalle.PrecioUnitario = Convert.ToSingle(datos.Lector["precio_unitario"]);

                        detalles.Add(detalle);
                    }

                    return detalles;
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
