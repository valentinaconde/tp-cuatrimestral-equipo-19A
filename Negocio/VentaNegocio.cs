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
                    aux.total = (float)datos.Lector["total"];
                    aux.nroFactura = datos.Lector["numero_factura"].ToString();
                    aux.clienteID = (int)datos.Lector["cliente_id"];
                    aux.usuarioID = (int)datos.Lector["usuario_id"];

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

                string ultimoNumeroFactura = "F00000";
                if (datos.Lector.Read())
                {
                    ultimoNumeroFactura = datos.Lector["numero_factura"].ToString();
                }

                string parteNumerica = ultimoNumeroFactura.Substring(1);
                if (!int.TryParse(parteNumerica, out int numero))
                {
                    throw new FormatException("El formato del número de factura es incorrecto.");
                }

                numero += 1;

                string nuevoNumeroFactura = "F" + numero.ToString("D5");

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

        
        /* Funcion para buscar ventas por id
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
                    aux.Total = (float)datos.Lector["total"];
                    aux.nroFactura = datos.Lector["numero_factura"].ToString();
                    aux.clienteID = (int)datos.Lector["cliente_id"];
                    aux.usuarioID = (int)datos.Lector["usuario_id"];

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
        }*/

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
       
        
    }
}
