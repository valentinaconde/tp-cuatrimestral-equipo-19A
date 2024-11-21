using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> listar()
        {
            AccesoDatos datos = new AccesoDatos();
            List<Marca> lista = new List<Marca>();

            try
            {
                datos.setearConsulta("select id, nombre, activo from marcas order by id desc");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.activo = Convert.ToBoolean(datos.Lector["activo"]);
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

        public void agregar(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into marcas (nombre) values (@nombre)");
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
                datos.setearConsulta("update marcas set nombre = @nombre where id = @id");
                datos.setearParametro("@id", id);
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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update marcas set activo = 0 where id = @id");
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
        public Marca buscarMarcaPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select id, nombre, activo from marcas where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                Marca aux = new Marca();

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

        public Marca buscarMarcaPorNombre(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("select id, nombre, activo from marcas where LOWER(nombre) = LOWER(@nombre)");
                datos.setearParametro("@nombre", nombre.Trim());
                datos.ejecutarLectura();

                Marca aux = new Marca();

                if (datos.Lector.Read())
                {
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.activo = (bool)datos.Lector["activo"];
                }

                datos.cerrarConexion();
                return aux;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void activarMarca(string nombre)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update marcas set activo = 1 where nombre = @nombre");
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
    }
}
 

