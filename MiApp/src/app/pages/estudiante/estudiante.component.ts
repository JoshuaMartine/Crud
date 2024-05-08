import { Component, Input, OnInit, inject } from '@angular/core';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {FormBuilder,FormGroup, ReactiveFormsModule,} from '@angular/forms';
import { EstudianteService } from '../../services/estudiante.service';
import { Route, Router } from '@angular/router';
import { core } from '@angular/compiler';
import { Estudiante } from '../../models/Estudiante';
@Component({
  selector: 'app-estudiante',
  standalone: true,
  imports: [MatFormFieldModule,MatInputModule,MatButtonModule,ReactiveFormsModule],
  templateUrl: './estudiante.component.html',
  styleUrl: './estudiante.component.css'
})
export class estudianteComponent implements OnInit {

  @Input('id')idestudiante! : number;
  private estudianteService = inject(EstudianteService)
  public formBuild = inject(FormBuilder);

  public formEstudiante:FormGroup =this.formBuild.group({
    nombre :[''],
    edad: [0],
    correo : [''],
    calificacion : [0],
    curso : ['']
  });

  constructor(private router:Router){}

  ngOnInit(): void {
    if(this.idestudiante !=0){
      this.estudianteService.obtener(this.idestudiante).subscribe({
        next:(data)=>{
          this.formEstudiante.patchValue({
            nombre: data.nombre,
            eded: data.edad,
            correo : data.correo,
            calificacion: data.calificacion,
            curso: data.curso
        })
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }
  }

   guardar(){
    const objeto: Estudiante = {
      idEstudiante: this.idestudiante,
      nombre: this.formEstudiante.value.nombre,
      edad: this.formEstudiante.value.edad,
      correo: this.formEstudiante.value.correo,
      calificacion: this.formEstudiante.value.calificacion,
      curso : this.formEstudiante.value.curso
    }
    if(this.idestudiante == 0){
      this.estudianteService.Crear(objeto).subscribe({
        next:(data)=>{
          if(data.isSuccess){
            this.router.navigate(["/"]);
          }else{
            alert("Error al crear")
          
          }
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }else{
      this.estudianteService.editar(objeto).subscribe({
        next:(data)=>{
          if(data.isSuccess){
            this.router.navigate(["/"])
          }else{
            alert("Error al editar")
          
          }
        },
        error:(err)=>{
          console.log(err.message)
        }
      })
    }

   }


   volver(){
    this.router.navigate(["/"]);
  }

}
