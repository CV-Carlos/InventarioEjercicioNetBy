import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Transaccion, CrearTransaccion } from '../models/transaccion.model';
import { environment } from '../../../environment';
import { Paginado } from '../models';

@Injectable({
  providedIn: 'root',
})
export class TransaccionService {
  private apiUrl = environment.transaccionesApiUrl;

  constructor(private http: HttpClient) {}

  obtenerTodos(filtros?: {
    productoId?: number;
    fechaInicio?: string;
    fechaFin?: string;
    tipo?: string;
    pagina?: number;
    itemsPorPagina?: number;
  }): Observable<Paginado<Transaccion>> {
    let params = new HttpParams();
    if (filtros?.productoId) params = params.set('productoId', filtros.productoId);
    if (filtros?.fechaInicio) params = params.set('fechaInicio', filtros.fechaInicio);
    if (filtros?.fechaFin) params = params.set('fechaFin', filtros.fechaFin);
    if (filtros?.tipo) params = params.set('tipo', filtros.tipo);
    params = params.set('pagina', filtros?.pagina ?? 1);
    params = params.set('itemsPorPagina', filtros?.itemsPorPagina ?? 10);
    return this.http.get<Paginado<Transaccion>>(`${this.apiUrl}/transacciones`, { params });
  }

  obtenerPorId(id: number): Observable<Transaccion> {
    return this.http.get<Transaccion>(`${this.apiUrl}/transacciones/${id}`);
  }

  crear(dto: CrearTransaccion): Observable<Transaccion> {
    return this.http.post<Transaccion>(`${this.apiUrl}/transacciones`, dto);
  }

  actualizar(id: number, dto: CrearTransaccion): Observable<Transaccion> {
    return this.http.put<Transaccion>(`${this.apiUrl}/transacciones/${id}`, dto);
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/transacciones/${id}`);
  }
}
