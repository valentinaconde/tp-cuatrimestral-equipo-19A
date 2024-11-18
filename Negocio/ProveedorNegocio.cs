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
                datos.setearConsulta("select id, nombre, direccion, telefono, email, cuit, activo from proveedores");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.id = (int)datos.Lector["id"];
                    aux.nombre = datos.Lector["nombre"].ToString();
                    aux.direccion = datos.Lector["direccion"].ToString();
                    aux.telefono = datos.Lector["telefono"].ToString();
                    aux.email = datos.Lector["email"].ToString();
                    aux.cuit = datos.Lector["cuit"].ToString();
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

        public void agregar(Proveedor proveedor)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into proveedores (nombre, direccion, telefono, email, cuit, activo) values (@nombre, @direccion, @telefono, @correo, @cuit, @activo)");
                datos.setearParametro("@nombre", proveedor.nombre);
                datos.setearParametro("@direccion", proveedor.direccion);
                datos.setearParametro("@telefono", proveedor.telefono);
                datos.setearParametro("@correo", proveedor.email);
                datos.setearParametro("@cuit", proveedor.cuit);
                datos.setearParametro("@activo", proveedor.activo);
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
                datos.setearConsulta("update proveedores set nombre = @nombre, direccion = @direccion, telefono = @telefono, email = @correo, cuit = @cuit, activo = @activo where id = @id");
                datos.setearParametro("@nombre", proveedor.nombre);
                datos.setearParametro("@direccion", proveedor.direccion);
                datos.setearParametro("@telefono", proveedor.telefono);
                datos.setearParametro("@correo", proveedor.email);
                datos.setearParametro("@cuit", proveedor.cuit);
                datos.setearParametro("@activo", proveedor.activo);
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
                datos.setearConsulta("update proveedores set activo = 0 where id = @id");
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
                    proveedor.id = (int)datos.Lector["id"];
                    proveedor.nombre = datos.Lector["nombre"].ToString();
                    proveedor.direccion = datos.Lector["direccion"].ToString();
                    proveedor.telefono = datos.Lector["telefono"].ToString();
                    proveedor.email = datos.Lector["email"].ToString();
                    proveedor.cuit = datos.Lector["cuit"].ToString();
                    proveedor.activo = (bool)datos.Lector["activo"];
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
