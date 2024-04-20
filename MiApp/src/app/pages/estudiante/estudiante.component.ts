import { Component, Input, OnInit, inject } from '@angular/core';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {FormBuilder,FormGroup, ReactiveFormsModule,} from '@angular/forms';
import { EstudianteService } from '../../services/estudiante.service';
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
}
