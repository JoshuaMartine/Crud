using WebApi.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Data
{
    public class EstudianteData
    {
        private readonly string conexion;

        public EstudianteData(IConfiguration configuration)
        {
            conexion = configuration.GetConnectionString("CadenaSQL")!;
        }

        public async Task<List<Estudiante>> Lista()
        {
            List<Estudiante> Lista = new List<Estudiante>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("ListaEstudiante", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Lista.Add(new Estudiante
                        {
                            IdEstudiante = Convert.ToInt32(reader["IdEstudiante"]),
                            Nombre = reader["Nombre"].ToString(),
                            Edad = Convert.ToInt32(reader["Edad"]),
                            Correo = reader["Correo"].ToString(),
                            Calificacion = Convert.ToInt32(reader["Calificacion"]),
                            Curso = reader["Curso"].ToString(),
                        });
                    }
                }
            }
            return Lista;
        }

        public async Task<Estudiante> Obtener(int Id)
        {
            Estudiante objeto = new Estudiante();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("ObtenerEstudiante", con);
                cmd.Parameters.AddWithValue("@IdEstudiante", Id);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Estudiante
                        {
                            IdEstudiante = Convert.ToInt32(reader["IdEstudiante"]),
                            Nombre = reader["Nombre"].ToString(),
                            Edad = Convert.ToInt32(reader["Edad"]),
                            Correo = reader["Correo"].ToString(),
                            Calificacion = Convert.ToInt32(reader["Calificacion"]),
                            Curso = reader["Curso"].ToString(),
                        };
                    }
                }
            }
            return objeto;
        }

        public async Task<bool> Crear(Estudiante objeto)
        {
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("CrearEstudiante", con);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@Edad", objeto.Edad);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@Calificacion", objeto.Calificacion);
                cmd.Parameters.AddWithValue("@Curso", objeto.Curso);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    return (await cmd.ExecuteNonQueryAsync()) > 0;
                }
                catch (Exception ex)
                {
                    // Considere loguear la excepción o devolver el mensaje de error
                    Console.Error.WriteLine(ex.Message); // Ejemplo de log
                    return false;
                }
            }
        }

        public async Task<bool> Editar(Estudiante Objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("EditarEstudiante", con);
                cmd.Parameters.AddWithValue("@IdEstudiante", Objeto.IdEstudiante);
                cmd.Parameters.AddWithValue("@Nombre", Objeto.Nombre);
                cmd.Parameters.AddWithValue("@Edad", Objeto.Edad);
                cmd.Parameters.AddWithValue("@Correo", Objeto.Correo);
                cmd.Parameters.AddWithValue("@Calificacion", Objeto.Calificacion);
                cmd.Parameters.AddWithValue("@Curso", Objeto.Curso);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int Id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("EliminarEstudiante", con);
                cmd.Parameters.AddWithValue("@IdEstudiante", Id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
