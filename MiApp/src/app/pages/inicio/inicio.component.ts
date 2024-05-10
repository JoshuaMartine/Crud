import { Component, OnInit } from '@angular/core';
import { EstudianteService } from '../../services/estudiante.service';
import { Estudiante } from '../../models/Estudiante';
import { Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';

@Component({
  standalone: true,
  imports: [MatCardModule, MatTableModule, MatIconModule, MatButtonModule],
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css'],
})
export class InicioComponent implements OnInit {

  public listaEstudiante: Estudiante[] = [];
  // Agregando las columnas de las nuevas calificaciones
  public displayedColumns: string[] = ['Nombre', 'Edad', 'Correo', 'CalificacionN', 'CalificacionM', 'CalificacionS', 'CalificacionL', 'Calificacion', 'Curso', 'accion'];

  constructor(private estudianteService: EstudianteService, private router: Router) { }

  ngOnInit(): void {
    this.obtenerEstudiante();
  }

  obtenerEstudiante() {
    this.estudianteService.lista().subscribe({
      next: (data) => {
        if (data.length > 0) {
          this.listaEstudiante = data;
          console.log(this.listaEstudiante);
        }
      },
      error: (err) => {
        console.log(err.message);
      }
    });
  }

  nuevo() {
    this.router.navigate(['/estudiante', 0]);
  }

  editar(estudiante: Estudiante) {
    this.router.navigate(['/estudiante', estudiante.idEstudiante]);
  }

  eliminar(estudiante: Estudiante) {
    if (confirm("¿Desea eliminar el estudiante " + estudiante.nombre + "?" + estudiante.idEstudiante)) {
      this.estudianteService.Eliminar(estudiante.idEstudiante).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this.obtenerEstudiante();
          } else {
            alert("No se pudo eliminar el estudiante.");
          }
        },
        error: (err) => {
          console.log(err.message);
        }
      });
    }
  }
}
