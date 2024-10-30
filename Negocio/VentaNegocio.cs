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

        public void agregar(DateTime fecha, float total, string nroFactura, int clienteID, int usuarioID)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ventas (fecha, total, numero_factura, cliente_id, usuario_id) VALUES (@Fecha, @Total, @NumeroFactura, @ClienteId, @UsuarioId); SELECT SCOPE_IDENTITY();");
                datos.setearParametro("@Fecha", fecha);
                datos.setearParametro("@Total", total);
                datos.setearParametro("@NumeroFactura", nroFactura);
                datos.setearParametro("@ClienteId",clienteID);
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
