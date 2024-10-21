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
                    aux.rol_id = datos.Lector["rol_id"].ToString();

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

        public void agregar(string nombre, string apellido, string email, string password, string rol_id)
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

        public void modificar(int id, string nombre, string apellido, string email, string password, string rol_id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update usuarios set nombre = @nombre, apellido = @apellido, email = @correo, password = @password, rol_id = @rol_id where id = @id");
                datos.setearParametro("@nombre", nombre);
                datos.setearParametro("@apellido", apellido);
                datos.setearParametro("@correo", email);
                datos.setearParametro("@password", password);
                datos.setearParametro("@rol_id", rol_id);
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
    }
}
