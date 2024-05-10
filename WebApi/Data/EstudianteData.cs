using WebApi.models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

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
            List<Estudiante> lista = new List<Estudiante>();

            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("ListaEstudiante", con);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Estudiante
                        {
                            IdEstudiante = Convert.ToInt32(reader["IdEstudiante"]),
                            Nombre = reader["Nombre"].ToString(),
                            Edad = Convert.ToInt32(reader["Edad"]),
                            Correo = reader["Correo"].ToString(),
                            CalificacionN = Convert.ToInt32(reader["CalificacionN"]),
                            CalificacionM = Convert.ToInt32(reader["CalificacionM"]),
                            CalificacionS = Convert.ToInt32(reader["CalificacionS"]),
                            CalificacionL = Convert.ToInt32(reader["CalificacionL"]),
                            Calificacion = Convert.ToDecimal(reader["Calificacion"]), // Asegurar que Calificacion se lee correctamente como decimal
                            Curso = reader["Curso"].ToString(),
                        });
                    }
                }
            }
            return lista;
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
                            CalificacionN = Convert.ToInt32(reader["CalificacionN"]),
                            CalificacionM = Convert.ToInt32(reader["CalificacionM"]),
                            CalificacionS = Convert.ToInt32(reader["CalificacionS"]),
                            CalificacionL = Convert.ToInt32(reader["CalificacionL"]),
                            Calificacion = Convert.ToDecimal(reader["Calificacion"]), // Asegurar que Calificacion se lee correctamente como decimal
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
                cmd.Parameters.AddWithValue("@CalificacionN", objeto.CalificacionN);
                cmd.Parameters.AddWithValue("@CalificacionM", objeto.CalificacionM);
                cmd.Parameters.AddWithValue("@CalificacionS", objeto.CalificacionS);
                cmd.Parameters.AddWithValue("@CalificacionL", objeto.CalificacionL);
                cmd.Parameters.AddWithValue("@Curso", objeto.Curso);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    return (await cmd.ExecuteNonQueryAsync()) > 0;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public async Task<bool> Editar(Estudiante objeto)
        {
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("EditarEstudiante", con);
                cmd.Parameters.AddWithValue("@IdEstudiante", objeto.IdEstudiante);
                cmd.Parameters.AddWithValue("@Nombre", objeto.Nombre);
                cmd.Parameters.AddWithValue("@Edad", objeto.Edad);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@CalificacionN", objeto.CalificacionN);
                cmd.Parameters.AddWithValue("@CalificacionM", objeto.CalificacionM);
                cmd.Parameters.AddWithValue("@CalificacionS", objeto.CalificacionS);
                cmd.Parameters.AddWithValue("@CalificacionL", objeto.CalificacionL);
                cmd.Parameters.AddWithValue("@Curso", objeto.Curso);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    return await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<bool> Eliminar(int Id)
        {
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("EliminarEstudiante", con);
                cmd.Parameters.AddWithValue("@IdEstudiante", Id);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    return await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
