export interface Producto {
  id: number;
  nombre: string;
  descripcion?: string;
  categoriaId: number;
  categoriaNombre?: string;
  imagenUrl?: string;
  precio: number;
  stock: number;
  activo: boolean;
}

export interface CrearProducto {
  nombre: string;
  descripcion?: string;
  categoriaId: number;
  imagenUrl?: string;
  precio: number;
  stock: number;
}

export interface ActualizarProducto extends CrearProducto {
  activo: boolean;
}

export interface ProductoSelect {
  id: number;
  nombre: string;
  stock: number;
  precio: number;
}
