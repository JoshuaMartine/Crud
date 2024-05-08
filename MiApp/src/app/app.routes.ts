import { Routes } from '@angular/router';
import { InicioComponent } from './pages/inicio/inicio.component';
import { estudianteComponent } from './pages/estudiante/estudiante.component';

export const routes: Routes = [
 {path:"",component:InicioComponent},
 {path:'Inicio',component:InicioComponent},
 {path:'estudiante/:id',component:estudianteComponent}
];
