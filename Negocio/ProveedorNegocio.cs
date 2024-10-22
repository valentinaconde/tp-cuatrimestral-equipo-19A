using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ProveedorNegocio
    {

        public List<Proveedor> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Proveedor> lista = new List<Proveedor>();
            

            try
            {
                datos.setearConsulta("select id, nombre, cuit, direccion, telefono, email from proveedores");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.cuit = datos.Lector["cuit"].ToString();
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

        public void agregar(string nombre, string cuit, string direccion, string telefono, string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into proveedores (nombre, cuit, direccion, telefono, email) values (@nombre, @cuit, @direccion, @telefono, @correo)");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@cuit", cuit);
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

        public void modificar(int id, string nombre, string cuit, string direccion, string telefono, string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update proveedores set nombre = @nombre, apellido = @apellido, direccion = @direccion, telefono = @telefono, email = @correo where id = @id");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@cuit",cuit);
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

                datos.setearConsulta("delete from proveedores where id = @id");
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

        public Usuario buscarUsuario(string email)
        {
            Usuario usuario = new Usuario();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select id, nombre, apellido, email, password, rol_id from usuarios where email = @email");
                datos.setearParametro("@email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.id = (int)datos.Lector["id"];
                    usuario.nombre = datos.Lector["nombre"].ToString();
                    usuario.apellido = datos.Lector["apellido"].ToString();
                    usuario.email = datos.Lector["email"].ToString();
                    usuario.password = datos.Lector["password"].ToString();
                    usuario.rol_id = (int)datos.Lector["rol_id"];
                }

                datos.cerrarConexion();
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;


            }
        }
    }
}

