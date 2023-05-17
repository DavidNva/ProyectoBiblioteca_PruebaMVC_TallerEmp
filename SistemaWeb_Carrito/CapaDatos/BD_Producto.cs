using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data; /*Acceso a sql conections*/
using System.Globalization;

namespace CapaDatos
{
    public class BD_Producto
    {
        public List<EN_Producto> Listar()
        {
            List<EN_Producto> lista = new List<EN_Producto>();
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder(); //Permite hacer saltos de linea

                    sb.AppendLine("select P.IdLibro, p.Titulo, p.Descripcion,");
                    sb.AppendLine("m.IDAutor, m.Nombre,");
                    sb.AppendLine("c.IDCategoria, c.Descripcion [DesCategoria],");
                    sb.AppendLine("p.Paginas, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo");
                    sb.AppendLine("from Libro p");
                    sb.AppendLine("inner join Autor m on m.IDAutor = p.IdAutor");
                    sb.AppendLine("inner join Categoria c on c.IDCategoria = p.IdCategoria");


                    SqlCommand cmd = new SqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;/*En este caso es de tipo Text (no usamos para este ejemplo, procedimientos almacenados*/

                    oConexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())/*Lee todos los resultados que aparecen en la ejecucion del select anter ior*/
                    {
                        while (dr.Read())/*Mientras reader esta leyendo, ira agregando a la lista dicha lectura*/
                        {
                            lista.Add(/*Agrega una nueva Producto la lista*/
                                new EN_Producto()
                                {
                                    IdLibro = Convert.ToInt32(dr["IdLibro"]),
                                    Titulo = dr["Titulo"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    //Lo primero es una instancia de EN_Marca
                                    //lo segundo es ya de los valores de esa instancia
                                    oId_Marca = new EN_Marca() { IdAutor = Convert.ToInt32(dr["IDAutor"]), Nombre = dr["Nombre"].ToString() },
                                    oId_Categoria = new EN_Categoria() { IdCategoria = Convert.ToInt32(dr["IDCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
                                    Paginas = Convert.ToInt32(dr["Paginas"]),//Indica que los decimales los trabaje con puntos
                                    Stock = Convert.ToInt32(dr["Stock"]), //Se referencio a usyng globalizacion para Culture Info
                                    RutaImagen = dr["RutaImagen"].ToString(),
                                    NombreImagen = dr["NombreImagen"].ToString(),
                                    Activo = Convert.ToBoolean(dr["Activo"])
                                });
                        }
                    }
                }
            }
            catch (Exception)
            {
                lista = new List<EN_Producto>();
            }

            return lista;
        }

        public int Registrar(EN_Producto obj, out string Mensaje)//out indica parametro de salida
        {
            int IdAutogenerado = 0; /*Recibe el id autogenerado*/

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarLibro", oConexion);
                    /*Los primeros valores de estos parametros, es la del procedimiento del sql y el segundo de donde toma el valor (las propieaddes de la clase EN_Producto*/
                    cmd.Parameters.AddWithValue("Titulo", obj.Titulo);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IDAutor", obj.oId_Marca.IdAutor);
                    cmd.Parameters.AddWithValue("IDCategoria", obj.oId_Categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Paginas", obj.Paginas);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    //Dos parametros de salida, un entero de resultaado y un string de mensaje
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    IdAutogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                IdAutogenerado = 0;/*Regresa a 0*/
                Mensaje = ex.Message;

            }
            return IdAutogenerado; /*Si cambia a un nuevo valor al agregar un nuevo id*/

        }

        public bool Editar(EN_Producto obj, out string Mensaje)//out indica parametro de salida
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarLibro", oConexion);
                    cmd.Parameters.AddWithValue("IdLibro", obj.IdLibro);
                    cmd.Parameters.AddWithValue("Titulo", obj.Titulo);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IDAutor", obj.oId_Marca.IdAutor);
                    cmd.Parameters.AddWithValue("IDCategoria", obj.oId_Categoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Paginas", obj.Paginas);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    //Dos parametros de salida, un entero de resultaado y un string de mensaje
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;

            }
            return resultado;
        }


        public bool GuardarDatosImagen(EN_Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    string query = "update Libro set RutaImagen = @rutaImagen, NombreImagen = @nombreImagen where IdLibro = @idLibro";

                    SqlCommand cmd = new SqlCommand(query, oConexion);
                    cmd.Parameters.AddWithValue("@rutaImagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue("@nombreImagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("@idLibro", obj.IdLibro);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true; /*Si la query realiza una accion, mayor de 0*/
                    }
                    else
                    {
                        Mensaje = "No se pudo actualizar imagen";
                    }
                   
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;

            }
            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)//out indica parametro de salida
        {
            bool resultado = false;

            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarLibro", oConexion);
                    cmd.Parameters.AddWithValue("IdLibro", id);
                    //Dos parametros de salida, un entero de resultaado y un string de mensaje
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;

            }
            return resultado;
        }
    }
}
