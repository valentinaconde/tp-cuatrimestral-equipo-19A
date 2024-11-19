using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select id, nombre, apellido, email, password, rol_id from usuarios");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.apellido = datos.Lector["apellido"].ToString();
                    aux.email = datos.Lector["email"].ToString();
                    aux.password = datos.Lector["password"].ToString();
                    aux.rol_id = (int)datos.Lector["rol_id"];
                    

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

        public void agregar(string nombre, string apellido, string email, string password, int rol_id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into usuarios (nombre, apellido, email, password, rol_id) values (@nombre, @apellido, @correo, @password, @rol_id)");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@apellido", apellido);
                datos.setearParametro("@correo", email);
                datos.setearParametro("@password", password);
                datos.setearParametro("@rol_id", rol_id);
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

        public void modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update usuarios set nombre = @nombre, apellido = @apellido, email = @correo, password = @password, rol_id = @rol_id where id = @id");
                datos.setearParametro("@nombre", usuario.nombre);
                datos.setearParametro("@apellido", usuario.apellido);
                datos.setearParametro("@correo", usuario.email);
                datos.setearParametro("@password", usuario.password);
                datos.setearParametro("@rol_id", usuario.rol_id);
                datos.setearParametro("@id", usuario.id);
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

                datos.setearConsulta("delete from usuarios where id = @id");
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

        public Usuario buscarUsuarioPorEmail(string email)
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

        public Usuario buscarUsuarioPorId(int id)
        {
            Usuario usuario = new Usuario();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select id, nombre, apellido, email, password, rol_id from usuarios where id = @id");
                datos.setearParametro("@id", id);
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

        public void activarUsuario(string email)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update usuarios set activo = 1 where email = @email");
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
