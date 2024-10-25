﻿using System;
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

        public void agregar(Cliente cliente)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into clientes (nombre, direccion, telefono, email) values (@nombre, @direccion, @telefono, @correo)");
                datos.setearParametro("@nombre", cliente.nombre);
                datos.setearParametro("@direccion", cliente.direccion);
                datos.setearParametro("@telefono", cliente.telefono);
                datos.setearParametro("@correo", cliente.email);
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
                datos.setearConsulta("update clientes set nombre = @nombre, direccion = @direccion, telefono = @telefono, email = @correo where id = @id");
                datos.setearParametro("@nombre", cliente.nombre);
                datos.setearParametro("@direccion", cliente.direccion);
                datos.setearParametro("@telefono", cliente.telefono);
                datos.setearParametro("@correo", cliente.email);
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

                datos.setearConsulta("delete from clientes where id = @id");
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
                    cliente = new Cliente();
                    cliente.id = (int)datos.Lector["id"];
                    cliente.nombre = datos.Lector["nombre"].ToString();
                    cliente.direccion = datos.Lector["direccion"].ToString();
                    cliente.telefono = datos.Lector["telefono"].ToString();
                    cliente.email = datos.Lector["email"].ToString();
                }

                datos.cerrarConexion();
                return cliente;
            }
            catch (Exception ex)
            {
                throw ex;


            }
        }



        }
}
