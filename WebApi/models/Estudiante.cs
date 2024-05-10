namespace WebApi.models
{
    public class Estudiante
    {
        public int IdEstudiante { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }
        // Calificaciones individuales
        public int CalificacionN { get; set; }
        public int CalificacionM { get; set; }
        public int CalificacionS { get; set; }
        public int CalificacionL { get; set; }
        // Promedio de calificaciones como decimal
        public decimal Calificacion { get; set; }
        public string Curso { get; set; }
    }
}
