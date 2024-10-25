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
                datos.setearConsulta("select id, nombre, direccion, telefono, email from proveedores");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
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

        public void agregar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into proveedores (nombre, direccion, telefono, email) values (@nombre, @direccion, @telefono, @correo)");
                datos.setearParametro("@nombre", proveedor.nombre);
                datos.setearParametro("@direccion", proveedor.direccion);
                datos.setearParametro("@telefono", proveedor.telefono);
                datos.setearParametro("@correo", proveedor.email);
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

        public void modificar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update proveedores set nombre = @nombre, direccion = @direccion, telefono = @telefono, email = @correo where id = @id");
                datos.setearParametro("@nombre", proveedor.nombre);
                datos.setearParametro("@direccion", proveedor.direccion);
                datos.setearParametro("@telefono", proveedor.telefono);
                datos.setearParametro("@correo", proveedor.email);
                datos.setearParametro("@id", proveedor.id);
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

        public Proveedor buscarProveedorPorId(int id)
        {
            Proveedor proveedor = new Proveedor();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("select * from proveedores where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    proveedor = new Proveedor();
                    proveedor.id = (int)datos.Lector["id"];
                    proveedor.nombre = datos.Lector["nombre"].ToString();
                    proveedor.direccion = datos.Lector["direccion"].ToString();
                    proveedor.telefono = datos.Lector["telefono"].ToString();
                    proveedor.email = datos.Lector["email"].ToString();
                }

                datos.cerrarConexion();
                return proveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}

