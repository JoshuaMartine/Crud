import { Component, Input, OnInit, inject } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { EstudianteService } from '../../services/estudiante.service';
import { Router } from '@angular/router';
import { Estudiante } from '../../models/Estudiante';

@Component({
  selector: 'app-estudiante',
  standalone: true,
  imports: [MatFormFieldModule, MatInputModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './estudiante.component.html',
  styleUrls: ['./estudiante.component.css']
})
export class estudianteComponent implements OnInit {

  @Input('id') idEstudiante!: number;
  private estudianteService = inject(EstudianteService);
  public formBuild = inject(FormBuilder);

  public formEstudiante: FormGroup = this.formBuild.group({
    nombre: [''],
    edad: [0],
    correo: [''],
    calificacionN: [0],
    calificacionM: [0],
    calificacionS: [0],
    calificacionL: [0],
    curso: ['']
  });

  constructor(private router: Router) {}

  ngOnInit(): void {
    if (this.idEstudiante !== 0) {
      this.estudianteService.obtener(this.idEstudiante).subscribe({
        next: (data) => {
          this.formEstudiante.patchValue({
            nombre: data.nombre,
            edad: data.edad,
            correo: data.correo,
            calificacionN: data.calificacionN,
            calificacionM: data.calificacionM,
            calificacionS: data.calificacionS,
            calificacionL: data.calificacionL,
            curso: data.curso
          });
        },
        error: (err) => {
          console.error(err.message);
        }
      });
    }
  }

  guardar(): void {
    const estudiante: Estudiante = {
      idEstudiante: this.idEstudiante,
      nombre: this.formEstudiante.value.nombre,
      edad: this.formEstudiante.value.edad,
      correo: this.formEstudiante.value.correo,
      calificacionN: this.formEstudiante.value.calificacionN,
      calificacionM: this.formEstudiante.value.calificacionM,
      calificacionS: this.formEstudiante.value.calificacionS,
      calificacionL: this.formEstudiante.value.calificacionL,
      calificacion: this.calcularPromedio(),
      curso: this.formEstudiante.value.curso
    };

    if (this.idEstudiante == 0) {
      this.estudianteService.Crear(estudiante).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this.router.navigate(['/']);
          } else {
            alert('Error al crear');
          }
        },
        error: (err) => {
          console.error(err.message);
        }
      });
    } else {
      this.estudianteService.editar(estudiante).subscribe({
        next: (data) => {
          if (data.isSuccess) {
            this.router.navigate(['/']);
          } else {
            alert('Error al editar');
          }
        },
        error: (err) => {
          console.error(err.message);
        }
      });
    }
  }

  calcularPromedio(): number {
    const n = this.formEstudiante.value.calificacionN;
    const m = this.formEstudiante.value.calificacionM;
    const s = this.formEstudiante.value.calificacionS;
    const l = this.formEstudiante.value.calificacionL;
    return (n + m + s + l) / 4;
  }

  volver(): void {
    this.router.navigate(['/']);
  }
}
