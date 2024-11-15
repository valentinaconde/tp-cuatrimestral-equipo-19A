using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select id, nombre, activo from categorias");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
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

        public void agregar(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into categorias (nombre) values (@nombre)");
                datos.setearParametro("@nombre", nombre);
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

        public void modificar(int id, string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update categorias set nombre = @nombre where id = @id");
                datos.setearParametro("@nombre", nombre);
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
                datos.setearConsulta("update categorias set activo = 0 where id = @id");

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

        public Categoria buscarCategoriaPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select id, nombre from categorias where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                Categoria aux = new Categoria();

                if (datos.Lector.Read())
                {
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.activo = Convert.ToBoolean(datos.Lector["activo"]);
                }

                datos.cerrarConexion();
                return aux;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
