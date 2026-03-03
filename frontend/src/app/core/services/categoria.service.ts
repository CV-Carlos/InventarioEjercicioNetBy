import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Categoria } from '../models/categoria.model';
import { environment } from '../../../environment';

@Injectable({
  providedIn: 'root',
})
export class CategoriaService {
  private apiUrl = environment.productosApiUrl;

  constructor(private http: HttpClient) {}

  obtenerTodos(): Observable<Categoria[]> {
    return this.http.get<Categoria[]>(`${this.apiUrl}/categorias`);
  }
}
