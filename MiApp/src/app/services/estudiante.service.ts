import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Estudiante } from '../models/Estudiante';
import { responseAPI } from '../models/responseAPI';

@Injectable({
  providedIn: 'root'
})
export class EstudianteService {

  private apiUrl: string = 'http://localhost:5153/api/Estudiantecontrollers'; // Modifica la URL según tu configuración

  constructor(private http: HttpClient) { }

  lista(): Observable<Estudiante[]> {
    return this.http.get<Estudiante[]>(this.apiUrl);
  }

  obtener(id: number): Observable<Estudiante> {
    return this.http.get<Estudiante>(`${this.apiUrl}/${id}`);
  }

  Crear(objeto: Estudiante): Observable<responseAPI> {
    return this.http.post<responseAPI>(this.apiUrl, objeto);
  }

  editar(objeto:Estudiante){
    return this.http.put<responseAPI>(this.apiUrl,objeto);
  }

  Eliminar(id: number): Observable<responseAPI> {
    console.log(id )
    return this.http.delete<responseAPI>(`${this.apiUrl}/${id}`);
  }

}

