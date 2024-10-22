using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;


namespace Negocio
{
    public class ClienteNegocio
    {

        public List<Cliente> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Cliente> lista = new List<Cliente>();


            try
            {
                datos.setearConsulta("select * from clientes");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.apellido = datos.Lector["apellido"].ToString();
                    aux.direccion = datos.Lector["direccion"].ToString();
                    aux.telefono = datos.Lector["telefono"].ToString();
                    aux.email = datos.Lector["email"].ToString();


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

        public void agregar(string nombre, string apellido, string direccion, string telefono, string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into clientes (nombre, apellido, direccion, telefono, email) values (@nombre, @apellido, @direccion, @telefono, @correo)");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@apellido", apellido);
                datos.setearParametro("@direccion", direccion);
                datos.setearParametro("@telefono", telefono);
                datos.setearParametro("@correo", email);
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

        public void modificar(int id, string nombre, string apellido, string direccion, string telefono, string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update clientes set nombre = @nombre, apellido = @apellido, direccion = @direccion, telefono = @telefono, email = @correo where id = @id");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@apellido", apellido);
                datos.setearParametro("@direccion", direccion);
                datos.setearParametro("@telefono", telefono);
                datos.setearParametro("@correo", email);
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
                ///revisar: datos.setearConsulta("delete from proveedores where id = @id");
                

                
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
