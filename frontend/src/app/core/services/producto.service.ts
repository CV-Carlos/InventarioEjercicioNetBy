import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  Producto,
  CrearProducto,
  ActualizarProducto,
  ProductoSelect,
} from '../models/producto.model';
import { Categoria } from '../models/categoria.model';
import { environment } from '../../../environment';
import { Paginado } from '../models';

@Injectable({
  providedIn: 'root',
})
export class ProductoService {
  private apiUrl = environment.productosApiUrl;

  constructor(private http: HttpClient) {}

  obtenerTodos(filtros?: {
    nombre?: string;
    categoriaId?: number;
    precioMin?: number;
    precioMax?: number;
    activo?: boolean;
    pagina?: number;
    itemsPorPagina?: number;
  }): Observable<Paginado<Producto>> {
    let params = new HttpParams();
    if (filtros?.nombre) params = params.set('nombre', filtros.nombre);
    if (filtros?.categoriaId) params = params.set('categoriaId', filtros.categoriaId);
    if (filtros?.precioMin) params = params.set('precioMin', filtros.precioMin);
    if (filtros?.precioMax) params = params.set('precioMax', filtros.precioMax);
    if (filtros?.activo !== undefined) params = params.set('activo', filtros.activo);
    params = params.set('pagina', filtros?.pagina ?? 1);
    params = params.set('itemsPorPagina', filtros?.itemsPorPagina ?? 10);
    return this.http.get<Paginado<Producto>>(`${this.apiUrl}/productos`, { params });
  }

  obtenerPorId(id: number): Observable<Producto> {
    return this.http.get<Producto>(`${this.apiUrl}/productos/${id}`);
  }

  crear(dto: CrearProducto): Observable<Producto> {
    return this.http.post<Producto>(`${this.apiUrl}/productos`, dto);
  }

  actualizar(id: number, dto: ActualizarProducto): Observable<Producto> {
    return this.http.put<Producto>(`${this.apiUrl}/productos/${id}`, dto);
  }

  eliminar(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/productos/${id}`);
  }

  obtenerCategorias(): Observable<Categoria[]> {
    return this.http.get<Categoria[]>(`${this.apiUrl}/categorias`);
  }

  obtenerParaSelect(): Observable<ProductoSelect[]> {
    return this.http.get<ProductoSelect[]>(`${this.apiUrl}/productos/select`);
  }
}
