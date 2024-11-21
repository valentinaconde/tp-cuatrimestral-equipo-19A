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
                datos.setearConsulta("select * from clientes order by id desc ");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.direccion = datos.Lector["direccion"].ToString();
                    aux.telefono = datos.Lector["telefono"].ToString();
                    aux.email = datos.Lector["email"].ToString();
                    aux.dni = datos.Lector["dni"].ToString();
                    aux.activo = (bool)datos.Lector["activo"];
                    if (aux.activo == true) lista.Add(aux);
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

        public void agregar(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into clientes (nombre, direccion, telefono, email, dni, activo) values (@nombre, @direccion, @telefono, @correo, @dni, @activo)");
                datos.setearParametro("@nombre", cliente.nombre);
                datos.setearParametro("@direccion", cliente.direccion);
                datos.setearParametro("@telefono", cliente.telefono);
                datos.setearParametro("@correo", cliente.email);
                datos.setearParametro("@dni", cliente.dni);
                datos.setearParametro("@activo", cliente.activo);
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

        public void modificar(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update clientes set nombre = @nombre, direccion = @direccion, telefono = @telefono, email = @correo, dni = @dni, activo = @activo where id = @id");
                datos.setearParametro("@nombre", cliente.nombre);
                datos.setearParametro("@direccion", cliente.direccion);
                datos.setearParametro("@telefono", cliente.telefono);
                datos.setearParametro("@correo", cliente.email);
                datos.setearParametro("@dni", cliente.dni);
                datos.setearParametro("@activo", cliente.activo);
                datos.setearParametro("@id", cliente.id);
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
                datos.setearConsulta("update clientes set activo = 0 where id = @id");
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

        public Cliente buscarClientePorId(int id)
        {
            Cliente cliente = new Cliente();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select * from clientes where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    cliente.id = (int)datos.Lector["id"];
                    cliente.nombre = datos.Lector["nombre"].ToString();
                    cliente.direccion = datos.Lector["direccion"].ToString();
                    cliente.telefono = datos.Lector["telefono"].ToString();
                    cliente.email = datos.Lector["email"].ToString();
                    cliente.dni = datos.Lector["dni"].ToString();
                    cliente.activo = (bool)datos.Lector["activo"];
                }

                datos.cerrarConexion();
                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Cliente buscarClientePorDni(string dni)
        {

            Cliente cliente = new Cliente();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select * from clientes where dni = @dni ");
                datos.setearParametro("@dni", dni);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    cliente.id = (int)datos.Lector["id"];
                    cliente.nombre = datos.Lector["nombre"].ToString();
                    cliente.direccion = datos.Lector["direccion"].ToString();
                    cliente.telefono = datos.Lector["telefono"].ToString();
                    cliente.email = datos.Lector["email"].ToString();
                    cliente.dni = datos.Lector["dni"].ToString();
                    cliente.activo = (bool)datos.Lector["activo"];
                }

                datos.cerrarConexion();
                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void activarCliente(string dni, string direccion, string telefono, string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update clientes set activo = 1, direccion = @direccion, telefono = @telefono, email = @email where dni = @dni");
                datos.setearParametro("@dni", dni);
                datos.setearParametro("@direccion", direccion);
                datos.setearParametro("@telefono", telefono);
                datos.setearParametro("@email", email);

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
