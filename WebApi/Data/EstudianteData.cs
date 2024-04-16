using WebApi.models; // Asumiendo que aquí está la definición de la clase Estudiante
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks; // Agregado para usar Task

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
				SqlCommand cmd = new SqlCommand("sp_listaEstudiantes", con);
				cmd.CommandType = CommandType.StoredProcedure;

				using (var reader = await cmd.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						Lista.Add(new Estudiante
						{
							IdEstudiante = Convert.ToInt32(reader["IdEstudiante"]),
							NombreCompleto = reader["NombreCompleto"].ToString(),
							Correo = reader["Correo"].ToString(),
							Materia = reader["Materia"].ToString(),
							FechaInicio = reader["FechaInicio"].ToString(),
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
				SqlCommand cmd = new SqlCommand("sp_ObtenerEstudiantes", con);
				cmd.Parameters.AddWithValue("@IdEstudiante", Id);
				cmd.CommandType = CommandType.StoredProcedure;

				using (var reader = await cmd.ExecuteReaderAsync())
				{
					while (await reader.ReadAsync())
					{
						objeto = new Estudiante
						{
							IdEstudiante = Convert.ToInt32(reader["IdEstudiante"]),
							NombreCompleto = reader["NombreCompleto"].ToString(),
							Correo = reader["Correo"].ToString(),
							Materia = reader["Materia"].ToString(),
							FechaInicio = reader["FechaInicio"].ToString(),
						};
					}
				}
			}
			return objeto;
		}

        public async Task<bool> Crear(Estudiante objeto)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_crearEstudiante", con);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@Materia", objeto.Materia);
                cmd.Parameters.AddWithValue("@FechaInicio", objeto.FechaInicio);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(Estudiante Objeto)
		{
			bool respuesta = true;

			using (var con = new SqlConnection(conexion))
			{
				await con.OpenAsync();
				SqlCommand cmd = new SqlCommand("sp_EditarEstudiante", con);
				cmd.Parameters.AddWithValue("@IdEstudiante", Objeto.IdEstudiante);
				cmd.Parameters.AddWithValue("@NombreCompleto", Objeto.NombreCompleto);
				cmd.Parameters.AddWithValue("@Correo", Objeto.Correo);
				cmd.Parameters.AddWithValue("@Materia", Objeto.Materia);
				cmd.Parameters.AddWithValue("@FechaInicio", Objeto.FechaInicio);
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
                SqlCommand cmd = new SqlCommand("sp_eliminarEstudiante", con);
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
